using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Switzerland
{
    public class GeoportalOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "https://wmts100.geo.admin.ch/1.0.0/ch.swisstopo.swissimage/default/current/3857/{zoom}/{x}/{y}.jpeg";

        public GeoportalOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public GeoportalOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpeg";
            this.source = OrthophotoSourceDirectoryName.CH_Geoportal;
            this.tiledWebMapType = TiledWebMapType.Google;

            AdditionalHttpHeaders = new Dictionary<string, string>();
            AdditionalHttpHeaders.Add("Referer", "https://map.geo.admin.ch");
            AdditionalHttpHeaders.Add("Origin", "https://map.geo.admin.ch");
            AdditionalHttpHeaders.Add("Connection", "keep-alive");
        }
    }
}
