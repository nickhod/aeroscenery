using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AIDFile
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<[file][][]");
            sb.AppendLine("<[tm_aerial_image_definition][][]");
            sb.AppendLine("<[string8][image][your_aerial_image.bmp]>");
            sb.AppendLine("<[string8][mask][]>     ");
            sb.AppendLine("<[vector2_float64][steps_per_pixel][3.57627868652344e-06 -2.68220901489258e-06]>");
            sb.AppendLine("<[vector2_float64][top_left][-81.8330883979797 24.5659098029137]>");
            sb.AppendLine("<[string8][coordinate_system][lonlat]>");
            sb.AppendLine("<[bool][flip_vertical][false]>  ");
            sb.AppendLine(">");
            sb.AppendLine(">");

            return sb.ToString();
        }

    }
}
