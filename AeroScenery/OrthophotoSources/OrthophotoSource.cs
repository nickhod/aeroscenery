using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthoPhotoSources
{
    public enum OrthophotoSource
    {
        Bing,
        Google,
        ArcGIS,
        US_USGS,
        NZ_Linz,
        ES_IDEIB,
        CH_Geoportal,
        NO_NorgeBilder,
        SE_Lantmateriet,
        ES_IGN,
        JP_GSI,
        // For backwards compatibility
        USGS,
        SE_Hitta
    }

    public abstract class OrthophotoSourceDirectoryName
    {
        public static readonly string Bing = "b";
        public static readonly string Google = "g";
        public static readonly string ArcGIS = "arcg";
        public static readonly string US_USGS = "us_usgs";
        public static readonly string NZ_Linz = "nz_linz";
        public static readonly string ES_IDEIB = "es_ideib";
        public static readonly string CH_Geoportal = "ch_geo";
        public static readonly string NO_NorgeBilder = "no_nb";
        public static readonly string SE_Lantmateriet = "se_lant";
        public static readonly string ES_IGN = "es_ign";
        public static readonly string JP_GSI = "jp_gsi";
        public static readonly string SE_Hitta = "se_hitta";

    }
}
