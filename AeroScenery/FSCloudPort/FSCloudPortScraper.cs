using AeroScenery.Common;
using AeroScenery.Data.Models;
using CsQuery;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.FSCloudPort
{
    public class FSCloudPortScraper
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");



        public async Task<IList<FSCloudPortAirport>> ScrapeAirportsAsync()
        {
            var urlTemplate = "http://www.fscloudport.com/phdi/p1.nsf/aeroscenery?OpenView&Start={0}&Count={1}";

            Dictionary<string, FSCloudPortAirport> airports = new Dictionary<string, FSCloudPortAirport>();

            try
            {
                using (var client = new BetterWebClient(null, false))
                {
                    bool dataAvailable = true;
                    int startIndex = 1;
                    int count = 1000;

                    do
                    {
                        string url = String.Format(urlTemplate, startIndex, count);
                        string page = await client.DownloadStringTaskAsync(url);

                        if (page.Contains("No documents found"))
                        {
                            dataAvailable = false;
                        }

                        ParseAirportListHtml(page, airports);
                        startIndex += 1000;
                    }
                    while (dataAvailable);




                }
            }
            catch (System.Net.WebException ex)
            {
                log.Error("Error scraping FSCloudPort airports", ex);
            }

            return airports.Values.ToList();
        }


        private void ParseAirportListHtml(string html, Dictionary<string, FSCloudPortAirport> airports)
        {
            CQ dom = CQ.Create(html);
            var dataTableRows = dom["body > table tr"].Elements;

            int ix = 0;

            foreach (IDomElement dataTableRow in dataTableRows)
            {
                // Skip the header
                if (ix > 0)
                {
                    var airport = new FSCloudPortAirport();
                    var trCQ = CQ.Create(dataTableRow);

                    // There's only one link so get the url directly
                    var url = trCQ["a"].Attr("href");
                    airport.ICAO = trCQ["a"].Text().Trim().ToUpper();

                    // Everything else in InnerText in tds
                    var tds = trCQ["td"].Elements;

                    if (!airports.ContainsKey(airport.ICAO))
                    {
                        int jx = 0;
                        foreach (var td in tds)
                        {
                            if (jx > 0)
                            {
                                switch (jx)
                                {
                                    // Lat / lon
                                    case 1:
                                        string[] latLonArray = td.InnerText.Split(',');

                                        if (latLonArray.Length == 2)
                                        {
                                            airport.Latitude = double.Parse(latLonArray[0]);
                                            airport.Longitude = double.Parse(latLonArray[1]);
                                        }

                                        break;
                                    // Runways, buildings, static aircraft
                                    case 2:
                                        string[] valArray = td.InnerText.Split(',');

                                        if (valArray.Length == 3)
                                        {
                                            airport.Runways = int.Parse(valArray[0].Trim());
                                            airport.Buildings = int.Parse(valArray[1].Trim());
                                            airport.StaticAircraft = int.Parse(valArray[2].Trim());
                                        }

                                        break;
                                    // Name
                                    case 3:
                                        airport.Name = td.InnerText.Trim();
                                        break;
                                    // Last modified
                                    case 4:
                                        DateTime lastModified = DateTime.UtcNow;
                                        if (DateTime.TryParseExact(td.InnerText, "dd-MMM yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None, out lastModified))
                                        {
                                            airport.LastModifiedDateTime = lastModified;
                                        }
                                        break;
                                }
                            }

                            jx++;
                        }

                        airports.Add(airport.ICAO, airport);
                    }





                }

                ix++;
            }



        }
    }
}
