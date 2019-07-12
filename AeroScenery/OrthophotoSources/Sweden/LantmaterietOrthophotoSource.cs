using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Sweden
{
    public class LantmaterietOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "https://kso.etjanster.lantmateriet.se/karta/ortofoto/wms/v1.2?REQUEST=GetMap&LAYERS=orto050,orto025&FORMAT=image/png&TRANSPARENT=TRUE&STYLES=default,default&SERVICE=WMS&VERSION=1.1.1&SRS=EPSG:3006&WIDTH=256&HEIGHT=256&BBOX={bbox}";

        public LantmaterietOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public LantmaterietOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "png";
            this.source = OrthophotoSourceDirectoryName.SE_Lantmateriet;
            this.tiledWebMapType = TiledWebMapType.WMS;


            AdditionalHttpHeaders = new Dictionary<string, string>();
            AdditionalHttpHeaders.Add("Referer", "https://kso.etjanster.lantmateriet.se/");
            AdditionalHttpHeaders.Add("Accept-Language", "sv-SE,sv;q=0.8,en-US;q=0.5,en;q=0.3");
            AdditionalHttpHeaders.Add("Connection", "keep-alive");
            AdditionalHttpHeaders.Add("Host", "kso.etjanster.lantmateriet.se");

        }

    }
}
