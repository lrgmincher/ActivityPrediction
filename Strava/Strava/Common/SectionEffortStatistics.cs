using Strava.Common.FromExternalSources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common
{
    public class SectionEffortStatistics
    {
         public segment segment { get; set; }
         public double meanElapsedTime { get; set; }
         public double medianElapsedTime { get; set; }
         public double standardDeviationElapsedTime { get; set; }
         public double meanMovingTime { get; set; }
         public double medianMovingTime { get; set; }
         public double standardDeviationMovingTime { get; set; }
    }
}
