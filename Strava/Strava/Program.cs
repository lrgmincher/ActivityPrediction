using Strava.Business.ConcreteClasses;
using Strava.Business.Interfaces;
using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources;
using Strava.Common.FromExternalSources.Domain;
using Strava.Providers.ConcreteClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Strava
{
    public class Program
    {
        public static void Main()
        {
            StravaBusiness stravaBusiness = new StravaBusiness();

            int numberOfLeaderBoardsToCheck = 10;

            IRandomNumberGen randomNumbersProvider = new PsuedoRandomNumberGen();

            List<Coordinates> seedCoordinates =  randomNumbersProvider.SaveRandomCoordinates(numberOfLeaderBoardsToCheck);

            List<segment> segmentsToSearch = stravaBusiness.GetCyclingSegments(seedCoordinates);

            //stravaBusiness.SaveSegmentsToSearch(segmentsToSearch);

            List<LeaderBoardResult> leaderBoardResults = new List<LeaderBoardResult>();
            List<SegmentAndEffortData> sectionAndEffortData = new List<SegmentAndEffortData>();

            foreach(var segementToSearch in segmentsToSearch)
            {
                leaderBoardResults = stravaBusiness.GetLeaderBoardResultsAsync(segementToSearch.id);
                foreach(var leaderBoardResult in leaderBoardResults)
                {
                    sectionAndEffortData.Add(new SegmentAndEffortData(segementToSearch, leaderBoardResult));
                }
            }
            

        }
    }     
}

