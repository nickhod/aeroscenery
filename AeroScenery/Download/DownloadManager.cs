using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Download
{
    public class DownloadManager
    {

        public DownloadManager()
        {
        }

        public async Task DownloadImageTiles(List<ImageTile> imageTiles)
        {
            await Task.Run(() => {

                using (HttpClient httpClient = new HttpClient())
                {
                    foreach (ImageTile imageTile in imageTiles)
                    {

                        string fullFilePath = AeroSceneryManager.Instance.Settings.WorkingDirectory + imageTile.FileName + ".jpg";

                        var responseResult = httpClient.GetAsync(imageTile.URL);
                        using (var memStream = responseResult.Result.Content.ReadAsStreamAsync().Result)
                        {
                            using (var fileStream = File.Create(fullFilePath))
                            {
                                memStream.CopyTo(fileStream);
                            }

                        }
                    }

                }
            });





        }
        
    }
}
