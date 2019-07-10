using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroScenery.AFS2;
using AeroScenery.Common;
using SmartFormat;

namespace AeroScenery.OrthophotoSources
{
    public class GenericOrthophotoSource : IOrthophotoSource
    {
        protected string urlTemplate;
        protected int width;
        protected int height;
        protected string imageExtension;
        protected string source;
        protected TiledWebMapType tiledWebMapType;
        protected string fakeHttpHeaders;

        public List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare, int zoomLevel)
        {
            List<ImageTile> imageTiles = new List<ImageTile>();

            // Just to make the code more readable
            var northWestCorner = afs2GridSquare.Coordinates[0];
            var northEastCorner = afs2GridSquare.Coordinates[1];
            var southEastCorner = afs2GridSquare.Coordinates[2];
            var southWestCorner = afs2GridSquare.Coordinates[3];

            // Get the tile X & Y of the first tile
            int tileX = 0;
            int tileY = 0;
            GenericOrthophotoTileHelper.LatLongToTileXY(northWestCorner.Lat, northWestCorner.Lng, zoomLevel, out tileX, out tileY);

            double currentTileNorthLatitude = 0;
            double currentTileSouthLatitude = 0;
            double currentTileWestLongitude = 0;
            double currentTileEastLongitude = 0;

            int currentTileX = tileX;
            int currentTileY = tileY;

            int currentRow = 1;
            int currentColumn = 1;

            // Work through the tiles East to West, then top North to South
            do
            {
                do
                {
                    GenericOrthophotoTileHelper.TileXYToLatLong(currentTileX, currentTileY, zoomLevel, out currentTileNorthLatitude, out currentTileWestLongitude);

                    // Get the lat long of the tile "to the left and down", which will give us the south and east edge of the previous tile
                    GenericOrthophotoTileHelper.TileXYToLatLong(currentTileX + 1, currentTileY + 1, zoomLevel, out currentTileSouthLatitude, out currentTileEastLongitude);

                    var urlParamsLookup = new Dictionary<string, object>();
                    urlParamsLookup.Add("zoom", zoomLevel);
                    urlParamsLookup.Add("x", currentTileX);
                    urlParamsLookup.Add("y", currentTileY);

                    ImageTile tile = new ImageTile();
                    tile.Width = width;
                    tile.Height = height;
                    tile.NorthLatitude = currentTileNorthLatitude;
                    tile.SouthLatitude = currentTileSouthLatitude;
                    tile.WestLongitude = currentTileWestLongitude;
                    tile.EastLongitude = currentTileEastLongitude;
                    tile.ImageExtension = imageExtension;
                    tile.TileX = currentTileX;
                    tile.TileY = currentTileY;
                    tile.LocalTileX = currentColumn;
                    tile.LocalTileY = currentRow;
                    tile.Source = source;
                    tile.ZoomLevel = zoomLevel;
                    tile.URL = Smart.Format(this.urlTemplate, urlParamsLookup);

                    imageTiles.Add(tile);

                    currentTileX++;
                    currentColumn++;
                }
                while (currentTileWestLongitude < southEastCorner.Lng);

                // Go back to the original tileX
                currentTileX = tileX;
                // Start at column 1 again
                currentColumn = 1;

                // Do the next row
                currentTileY++;
                currentRow++;
            }
            while (currentTileNorthLatitude > southEastCorner.Lat);

            return imageTiles;
        }

    }
}
