using AeroScenery.AFS2;
using AeroScenery.Common;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public class BingOrthophotoSource
    {
        public List<ImageTile> ImageTilesForGridSquares(List<AFS2GridSquare> afs2GridSquares)
        {
            List<ImageTile> imageTiles = new List<ImageTile>();

            foreach (AFS2GridSquare afs2GrideSquare in afs2GridSquares)
            {

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
