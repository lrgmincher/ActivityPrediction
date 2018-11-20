using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CompileResults.Statistics;

namespace CompileResults
{
    public class GeoNames : IGeoNames

    {
        public async Task<CountryDetails> getCounty(double latt, double longt)
        {
            GeoNamesResponse geoNamesResponse;
            string uri = "http://api.geonames.org/findNearbyPlaceNameJSON?lat=" + latt + "&lng=" + longt + "&username=JamesFJStephenson";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                var result = response.Content.ReadAsStringAsync().Result;
                geoNamesResponse = JsonConvert.DeserializeObject<GeoNamesResponse>(result);
            }
            return new CountryDetails(geoNamesResponse.geonames[0].countryName, geoNamesResponse.geonames[0].population);
        }
    }
}
