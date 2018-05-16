using AeroScenery.AFS2;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.UI
{
    public class GridSquareViewModel
    {
        public AFS2GridSquare AFS2GridSquare { get; set; }

        public GMapOverlay GMapOverlay { get; set; }
    }
}
