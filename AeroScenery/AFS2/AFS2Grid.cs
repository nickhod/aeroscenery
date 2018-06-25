using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AFS2Grid
    {
        /// <summary>
        /// Returns the coordinates and unique name of a AFS2 grid square given latitude, longitude and 
        /// the AFS2 grid level.
        /// 
        /// Thanks go to this thread
        /// https://www.aerofly.com/community/forum/index.php?thread/12550-image-tile-coordinates/&pageNo=1
        /// and users: qwery42, Rodeo, vogel69
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public AFS2GridSquare GetGridSquareAtLatLon(double lat, double lon, int level)
        {
            var afs2GridSquare = new AFS2GridSquare();

            var worldGridConstant = 2.3311223704144;

            var lonLatX = (lon * Math.PI) / 180;
            var lonLatY = (lat * Math.PI) / 180;

            var x0 = lonLatX / Math.PI;
            var y0 = lonLatY / Math.PI;
            var y1 = Math.Tan(worldGridConstant * y0) / worldGridConstant;
            var x1 = 0.5 + 0.5 * x0;
            var y2 = 0.5 + 0.5 * y1;

            var outX = Math.Pow(2, level) * x1;
            var outY = Math.Pow(2, level) * y2;

            // --------------
            // NW
            var nwCornerWorldGridX = Math.Floor(outX);
            var nwCornerWorldGridY = Math.Ceiling(outY);

            var nwX0 = nwCornerWorldGridX / Math.Pow(2, level);
            var nwY0 = nwCornerWorldGridY / Math.Pow(2, level);
            var nwX1 = 2 * (nwX0 - 0.5);
            var nwY1 = 2 * (nwY0 - 0.5);
            var nwY2 = Math.Atan(worldGridConstant * nwY1) / worldGridConstant;
            var nwX2 = nwX1 * Math.PI;
            var nwY3 = nwY2 * Math.PI;

            var westLongitude = (nwX2 * 180) / Math.PI;
            var northLatitude = (nwY3 * 180) / Math.PI;

            //--------------
            // SE            
            var seCornerWorldGridY = Math.Floor(outY);

            var seCornerWorldGridX = 0;
            if (Math.Ceiling(outX) == nwCornerWorldGridX)
            {
                seCornerWorldGridX = (int)Math.Ceiling(outX + 0.75);
            }
            else
            {
                seCornerWorldGridX = (int)Math.Ceiling(outX);
            }

            var seX0 = seCornerWorldGridX / Math.Pow(2, level);
            var seY0 = seCornerWorldGridY / Math.Pow(2, level);
            var seX1 = 2 * (seX0 - 0.5);
            var seY1 = 2 * (seY0 - 0.5);
            var seY2 = Math.Atan(worldGridConstant * seY1) / worldGridConstant;
            var seX2 = seX1 * Math.PI;
            var seY3 = seY2 * Math.PI;


            var eastLongitude = (seX2 * 180) / Math.PI;
            var southLatitude = (seY3 * 180) / Math.PI;

            // -------------
            var pointLatLon1 = new PointLatLng(northLatitude, westLongitude);
            var pointLatLon2 = new PointLatLng(northLatitude, eastLongitude);
            var pointLatLon3 = new PointLatLng(southLatitude, eastLongitude);
            var pointLatLon4 = new PointLatLng(southLatitude, westLongitude);

            afs2GridSquare.Coordinates.Add(pointLatLon1);
            afs2GridSquare.Coordinates.Add(pointLatLon2);
            afs2GridSquare.Coordinates.Add(pointLatLon3);
            afs2GridSquare.Coordinates.Add(pointLatLon4);

            // Aerofly names grid squares as follows:
            // map_level_ hex((65536 / 2 ^ level) * grid square x) _ hex((65536 / 2 ^ level) * grid square y)

            int xInt = (int)((65536 / Math.Pow(2,level)) * nwCornerWorldGridX);
            int yInt = (int)((65536 / Math.Pow(2, level)) * nwCornerWorldGridY);
            var xHex = xInt.ToString("x4");
            var yHex = yInt.ToString("x4");

            afs2GridSquare.Name = String.Format("map_{0}_{1}_{2}", level.ToString("d2"), xHex, yHex);
            afs2GridSquare.Level = level;

            return afs2GridSquare;
        }
    }
}
