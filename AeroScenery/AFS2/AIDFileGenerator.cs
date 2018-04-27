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
                    var aidFileStr = aidFile.ToString();

                    string path = AeroSceneryManager.Instance.Settings.WorkingDirectory + imageTile.FileName + ".aid";

                    // This text is added only once to the file.
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
