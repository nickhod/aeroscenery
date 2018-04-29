using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AIDFileGenerator
    {
        
        public async Task GenerateAIDFilesAsync(List<ImageTile> imageTiles)
        {
            await Task.Run(() => {

                foreach(ImageTile imageTile in imageTiles)
                {
                    AIDFile aidFile = new AIDFile();

                    //aidFile.ImageFile = imageTile.FileName + "." + imageTile.ImageExtension;
                    //aidFile.FlipVertical = false;
                    //aidFile.StepsPerPixelX = imageTile.LatitudeStepsPerPixel;
                    //aidFile.StepsPerPixelY = imageTile.LongitudeStepsPerPixel;
                    //aidFile.X = imageTile.LatitudeTop;
                    //aidFile.Y = imageTile.LongitudeLeft;

                    var aidFileStr = aidFile.ToString();

                    string path = AeroSceneryManager.Instance.Settings.WorkingDirectory + imageTile.FileName + ".aid";

                    if (!File.Exists(path))
                    {
                        File.WriteAllText(path, aidFileStr);
                    }

                }
            });

            return;
        }
    }
}
