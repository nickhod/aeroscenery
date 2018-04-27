using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class TMCColormapRegions
    {
        public int Level { get; set; }
        public double[] LonLatMin { get; set; }
        public double[] LonLatMax { get; set; }
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

        List<TMCColormapRegions> Regions { get; set; }

        public TMCFile()
        {
            Regions = new List<TMCColormapRegions>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<[file]" + "[]" + "[" + "]");
            sb.AppendLine("/t" + "<[tmcolormap_regions]" + "[]" + "[]");
            sb.AppendLine("/t/t" + "<[string8]" + "[folder_source_files]" + "[" + FolderSourceFiles + "]>");
            sb.AppendLine("/t/t" + "<[string8]" + "[folder_destination_ttc]" + "[" + FolderDestinationTTC + "]>");
            sb.AppendLine("/t/t" + "<[string8]" + "[folder_source_files]" + "[" + FolderDestinationRaw + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[write_ttc_files]" + "[" + WriteTTCFiles.ToString() + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[write_raw_files]" + "[" + WriteRawFiles.ToString() + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[do_heightmaps]" + "[" + DoHeightmaps + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[always_overwrite]" + "[" + AlwaysOverwrite + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[write_images_with_mask]" + "[" + WriteImagesWithMask + "]>");
            sb.AppendLine("/t/t" + "<[bool]" + "[always_overwrite]" + "[" + AlwaysOverwrite + "]>");
            sb.AppendLine("");
            sb.AppendLine("/t/t" + "<[list]" + "[region_list]" + "[]");

            return sb.ToString();

        }

    }
}
