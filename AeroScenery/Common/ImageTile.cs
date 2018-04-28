using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
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
        /// The latitude of the top of the image tile 
        /// </summary>
        public double LatitudeTop { get; set; }

        /// <summary>
        /// The longitude of the left of the time tile
        /// </summary>
        public double LongitudeLeft { get; set; }

        /// <summary>
        /// The fraction increase in latitude per image pixel
        /// </summary>
        public double LatitudeStepsPerPixel { get; set; }

        /// <summary>
        /// The fraction increaese in longitude per image pixel
        /// </summary>
        public double LongitudeStepsPerPixel { get; set; }

        /// <summary>
        /// The size of the image tile in pixels
        /// </summary>
        public int Size { get; set; }
    }
}
