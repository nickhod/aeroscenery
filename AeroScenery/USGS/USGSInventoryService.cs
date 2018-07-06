using AeroScenery.USGS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS
{
    public class USGSInventoryService
    {
        private string baseUrl = "https://earthexplorer.usgs.gov/inventory/json/v/1.4.0/";
        private string apiKey;

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var requestString = JsonConvert.SerializeObject(loginRequest);
                StringContent stringContent = new StringContent(requestString, UnicodeEncoding.UTF8, "application/json");

                var fullUrl = String.Format("login?jsonRequest={0}", requestString);

                var response = await client.PostAsync(fullUrl, stringContent);
                string responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<LoginResponse>(responseString);
                this.apiKey = result.Data;
                return result;
            }
        }

        /// <summary>
        /// Dataset Search
        /// </summary>
        /// <param name="datasetSearchRequest"></param>
        /// <returns></returns>
        public async Task<DatasetSearchResponse> DatasetSearchAsync(DatasetSearchRequest datasetSearchRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                if (!String.IsNullOrEmpty(this.apiKey) && String.IsNullOrEmpty(datasetSearchRequest.ApiKey))
                {
                    datasetSearchRequest.ApiKey = this.apiKey;
                }

                var requestString = JsonConvert.SerializeObject(datasetSearchRequest);
                StringContent stringContent = new StringContent(requestString, UnicodeEncoding.UTF8, "application/json");

                var fullUrl = String.Format("datasets?jsonRequest={0}", requestString);

                var response = await client.PostAsync(fullUrl, stringContent);
                string responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<DatasetSearchResponse>(responseString);
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneSearchRequest"></param>
        /// <returns></returns>
        public async Task<SceneSearchResponse> SceneSearch(SceneSearchRequest sceneSearchRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                if (!String.IsNullOrEmpty(this.apiKey) && String.IsNullOrEmpty(sceneSearchRequest.ApiKey))
                {
                    sceneSearchRequest.ApiKey = this.apiKey;
                }

                var requestString = JsonConvert.SerializeObject(sceneSearchRequest);
                StringContent stringContent = new StringContent(requestString, UnicodeEncoding.UTF8, "application/json");

                var fullUrl = String.Format("search?jsonRequest={0}", requestString);

                var response = await client.PostAsync(fullUrl, stringContent);
                string responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<SceneSearchResponse>(responseString);
                return result;
            }
        }


        public async Task<DownloadOptionsResponse> DownloadOptions(DownloadOptionsRequest downloadOptionsRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                if (!String.IsNullOrEmpty(this.apiKey) && String.IsNullOrEmpty(downloadOptionsRequest.ApiKey))
                {
                    downloadOptionsRequest.ApiKey = this.apiKey;
                }

                var requestString = JsonConvert.SerializeObject(downloadOptionsRequest);
                StringContent stringContent = new StringContent(requestString, UnicodeEncoding.UTF8, "application/json");

                var fullUrl = String.Format("downloadoptions?jsonRequest={0}", requestString);

                var response = await client.PostAsync(fullUrl, stringContent);
                string responseString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<DownloadOptionsResponse>(responseString);
                return result;
            }
        }
    }
}
