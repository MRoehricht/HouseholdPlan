using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;

namespace HouseholdPlan.Core.Test.Models.Work
{
    public class ProcessingTimeTest
    {
        [Test]
        public void InitTest()
        {
            DateTime initialDate = DateTime.Now;

            ProcessingTimeEntity processingTime = new ProcessingTimeEntity
            {
                InitialDate = initialDate,
                Replay = ProcessingDateReplay.Daily
            };

            Assert.IsNotNull(processingTime);

            Assert.AreEqual(initialDate, processingTime.InitialDate);
            Assert.AreEqual(ProcessingDateReplay.Daily, processingTime.Replay);
        }
    }
}