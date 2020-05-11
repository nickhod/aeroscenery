using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Norway
{
    public class GuleSiderOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "https://map02.eniro.no/geowebcache/service/tms1.0.0/aerial/{zoom}/{x}/{y}.jpeg"; 

        public GuleSiderOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public GuleSiderOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.NO_GuleSider;
            this.tiledWebMapType = TiledWebMapType.TMS;
        }
    }
}

