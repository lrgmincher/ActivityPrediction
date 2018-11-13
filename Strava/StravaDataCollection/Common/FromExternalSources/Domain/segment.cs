using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common.FromExternalSources.Domain
{
    //class to convert json responses to objects
    public class segment
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "resource_state")]
        public int resource_state { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "climb_category")]
        public int climb_category { get; set; }

        [JsonProperty(PropertyName = "climb_category_desc")]
        public string climb_category_desc { get; set; }

        [JsonProperty(PropertyName = "avg_grade")]
        public double avg_grade { get; set; }

        [JsonProperty(PropertyName = "start_latlng")]
        public double[] start_latlng { get; set; }

        [JsonProperty(PropertyName = "end_latlng")]
        public double[] end_latlng { get; set; }

        [JsonProperty(PropertyName = "elev_difference")]
        public double elev_difference { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public double distance { get; set; }

        [JsonProperty(PropertyName = "points")]
        public string points { get; set; }
         
        [JsonProperty(PropertyName = "starred")]
        public bool starred { get; set; }
    }
}
