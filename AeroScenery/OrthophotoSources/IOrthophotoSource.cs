using AeroScenery.AFS2;
using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources
{
    public interface IOrthophotoSource
    {
        List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare);
    }
}
