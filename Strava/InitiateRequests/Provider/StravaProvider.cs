using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InitiateRequests.Provider
{
    public class StravaProvider : IStravaProvider
    {
        private string clientSecret { get; set; }
        private string clientId { get; set; }

        public StravaProvider()
        {
            clientSecret = "07dc460141e2dc25e81c3f1b9bb9bc01efb52a54";
            clientId = "29906";
        }

        public async Task<string> GetToken(string code)
        {
            string data;
            using(HttpClient httpClient = new HttpClient())
            {

                // HttpResponseMessage response = await httpClient.PostAsync($"https://www.strava.com/oauth/token?client_id=" + clientId + "&client_secret=" + clientSecret + "&code" = code);
                // HttpResponseMessage response = await httpClient.PostAsync("https://www.strava.com/oauth/token?client_id=" + clientId + "&client_secret=" + clientSecret + "&code" + code, null);
                httpClient.BaseAddress = new Uri("https://www.strava.com/oauth/token?client_id=29906&client_secret=07dc460141e2dc25e81c3f1b9bb9bc01efb52a54&code=5740b5a2f95f55db4a47e16535a4da272ecff9cb");
                var request = new HttpRequestMessage(HttpMethod.Post, httpClient.BaseAddress);
                request.Headers.Add("Accept", "application/json");
                // Add body content
                request.Content = new StringContent(
                    "",
                    Encoding.UTF8,
                    "application/json"
                );
                //                HttpResponseMessage response = await httpClient.PostAsync("https://www.strava.com/oauth/token?client_id=29906&client_secret=07dc460141e2dc25e81c3f1b9bb9bc01efb52a54&code=5740b5a2f95f55db4a47e16535a4da272ecff9cb", null);
                HttpResponseMessage response = await httpClient.SendAsync(request);

                data = response.Content.ReadAsStringAsync().Result;
            }
            return data;
        }
    }
}