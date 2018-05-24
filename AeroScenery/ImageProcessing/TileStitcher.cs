using AeroScenery.Common;
using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroScenery.ImageProcessing
{
    public class TileStitcher
    {
        // The prefix of each image tile and aero file, e.g. g_19_
        private string filenamePrefix;

        private XmlSerializer xmlSerializer;

        public async Task StitchImageTilesAsync(string tileDownloadDirectory, string stitchedTilesDirectory, bool deleteOriginals)
        {
            await Task.Run(() =>
            {
                this.xmlSerializer = new XmlSerializer(typeof(ImageTile));

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
                //var requiredStichedImages = requiredStitchedImagesX * requiredStitchedImagesY;

                int imageTileOffsetX = 0;
                int imagetileOffsetY = 0;

                // Loop through each stitched image that we will need
                for (int stitchedImagesYIx = 0; stitchedImagesYIx < requiredStitchedImagesY; stitchedImagesYIx++)
                {

                    for (int stitchedImagesXIx = 0; stitchedImagesXIx < requiredStitchedImagesX; stitchedImagesXIx++)
                    {
                        imageTileOffsetX = stitchedImagesXIx * maxTilesPerStitchedImageX;
                        imagetileOffsetY = stitchedImagesYIx * maxTilesPerStitchedImageY;

                        int columnsUsed = 0;
                        int rowsUsed = 0;

                        using (Bitmap bitmap = new System.Drawing.Bitmap(imageSizeX, imageSizeY))
                        {
                            bitmap.MakeTransparent();

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
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

                            // Have we drawn an image to the maximum number of rows and columns for this image?
                            if (columnsUsed == maxTilesPerStitchedImageX && rowsUsed == maxTilesPerStitchedImageY)
                            {
                                // Save the bitmpa as it is
                                bitmap.Save(stitchedTilesDirectory + stitchFilename, ImageFormat.Png);
                            }
                            else
                            {
                                // Resize the bitmap down to the used number of rows and columns
                                var croppedBitmap = CropBitmap(bitmap, new Rectangle(0, 0, columnsUsed * tileWidth, rowsUsed * tileHeight));
                                croppedBitmap.Save(stitchedTilesDirectory + stitchFilename, ImageFormat.Png);
                            }

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

        public static Bitmap CropBitmap(Bitmap bitmap, Rectangle rectangle)
        {
            Bitmap croppedBitmap = new Bitmap(rectangle.Width, rectangle.Height);
            Graphics g = Graphics.FromImage(croppedBitmap);
            g.DrawImage(bitmap, -rectangle.X, -rectangle.Y);
            return croppedBitmap;
        }


        private ImageTile LoadImageTile(string tileDownloadDirectory, int tileX, int tileY)
        {
            string filePath = string.Format("{0}{1}{2}_{3}.aero", tileDownloadDirectory, this.filenamePrefix, tileX, tileY);

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var imageTile = (ImageTile)xmlSerializer.Deserialize(reader);
                    reader.Close();
                    return imageTile;
                }
            }
            catch(Exception ex)
            {

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


        private void SaveStitchedImageTileAeroFile(XmlSerializer xmlSerializer, ImageTile imageTile, string path)
        {
            using (TextWriter tw = new StreamWriter(path + imageTile.FileName + ".aero"))
            {
                xmlSerializer.Serialize(tw, imageTile);
            }
        }

        public void Temp()
        {
            //if (imageTiles.Count > 0)
            //{
            //int columns = 0;
            //int rows = 0;

            //Dictionary<string, ImageTile> imageTileLookup = new Dictionary<string, ImageTile>();

            //// Loop through our list of image tiles and figure out how
            //// many rows and columns we are dealing with
            //foreach (var imageTile in imageTiles)
            //{
            //    if (imageTile.LocalTileX > columns)
            //    {
            //        columns = imageTile.LocalTileX;
            //    }

            //    if (imageTile.LocalTileY > rows)
            //    {
            //        rows = imageTile.LocalTileY;
            //    }

            //    imageTileLookup.Add(imageTile.LocalTileX + "-" + imageTile.LocalTileY, imageTile);
            //}






            //}
            //}
        }
    }
}


//var allTilesStitched = false;

//var currentTileX = startingTileX;
//var currentTileY = startingTileY;

//var currentXIndex = 1;
//var currentYIndex = 1;

//var currentStitchedImageX = 1;
//var currentStitchedImageY = 1;

////var firstImageTile = LoadAeroFile(xmlSerializer, )


//// TODO - Needs to come from config
//int maxTilesPerStitchedImageX = 48;
//int maxTilesPerStitchedImageY = 48;

//var tileWidth = 256;
//var tileHeight = 256;

//// Calculate the size of our stitched images in each direction
//var imageSizeX = maxTilesPerStitchedImageX * tileWidth;
//var imageSizeY = maxTilesPerStitchedImageY * tileHeight;

//// While loop for total number of stitched images
//do
//{
//    // While loop for rows on the current stiched image
//    do
//    {
//        // While loop for columns on the current stitched image
//        do
//        {
//            currentXIndex++;

//        }
//        while (currentXIndex < maxTilesPerStitchedImageX);


//        currentYIndex++;
//    }
//    while (currentYIndex < maxTilesPerStitchedImageY);

//    allTilesStitched = true;
//}
//while (!allTilesStitched);