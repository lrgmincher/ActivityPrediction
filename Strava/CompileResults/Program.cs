using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileResults
{
    class Program
    {
        static void Main(string[] args)
        {
            //This project simply complies the results from the Strava Datastore into singles csv files
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\JFJSt\source\repos\ActivityPrediction\Strava\Strava\DataStore");

            FileInfo[] results = d.GetFiles("Results*"); //Getting Text files
            FileInfo[] stats =  d.GetFiles("Stats*.csv"); //Getting Text files

            string compliledResultsPath = @"C:\Users\JFJSt\source\repos\ActivityPrediction\Strava\CompileResults\Results\CompliedResults";
            string compliledStatsPath = @"C:\Users\JFJSt\source\repos\ActivityPrediction\Strava\CompileResults\Results\CompliedStats";

            //newFile(compliledResultsPath, results);
            newFile(compliledStatsPath, stats);

        }

        private static void newFile(string existingPath, FileInfo[] newFiles)
        {
            if (File.Exists(existingPath))
            {
                File.Delete(existingPath);
            }
            using(StreamWriter f = new StreamWriter(existingPath))
            {
                f.WriteLine(
                    "SectionId, " +
                    "resource_state, " +
                    "climb_category, " +
                    "climb_category_desc, " +
                    "avg_grade, " +
                    "start_coords_lattitude," +
                    "start_coords_longtitude, " +
                    "end_coords_lattitude, " +
                    "end_coords_longtitude, " +
                    "elev_difference, " +
                        "distance, " +
                        "starred, " +
                        "meanElapsedTime, " +
                        "meanMovingTime, " +
                        "medianElapsedTime, " +
                        "medianMovingTime, " +
                        "standardDeviationElapsedTime, " +
                        "standardDeviationMovingTime, " +
                        "country" +
                        "population"
                        );
                foreach (var file in newFiles)
                {
                    using (StreamReader sr = file.OpenText())
                    {
                        string s = "";
                        int lineNumber = 0;
                        Statistics statistics;
                        while ((s = sr.ReadLine()) != null)
                        {
                            if(lineNumber != 0)
                            {
                                statistics = Statistics.GetStatistics(s);
                                if(statistics != null)
                                {
                                    f.WriteLine
                                        (
                                        statistics.SectionId + "," +
                                        statistics.resource_state + "," +
                                        statistics.climb_catagory + "," +
                                        statistics.climb_catagory_desc + "," +
                                        statistics.avg_grade + "," +
                                        statistics.start_coords_lattitude + "," +
                                        statistics.start_coords_longtitude + "," +
                                        statistics.end_coords_lattitude + "," +
                                        statistics.end_coords_longtitude + "," +
                                        statistics.elev_difference + "," +
                                        statistics.distance + "," +
                                        statistics.starred + "," +
                                        statistics.meanElapsedTime + "," +
                                        statistics.meanMovingTime + "," +
                                        statistics.medianElapsedTime + "," +
                                        statistics.medianMovingTime + "," +
                                        statistics.standardDeviationElapsedTime + "," +
                                        statistics.standardDeviationMovingTime + "," +
                                        statistics.countryDetails.country + "," +
                                        statistics.countryDetails.population + ","
                                        );
                                }
                            }
                            lineNumber++;
                        }
                    }
                }
            }

        }
    }
}
