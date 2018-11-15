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
            FileInfo[] stats =  d.GetFiles("Stats*"); //Getting Text files

            string compliledResultsPath = @"C:\Users\JFJSt\source\repos\ActivityPrediction\Strava\CompileResults\Results\CompliedResults";
            string compliledStatsPath = @"C:\Users\JFJSt\source\repos\ActivityPrediction\Strava\CompileResults\Results\CompliedStats";

            newFile(compliledResultsPath, results);
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
                foreach (var file in newFiles)
                {
                    using (StreamReader sr = file.OpenText())
                    {
                        string s = "";
                        int lineNumber = 0;
                        while ((s = sr.ReadLine()) != null)
                        {
                            if(lineNumber != 0)
                            {
                                f.WriteLine(s);
                            }
                            lineNumber++;
                        }
                    }
                }
            }

        }
    }
}
