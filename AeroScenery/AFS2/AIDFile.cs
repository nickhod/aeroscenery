using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AIDFile
    {
        public string ImageFile { get; set; }
        public bool FlipVertical { get; set; }

        public double StepsPerPixelX { get; set; }
        public double StepsPerPixelY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<[file][][]");
            sb.AppendLine("\t<[tm_aerial_image_definition][][]");
            sb.AppendLine(String.Format("\t\t<[string8][image][{0}]>", ImageFile));
            sb.AppendLine("\t\t<[string8][mask][]>");
            sb.AppendLine(String.Format("\t\t<[vector2_float64][steps_per_pixel][{0} {1}]>", StepsPerPixelX, StepsPerPixelY));
            sb.AppendLine(String.Format("\t\t<[vector2_float64][top_left][[{0} {1}]>", X, Y));
            sb.AppendLine("\t\t<[string8][coordinate_system][lonlat]>");
            sb.AppendLine(String.Format("\t\t<[bool][flip_vertical][{0}]>", FlipVertical.ToString().ToLower()));
            sb.AppendLine("\t>");
            sb.AppendLine(">");

            return sb.ToString();
        }

    }
}
