using Strava.Common.FromExternalSources.Domain;
using System.Threading.Tasks;

namespace Strava
{
    public interface IStravaProvider
    {
        Task<segmentJsonArray> GetSegments(float[] coordinateBoundaries, string travelType);
        Task<string> GetLeaderBoardResultsAsync(int segmentId);
    }
}
