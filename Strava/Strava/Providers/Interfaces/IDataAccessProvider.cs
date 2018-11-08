using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources.Domain;

namespace Strava
{
    public interface IDataAccessProvider
    {
        void SaveSegmentDetails(segment[] segments);
        void SaveRiderData(Cyclist[] cyclistData);
        
    }
}
