using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroScenery.AFS2;
using AeroScenery.Common;

namespace AeroScenery.OrthophotoSources
{
    public class GoogleOrthophotoSource : IOrthophotoSource
    {
        private string urlTemplate = "http://mt1.google.com/vt/lyrs=s&x={0}&y={1}&z={2}";


        public List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare, int zoomLevel)
        {
            List<ImageTile> imageTiles = new List<ImageTile>();

            // Just to make the code more readable
            var northWestCorner = afs2GridSquare.Coordinates[0];
            var northEastCorner = afs2GridSquare.Coordinates[1];
            var southEastCorner = afs2GridSquare.Coordinates[2];
            var southWestCorner = afs2GridSquare.Coordinates[3];

            // Get the tile X & Y of the frst tile
            int tileX = 0;
            int tileY = 0;
            GoogleHelper.LatLongToTileXY(northWestCorner.Lat, northWestCorner.Lng, zoomLevel, out tileX, out tileY);

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
                    GoogleHelper.TileXYToLatLong(currentTileX, currentTileY, zoomLevel, out currentTileLatitude, out currentTileLongitude);

                    ImageTile tile = new ImageTile();
                    tile.Width = 256;
                    tile.Height = 256;
                    tile.NorthWestCornerLatitude = currentTileLatitude;
                    tile.NorthWestCornerLongitude = currentTileLongitude;
                    tile.ImageExtension = "jpg";
                    tile.TileX = currentTileX;
                    tile.TileY = currentTileY;
                    tile.LocalTileX = currentColumn;
                    tile.LocalTileY = currentRow;
                    tile.Source = "g";
                    tile.ZoomLevel = zoomLevel;
                    tile.URL = String.Format(this.urlTemplate, currentTileX, currentTileY, zoomLevel);

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
