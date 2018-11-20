using Newtonsoft.Json;
using Strava.Common;
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
        string IStravaProvider.accessToken { get {return accessToken; } set {accessToken = value; } }

        string accessToken;
        string baseUrl;
        string atheleteId;
        string leaderBoardUrl;
        string segmentsUrl;
        string boundsQuery;
        string activityTypeQuery;
        string accessTokenQuery;
        string dateRangeQuery;
        string perPageQuery;
        string exploreUrl;
        string genderQuery;
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
            genderQuery = "gender=";
        }


        public async Task<LeaderBoardResult> GetLeaderBoardResultsAsync(int segmentId, searchMetaData searchMetaData)
        {
            string uri = baseUrl + 
                segmentsUrl + "/" + 
                segmentId + "/" + 
                leaderBoardUrl + "?" + 
                "per_page=" + searchMetaData.per_page + "&" + 
                "date_range=" + searchMetaData.date_range + "&" +
                "gender=" + searchMetaData.Gender + "&" +
                "age_group=" + searchMetaData.age_group + "&" +
                "weight_class=" + searchMetaData.weight_class + "&" +
                accessTokenQuery + accessToken;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uri);
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
            segmentJsonArray jsonArray = new segmentJsonArray();
            using (HttpClient client = new HttpClient())
            {
                string url = baseUrl + segmentsUrl + "/" + exploreUrl + "?" + boundsQuery + "[" + coordinateBoundaries[0] + "," + coordinateBoundaries[1] + "," + coordinateBoundaries[2] + "," + coordinateBoundaries[3] + "]" + "&" + activityTypeQuery + travelType + "&" + accessTokenQuery + accessToken;
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    response = await client.GetAsync(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
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
