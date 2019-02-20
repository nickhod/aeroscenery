using CultivationCompiler.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CultivationCompiler.Models
{
    public class Project
    {
        public string MinimumCultivationCompilerVersion { get; set; }
        public GeoData GeoData { get; set; }
        public IList<Rule> Rules { get; set; }
        public Output Output { get; set; }
    }
}
