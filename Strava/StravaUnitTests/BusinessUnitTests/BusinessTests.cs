using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Strava;
using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources;
using Strava.Common.FromExternalSources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StravaUnitTests.BusinessUnitTests
{
    [TestClass]
    public class BusinessTests
    {
        Mock<IStravaProvider> mockStravaProvider = new Mock<IStravaProvider>();

        LeaderBoardResult expectedLeaderBoardResult;

        Task<LeaderBoardResult> expectedLeaderBoardResultTask;

        Task<segmentJsonArray> TestSegmentJsonArray;

        List<Coordinates> emptyCoords;



        [TestInitialize]
        public void SetUp()
        {
            expectedLeaderBoardResultTask = Task.Run(() => expectedLeaderBoardResult);
            TestSegmentJsonArray = Task.Run(() => new segmentJsonArray());
            expectedLeaderBoardResult = new LeaderBoardResult()
            {
                effort_count = 999
            };
            emptyCoords = new List<Coordinates>();
        }
        [TestMethod]
        public void GetLeaderBoardResultSimplyReturnsProviderResponse()
        {
            mockStravaProvider.Setup(l => l.GetLeaderBoardResultsAsync(It.IsAny<int>(), It.IsAny<searchMetaData>())).Returns(expectedLeaderBoardResultTask);

            StravaBusiness stravaBusiness = new StravaBusiness(mockStravaProvider.Object);

            Thread.Sleep(500);

            var res = stravaBusiness.GetLeaderBoardResultsAsync(0, new searchMetaData());
            Thread.Sleep(500);


            Assert.AreEqual(res.effort_count, expectedLeaderBoardResult.effort_count);
        }

        [TestMethod]
        public void GetCyclingSegmentsReturnNoResults()
        {


            mockStravaProvider.Setup(g => g.GetSegments(It.IsAny<float[]>(), It.IsAny<string>())).Returns(TestSegmentJsonArray);

            StravaBusiness stravaBusiness = new StravaBusiness(mockStravaProvider.Object);

            var res =  stravaBusiness.GetCyclingSegments(emptyCoords);

            Assert.AreEqual(res.Count, 0);
        }


        [TestMethod]
        public void GetCyclingSegmentsReturnsResults()
        {
            List<Coordinates> TestCoords = new List<Coordinates>()
            {
                new Coordinates()
                {
                    Lattitude = 11,
                    Longtitude = 11
                }
            };


            var returnedResults = new segmentJsonArray()
            {
                segments = new List<segment>()
                {
                    new segment()
                }
            };

            TestSegmentJsonArray = Task.Run(() => returnedResults);

            mockStravaProvider.Setup(g => g.GetSegments(It.IsAny<float[]>(), It.IsAny<string>())).Returns(TestSegmentJsonArray);

            StravaBusiness stravaBusiness = new StravaBusiness(mockStravaProvider.Object);

            var res = stravaBusiness.GetCyclingSegments(TestCoords);

            Assert.AreEqual(res.Count, 1);
        }
    }
}
