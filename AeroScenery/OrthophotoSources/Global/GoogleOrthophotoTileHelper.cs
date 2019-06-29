using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    /// <summary>
    /// Conventience methods for working with Google Maps without the API
    /// https://developers.google.com/maps/documentation/javascript/examples/map-coordinates?csw=1
    /// This is very similar to the Bing calculations, but the way Google rounds things gives slightly different results
    /// </summary>
    public class GoogleOrthophotoTileHelper
    { 

        private const int TileSize = 256;

        public static uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        private static double Clip(double n, double minValue, double maxValue)
        {
            return Math.Min(Math.Max(n, minValue), maxValue);
        }



        /// <summary>
        /// Converts a point from latitude/longitude WGS-84 coordinates (in degrees)
        /// into pixel XY coordinates at a specified level of detail.
        /// </summary>
        /// <param name="latitude">Latitude of the point, in degrees.</param>
        /// <param name="longitude">Longitude of the point, in degrees.</param>
        /// <param name="levelOfDetail">Level of detail, from 1 (lowest detail)
        /// to 23 (highest detail).</param>
        /// <param name="pixelX">Output parameter receiving the X coordinate in pixels.</param>
        /// <param name="pixelY">Output parameter receiving the Y coordinate in pixels.</param>
        public static void LatLongToPixelXY(double latitude, double longitude, int levelOfDetail, out int pixelX, out int pixelY)
        {
            double worldX;
            double worldY;
            Project(latitude, longitude, out worldX, out worldY);

            var scale = 1 << levelOfDetail;

            pixelX = (int)Math.Floor(worldX * scale);
            pixelY = (int)Math.Floor(worldY * scale);
        }


        /// <summary>
        /// Converts pixel XY coordinates into tile XY coordinates of the tile containing
        /// the specified pixel.
        /// </summary>
        public static void LatLongToTileXY(double latitude, double longitude, int levelOfDetail, out int tileX, out int tileY)
        {
            double worldX;
            double worldY;

            Project(latitude, longitude, out worldX, out worldY);

            var scale = 1 << levelOfDetail;

            tileX = (int)Math.Floor(worldX * scale / TileSize);
            tileY = (int)Math.Floor(worldY * scale / TileSize);
        }


        public static void PixelXYToLatLong(int pixelX, int pixelY, int levelOfDetail, out double latitude, out double longitude)
        {
            double mapSize = MapSize(levelOfDetail);
            double x = (Clip(pixelX, 0, mapSize - 1) / mapSize) - 0.5;
            double y = 0.5 - (Clip(pixelY, 0, mapSize - 1) / mapSize);

            latitude = 90 - 360 * Math.Atan(Math.Exp(-y * 2 * Math.PI)) / Math.PI;
            longitude = 360 * x;
        }

        public static void TileXYToLatLong(int tileX, int tileY, int levelOfDetail, out double latitude, out double longitude)
        {
            var pixelX = tileX * 256;
            var pixelY = tileY * 256;

            PixelXYToLatLong(pixelX, pixelY, levelOfDetail, out latitude, out longitude);
        }

        public static void TileXYToPixelXY(int tileX, int tileY, out int pixelX, out int pixelY)
        {
            pixelX = tileX * 256;
            pixelY = tileY * 256;
        }

        /// <summary>
        /// The mapping between latitude, longitude and pixels is defined by the web
        /// mercator projection.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        private static void Project(double latitude, double longitude, out double worldX, out double worldY)
        {
            var siny = Math.Sin(latitude * Math.PI / 180);

            // Truncating to 0.9999 effectively limits latitude to 89.189. This is
            // about a third of a tile past the edge of the world tile.
            siny = Math.Min(Math.Max(siny, -0.9999), 0.9999);


            worldX = TileSize * (0.5 + longitude / 360);
            worldY = TileSize * (0.5 - Math.Log((1 + siny) / (1 - siny)) / (4 * Math.PI));
        }

    }
}
