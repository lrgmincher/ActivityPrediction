using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources;
using Strava.Common.FromExternalSources.Domain;
using Strava.Common.InternalData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava
{
    public class StravaBusiness : IStravaBusiness
    {
        private IStravaProvider _stravaProvider;
        private string path = "";
        public StravaBusiness(string accessToken)
        {
            _stravaProvider = new StravaProvider(accessToken);
        }

        public LeaderBoardResult GetLeaderBoardResultsAsync(int id)
        {
           Task<LeaderBoardResult> task = Task.Run(() => _stravaProvider.GetLeaderBoardResultsAsync(id));
            task.Wait();
            return task.Result;
        }

        public List<SegmentToSearch> GetCyclingSegmentsToSearch(IEnumerable<Coordinates> coordinates)
        {
            List<segment> segments = GetCyclingSegments(coordinates);
            List<SegmentToSearch> segmentsToSearch = new List<SegmentToSearch>();
            foreach(var segment in segments)
            {
                segmentsToSearch.Add(new SegmentToSearch(segment.id));
            }
            return segmentsToSearch;
        }


        public List<segment> GetCyclingSegments(IEnumerable<Coordinates> coordinates)
        {
            Random rnd = new Random();
            List<segment> segments = new List<segment>();
            float[] coordarea = new float[4];
            segmentJsonArray segmentData;

            int numberOfCoords = 0;

            foreach (var coord in coordinates)
            {
                Console.WriteLine("Getting Segments From Coords" + ++numberOfCoords);
                var segmentDataTask = Task.Run(() => _stravaProvider.GetSegments(getCoordArea(coord), TravelTypes.travelTypesApiParameters[travelEnum.Cycling]));
                segmentDataTask.Wait();
                segmentData = segmentDataTask.Result;

                if (segmentData.segments != null)
                {
                    if (segmentData.segments.Count > 0)
                    {
                        foreach (var segment in segmentData.segments)
                        {
                            segments.Add(segment);
                        }
                    }
                }

            }
            return segments;
        }

        private float[] getCoordArea(Coordinates coordinates)
        {
            return new float[]
            {
                coordinates.Lattitude,
                coordinates.Longtitude,
                coordinates.Lattitude + constants.lattitudeWidth,
                coordinates.Longtitude + constants.longtitudeHeight

                //37.06975F,
                //88.15225F,
                //37.06975F + constants.lattitudeWidth,
                //88.15225F + constants.longtitudeHeight
            };
        }

        public void SaveSegmentsToSearch(List<SegmentToSearch> segmentsToSearch)
        {

                path = DataAccessContext.SegmentToSearchPath + "Sections_" + DateTime.Now.Ticks;

                using (StreamWriter file = new StreamWriter(path))
                {
                foreach(var segment in segmentsToSearch)
                {
                    file.WriteLine(segment.Id + "," + segment.searched);
                }
                
                }
        }

        public void SaveDetails(List<SegmentAndEffortData> data)
        {
            //Dump all data for audit purposes
            path = DataAccessContext.SegmentToSearchPath + "/Results_" + DateTime.Now.Ticks;
            using (StreamWriter file = new StreamWriter(path))
            { 
                try
                {
                    file.WriteLine("SectionId, resource_state, name, climb_category, climb_category_desc, avg_grade, start_coords_lattitude,start_coords_longtitude, end_coords_lattitude, end_coords_longtitude, elev_difference, resource_state, distance, points, starred, resource_state, athlete_name, elapsed_name, moving_time, start_date, start_date_local, rank");
                    foreach (var result in data)
                    {
                        file.WriteLine(result.Segment.id + "," + result.Segment.resource_state + "," + result.Segment.name + "," + result.Segment.climb_category + "," + result.Segment.climb_category_desc + "," + result.Segment.avg_grade + "," + result.Segment.start_latlng[0] + "," + result.Segment.start_latlng[1] + "," + result.Segment.end_latlng[0] + "," + result.Segment.end_latlng[1] + "," + result.Segment.elev_difference + "," +
                            result.Segment.resource_state + "," + result.Segment.distance + "," + result.Segment.points + "," + result.Segment.starred + "," + result.Segment.resource_state + "," + result.Effort.athlete_name + "," + result.Effort.elapsed_time + "," + result.Effort.moving_time + "," + result.Effort.start_date + "," + result.Effort.start_date_local + "," + result.Effort.rank);
                    };
                }           
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

  
        }

        public void SaveStatistics(List<SectionEffortStatistics> data)
        {
            path = DataAccessContext.SegmentToSearchPath + "/Stats" + DateTime.Now.Ticks;
            using (StreamWriter file = new StreamWriter(path))
            {
                try
                {
                    file.WriteLine("SectionId, resource_state, name, climb_category, climb_category_desc, avg_grade, start_coords_lattitude,start_coords_longtitude, end_coords_lattitude, end_coords_longtitude, elev_difference, resource_state, distance, points, starred, meanElapsedTime, meanMovingTime, medianElapsedTime, medianMovingTime, standardDeviationElapsedTime, standardDeviationMovingTime");
                    foreach (var result in data)
                    {
                        file.WriteLine(result.segment.id + "," + result.segment.resource_state + "," + result.segment.name + "," + result.segment.climb_category + "," + result.segment.climb_category_desc + "," + result.segment.avg_grade + "," + result.segment.start_latlng[0] + "," + result.segment.start_latlng[1] + "," + result.segment.end_latlng[0] + "," + result.segment.end_latlng[1] + "," + result.segment.elev_difference + "," +
                            result.segment.resource_state + "," + result.segment.distance + "," + result.segment.points + "," + result.segment.starred + "," + result.meanElapsedTime + "," + result.meanMovingTime + "," + result.medianElapsedTime + "," + result.medianMovingTime + "," + result.standardDeviationElapsedTime + "," + result.standardDeviationMovingTime);
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

        }

        public List<SectionEffortStatistics> GetSectionEffortStatistics(List<SegmentAndEffortData> data)
        {
            var averageAlapsedTimeQuery = from d in data
            group d by new
            {
                d.Segment.id
            } into s
            select new
            {
                Average = s.Average(e => e.Effort.elapsed_time)
            };

            var averageMovingTimeQuery = from d in data
                                          group d by new
                                          {
                                              d.Segment.id
                                          } into s
                                          select new
                                          {
                                              Average = s.Average(e => e.Effort.moving_time)
                                          };


            var uniqueSections = data.GroupBy(s => new { s.Segment.id })
                .Select(s => s.First())
                .ToList();

            var groupedEfforts = data.GroupBy(e => new { e.Segment.id })
                                 .Select(efforts => efforts.ToList())
                                 .ToList();



            List<List<double>> groupedElasedTimes = new List<List<double>>();
            List<List<double>> groupedMovingTimes = new List<List<double>>();


           for(int i=0; i< groupedEfforts.Count; i++)
            {
                groupedElasedTimes.Add(new List<double>());
                groupedMovingTimes.Add(new List<double>());
                for (int j =0; j< groupedEfforts[i].Count; j++)
                {
                    groupedElasedTimes[i].Add((groupedEfforts[i][j].Effort.elapsed_time));
                    groupedMovingTimes[i].Add((groupedEfforts[i][j].Effort.moving_time));
                }
            }

            List<List<double>> orderedElapsedTimes = new List<List<double>>();
            List<List<double>> orderedMovingTimes = new List<List<double>>();

            for (int i = 0; i< groupedEfforts.Count; i++)
            {
                orderedElapsedTimes.Add((from e in groupedElasedTimes[i]
                                      orderby e ascending
                                      select e).ToList());
                orderedMovingTimes.Add((from e in groupedMovingTimes[i]
                                          orderby e ascending
                                          select e).ToList());
            }

            var MeanElapsedTimePerSection = averageAlapsedTimeQuery.ToList();
            var MeanMovingTimePerSection = averageMovingTimeQuery.ToList();
            

            List<SectionEffortStatistics> sectionEffortStatistics = new List<SectionEffortStatistics>();



            for (int i = 0; i < uniqueSections.Count; i++)
            {

                sectionEffortStatistics.Add(new SectionEffortStatistics()
                {
                    meanElapsedTime = MeanElapsedTimePerSection[i].Average,
                    meanMovingTime = MeanMovingTimePerSection[i].Average,
                    segment = uniqueSections[i].Segment,
                    standardDeviationElapsedTime = standardDeviation(groupedElasedTimes[i], MeanElapsedTimePerSection[i].Average),
                    standardDeviationMovingTime = standardDeviation(groupedMovingTimes[i], MeanMovingTimePerSection[i].Average),
                    medianElapsedTime = Median(orderedElapsedTimes[i]),
                    medianMovingTime = Median(orderedMovingTimes[i])
                   
                });
            }
            return sectionEffortStatistics;
        }

        private double standardDeviation(IEnumerable<double> inputEnumeration, double meanInput)
        {
            double stdDev = 0;
            foreach(var input in inputEnumeration)
            {
                stdDev += Math.Pow(input - meanInput, 2);
            }
            stdDev /= inputEnumeration.Count();

            return Math.Pow(stdDev, 0.5);
        }

        private double Median(List<double> inputEnumeration)
        {
            if (inputEnumeration.Count > 1)
            {
                return inputEnumeration[inputEnumeration.Count / 2];
            }
            else return 0;
        }
    }
}
