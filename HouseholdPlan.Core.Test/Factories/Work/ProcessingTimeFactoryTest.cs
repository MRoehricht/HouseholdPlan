using HouseholdPlan.Core.Factories.Work;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Access;
using HouseholdPlan.Data.Constants;
using NUnit.Framework;
using System;

namespace HouseholdPlan.Core.Test.Factories.Work
{
    public class ProcessingTimeFactoryTest : FactoryTestBase
    {
        public static readonly DateTime Time1 = DateTime.Parse("01.01.2020 12:00:00");
        public static readonly ProcessingDateReplay Time1Reply = ProcessingDateReplay.Weekly;
        public static readonly int Time1Every = 1;

        public static readonly DateTime Time2 = DateTime.Parse("01.01.2020 13:00:00");
        public static readonly ProcessingDateReplay Time2Reply = ProcessingDateReplay.Monthly;
        public static readonly int Time2Every = 3;

        [Test]
        public void LoadTest()
        {
            CreateProcessingTimes();

            ProcessingTimeFactory processingTimeFactory = new ProcessingTimeFactory(access);

            ProcessingTime time1 = processingTimeFactory.LoadProcessingTime(1);
            Assert.AreEqual(1, time1.Id);
            Assert.AreEqual(Time1, time1.InitialDate);
            Assert.AreEqual(Time1Reply, time1.Replay);

            ProcessingTime time2 = processingTimeFactory.LoadProcessingTime(2);
            Assert.AreEqual(2, time2.Id);
            Assert.AreEqual(Time2, time2.InitialDate);
            Assert.AreEqual(Time2Reply, time2.Replay);
        }

        [Test]
        public void CreateProcessingTimes()
        {
            ProcessingTime time1 = new ProcessingTime
            {
                Every = Time1Every,
                InitialDate = Time1,
                Replay = Time1Reply
            };

            access.CreateOrUpdate(time1);

            ProcessingTime time2 = new ProcessingTime
            {
                Every = Time2Every,
                InitialDate = Time2,
                Replay = Time2Reply
            };

            access.CreateOrUpdate(time2);
        }

        public void CreateProcessingTimes(SQLiteAccess access)
        {
            this.access = access;
            CreateProcessingTimes();
        }
    }
}