using AeroScenery.AFS2;
using AeroScenery.Common;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public class BingOrthophotoSource : IOrthophotoSource
    {
        private string urlTemplate = "http://ecn.t1.tiles.virtualearth.net/tiles/a{0}.jpeg?g=42";

        public List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare)
        {
            List<ImageTile> imageTiles = new List<ImageTile>();

            // Temp
            int zoomLevel = 14;

            // Just to make the code more readable
            var northWestCorner = afs2GridSquare.Coordinates[0];
            var northEastCorner = afs2GridSquare.Coordinates[1];
            var southEastCorner = afs2GridSquare.Coordinates[2];
            var southWestCorner = afs2GridSquare.Coordinates[3];

            // Get the pixel X & Y of the first tile
            int pixelX = 0;
            int pixelY = 0;
            BingHelper.LatLongToPixelXY(northWestCorner.Lat, northWestCorner.Lng, zoomLevel, out pixelX, out pixelY);

            // Get the tile X & Y of the frst tile
            int tileX = 0;
            int tileY = 0;
            BingHelper.PixelXYToTileXY(pixelX, pixelY, out tileX, out tileY);

            double currentTileLatitude = 0;
            double currentTileLongitude = 0;

            int currentTileX = tileX;
            int currentTileY = tileY;

            int currentRow = 1;
            int currentColumn = 1;

            // Work through the tiles East to West, then top North to South
            do
            {
                do
                {
                    int currentPixelX;
                    int currentPixelY;
                    BingHelper.TileXYToPixelXY(currentTileX, currentTileY, out currentPixelX, out currentPixelY);

                    BingHelper.PixelXYToLatLong(currentPixelX, currentPixelY, zoomLevel, out currentTileLatitude, out currentTileLongitude);

                    var quadKey1 = BingHelper.TileXYToQuadKey(currentTileX, currentTileY, zoomLevel);

                    ImageTile tile = new ImageTile();
                    tile.FileName = "R" + currentRow.ToString() + "-C" + currentColumn.ToString();
                    tile.LatitudeStepsPerPixel = 0.00000000001;
                    tile.LongitudeStepsPerPixel = 0.00000000001;
                    tile.Size = 256;
                    tile.LatitudeTop = currentTileLatitude;
                    tile.LongitudeLeft = currentTileLongitude;
                    tile.ImageExtension = "jpg";
                    tile.URL = String.Format(this.urlTemplate, quadKey1);

                    imageTiles.Add(tile);

                    currentTileX++;
                    currentColumn++;
                }
                while (currentTileLongitude < southEastCorner.Lng);

                // Go back to the original tileX
                currentTileX = tileX;
                // Start at column 1 again
                currentColumn = 1;

                // Do the next row
                currentTileY++;
                currentRow++;
            }
            while (currentTileLatitude > southEastCorner.Lat);

            return imageTiles;
        }
    }
}
