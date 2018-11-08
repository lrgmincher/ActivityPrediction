using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common.FromExternalSources.Domain
{
    public class segmentJsonArray
    {
        public List<segment> segments { get; set; }
    }
}
