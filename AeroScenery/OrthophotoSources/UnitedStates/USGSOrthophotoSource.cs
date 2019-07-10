using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.OrthoPhotoSources;

namespace AeroScenery.OrthophotoSources.UnitedStates
{
    public class USGSOrthophotoSource : GenericOrthophotoSource
    {

        public static string DefaultUrlTemplate = "http://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/tile/{zoom}/{y}/{x}";

        public USGSOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public USGSOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.US_USGS;
            this.tiledWebMapType = TiledWebMapType.Google;
        }

    }
}
