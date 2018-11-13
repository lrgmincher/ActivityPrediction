using Strava.Common.Domain;
using Strava.Common.FromExternalSources.Domain;
using System.Threading.Tasks;

namespace Strava
{
    public interface IStravaProvider
    {
        Task<segmentJsonArray> GetSegments(float[] coordinateBoundaries, string travelType);
        Task<LeaderBoardResult> GetLeaderBoardResultsAsync(int segmentId);
    }
}
