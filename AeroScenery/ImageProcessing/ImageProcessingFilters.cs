using AForge;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.ImageProcessing
{
    public class ImageProcessingFilters
    {
        public IList<IFilter> GetFilterList(ImageProcessingSettings imageProcessingSettings)
        {
            var filterList = new List<IFilter>();

            // Brightness
            if (imageProcessingSettings.BrightnessAdjustment.Value != 0)
            {
                BrightnessCorrection filter = new BrightnessCorrection(imageProcessingSettings.BrightnessAdjustment.Value);
                filterList.Add(filter);
            }

            // Contrast
            if (imageProcessingSettings.ContrastAdjustment.Value != 0)
            {
                ContrastCorrection filter = new ContrastCorrection(imageProcessingSettings.ContrastAdjustment.Value);
                filterList.Add(filter);
            }

            // Saturation
            if (imageProcessingSettings.SaturationAdjustment.Value != 0)
            {
                // Only use 60% of the available correction range
                float adjustedValue = ((float)imageProcessingSettings.SaturationAdjustment.Value / 100) * 60;

                float value = (float)adjustedValue / 100;
                SaturationCorrection filter = new SaturationCorrection(value);
                filterList.Add(filter);
            }

            // Sharpness
            if (imageProcessingSettings.SharpnessAdjustment.Value != 0)
            {
                double sigma = 1.5;
                GaussianSharpen filter = new GaussianSharpen(sigma, imageProcessingSettings.SharpnessAdjustment.Value);
                filterList.Add(filter);
            }


            // Red, Green, Blue
            if (imageProcessingSettings.RedAdjustment.Value != 0 ||
                imageProcessingSettings.GreenAdjustment.Value != 0 ||
                imageProcessingSettings.BlueAdjustment.Value != 0)
            {
                LevelsLinear filter = new LevelsLinear();

                if (imageProcessingSettings.RedAdjustment.Value != 0)
                {
                    float val = ((float)imageProcessingSettings.RedAdjustment.Value / 100) * 255;

                    if (imageProcessingSettings.RedAdjustment.Value > 0)
                    {
                        var finalVal = 255 - (int)val;
                        filter.InRed = new IntRange(0, finalVal);
                    }
                    else
                    {
                        val = val * -1;
                        filter.InRed = new IntRange((int)val, 255);
                    }
                }

                if (imageProcessingSettings.GreenAdjustment.Value != 0)
                {
                    float val = ((float)imageProcessingSettings.GreenAdjustment.Value / 100) * 255;

                    if (imageProcessingSettings.GreenAdjustment.Value > 0)
                    {
                        var finalVal = 255 - (int)val;
                        filter.InGreen = new IntRange(0, finalVal);
                    }
                    else
                    {
                        val = val * -1;
                        filter.InGreen = new IntRange((int)val, 255);
                    }
                }

                if (imageProcessingSettings.BlueAdjustment.Value != 0)
                {
                    float val = ((float)imageProcessingSettings.BlueAdjustment.Value / 100) * 255;

                    if (imageProcessingSettings.BlueAdjustment.Value > 0)
                    {
                        var finalVal = 255 - (int)val;
                        filter.InBlue = new IntRange(0, finalVal);
                    }
                    else
                    {
                        val = val * -1;
                        filter.InBlue = new IntRange((int)val, 255);
                    }
                }

                filterList.Add(filter);
            }


            return filterList;
        }
    }
}
