using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.OrthophotoSources;
using AeroScenery.OrthophotoSources.Switzerland;
using AeroScenery.OrthoPhotoSources;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroScenery.Download
{

    public class DownloadManager
    {
        private int downloadThreads = 4;
        private int maxDownloadRetryAttempts = 5;
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        private CancellationTokenSource cancellationTokenSource;

        public DownloadManager()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void StopDownloads()
        {
            cancellationTokenSource.Cancel();
        }

        public async Task DownloadImageTiles(OrthophotoSource orthophotoSource, List<ImageTile> imageTiles, IProgress<DownloadThreadProgress> threadProgress, 
            string downloadDirectory, GenericOrthophotoSource orthophotoSourceInstance)
        {

            // Reset cancellation token status
            cancellationTokenSource = new CancellationTokenSource();

            if (imageTiles.Count > 0)
            {
                log.InfoFormat("Beginning download of {0} image tiles from {1}", imageTiles.Count, orthophotoSource.ToString());

                int downloadsPerThread = imageTiles.Count / this.downloadThreads;
                int downloadsPerThreadMod = imageTiles.Count % this.downloadThreads;

                var tasks = new List<Task>();

                // Spawn the required number of threads
                for (int i = 0; i < this.downloadThreads; i++)
                {
                    var threadNumber = i;

                    tasks.Add(Task.Run(async () =>
                    {
                        var xmlSerializer = new XmlSerializer(typeof(ImageTile));


                        var maxWait = AeroSceneryManager.Instance.Settings.DownloadWaitMs.Value + AeroSceneryManager.Instance.Settings.DownloadWaitRandomMs.Value;
                        var minWait = AeroSceneryManager.Instance.Settings.DownloadWaitMs.Value - AeroSceneryManager.Instance.Settings.DownloadWaitRandomMs.Value;
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
                        {
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

                                    // We nee to re-eval this. If the user has cancelled, image tiles will be cleared
                                    if (imageTiles != null && imageTiles.Count > 0)
                                    {
                                        await this.DownloadFile(httpClient, cookieContainer, imageTiles[j], downloadDirectory, orthophotoSource, orthophotoSourceInstance, waitTimeSpan);
                                    }

                                    // We nee to re-eval this. If the user has cancelled, image tiles will be cleared
                                    if (imageTiles != null && imageTiles.Count > 0)
                                    {
                                        this.SaveImageTileAeroFile(xmlSerializer, imageTiles[j], downloadDirectory);
                                    }

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

                                        // We nee to re-eval this. If the user has cancelled, image tiles will be cleared
                                        if (imageTiles != null && imageTiles.Count > 0)
                                        {
                                            await this.DownloadFile(httpClient, cookieContainer, imageTiles[index], downloadDirectory, orthophotoSource, orthophotoSourceInstance, waitTimeSpan);
                                        }

                                        // We nee to re-eval this. If the user has cancelled, image tiles will be cleared
                                        if (imageTiles != null && imageTiles.Count > 0)
                                        {
                                            this.SaveImageTileAeroFile(xmlSerializer, imageTiles[index], downloadDirectory);
                                        }


                                        downloadThreadProgress.FilesDownloaded++;
                                        threadProgress.Report(downloadThreadProgress);

                                        //Debug.WriteLine("Thread " + threadNumber.ToString() + "Index " + k.ToString());

                                    }
                                }


                            }

                        }



                        xmlSerializer = null;

                    }, this.cancellationTokenSource.Token));
                }

                await Task.WhenAll(tasks);
                log.InfoFormat("Finished download of {0} image tiles from {1}", imageTiles.Count, orthophotoSource.ToString());

            }
        }

        private async Task DownloadFile(HttpClient httpClient, CookieContainer cookieContainer, ImageTile imageTile, string path, 
            OrthophotoSource orthophotoSource, GenericOrthophotoSource orthophotoSourceInstance, TimeSpan retryWaitTimeSpan)
        {
            string fullFilePath = path + imageTile.FileName + "." + imageTile.ImageExtension;

            cookieContainer = new CookieContainer();
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("APISID", Guid.NewGuid().ToString()));
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("NID", "119=" + Guid.NewGuid().ToString()));
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("NID", "129=" + Guid.NewGuid().ToString()));
            cookieContainer.Add(new Uri(imageTile.URL), new Cookie("_ga", "GA1.3.456545.34536772112" + Guid.NewGuid().ToString()));


            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AeroSceneryManager.Instance.Settings.UserAgent);
            httpClient.DefaultRequestHeaders.Referrer = new Uri("http://google.com/");
            httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");

            if (orthophotoSourceInstance.AdditionalHttpHeaders != null)
            {
                foreach (var key in orthophotoSourceInstance.AdditionalHttpHeaders.Keys)
                {
                    if (key.ToLower() == "referrer" || key.ToLower() == "referer")
                    {
                        httpClient.DefaultRequestHeaders.Referrer = new Uri(orthophotoSourceInstance.AdditionalHttpHeaders[key]);
                    }
                    else
                    {
                        httpClient.DefaultRequestHeaders.Add(key, orthophotoSourceInstance.AdditionalHttpHeaders[key]);
                    }
                }
            }

            try
            {
                var responseResult = httpClient.GetAsync(imageTile.URL);

                bool saveFile = true;


                switch (orthophotoSource)
                {
                    case OrthophotoSource.Bing:
                        // If we are Bing we might be served a valid image but there is really no tile available
                        // Check the Bing tile info header
                        if (responseResult.Result.Headers.Contains("X-VE-Tile-Info"))
                        {
                            var tileInfoHeaderValue = responseResult.Result.Headers.GetValues("X-VE-Tile-Info").FirstOrDefault();

                            // If there is really no file, the header value will be no-tile
                            // In this case we shouldn't save the image
                            if (tileInfoHeaderValue == "no-tile")
                            {
                                saveFile = false;
                            }
                        }

                        break;
                    case OrthophotoSource.US_USGS:

                        // If we don't get a success status code or not modified, try again
                        if (!(responseResult.Result.IsSuccessStatusCode || responseResult.Result.StatusCode == HttpStatusCode.NotModified))
                        {
                            log.DebugFormat("Invalid USGS tile {0}. Status is {1}", imageTile.FileName, responseResult.Result.StatusCode);

                            for (int i = 0; i < maxDownloadRetryAttempts; i++)
                            {
                                await Task.Delay(retryWaitTimeSpan);

                                responseResult = httpClient.GetAsync(imageTile.URL);

                                if (responseResult.Result.IsSuccessStatusCode || responseResult.Result.StatusCode == HttpStatusCode.NotModified)
                                {
                                    break;
                                }
                                else
                                {
                                    log.DebugFormat("Invalid USGS tile {0}. Retry {1} Status is {0}", imageTile.FileName, i, responseResult.Result.StatusCode);
                                }
                            }
                        }

                        break;
                }


                bool gzipped = false;

                if (responseResult.Result.Content.Headers.Contains("Content-Encoding"))
                {
                    var contentEncodingValue = responseResult.Result.Content.Headers.GetValues("Content-Encoding").FirstOrDefault();

                    if (contentEncodingValue == "gzip")
                    {
                        gzipped = true;
                    }

                }


                if (saveFile)
                {
                    using (var memStream = responseResult.Result.Content.ReadAsStreamAsync().Result)
                    {
                        if (gzipped)
                        {
                            using (var fileStream = File.Create(fullFilePath))
                            {
                                using (GZipStream compressionStream = new GZipStream(memStream, CompressionMode.Decompress))
                                {
                                    compressionStream.CopyTo(fileStream);
                                }
                            }

                        }
                        else
                        {
                            using (var fileStream = File.Create(fullFilePath))
                            {
                                var fileSize = memStream.Length;

                                if (fileSize == 1033)
                                {
                                    //var afd = "asdf";
                                }

                                memStream.CopyTo(fileStream);
                            }
                        }


                    }
                }


            }
            catch (Exception ex)
            {
                log.Error("There was an error downloading " + imageTile.URL, ex);
            }
            finally
            {

            }
        }

        private void SaveImageTileAeroFile(XmlSerializer xmlSerializer, ImageTile imageTile, string path)
        {
            using (TextWriter tw = new StreamWriter(path + imageTile.FileName + ".aero"))
            {
                xmlSerializer.Serialize(tw, imageTile);
            }
        }

    }
}
