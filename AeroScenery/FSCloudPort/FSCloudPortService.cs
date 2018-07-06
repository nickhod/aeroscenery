using AeroScenery.Data;
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
        }

        public async Task UpdateAirportsIfRequiredAsync()
        {
            var airports = await this.scraper.ScrapeAirportsAsync();
            await dataRepository.UpdateFSCloudPortAirportsAsync(airports);

        }
    }
}
