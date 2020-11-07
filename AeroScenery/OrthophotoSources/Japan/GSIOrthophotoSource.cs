using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Japan
{
    public class GSIOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "http://cyberjapandata.gsi.go.jp/xyz/ort/{zoom}/{x}/{y}.jpg";

        public GSIOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public GSIOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.JP_GSI;
            this.tiledWebMapType = TiledWebMapType.Google;
        }
    }
}
