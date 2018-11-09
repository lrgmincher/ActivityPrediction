using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common.FromExternalSources.Domain
{
    public class entry
    {
        [JsonProperty(PropertyName = "athlete_name")]
        public string athlete_name { get; set; }

        [JsonProperty(PropertyName = "elapsed_time")]
        public int elapsed_time { get; set; }

        [JsonProperty(PropertyName = "moving_time")]
        public int moving_time { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime start_date { get; set; }

        [JsonProperty(PropertyName = "start_date_local")]
        public DateTime start_date_local { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int rank { get; set; }

    }
}
