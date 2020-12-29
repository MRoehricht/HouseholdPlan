using HouseholdPlan.Core.Services.Work;
using HouseholdPlan.Core.Test.Models.Work;
using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;
using System.Globalization;

namespace HouseholdPlan.Core.Test.Services.Work
{
    public class NextWorkingTimeServiceTest
    {
        [Test]
        public void GetNextWorkingTime_Once_Test()
        {
            ProcessingTimeEntity processingTime = HouseholdTaskTest.GetProcessingTime();

            processingTime.InitialDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            processingTime.Replay = ProcessingDateReplay.Once;

            NextWorkingTimeService service = new NextWorkingTimeService
            {
                ProcessingTime = processingTime
            };

            DateTime expectedDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime actualDate = service.GetNextWorkingTime();

            Assert.IsNotNull(actualDate);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [Test]
        public void GetNextWorkingTime_Daily_Test()
        {
            ProcessingTimeEntity processingTime = HouseholdTaskTest.GetProcessingTime();

            processingTime.InitialDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            processingTime.Replay = ProcessingDateReplay.Daily;

            NextWorkingTimeService service = new NextWorkingTimeService
            {
                ProcessingTime = processingTime
            };

            DateTime expectedDate = DateTime.ParseExact("02.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime actualDate = service.GetNextWorkingTime();

            Assert.IsNotNull(actualDate);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [Test]
        public void GetNextWorkingTime_Weekly_Test()
        {
            ProcessingTimeEntity processingTime = HouseholdTaskTest.GetProcessingTime();

            processingTime.InitialDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            processingTime.Replay = ProcessingDateReplay.Weekly;

            NextWorkingTimeService service = new NextWorkingTimeService
            {
                ProcessingTime = processingTime
            };

            DateTime expectedDate = DateTime.ParseExact("08.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime actualDate = service.GetNextWorkingTime();

            Assert.IsNotNull(actualDate);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [Test]
        public void GetNextWorkingTime_Monthly_Test()
        {
            ProcessingTimeEntity processingTime = HouseholdTaskTest.GetProcessingTime();

            processingTime.InitialDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            processingTime.Replay = ProcessingDateReplay.Monthly;

            NextWorkingTimeService service = new NextWorkingTimeService
            {
                ProcessingTime = processingTime
            };

            DateTime expectedDate = DateTime.ParseExact("01.02.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime actualDate = service.GetNextWorkingTime();

            Assert.IsNotNull(actualDate);
            Assert.AreEqual(expectedDate, actualDate);
        }

        [Test]
        public void GetNextWorkingTime_Yearly_Test()
        {
            ProcessingTimeEntity processingTime = HouseholdTaskTest.GetProcessingTime();

            processingTime.InitialDate = DateTime.ParseExact("01.01.2020 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            processingTime.Replay = ProcessingDateReplay.Yearly;

            NextWorkingTimeService service = new NextWorkingTimeService
            {
                ProcessingTime = processingTime
            };

            DateTime expectedDate = DateTime.ParseExact("01.01.2021 12:00:00", "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            DateTime actualDate = service.GetNextWorkingTime();

            Assert.IsNotNull(actualDate);
            Assert.AreEqual(expectedDate, actualDate);
        }
    }
}