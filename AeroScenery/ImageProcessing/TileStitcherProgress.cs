using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.ImageProcessing
{
    public class TileStitcherProgress
    {
        public int TotalStitchedImages { get; set; }
        public int CurrentStitchedImage { get; set; }
        public int TotalImageTilesForCurrentStitchedImage { get; set; }
        public int CurrentTilesRenderedForCurrentStitchedImage { get; set; }
    }
}
