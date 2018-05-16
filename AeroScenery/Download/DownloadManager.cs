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
using System.Threading;
using System.Threading.Tasks;

namespace AeroScenery.Download
{

    public class DownloadManager
    {
        private int downloadThreads = 4;

        private CancellationTokenSource cancellationTokenSource;

        public DownloadManager()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void StopDownloads()
        {
            cancellationTokenSource.Cancel();
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

                    tasks.Add(Task.Run(async () =>
                    {
                        var maxWait = AeroSceneryManager.Instance.Settings.DownloadWaitMs + AeroSceneryManager.Instance.Settings.DownloadWaitRandomMs;
                        var minWait = AeroSceneryManager.Instance.Settings.DownloadWaitMs - AeroSceneryManager.Instance.Settings.DownloadWaitRandomMs;
                        Random random = new Random();


                        var downloadThreadProgress = new DownloadThreadProgress();
                        downloadThreadProgress.TotalFiles = downloadsPerThread;
                        downloadThreadProgress.DownloadThreadNumber = threadNumber;

                        if (threadNumber == this.downloadThreads - 1)
                        {
                            downloadThreadProgress.TotalFiles += downloadsPerThreadMod;
                        }

                        //Debug.WriteLine("Thread " + threadNumber.ToString());
                        var cookieContainer = new CookieContainer();

                        using (var handler = new HttpClientHandler()
                        {
                            CookieContainer = cookieContainer
                        })


                        using (HttpClient httpClient = new HttpClient(handler))
                        {
                            long lastDownloadTimestamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

                            // Work through this threads share of downloads
                            for (int j = 0 + (threadNumber * downloadsPerThread); j < (threadNumber + 1) * downloadsPerThread; j++)
                            {
                                if (this.cancellationTokenSource.Token.IsCancellationRequested)
                                {
                                    break;
                                }

                                int waitTime = random.Next(minWait, maxWait);
                                var waitTimeSpan = new TimeSpan(waitTime * TimeSpan.TicksPerMillisecond);
                                await Task.Delay(waitTimeSpan);

                                this.DownloadFile(httpClient, cookieContainer, imageTiles[j], downloadDirectory);
                                downloadThreadProgress.FilesDownloaded++;
                                threadProgress.Report(downloadThreadProgress);

                                //Debug.WriteLine("Thread " + threadNumber.ToString() + " Index " + j.ToString());
                            }

                            // If this is the 'last' thread, also work through the remainder 
                            if (threadNumber == this.downloadThreads - 1)
                            {
                                for (int k = 0; k < downloadsPerThreadMod; k++)
                                {
                                    if (this.cancellationTokenSource.Token.IsCancellationRequested)
                                    {
                                        break;
                                    }


                                    int waitTime = random.Next(minWait, maxWait);
                                    var waitTimeSpan = new TimeSpan(waitTime * TimeSpan.TicksPerMillisecond);
                                    await Task.Delay(waitTimeSpan);

                                    var index = k + (downloadsPerThread * this.downloadThreads);
                                    this.DownloadFile(httpClient, cookieContainer, imageTiles[index], downloadDirectory);
                                    downloadThreadProgress.FilesDownloaded++;
                                    threadProgress.Report(downloadThreadProgress);

                                    //Debug.WriteLine("Thread " + threadNumber.ToString() + "Index " + k.ToString());

                                }
                            }


                        }
                    }, this.cancellationTokenSource.Token));
                }

                await Task.WhenAll(tasks);
            }
        }

        private void DownloadFile(HttpClient httpClient, CookieContainer cookieContainer, ImageTile imageTile, string path)
        {
            string fullFilePath = path + imageTile.FileName + "." + imageTile.ImageExtension;

            cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("APISID", Guid.NewGuid().ToString()));
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("NID", "119=" + Guid.NewGuid().ToString()));
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("NID", "129=" + Guid.NewGuid().ToString()));

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AeroSceneryManager.Instance.Settings.UserAgent);
            httpClient.DefaultRequestHeaders.Referrer = new Uri("http://google.com/");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");

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
