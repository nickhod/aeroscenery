using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultivationCompiler.AFS
{
    public class TOCFile
    {
        public class TOCPlant
        {

        }

        public class TOCBuilding
        {

        }

        public class TOCLight
        {

        }

        public class TOCFile
        {
            public string ToString()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<[file][][map]");
                sb.AppendLine("\t<[cultivation][][]");

                // Plant ---------------------
                sb.AppendLine("\t\t<[list_plant][plant_list][]");

                sb.AppendLine("\t\t\t<[plant][element][0]");
                sb.AppendLine("\t\t\t\t<[vector3_float64][position][-95.555474 30.054864 0]>");
                sb.AppendLine("\t\t\t\t<[vector2_float32][height_range][8 25]>");
                sb.AppendLine("\t\t\t\t<[string8][group][broadleaf]>");
                sb.AppendLine("\t\t\t\t<[string8][species][01_1650]>");
                sb.AppendLine("\t\t\t>");

                sb.AppendLine("\t\t>");
                // --------------------------

                sb.AppendLine("\t>");
                sb.AppendLine(">");

                return sb.ToString();
            }
        }
    }
}
