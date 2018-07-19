using AeroScenery.Data;
using AeroScenery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.FSCloudPort
{
    public class FSCloudPortService
    {
        private FSCloudPortScraper scraper;
        private SqlLiteDataRepository dataRepository;

        public FSCloudPortService()
        {
            this.scraper = new FSCloudPortScraper();
            dataRepository = new SqlLiteDataRepository();
            dataRepository.Settings = AeroSceneryManager.Instance.Settings;
        }

        public async Task UpdateAirportsIfRequiredAsync()
        {
            await Task.Run(async () =>
            {
                var lastCached = dataRepository.GetFSCloudPortAirportsLastCachedDateTime();

                // Only update airports if we cached them longer than a day ago
                if (lastCached <= DateTime.UtcNow.AddDays(-1))
                {
                    var airports = await this.scraper.ScrapeAirportsAsync();
                    dataRepository.UpdateFSCloudPortAirports(airports);
                }
            });

        }

        public async Task<IList<FSCloudPortAirport>> GetAirportsAsync()
        {
            return await Task.Run(() =>
            {
                return dataRepository.GetAllFSCloudPortAirports();
            });

        }
    }
}
