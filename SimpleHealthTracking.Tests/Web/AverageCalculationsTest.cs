namespace SimpleHealthTracking.Tests.Web
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Repository.Entities;
    using SimpleHealthTracking.Web.Classes;

    [TestClass]
    public class AverageCalculationsTest
    {
        string userId = "1f19feb6-3a7d-4903-bdeb-610826f779bb";

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
            double maxWeight = hs.GetMaxWeight();

            Assert.AreNotEqual(0, maxWeight);
        }

        [TestMethod]
        public void TestSetAllStats()
        {
            HealthStatistics hs = new HealthStatistics(userId, true);

            Assert.AreNotEqual(0, hs.AverageHeartrate);
        }
    }
}