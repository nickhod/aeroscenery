using AeroScenery.AFS2;
using AeroScenery.Data.Models;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Data.Mappers
{
    public class GridSquareMapper
    {
        public GridSquare ToModel(AFS2GridSquare aFS2GridSquare)
        {
            GridSquare gridSquare = new GridSquare();

            gridSquare.Name = aFS2GridSquare.Name;
            gridSquare.NorthLatitude = aFS2GridSquare.Coordinates[0].Lat;
            gridSquare.EastLongitude = aFS2GridSquare.Coordinates[1].Lng;
            gridSquare.SouthLatitude = aFS2GridSquare.Coordinates[2].Lat;
            gridSquare.WestLongitude = aFS2GridSquare.Coordinates[3].Lng;

            return gridSquare;
        }

        public AFS2GridSquare ToAFS2GridSquare(GridSquare gridSquare)
        {
            AFS2GridSquare afsS2GridSquare = new AFS2GridSquare();
            afsS2GridSquare.Name = gridSquare.Name;

            var nwCorder = new PointLatLng(gridSquare.NorthLatitude, gridSquare.WestLongitude);
            var neCorder = new PointLatLng(gridSquare.NorthLatitude, gridSquare.EastLongitude);
            var seCorder = new PointLatLng(gridSquare.SouthLatitude, gridSquare.EastLongitude);
            var swCorder = new PointLatLng(gridSquare.SouthLatitude, gridSquare.WestLongitude);

            afsS2GridSquare.Coordinates.Add(nwCorder);
            afsS2GridSquare.Coordinates.Add(neCorder);
            afsS2GridSquare.Coordinates.Add(seCorder);
            afsS2GridSquare.Coordinates.Add(swCorder);

            return afsS2GridSquare;
        }
    }
}
