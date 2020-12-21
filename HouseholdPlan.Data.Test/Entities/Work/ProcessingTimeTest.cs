using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;

namespace HouseholdPlan.Data.Test.Entities.Work
{
    public class ProcessingTimeTest
    {
        [Test]
        public void InitTest()
        {
            DateTime initialDate = DateTime.Now;

            ProcessingTimeEntity processingTime = new ProcessingTimeEntity
            {
                Every = 2,
                InitialDate = initialDate,
                Replay = ProcessingDateReplay.Daily
            };

            Assert.IsNotNull(processingTime);

            Assert.AreEqual(2, processingTime.Every);
            Assert.AreEqual(initialDate, processingTime.InitialDate);
            Assert.AreEqual(ProcessingDateReplay.Daily, processingTime.Replay);
        }
    }
}