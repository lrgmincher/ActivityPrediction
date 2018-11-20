using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common
{
    public class searchMetaData
    {
        public string Gender { get; set; }
        public string age_group { get; set; }
        public string weight_class { get; set; }
        public string date_range { get; set; }
        public int page { get; set; }
        public int per_page { get; set; }
        public string country { get; set; }


        public static List<string> Genders = new List<string>()
        {
             "M",
             "F"
        };

        public static List<string> Age_Groups = new List<string>()
        {
            "0_19",
            "20_24",
            "25_34",
            "35_44",
            "45_54",
            "55_64",
            "65_69",
            "65_69",
            "70_74",
            "75_plus"
        };


        public static List<string> Weight_Classes = new List<string>()
        {
            "0_54",
            "55_64",
            "65_74",
            "75_84",
            "85_94",
            "95_104",
            "105_114",
            "115_plus"
        };






        public static searchMetaData randomMetaData()
        {
            Random rnd = new Random();

            return new searchMetaData()
            {
                age_group = Age_Groups[rnd.Next(Age_Groups.Count - 1)],
                weight_class = Weight_Classes[rnd.Next(Weight_Classes.Count - 1)],
                Gender = Genders[rnd.Next(Genders.Count - 1)],
                date_range = "this_year",
                page = 1,
                per_page = 200
            };

        }


    }

    
}
