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
            accessToken = "0b0ca9d7ba064bf81fe973dc683d0e542c950720";
            
        }

        public async Task<LeaderBoardResult> GetLeaderBoardResultsAsync(int segmentId)
        {
            int perPage = ApiDetails.MaxResultsPerPage;
            string dateRange = "this_week";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + segmentsUrl + "/" + segmentId + "/" + leaderBoardUrl + "?" + perPageQuery + perPage + "&" + dateRangeQuery + dateRange + "&" + accessTokenQuery + accessToken);
                var data =  response.Content.ReadAsStringAsync().Result;
                LeaderBoardResult leaderBoardResult = new LeaderBoardResult();
                try
                {
                    leaderBoardResult = JsonConvert.DeserializeObject<LeaderBoardResult>(data);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return leaderBoardResult;
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
