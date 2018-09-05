using AeroScenery.Common;
using AeroScenery.USGS.Models;
using CsQuery;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS
{
    /// <summary>
    /// A class to work with the USGS web app direclty
    /// </summary>
    public class USGSScraper
    {
        private CookieContainer cookieContainer;

        public async Task LoginAsync(string username, string password)
        {
            // As of 2018-07-05 the following is posted to login
            // __ncforminfo:{from hidden input}
            // csrf_token:{from hidden input}
            // password:******
            // username:username

            var loginUrl = "https://ers.cr.usgs.gov/login/";

            this.cookieContainer = new CookieContainer();

            using (var client = new BetterWebClient(this.cookieContainer, false))
            {
                string data = await client.DownloadStringTaskAsync(loginUrl);

                CQ dom = CQ.Create(data);
                var csrfValue = dom["#csrf_token"].Val();
                var ncFormInfo = dom["input[name='__ncforminfo']"].Val();

                var postParams = new NameValueCollection();
                postParams.Add("__ncforminfo", ncFormInfo);
                postParams.Add("csrf_token", csrfValue);
                postParams.Add("username", username);
                postParams.Add("password", password);

                client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.Headers.Add("Accept-Encoding", "gzip, deflate");
                client.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                client.Headers.Add("Cache-Control", "no-cache");
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("Host", "ers.cr.usgs.gov");
                client.Headers.Add("Pragma", "no-cache");
                client.Headers.Add("Referer", loginUrl);
                client.Headers.Add("User-Agent", AeroSceneryManager.Instance.Settings.UserAgent);

                var responseBytes = await client.UploadValuesTaskAsync(loginUrl, "POST", postParams);
                string responsebody = Encoding.UTF8.GetString(responseBytes);

                var locationHeader = client.ResponseHeaders["Location"].ToString();

                //string data2 = await client.DownloadStringTaskAsync("https://earthexplorer.usgs.gov/");

                int i = 0;

            }
        }

        public async Task DownloadAsync(string downloadUrl, string sdf)
        {
            string downloadLocation = "";

            using (var client = new BetterWebClient(this.cookieContainer, false))
            {
                string downloadPage = await client.DownloadStringTaskAsync(downloadUrl);

                CQ downloadPageDom = CQ.Create(downloadPage);
                var downloadInputButton = downloadPageDom.Find("#optionsPage input");
                downloadLocation = downloadInputButton.Attr("onClick");

                // e.g. window.location='https://earthexplorer.usgs.gov/download/4220/ASTGDEMV2_0N51W004/STANDARD/INVSVC'
                downloadLocation = downloadLocation.Replace("window.location", "");
                downloadLocation = downloadLocation.Replace("=", "");
                downloadLocation = downloadLocation.Trim('\'');
            }

            if (!String.IsNullOrEmpty(downloadLocation))
            {
                using (var client = new BetterWebClient(this.cookieContainer, false))
                {
                    string nextPage = await client.DownloadStringTaskAsync(downloadLocation);
                    CQ nextPageDom = CQ.Create(nextPage);

                    string nextPageLower = nextPage.ToLower();

                    if (nextPageLower.Contains("agree to") && nextPageLower.Contains("end user license"))
                    {
                        await this.AcceptDownloadLicenseAgreement(client, downloadLocation, nextPageDom);
                    }


                }
            }

        }

        private async Task AcceptDownloadLicenseAgreement(BetterWebClient client, string pageUrl, CQ licencePageDom)
        {
            var mainContentForm = licencePageDom.Find("#maincontent form");
            var licenseCode = mainContentForm.Find("input[name='licenseCode']").Val();


            var postParams = new NameValueCollection();
            postParams.Add("licenseCode", licenseCode);

            client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            client.Headers.Add("Cache-Control", "no-cache");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("Pragma", "no-cache");
            client.Headers.Add("User-Agent", AeroSceneryManager.Instance.Settings.UserAgent);
            client.Headers.Add("Referer", pageUrl);


            await client.UploadValuesTaskAsync(pageUrl, "POST", postParams);
            //string responsebody = Encoding.UTF8.GetString(responseBytes);

            var locationHeader = client.ResponseHeaders["Location"].ToString();

        }
    }
}
