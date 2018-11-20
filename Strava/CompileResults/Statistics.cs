using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileResults
{
    public class Statistics
    {
        public string SectionId { get; set; }
        public string resource_state { get; set; }
        public string climb_catagory { get; set; }
        public string climb_catagory_desc { get; set; }
        public double avg_grade { get; set; }
        public double start_coords_lattitude { get; set; }
        public double start_coords_longtitude { get; set; }
        public double end_coords_lattitude { get; set; }
        public double end_coords_longtitude { get; set; }
        public double elev_difference { get; set; }
        public double distance { get; set; }
        public bool starred { get; set; }
        public double meanElapsedTime { get; set; }
        public double meanMovingTime { get; set; }
        public double medianElapsedTime { get; set; }
        public double medianMovingTime { get; set; }
        public double standardDeviationElapsedTime { get; set; }
        public double standardDeviationMovingTime { get; set; }
        public CountryDetails countryDetails { get; set; }

        public static Statistics GetStatistics(string csvLine)
        {
            GeoNames g = new GeoNames();
            Statistics s = new Statistics();

            try
            {
                string[] values = csvLine.Split(',');
                int index = 0;
                s.SectionId = values[index++];
                s.resource_state = values[index++];
                s.climb_catagory = values[index++];
                s.climb_catagory_desc = values[index++];
                s.avg_grade = Convert.ToDouble(values[index++]);
                s.start_coords_lattitude = Convert.ToDouble(values[index++]);
                s.start_coords_longtitude = Convert.ToDouble(values[index++]);
                s.end_coords_lattitude = Convert.ToDouble(values[index++]);
                s.end_coords_longtitude = Convert.ToDouble(values[index++]);
                s.elev_difference = Convert.ToDouble(values[index++]);
                s.distance = Convert.ToDouble(values[index++]);
                s.starred = Convert.ToBoolean(values[index++]);
                s.meanElapsedTime = Convert.ToDouble(values[index++]);
                s.meanMovingTime = Convert.ToDouble(values[index++]);
                s.medianElapsedTime = Convert.ToDouble(values[index++]);
                s.medianMovingTime = Convert.ToDouble(values[index++]);
                s.standardDeviationElapsedTime = Convert.ToDouble(values[index++]);
                s.standardDeviationMovingTime = Convert.ToDouble(values[index++]);

                var countryTask = Task.Run(() => g.getCounty(s.start_coords_lattitude, s.start_coords_longtitude));
                countryTask.Wait();
                s.countryDetails = countryTask.Result;
            }
            catch (Exception e)
            {
                return null;
                //If trouble converting, can assume the data is curropt, so can remove line. 
            }
            return s;
        }


    
    }
}
