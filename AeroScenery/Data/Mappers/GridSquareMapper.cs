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

            gridSquare.Level = aFS2GridSquare.Level;

            return gridSquare;
        }

        public AFS2GridSquare ToAFS2GridSquare(GridSquare gridSquare)
        {
            AFS2GridSquare afsS2GridSquare = new AFS2GridSquare();
            afsS2GridSquare.Name = gridSquare.Name;

            var nwCorner = new PointLatLng(gridSquare.NorthLatitude, gridSquare.WestLongitude);
            var neCorner = new PointLatLng(gridSquare.NorthLatitude, gridSquare.EastLongitude);
            var seCorner = new PointLatLng(gridSquare.SouthLatitude, gridSquare.EastLongitude);
            var swCorner = new PointLatLng(gridSquare.SouthLatitude, gridSquare.WestLongitude);

            afsS2GridSquare.Coordinates.Add(nwCorner);
            afsS2GridSquare.Coordinates.Add(neCorner);
            afsS2GridSquare.Coordinates.Add(seCorner);
            afsS2GridSquare.Coordinates.Add(swCorner);

            return afsS2GridSquare;
        }
    }
}
