using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    [Serializable()]
    public class ImageTile
    {
        /// <summary>
        /// The direct download url of the image tile
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Filename extension of the image file related to this iage tile
        /// </summary>
        public string ImageExtension { get; set; }

        /// <summary>
        /// The latitude of North West corner of the tile
        /// </summary>
        public double NorthWestCornerLatitude { get; set; }

        /// <summary>
        /// The longitude of the left of the time tile
        /// </summary>
        public double NorthWestCornerLongitude { get; set; }


        /// <summary>
        /// The latitude of North West corner of the tile
        /// </summary>
        public double SouthEastCornerLatitude { get; set; }

        /// <summary>
        /// The longitude of the left of the time tile
        /// </summary>
        public double SouthEastCornerLongitude { get; set; }

        /// <summary>
        /// The width of the image tile in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The width of the image tile in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The column number (x) of the tile on this orthophoto source
        /// </summary>
        public int TileX { get; set; }

        /// <summary>
        /// The row number (y) of the tile on this orthophoto source
        /// </summary>
        public int TileY { get; set; }

        /// <summary>
        /// The column number (x) of the tile relative to this downloaded set of tiles
        /// </summary>
        public int LocalTileX { get; set; }

        /// <summary>
        /// The row number (y) of the tile relative to this downloaded set of tiles
        /// </summary>
        public int LocalTileY { get; set; }

        /// <summary>
        /// The source of this tile
        /// </summary>
        public string Source { get; set; }

        public int ZoomLevel { get; set; }

        /// <summary>
        /// The filename, or future filename of the downloaded image
        /// </summary>
        public string FileName {
            get
            {
                return String.Format("{0}_{1}_{2}_{3}", Source, ZoomLevel, TileX, TileY);
            }
        }
    }
}
