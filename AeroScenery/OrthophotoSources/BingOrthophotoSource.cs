using AeroScenery.AFS2;
using AeroScenery.Common;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public class BingOrthophotoSource
    {
        public List<ImageTile> ImageTilesForGridSquares(List<AFS2GridSquare> afs2GridSquares)
        {
            List<ImageTile> imageTiles = new List<ImageTile>();

            foreach (AFS2GridSquare afs2GridSquare in afs2GridSquares)
            {

            }

            // Genereate some fake image tiles for now until we get this done
            for (int i = 0; i < 1000; i++)
            {
                ImageTile tile = new ImageTile();
                tile.FileName = "img" + i;
                tile.LatitudeStepsPerPixel = 0.00000000001;
                tile.LongitudeStepsPerPixel = 0.00000000001;
                tile.Size = 256;
                tile.LatitudeTop = 42;
                tile.LongitudeLeft = 42;
                tile.URL = "http://ecn.t1.tiles.virtualearth.net/tiles/a12030003131321231.jpeg?g=875";

                imageTiles.Add(tile);
            }

            return imageTiles;
        }


        public void DoStuff(GPoint gPoint)
        {
            BingSatelliteMapProvider bingSatelliteMapProvider = BingSatelliteMapProvider.Instance;
            GoogleSatelliteMapProvider googleSatelliteMapProvider = GoogleSatelliteMapProvider.Instance;

            var image = googleSatelliteMapProvider.GetTileImage(gPoint, 12);
            //var image = bingSatelliteMapProvider.GetTileImage(gPoint, 8);


            using (FileStream file = new FileStream("D:\\file.jpg", FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[image.Data.Length];
                image.Data.Read(bytes, 0, (int)image.Data.Length);
                file.Write(bytes, 0, bytes.Length);
                image.Data.Close();
            }

        }
    }
}
