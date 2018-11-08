using Newtonsoft.Json;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Strava
{
    public class StravaProvider : IStravaProvider
    {
        string baseUrl;
        string atheleteId;
        string leaderBoardUrl;
        string segmentsUrl;
        string accessToken;
        string boundsQuery;
        string activityTypeQuery;
        string accessTokenQuery;
        string dateRangeQuery;
        string perPageQuery;
        string exploreUrl;
        public StravaProvider()
        {
            baseUrl = "https://www.strava.com/api/v3/";
            
           // leaderBoardUrl = "segments/1133828/leaderboard?per_page=100&date_range=this_month&access_token=7ca960a7dcb1a723158b79b75e00e5f2ae580513";
            leaderBoardUrl = "leaderboard";
            segmentsUrl = "segments";
            exploreUrl = "explore";
            boundsQuery = "bounds=";
            activityTypeQuery = "activity_type=";
            perPageQuery = "per_page=";
            accessTokenQuery = "access_token=";
            dateRangeQuery = "this_month";
            accessToken = "5bf43834e6524a289832143b09d03dde0a8e7105";
            
        }

        public async Task<string> GetLeaderBoardResultsAsync(int segmentId)
        {
            int perPage = 100;
            string dateRange = "this_month";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + segmentsUrl + "/" + segmentId + "/" + leaderBoardUrl + "?" + perPageQuery + perPage + "&" + dateRangeQuery + dateRange + "&" + accessTokenQuery + accessToken);
                var data =  response.Content.ReadAsStringAsync().Result;
                try
                {
                    LeaderBoardResult leaderBoardResult = JsonConvert.DeserializeObject<LeaderBoardResult>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return data;
            }

        }

        public async Task<segmentJsonArray> GetSegments(float[] coordinateBoundaries, string travelType)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + segmentsUrl + "/" + exploreUrl + "?" + boundsQuery + "[" + coordinateBoundaries[0] + "," + coordinateBoundaries[1] + "," + coordinateBoundaries[2] + "," + coordinateBoundaries[3] + "]" + "&" + activityTypeQuery + travelType + "&" + accessTokenQuery + accessToken;
                HttpResponseMessage response = await client.GetAsync(url);
                segmentJsonArray jsonArray = null;
                string data = await response.Content.ReadAsStringAsync();
                try
                {
                    jsonArray = JsonConvert.DeserializeObject<segmentJsonArray>(data);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return jsonArray;
            }
        }


         
    }
}
