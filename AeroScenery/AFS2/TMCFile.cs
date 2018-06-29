using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class TMCRegion
    {
        public int Level { get; set; }
        public double LonMin { get; set; }
        public double LatMin { get; set; }
        public double LonMax { get; set; }
        public double LatMax { get; set; }
        public bool WriteImagesWithMask { get; set; }
    }

    public class TMCFile
    {
        public bool WriteImagesWithMask { get; set; }
        public bool WriteRawFiles { get; set; }
        public bool WriteTTCFiles { get; set; }
        public bool AlwaysOverwrite { get; set; }
        public bool DoHeightmaps { get; set; }
        public string FolderDestinationTTC { get; set; }
        public string FolderDestinationRaw { get; set; }
        public string FolderSourceFiles { get; set; }

        public List<TMCRegion> Regions { get; set; }

        public TMCFile()
        {
            Regions = new List<TMCRegion>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<[file]" + "[]" + "[" + "]");
            sb.AppendLine("\t" + "<[tmcolormap_regions]" + "[]" + "[]");

            if (!String.IsNullOrEmpty(this.FolderSourceFiles))
            {
                sb.AppendLine("\t\t" + "<[string8]" + "[folder_source_files]" + "[" + FolderSourceFiles + "]>");
            }

            sb.AppendLine("\t\t" + "<[bool]" + "[write_ttc_files]" + "[" + WriteTTCFiles.ToString().ToLower() + "]>");
            sb.AppendLine("\t\t" + "<[string8]" + "[folder_destination_ttc]" + "[" + FolderDestinationTTC + "]>");
            sb.AppendLine("\t\t" + "<[bool]" + "[write_raw_files]" + "[" + WriteRawFiles.ToString().ToLower() + "]>");
            sb.AppendLine("\t\t" + "<[string8]" + "[folder_destination_raw]" + "[" + FolderDestinationRaw + "]>");
            sb.AppendLine("\t\t" + "<[bool]" + "[do_heightmaps]" + "[" + DoHeightmaps.ToString().ToLower() + "]>");
            sb.AppendLine("\t\t" + "<[bool]" + "[always_overwrite]" + "[" + AlwaysOverwrite.ToString().ToLower() + "]>");
            sb.AppendLine("\t\t" + "<[bool]" + "[write_images_with_mask]" + "[" + WriteImagesWithMask.ToString().ToLower() + "]>");
            sb.AppendLine("");
            sb.AppendLine("\t\t" + "<[list]" + "[region_list]" + "[]");
            sb.AppendLine("");

            if (this.Regions != null)
            {
                foreach (var region in this.Regions)
                {
                    sb.AppendLine("\t\t\t<[tmcolormap_region][element][0]");
                    sb.AppendLine(String.Format("\t\t\t\t<[uint32] [level] [{0}]>", region.Level));
                    sb.AppendLine(String.Format("\t\t\t\t<[vector2_float64] [lonlat_min] [{0} {1}]>", region.LonMin.ToString("0.00", CultureInfo.InvariantCulture), region.LatMin.ToString("0.00", CultureInfo.InvariantCulture)));
                    sb.AppendLine(String.Format("\t\t\t\t<[vector2_float64] [lonlat_max] [{0} {1}]>", region.LonMax.ToString("0.00", CultureInfo.InvariantCulture), region.LatMax.ToString("0.00", CultureInfo.InvariantCulture)));
                    sb.AppendLine(String.Format("\t\t\t\t<[bool] [write_images_with_mask] [{0}]>", region.WriteImagesWithMask.ToString().ToLower()));
                    sb.AppendLine("\t\t\t" + ">");
                    sb.AppendLine("");
                }
            }


            sb.AppendLine("");
            sb.AppendLine("\t\t" + ">");
            sb.AppendLine("\t" + ">");
            sb.AppendLine(">");

            return sb.ToString();

        }

    }
}
