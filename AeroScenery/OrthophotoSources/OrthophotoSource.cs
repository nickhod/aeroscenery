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
        USGS
    }
}
