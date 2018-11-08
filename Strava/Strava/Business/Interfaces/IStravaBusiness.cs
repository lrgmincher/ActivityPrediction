using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources;
using System;
using System.Collections.Generic;
using Strava.Common.FromExternalSources.Domain;


namespace Strava
{
    public interface IStravaBusiness
    {
        List<LeaderBoardResult> GetLeaderBoardResultsAsync(int id);
        List<segment> GetCyclingSegments(IEnumerable<Coordinates> coordinates);
        void SaveSegmentsToSearch(List<SegmentToSearch> segmentsToSearch);
        
    }
}
