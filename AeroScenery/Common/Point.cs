using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    /// <summary>
    /// A Point. Why oh why is the System.Drawing one a struct?
    /// </summary>
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
