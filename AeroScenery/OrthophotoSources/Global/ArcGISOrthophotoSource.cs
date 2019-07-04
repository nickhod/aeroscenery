using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public class ArcGISOrthophotoSource : GenericOrthophotoSource
    {
        public new static string DefaultUrlTemplate = "http://mt1.google.com/vt/lyrs=s&x={0}&y={1}&z={2}";

        public ArcGISOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
        }

        public ArcGISOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
        }

    }
}
