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
        public async Task StitchImageTilesAsync(List<ImageTile> imageTiles)
        {
            await Task.Run(() =>
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


                using (Bitmap bitmap = new System.Drawing.Bitmap(1000, 1000))
                {
                    bitmap.MakeTransparent();

                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        g.DrawLine(new Pen(Color.Red), new System.Drawing.Point(1, 1), new System.Drawing.Point(50, 50));
                    }


                    bitmap.Save("D:\temp\asdf.png", ImageFormat.Png);
                }


            });
        }
    }
}
