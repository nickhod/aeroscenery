using AeroScenery.AFS2;
using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int downloadThreads = 4;

        public DownloadManager()
        {
        }

        public async Task DownloadImageTiles(List<ImageTile> imageTiles, IProgress<DownloadThreadProgress> threadProgress, string downloadDirectory)
        {
            if (imageTiles.Count > 0)
            {
                int downloadsPerThread = imageTiles.Count / this.downloadThreads;
                int downloadsPerThreadMod = imageTiles.Count % this.downloadThreads;

                var tasks = new List<Task>();

                // Spawn the required number of threads
                for (int i = 0; i < this.downloadThreads; i++)
                {
                    var threadNumber = i;

                    tasks.Add(Task.Run(() =>
                    {
                        var downloadThreadProgress = new DownloadThreadProgress();
                        downloadThreadProgress.TotalFiles = downloadsPerThread;
                        downloadThreadProgress.DownloadThreadNumber = threadNumber;

                        if (threadNumber == this.downloadThreads - 1)
                        {
                            downloadThreadProgress.TotalFiles += downloadsPerThreadMod;
                        }

                        //Debug.WriteLine("Thread " + threadNumber.ToString());

                        using (HttpClient httpClient = new HttpClient())
                        {
                            // Work through this threads share of downloads
                            for (int j = 0 + (threadNumber * downloadsPerThread); j < (threadNumber + 1) * downloadsPerThread; j++)
                            {
                                this.DownloadFile(httpClient, imageTiles[j], downloadDirectory);
                                downloadThreadProgress.FilesDownloaded++;
                                threadProgress.Report(downloadThreadProgress);

                                //Debug.WriteLine("Thread " + threadNumber.ToString() + " Index " + j.ToString());
                            }

                            // If this is the 'last' thread, also work through the remainder 
                            if (threadNumber == this.downloadThreads - 1)
                            {
                                for (int k = 0; k < downloadsPerThreadMod; k++)
                                {
                                    var index = k + (downloadsPerThread * this.downloadThreads);
                                    this.DownloadFile(httpClient, imageTiles[index], downloadDirectory);
                                    downloadThreadProgress.FilesDownloaded++;
                                    threadProgress.Report(downloadThreadProgress);

                                    //Debug.WriteLine("Thread " + threadNumber.ToString() + "Index " + k.ToString());

                                }
                            }


                        }
                    }));
                }

                await Task.WhenAll(tasks);
            }
        }

        private void DownloadFile(HttpClient httpClient, ImageTile imageTile, string path)
        {
            string fullFilePath = path + imageTile.FileName + "." + imageTile.ImageExtension;

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
}
