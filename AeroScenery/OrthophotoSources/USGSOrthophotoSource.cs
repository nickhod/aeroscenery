using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroScenery.AFS2;
using AeroScenery.Common;

namespace AeroScenery.OrthophotoSources
{
    public class USGSOrthophotoSource : IOrthophotoSource
    {
        public List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare, int zoomLevel)
        {
            throw new NotImplementedException();
        }
    }
}
