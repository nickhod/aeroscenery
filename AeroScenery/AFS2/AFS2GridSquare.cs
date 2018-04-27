using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AFS2GridSquare
    {

        public string Name { get; set; }
        public List<PointLatLng> Coordinates { get; set; }

        public AFS2GridSquare()
        {
            this.Coordinates = new List<PointLatLng>();
        }

    }
}
