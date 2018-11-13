using Strava.Common.Domain;
using Strava.Common.FromExternalSources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common
{
    public struct SegmentAndEffortData
    {
        public segment Segment { get; set; }
        public entry Effort { get; set; }
        public SegmentAndEffortData(segment segment, entry leaderBoardEntry)
        {
            Segment = segment;
            Effort = leaderBoardEntry;
        }

    }
}
