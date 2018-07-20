using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AFSLevel
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public bool IsChecked { get; set; }

        public AFSLevel()
        {
            this.IsChecked = false;        
        }

        public AFSLevel(string name, int level)
        {
            this.Name = name;
            this.Level = level;
            this.IsChecked = false;
        }
    }
}
