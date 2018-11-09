using Newtonsoft.Json;
using Strava.Common.FromExternalSources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common.Domain
{
    public class LeaderBoardResult
    {
        [JsonProperty(PropertyName = "effort_count")]
        public int effort_count { get; set; }

        [JsonProperty(PropertyName = "entry_count")]
        public int entry_count { get; set; }

        [JsonProperty(PropertyName = "kom_type")]
        public string kom_type { get; set; }

        [JsonProperty(PropertyName = "entries")]
        public List<entry> entries { get; set; }

    }
}
