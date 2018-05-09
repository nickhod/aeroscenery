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

                    Dictionary<string, ImageTile> imageTileLookup = new Dictionary<string, ImageTile>();

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

                        imageTileLookup.Add(imageTile.LocalTileX + "-" + imageTile.LocalTileY, imageTile);
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

                    int imageTileOffsetX = 1;
                    int imagetileOffsetY = 1;

                    for (int stitchedImageIx = 0; stitchedImageIx < requiredStichedImages; stitchedImageIx++)
                    {
                        using (Bitmap bitmap = new System.Drawing.Bitmap(imageSizeX, imageSizeY))
                        {
                            bitmap.MakeTransparent();

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                // Work left to right, top to bottom
                                // Loop through rows
                                for (int yIx = 0; yIx < maxTilesPerStitchedImageY; yIx++)
                                {
                                    // Loop through columns
                                    for (int xIx = 0; xIx < maxTilesPerStitchedImageX; xIx++)
                                    {
                                        var localX = xIx + imageTileOffsetX;
                                        var localY = yIx + imagetileOffsetY;

                                        if (imageTileLookup.ContainsKey(localX + "-" + localY))
                                        {
                                            var imageTile = imageTileLookup[localX + "-" + localY];


                                            var tileFilename = directory + imageTile.FileName + "." + imageTile.ImageExtension;

                                            Image tile = null;

                                            try
                                            {
                                                tile = Image.FromFile(tileFilename);

                                                if (tile != null)
                                                {
                                                    var imagePointX = (xIx * imageTile.Width);
                                                    var imagePointY = (yIx * imageTile.Width);

                                                    g.DrawImage(tile, new PointF(imagePointX, imagePointY));
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
                                }

                            }



                            var stitchFilename = String.Format("{0}_{1}_stitch_{2}.png", imageSource, zoomLevel, stitchedImageIx);
                            bitmap.Save(directory + stitchFilename, ImageFormat.Png);
                        }

                        imageTileOffsetX += maxTilesPerStitchedImageX;
                        imagetileOffsetY += maxTilesPerStitchedImageY;

                    }



                    if (deleteOriginals)
                    {

                    }
                }



            });
        }
    }
}
