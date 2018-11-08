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
        public StravaBusiness()
        {
            _stravaProvider = new StravaProvider();
        }

        public List<LeaderBoardResult> GetLeaderBoardResultsAsync(int id)
        {
           Task<string> r = _stravaProvider.GetLeaderBoardResultsAsync(id);
            //  return r.Result;
            return new List<LeaderBoardResult>();

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

            foreach (var coord in coordinates)
            {
                segmentData = _stravaProvider.GetSegments(getCoordArea(coord), TravelTypes.travelTypesApiParameters[travelEnum.Cycling]).Result;
                if(segmentData != null)
                {
                    segments.Add(segmentData.segments[rnd.Next(0, segmentData.segments.Count - 1)]);
                }

            }
            return segments;
        }

        private float[] getCoordArea(Coordinates coordinates)
        {
            return new float[]
            {
                //coordinates.Lattitude,
                //coordinates.Longtitude,
                //coordinates.Lattitude + constants.lattitudeWidth,
                //coordinates.Longtitude + constants.longtitudeHeight

                37.06975F,
                88.15225F,
                37.06975F + constants.lattitudeWidth,
                88.15225F + constants.longtitudeHeight
            };
        }

        public void SaveSegmentsToSearch(List<SegmentToSearch> segmentsToSearch)
        {

                path += DataAccessContext.SegmentToSearchPath + "Sections_" + DateTime.Now.Ticks;

                using (StreamWriter file = new StreamWriter(path))
                {
                foreach(var segment in segmentsToSearch)
                {
                    file.WriteLine(segment.Id + "," + segment.searched);
                }
                
                }
        }
    }
}
