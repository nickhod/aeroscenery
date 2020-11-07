﻿using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.OrthoPhotoSources;
using log4net;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AeroScenery.ImageProcessing
{
    public class TileStitcher
    {
        // The prefix of each image tile and aero file, e.g. g_19_
        private string filenamePrefix;

        private XmlSerializer xmlSerializer;
        private XmlSerializer stitchedImageXmlSerializer;
        private ImageProcessingFilters imageProcessingFilters;

        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        public async Task StitchImageTilesAsync(string tileDownloadDirectory, string stitchedTilesDirectory, bool deleteOriginals, IProgress<TileStitcherProgress> progress)
        {
            // TODO This method really needs refactoring

            await Task.Run(() =>
            {
                var tileStitcherProgress = new TileStitcherProgress();

                this.imageProcessingFilters = new ImageProcessingFilters();
                this.xmlSerializer = new XmlSerializer(typeof(ImageTile));
                this.stitchedImageXmlSerializer = new XmlSerializer(typeof(StitchedImage));

                int startTileX;
                int startTileY;
                int endTileX;
                int endTileY;
                int tileCount;

                // Get the top left tile X & Y and the bottom right tile X & Y
                // We can work everything else out from this
                this.GetStartingAndEndTileXY(tileDownloadDirectory, out startTileX, out startTileY, out endTileX, out endTileY, out tileCount);

                if (tileCount > 0)
                {
                    //Debug.WriteLine("startTileX " + startTileX);
                    //Debug.WriteLine("startTileY " + startTileY);
                    //Debug.WriteLine("endTileX " + endTileX);
                    //Debug.WriteLine("endTileY " + endTileY);

                    int numberOfTilesX = (endTileX - startTileX) + 1;
                    int numberOfTilesY = (endTileY - startTileY) + 1;

                    //Debug.WriteLine("Tiles X " + numberOfTilesX);
                    //Debug.WriteLine("Tiles Y " + numberOfTilesY);
                    //Debug.WriteLine("Filename prefix " + filenamePrefix);

                    var firstImageTile = this.LoadImageTile(tileDownloadDirectory, startTileX, startTileY);

                    // Get some info from the first tile. We can assume all tiles have these
                    // properties or something is very wrong
                    var imageSource = firstImageTile.Source;
                    var zoomLevel = firstImageTile.ZoomLevel;
                    var tileWidth = firstImageTile.Width;
                    var tileHeight = firstImageTile.Height;

                    int maxTilesPerStitchedImageX = AeroSceneryManager.Instance.Settings.MaximumStitchedImageSize.Value;
                    int maxTilesPerStitchedImageY = AeroSceneryManager.Instance.Settings.MaximumStitchedImageSize.Value;

                    // Calculate the size of our stitched images in each direction
                    var imageSizeX = maxTilesPerStitchedImageX * tileWidth;
                    var imageSizeY = maxTilesPerStitchedImageY * tileHeight;

                    // Calculate how many images we need
                    var requiredStitchedImagesX = (int)Math.Ceiling((float)numberOfTilesX / (float)maxTilesPerStitchedImageX);
                    var requiredStitchedImagesY = (int)Math.Ceiling((float)numberOfTilesY / (float)maxTilesPerStitchedImageY);
                    var requiredStichedImages = requiredStitchedImagesX * requiredStitchedImagesY;

                    int imageTileOffsetX = 0;
                    int imagetileOffsetY = 0;

                    tileStitcherProgress.TotalStitchedImages = requiredStichedImages;

                    // Loop through each stitched image that we will need
                    for (int stitchedImagesYIx = 0; stitchedImagesYIx < requiredStitchedImagesY; stitchedImagesYIx++)
                    {

                        for (int stitchedImagesXIx = 0; stitchedImagesXIx < requiredStitchedImagesX; stitchedImagesXIx++)
                        {
                            imageTileOffsetX = stitchedImagesXIx * maxTilesPerStitchedImageX;
                            imagetileOffsetY = stitchedImagesYIx * maxTilesPerStitchedImageY;

                            // This might not be right, but it's a reasonable estimate. We wont know until we read each file
                            tileStitcherProgress.TotalImageTilesForCurrentStitchedImage = maxTilesPerStitchedImageX * maxTilesPerStitchedImageY;

                            int columnsUsed = 0;
                            int rowsUsed = 0;

                            // By giving these incorrect values, we can be sure they will we overwritten without having
                            // to make them nullable and do null checks
                            double northLatitude = -500;
                            double westLongitude = 500;
                            double southLatitude = 500;
                            double eastLongitude = -500;

                            using (Bitmap bitmap = new System.Drawing.Bitmap(imageSizeX, imageSizeY))
                            {
                                bitmap.MakeTransparent();

                                using (Graphics g = Graphics.FromImage(bitmap))
                                {
                                    tileStitcherProgress.CurrentTilesRenderedForCurrentStitchedImage = 0;

                                    // Work left to right, top to bottom
                                    // Loop through rows
                                    for (int yIx = 0; yIx < maxTilesPerStitchedImageY; yIx++)
                                    {
                                        bool rowHasImages = false;

                                        // Loop through columns
                                        for (int xIx = 0; xIx < maxTilesPerStitchedImageX; xIx++)
                                        {
                                            int currentTileX = xIx + imageTileOffsetX + startTileX;
                                            int currentTileY = yIx + imagetileOffsetY + startTileY;

                                            var imageTileData = this.LoadImageTile(tileDownloadDirectory, currentTileX, currentTileY);

                                            if (imageTileData != null)
                                            {
                                                // Even if all the images in this row are invalid, the aero files are present
                                                // so an attempt was made to download something
                                                rowHasImages = true;

                                                // Update our overall stitched image lat and long maxima and minima
                                                // We want the highest NorthLatitude value of any image tile for this stitched image
                                                if (imageTileData.NorthLatitude > northLatitude)
                                                {
                                                    northLatitude = imageTileData.NorthLatitude;
                                                }

                                                // We want the lowest SouthLatitude value of any image tile for this stitched image
                                                if (imageTileData.SouthLatitude < southLatitude)
                                                {
                                                    southLatitude = imageTileData.SouthLatitude;
                                                }

                                                // We want the lowest WestLongitude value of any image tile for this stitched image
                                                if (imageTileData.WestLongitude < westLongitude)
                                                {
                                                    westLongitude = imageTileData.WestLongitude;
                                                }

                                                // We want the highest EastLongitude value of any image tile for this stitched image
                                                if (imageTileData.EastLongitude > eastLongitude)
                                                {
                                                    eastLongitude = imageTileData.EastLongitude;
                                                }

                                                var imageTileFilename = tileDownloadDirectory + imageTileData.FileName + "." + imageTileData.ImageExtension;

                                                Bitmap tile = null;
                                                Image tileImage = null;

                                                try
                                                {
                                                    //tile = Image.FromFile(imageTileFilename); 
                                                    tileImage = Image.FromFile(imageTileFilename);
                                                    tile = new Bitmap(tileImage);

                                                    if (tile != null)
                                                    {
                                                        if (tile.HorizontalResolution != 96f || tile.VerticalResolution != 96f)
                                                        {
                                                            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                                                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

                                                            var myEncoderParameters = new EncoderParameters(1);
                                                            var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                                                            myEncoderParameters.Param[0] = myEncoderParameter;

                                                            tile.SetResolution(96.0f, 96.0f);
                                                        }

                                                        var imagePointX = (xIx * imageTileData.Width);
                                                        var imagePointY = (yIx * imageTileData.Width);

                                                        g.DrawImage(tile, new PointF(imagePointX, imagePointY));
                                                        tileStitcherProgress.CurrentTilesRenderedForCurrentStitchedImage++;
                                                        progress.Report(tileStitcherProgress);

                                                    }

                                                    tile.Dispose();
                                                }
                                                catch (Exception)
                                                {
                                                    // The image file was probably invalid, but there's not a lot we can do
                                                    // Leave it transparent
                                                }
                                                finally
                                                {
                                                    // Even if the image was invalid, we still had an aero file for it
                                                    // so it counts as a used column
                                                    var colsUsedInThisRow = xIx + 1;

                                                    if (columnsUsed < colsUsedInThisRow)
                                                    {
                                                        columnsUsed = colsUsedInThisRow;
                                                    }

                                                    if (tile != null)
                                                    {
                                                        tile.Dispose();
                                                    }

                                                    if (tileImage != null)
                                                    {
                                                        tileImage.Dispose();
                                                    }
                                                }

                                            }

                                        }

                                        if (rowHasImages)
                                        {
                                            rowsUsed++;
                                        }
                                    }

                                }

                                var stitchFilename = String.Format("{0}_{1}_stitch_{2}_{3}.png", imageSource, zoomLevel, stitchedImagesXIx + 1, stitchedImagesYIx + 1);
                                var stitchedImage = new StitchedImage();

                                stitchedImage.ImageExtension = "png";
                                stitchedImage.NorthLatitude = northLatitude;
                                stitchedImage.WestLongitude = westLongitude;
                                stitchedImage.SouthLatitude = southLatitude;
                                stitchedImage.EastLongitude = eastLongitude;
                                stitchedImage.Width = columnsUsed * tileWidth;
                                stitchedImage.Height = rowsUsed * tileHeight;
                                stitchedImage.Source = imageSource;
                                stitchedImage.ZoomLevel = zoomLevel;
                                stitchedImage.StichedImageSetX = stitchedImagesXIx + 1;
                                stitchedImage.StichedImageSetY = stitchedImagesYIx + 1;

                                //Debug.WriteLine("Rows Used " + rowsUsed);
                                //Debug.WriteLine("Columns Used " + columnsUsed);

                                var settings = AeroSceneryManager.Instance.Settings;

                                if (settings.EnableImageProcessing.Value)
                                {
                                    var imageProcessingSettings = new ImageProcessingSettings();
                                    imageProcessingSettings.BrightnessAdjustment = settings.BrightnessAdjustment;
                                    imageProcessingSettings.ContrastAdjustment = settings.ContrastAdjustment;
                                    imageProcessingSettings.SaturationAdjustment = settings.SaturationAdjustment;
                                    imageProcessingSettings.SharpnessAdjustment = settings.SharpnessAdjustment;
                                    imageProcessingSettings.RedAdjustment = settings.RedAdjustment;
                                    imageProcessingSettings.GreenAdjustment = settings.GreenAdjustment;
                                    imageProcessingSettings.BlueAdjustment = settings.BlueAdjustment;

                                    this.imageProcessingFilters.ApplyFilters(imageProcessingSettings, bitmap);
                                }


                                // Have we drawn an image to the maximum number of rows and columns for this image?
                                if (columnsUsed == maxTilesPerStitchedImageX && rowsUsed == maxTilesPerStitchedImageY)
                                {
                                    // Save the bitmap as it is
                                    log.InfoFormat("Saving stitched image {0}", stitchFilename);
                                    bitmap.Save(stitchedTilesDirectory + stitchFilename, ImageFormat.Png);
                                    tileStitcherProgress.CurrentStitchedImage++;
                                }
                                else
                                {
                                    // Resize the bitmap down to the used number of rows and columns
                                    log.InfoFormat("Cropping stitched image {0}", stitchFilename);
                                    CropBitmap(bitmap, new Rectangle(0, 0, columnsUsed * tileWidth, rowsUsed * tileHeight), stitchedTilesDirectory, stitchFilename);
                                    tileStitcherProgress.CurrentStitchedImage++;
                                }

                                this.SaveStitchedImageAeroFile(this.stitchedImageXmlSerializer, stitchedImage, stitchedTilesDirectory);
                            }

                        }

                    }

                    if (deleteOriginals)
                    {
                    }

                }


            });
        }


        public void CropBitmap(Bitmap bitmap, Rectangle rectangle, string stitchedTilesDirectory, string stitchFilename)
        {
            try
            {
                if (rectangle.Width > 0 && rectangle.Height > 0)
                {
                    var croppedBitmap = bitmap.Clone(rectangle, bitmap.PixelFormat);
                    croppedBitmap.Save(stitchedTilesDirectory + stitchFilename, ImageFormat.Png);
                    croppedBitmap.Dispose();
                    croppedBitmap = null;

                    GC.Collect();
                }
                else
                {
                    log.ErrorFormat("Crop rectangle for image {0} was 0 width or height", stitchFilename);
                }

            }
            catch (Exception ex)
            {
                log.Error("There was an error cropping the file " + stitchFilename, ex);

                var messageBox = new CustomMessageBox(String.Format("There was an error cropping the file {0}.", stitchFilename), 
                    "AeroScenery", 
                    MessageBoxIcon.Error);

                messageBox.ShowDialog();
            }


        }


        private ImageTile LoadImageTile(string tileDownloadDirectory, int tileX, int tileY)
        {
            string filePath = string.Format("{0}{1}{2}_{3}.aero", tileDownloadDirectory, this.filenamePrefix, tileX, tileY);

            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        var imageTile = (ImageTile)xmlSerializer.Deserialize(reader);
                        reader.Close();
                        return imageTile;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("There was an error loading the aero file " + filePath, ex);
                }
            }


            return null;

        }

        private void GetStartingAndEndTileXY(string tileDownloadDirectory, out int startTileX, out int startTileY, out int endTileX, out int endTileY, out int tileCount)
        {
            var aeroFiles = Directory.EnumerateFiles(tileDownloadDirectory, "*.aero").ToList();
            tileCount = aeroFiles.Count();

            int lowestTileNumberX = int.MaxValue;
            int lowestTileNumberY = int.MaxValue;

            int highestTileNumberX = 0;
            int highestTileNumberY = 0;

            bool filenamePrefixSet = false;

            foreach (string aeroFile in aeroFiles)
            {
                var cleanedFileName = aeroFile.Replace('\\', '/');
                var filepathParts = cleanedFileName.Split('/');
                var filename = filepathParts[filepathParts.Length - 1];
                filename = filename.Replace(".aero", "");

                var filenameParts = filename.Split('_');

                var tileX = int.Parse(filenameParts[(filenameParts.Length - 1) - 1]);
                var tileY = int.Parse(filenameParts[filenameParts.Length - 1]);
                var zoomLevel = filenameParts[(filenameParts.Length - 1) - 2];
                var orthoSource = "";

                // More than 4 if we have a country specific ortho source
                // e.g. us_usgs
                if (filenameParts.Length > 4)
                {
                    orthoSource = String.Format("{0}_{1}", filenameParts[0], filenameParts[1]);
                }
                else
                {
                    orthoSource = filenameParts[0];
                }

                if (!filenamePrefixSet)
                {
                    this.filenamePrefix = String.Format("{0}_{1}_", orthoSource, zoomLevel);
                    filenamePrefixSet = true;
                }


                if (tileX < lowestTileNumberX)
                {
                    lowestTileNumberX = tileX;
                }

                if (tileY < lowestTileNumberY)
                {
                    lowestTileNumberY = tileY;
                }

                if (tileX > highestTileNumberX)
                {
                    highestTileNumberX = tileX;
                }

                if (tileY > highestTileNumberY)
                {
                    highestTileNumberY = tileY;
                }
            }

            var value = new Tuple<int, int>(lowestTileNumberX, lowestTileNumberY);

            startTileX = lowestTileNumberX;
            startTileY = lowestTileNumberY;
            endTileX = highestTileNumberX;
            endTileY = highestTileNumberY;
        }


        private void SaveStitchedImageAeroFile(XmlSerializer xmlSerializer, StitchedImage stitchedImage, string path)
        {
            using (TextWriter tw = new StreamWriter(path + stitchedImage.FileName + ".aero"))
            {
                xmlSerializer.Serialize(tw, stitchedImage);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
