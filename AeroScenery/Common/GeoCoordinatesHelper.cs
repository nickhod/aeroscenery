using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public class GeoCoordinatesHelper
    {
        public static double CalculateOffset(double original, double offset, Direction direction)
        {
            double result = 0;
            switch (direction)
            {
                case Direction.North:
                    result = original + offset;
                    break;
                case Direction.South:
                    result = original - offset;
                    break;
                case Direction.East:
                    result = original + offset;
                    break;
                case Direction.West:
                    result = original - offset;
                    break;
            }

            return result;
        }

        public static GeoArea RectangleFromCenterPoint(GeoCoordinate centerPoint, double width, double height)
        {
            var latOffset = height / 2;
            var lonOffset = width / 2;

            var north = CalculateOffset(centerPoint.Latitude, latOffset, Direction.North);
            var south = CalculateOffset(centerPoint.Latitude, latOffset, Direction.South);
            var east = CalculateOffset(centerPoint.Longitude, lonOffset, Direction.East);
            var west = CalculateOffset(centerPoint.Longitude, lonOffset, Direction.West);

            var result = new GeoArea(north, south, east, west);

            return result;
        }

        public static List<GeoCoordinate> RotatedRectangleFromCenterPoint(GeoCoordinate centerPoint, double width, double height, double? rotation)
        {
            return null;
        }
    }
}
