using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.ImageProcessing
{
    public class TileStitcher
    {
        public async Task StitchImageTilesAsync(List<ImageTile> imageTiles, string directory, bool deleteOriginals)
        {
            await Task.Run(() =>
            {
                if (imageTiles.Count > 0)
                {
                    int columns = 0;
                    int rows = 0;

                    // Loop through our list of image tiles and figure out how
                    // many rows and columns we are dealing with
                    foreach (var imageTile in imageTiles)
                    {
                        if (imageTile.LocalTileX > columns)
                        {
                            columns = imageTile.LocalTileX;
                        }

                        if (imageTile.LocalTileY > rows)
                        {
                            rows = imageTile.LocalTileY;
                        }
                    }

                    // Get some info from the first tile. We can assume all tiles have these
                    // properties or something is very wrong
                    var imageSource = imageTiles[0].Source;
                    var zoomLevel = imageTiles[0].ZoomLevel;
                    var tileWidth = imageTiles[0].Width;
                    var tileHeight = imageTiles[0].Height;

                    // TODO - Needs to come from config
                    int maxTilesPerStitchedImageX = 48;
                    int maxTilesPerStitchedImageY = 48;

                    // Calculate the size of our stitched images in each direction
                    var imageSizeX = maxTilesPerStitchedImageX * tileWidth;
                    var imageSizeY = maxTilesPerStitchedImageY * tileHeight;

                    // Calculate how many images we need
                    var requiredStitchedImagesX = (int)Math.Ceiling((float)columns / (float)maxTilesPerStitchedImageX);
                    var requiredStitchedImagesY = (int)Math.Ceiling((float)rows / (float)maxTilesPerStitchedImageY);
                    var requiredStichedImages = requiredStitchedImagesX * requiredStitchedImagesY;

                    for (int stitchedImageIx = 0; stitchedImageIx < requiredStichedImages; stitchedImageIx++)
                    {
                        using (Bitmap bitmap = new System.Drawing.Bitmap(imageSizeX, imageSizeY))
                        {
                            bitmap.MakeTransparent();

                            // Work left to right, top to bottom
                            // Loop through rows
                            for(int yIx = 0; yIx < maxTilesPerStitchedImageY; yIx++)
                            {
                                // Loop through columns
                                for (int xIx = 0; xIx < maxTilesPerStitchedImageX; xIx++)
                                {

                                }
                            }

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                var tileFilename = directory + imageTiles[stitchedImageIx].FileName + "." + imageTiles[stitchedImageIx].ImageExtension;
                                Image tile = Image.FromFile(tileFilename);

                                if (tile != null)
                                {
                                    g.DrawImage(tile, new PointF(0, 0));
                                }

                                tile.Dispose();
                            }




                            var stitchFilename = String.Format("{0}_{1}_stitch_{2}.png", imageSource, zoomLevel, stitchedImageIx);
                            bitmap.Save(directory + stitchFilename, ImageFormat.Png);
                        }
                    }



                    if (deleteOriginals)
                    {

                    }
                }



            });
        }
    }
}
