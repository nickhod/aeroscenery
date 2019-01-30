using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.ImageProcessing
{
    public class ImageProcessingSettings
    {
        public int? BrightnessAdjustment { get; set; }
        public int? ContrastAdjustment { get; set; }
        public int? SaturationAdjustment { get; set; }
        public int? SharpnessAdjustment { get; set; }
        public int? RedAdjustment { get; set; }
        public int? GreenAdjustment { get; set; }
        public int? BlueAdjustment { get; set; }
    }
}
