using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Sweden
{
    public class HittaOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "https://static.hitta.se/tile/v3/1/{zoom}/{x}/{y}";

        public HittaOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public HittaOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.SE_Hitta;
            this.tiledWebMapType = TiledWebMapType.TMS;
        }
    }
}
