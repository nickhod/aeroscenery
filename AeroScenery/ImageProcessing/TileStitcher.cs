using AeroScenery.Common;
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
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        public async Task StitchImageTilesAsync(string tileDownloadDirectory, string stitchedTilesDirectory, bool deleteOriginals, IProgress<TileStitcherProgress> progress)
        {
            await Task.Run(() =>
            {
                var tileStitcherProgress = new TileStitcherProgress();

                this.xmlSerializer = new XmlSerializer(typeof(ImageTile));
                this.stitchedImageXmlSerializer = new XmlSerializer(typeof(StitchedImage));

                int startTileX;
                int startTileY;
                int endTileX;
                int endTileY;

                // Get the top left tile X & Y and the bottom right tile X & Y
                // We can work everything else out from this
                this.GetStartingAndEndTileXY(tileDownloadDirectory, out startTileX, out startTileY, out endTileX, out endTileY);

                Debug.WriteLine(startTileX);
                Debug.WriteLine(startTileY);
                Debug.WriteLine(endTileX);
                Debug.WriteLine(endTileY);

                int numberOfTilesX = (int)(endTileX - startTileX);
                int numberOfTilesY = (int)(endTileY - startTileY);

                Debug.WriteLine("Tiles X " + numberOfTilesX);
                Debug.WriteLine("Tiles Y " + numberOfTilesY);
                Debug.WriteLine("Filename prefix " + filenamePrefix);

                var firstImageTile = this.LoadImageTile(tileDownloadDirectory, startTileX, startTileY);

                // Get some info from the first tile. We can assume all tiles have these
                // properties or something is very wrong
                var imageSource = firstImageTile.Source;
                var zoomLevel = firstImageTile.ZoomLevel;
                var tileWidth = firstImageTile.Width;
                var tileHeight = firstImageTile.Height;


                // TODO - Needs to come from config
                int maxTilesPerStitchedImageX = 48;
                int maxTilesPerStitchedImageY = 48;

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

                                            Image tile = null;

                                            try
                                            {
                                                tile = Image.FromFile(imageTileFilename);

                                                if (tile != null)
                                                {
                                                    var imagePointX = (xIx * imageTileData.Width);
                                                    var imagePointY = (yIx * imageTileData.Width);

                                                    g.DrawImage(tile, new PointF(imagePointX, imagePointY));
                                                    tileStitcherProgress.CurrentTilesRenderedForCurrentStitchedImage++;
                                                    progress.Report(tileStitcherProgress);

                                                    rowHasImages = true;

                                                    if (xIx > columnsUsed)
                                                    {
                                                        columnsUsed = xIx + 1;
                                                    }
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
                                                if (tile != null)
                                                {
                                                    tile.Dispose();
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
                                CropBitmap(bitmap, new Rectangle(0, 0, columnsUsed * tileWidth, rowsUsed * tileHeight), stitchedTilesDirectory, stitchFilename);
                                tileStitcherProgress.CurrentStitchedImage++;
                            }

                            this.SaveStitchedImageAeroFile(this.stitchedImageXmlSerializer, stitchedImage, stitchedTilesDirectory);

                            //Debug.WriteLine("Columns Used: " + columnsUsed);
                            //Debug.WriteLine("Rows Used: " + rowsUsed);
                        }

                    }

                }

                if (deleteOriginals)
                {
                }

            });
        }

        public void CropBitmap(Bitmap bitmap, Rectangle rectangle, string stitchedTilesDirectory, string stitchFilename)
        {
            try
            {
                using (Bitmap croppedBitmap = new Bitmap(rectangle.Width, rectangle.Height, bitmap.PixelFormat))
                {
                    using (Graphics g = Graphics.FromImage(croppedBitmap))
                    {
                        g.DrawImage(bitmap, 0, 0);
                        log.InfoFormat("Cropping stitched image {0}", stitchFilename);
                        croppedBitmap.Save(stitchedTilesDirectory + stitchFilename, ImageFormat.Png);
                    }
                }

                GC.Collect();
            }
            catch (Exception ex)
            {
                log.Error("There was an error cropping the file " + stitchFilename, ex);
                log.InfoFormat("Available physical memory {0}", new ComputerInfo().AvailablePhysicalMemory);

                DialogResult result = MessageBox.Show(String.Format("There was an error cropping the file {0}.", stitchFilename),
                    "AeroScenery",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void GetStartingAndEndTileXY(string tileDownloadDirectory, out int startTileX, out int startTileY, out int endTileX, out int endTileY)
        {
            var aeroFiles = Directory.EnumerateFiles(tileDownloadDirectory, "*.aero").ToList();

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

                if (!filenamePrefixSet)
                {
                    this.filenamePrefix = String.Format("{0}_{1}_", filenameParts[0], filenameParts[1]);
                    filenamePrefixSet = true;
                }

                var tileX = int.Parse(filenameParts[2]);
                var tileY = int.Parse(filenameParts[3]);



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
    }
}
