using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Spain
{
    public class IDEIBOrthophotoSource : GenericOrthophotoSource
    {
        //public static string DefaultUrlTemplate = "https://ideib.caib.es/geoserveis/rest/services/imatges/GOIB_Orto_IB/MapServer/tile/{zoom}/{x}/{y}";
        public static string DefaultUrlTemplate = "https://ideib.caib.es/geoserveis/services/imatges/GOIB_Orto_IB/MapServer/WmsServer?REQUEST=GetMap&LAYERS=0,1,2&FORMAT=image/jpeg&TRANSPARENT=TRUE&STYLES=default,default,default&SERVICE=WMS&VERSION=1.3.0&CRS=CRS:84&WIDTH=256&HEIGHT=256&BBOX={bbox}";

        public IDEIBOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public IDEIBOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.ES_IDEIB;
            this.tiledWebMapType = TiledWebMapType.WMS;
            this.wmsVersion = "1.3";
            this.wmsCoordinateSystem = "CRS:84";
        }
    }
}
