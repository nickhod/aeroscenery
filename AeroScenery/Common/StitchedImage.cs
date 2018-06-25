using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    [Serializable()]
    public class StitchedImage
    {
        /// <summary>
        /// Filename extension of the image file related to this iage tile
        /// </summary>
        public string ImageExtension { get; set; }

        /// <summary>
        /// The latitude of North West corner of the tile
        /// </summary>
        public double NorthLatitude { get; set; }

        /// <summary>
        /// The longitude of the left of the time tile
        /// </summary>
        public double WestLongitude { get; set; }


        /// <summary>
        /// The latitude of North West corner of the tile
        /// </summary>
        public double SouthLatitude { get; set; }

        /// <summary>
        /// The longitude of the left of the time tile
        /// </summary>
        public double EastLongitude { get; set; }

        /// <summary>
        /// The width of the image tile in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The width of the image tile in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The orthophoto source of this stitched image
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The zoom level of the orthophoto source of this stitched image
        /// </summary>
        public int ZoomLevel { get; set; }

        /// <summary>
        /// The column number of this image in the set of stiched images
        /// </summary>
        public int StichedImageSetX { get; set; }

        /// <summary>
        /// The row number of this image in the set of stiched images
        /// </summary>
        public int StichedImageSetY { get; set; }

        /// <summary>
        /// The filename, or future filename of the downloaded image
        /// </summary>
        public string FileName
        {
            get
            {
                return String.Format("{0}_{1}_stitch_{2}_{3}", Source, ZoomLevel, StichedImageSetX, StichedImageSetY);
            }
        }

    }
}
