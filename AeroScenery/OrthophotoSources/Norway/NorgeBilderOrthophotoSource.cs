using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AeroScenery.OrthophotoSources.Norway
{
    public class NorgeBilderOrthophotoSource : GenericOrthophotoSource
    {
        public static string DefaultUrlTemplate = "https://waapi.webatlas.no/maptiles/tiles/webatlas-orto-newup/wa_grid/{zoom}/{x}/{y}.jpeg?api_key=b8e36d51-119a-423b-b156-d744d54123d5";

        public NorgeBilderOrthophotoSource()
        {
            this.urlTemplate = DefaultUrlTemplate;
            Initialize();
        }

        public NorgeBilderOrthophotoSource(string urlTemplate)
        {
            this.urlTemplate = urlTemplate;
            Initialize();
        }

        private void Initialize()
        {
            this.width = 256;
            this.height = 256;
            this.imageExtension = "jpg";
            this.source = OrthophotoSourceDirectoryName.NO_NorgeBilder;
            this.tiledWebMapType = TiledWebMapType.Google;
        }


        // ** Leaving previous attempts at this provider for future reference

        //public static string DefaultUrlTemplate = "https://agsservices.norgeibilder.no/arcgis/rest/services/Nibcache_UTM33_EUREF89_v2/MapServer/tile/{zoom}/{x}/{y}?token={token}";
        //public static string DefaultUrlTemplate = "http://gatekeeper1.geonorge.no/BaatGatekeeper/gk/gk.nib_utm33_wmts_v2?&gkt=288C74794A5472008F6943FEEDABBCB1782D7192ABD2113466BAD6D8528F432EE335985CF900D7FDD4CB87562C7799F220C2F7F745477488D018F0853CD0DA1&layer=Nibcache_UTM33_EUREF89&style=default&tilematrixset=default028mm&Service=WMTS&Request=GetTile&Version=1.0.0&Format=image%2Fpng&TileMatrix={zoom}&TileCol={x}&TileRow={y}";
        //public static string DefaultUrlTemplate = "https://agsservices-dev.norgeibilder.no:443/arcgis/services/Nibcache_UTM33_EUREF89_v2/MapServer/WmsServer?REQUEST=GetMap&LAYERS=0&FORMAT=image/jpeg&TRANSPARENT=TRUE&STYLES=default&SERVICE=WMS&VERSION=1.3.0&CRS=CRS:84&WIDTH=256&HEIGHT=256&BBOX={bbox}";


        //public NorgeBilderOrthophotoSource()
        //{
        //    this.urlTemplate = DefaultUrlTemplate;
        //    Initialize();
        //}

        //public NorgeBilderOrthophotoSource(string urlTemplate)
        //{
        //    this.urlTemplate = urlTemplate;
        //    Initialize();
        //}

        //private void Initialize()
        //{
        //    this.width = 256;
        //    this.height = 256;
        //    this.imageExtension = "jpg";
        //    this.source = OrthophotoSourceDirectoryName.NO_NorgeBilder;
        //    //his.tiledWebMapType = TiledWebMapType.TMS;
        //    this.tiledWebMapType = TiledWebMapType.WMS;
        //    this.wmsVersion = "1.3";
        //    this.wmsCoordinateSystem = "CRS:84";

        //    AdditionalHttpHeaders = new Dictionary<string, string>();
        //    AdditionalHttpHeaders.Add("Referer", "www.norgeibilder.no");
        //}

        //public new List<ImageTile> ImageTilesForGridSquares(AFS2GridSquare afs2GridSquare, int zoomLevel)
        //{
        //    this.AdditionalUrlParams = new Dictionary<string, string>();

        //    //var cookieContainer = new CookieContainer();

        //    //using (var handler = new HttpClientHandler()
        //    //{
        //    //    CookieContainer = cookieContainer
        //    //})


        //    using (HttpClient httpClient = new HttpClient(handler))
        //    {

        //        cookieContainer = new CookieContainer();
        //        cookieContainer.Add(new Uri("https://www.norgeibilder.no/"), new Cookie("APISID", Guid.NewGuid().ToString()));
        //        cookieContainer.Add(new Uri("https://www.norgeibilder.no/"), new Cookie("NID", "119=" + Guid.NewGuid().ToString()));
        //        cookieContainer.Add(new Uri("https://www.norgeibilder.no/"), new Cookie("NID", "129=" + Guid.NewGuid().ToString()));


        //        httpClient.DefaultRequestHeaders.Clear();
        //        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AeroSceneryManager.Instance.Settings.UserAgent);
        //        httpClient.DefaultRequestHeaders.Referrer = new Uri("http://google.com/");
        //        httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        //        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
        //        httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");

        //        var responseResult = httpClient.GetStringAsync("https://www.norgeibilder.no/").Result;

        //        string pattern = @"nib[Tt]oken:.?'(.*?)'";

        //        var matches = Regex.Matches(responseResult, pattern);

        //        if (matches != null)
        //        {
        //            var groups = matches[0].Groups;

        //            if (groups != null && groups.Count > 1)
        //            {
        //                this.AdditionalUrlParams.Add("token", groups[1].Value);
        //            }

        //        }

        //    }

        //    return base.ImageTilesForGridSquares(afs2GridSquare, zoomLevel);
        //}
    }
}
