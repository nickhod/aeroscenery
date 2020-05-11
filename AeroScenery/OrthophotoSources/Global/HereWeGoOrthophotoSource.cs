using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public class HereWeGoOrthophotoSource : GenericOrthophotoSource
    {
        //public static string DefaultUrlTemplate = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{zoom}/{y}/{x}";

        public static string DefaultUrlTemplate = "http://1.aerial.maps.api.here.com/maptile/2.1/maptile/4c6170d81c/satellite.day/{zoom}/{x}/{y}/256/jpg?app_id=VgTVFr1a0ft1qGcLCVJ6&app_code=LJXqQ8ErW71UsRUK3R33Ow";

        public HereWeGoOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public HereWeGoOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.HereWeGo;
            this.tiledWebMapType = TiledWebMapType.Google;
        }

    }
}
