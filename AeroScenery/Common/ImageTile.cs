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
        /// The filename, or future filename of the downloaded image
        /// </summary>
        public string FileName { get; set; }

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
    }
}
