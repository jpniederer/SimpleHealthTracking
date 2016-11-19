namespace SimpleHealthTracking.Tests.Web
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repository.Entities;
    using SimpleHealthTracking.Web.Classes;

    [TestClass]
    public class AverageCalculationsTest
    {
        //string userId = "1f19feb6-3a7d-4903-bdeb-610826f779bb";
        string userId = "47db267f-172f-43b2-b077-07883448c0ef";

        [TestMethod]
        public void TestAverageWeight()
        {
            HealthStatistics hs = new HealthStatistics(userId);
            double averageWeight = hs.GetAverageWeight();

            Assert.AreNotEqual(0, averageWeight);
        }

        [TestMethod]
        public void TestMaxWeight()
        {
            HealthStatistics hs = new HealthStatistics(userId);
            Checkin maxWeight = hs.GetMaxWeight();

            Assert.AreNotEqual(0, maxWeight.Weight);
        }

        [TestMethod]
        public void TestSetAllStats()
        {
            HealthStatistics hs = new HealthStatistics(userId, true);

            Assert.AreNotEqual(0, hs.AverageHeartrate);
        }
    }
}