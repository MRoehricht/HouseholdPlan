using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;

namespace HouseholdPlan.Core.Test.Models.Work
{
    public class HouseholdTaskTest
    {
        [Test]
        public void InitTest()
        {
            HouseholdTaskEntity householdTask = GetHouseholdTask();

            Assert.IsNotNull(householdTask);

            Assert.AreEqual(id, householdTask.Id);
            Assert.AreEqual(creatorId, householdTask.CreatorId);
            Assert.AreEqual(description, householdTask.Description);
            Assert.AreEqual(title, householdTask.Title);
            Assert.AreEqual(processingTimeId, householdTask.ProcessingTimeId);

            ProcessingTimeEntity processingTime = GetProcessingTime();

            Assert.IsNotNull(processingTime);

            Assert.AreEqual(processingTimeId, processingTime.Id);
            Assert.AreEqual(initialDate, processingTime.InitialDate);
            Assert.AreEqual(ProcessingDateReplay.Daily, processingTime.Replay);
        }

        public const int id = 1;
        public const int creatorId = 2;
        public const int processingTimeId = 3;
        public const string description = "Beschreibung";
        public const string title = "Titel";
        public readonly static DateTime initialDate = DateTime.Parse("01.01.2020 12:00:00");

        public static HouseholdTaskEntity GetHouseholdTask()
        {
            HouseholdTaskEntity householdTask = new HouseholdTaskEntity
            {
                Id = id,
                CreatorId = creatorId,
                Description = description,
                Title = title,
                ProcessingTimeId = processingTimeId
            };

            return householdTask;
        }

        public static ProcessingTimeEntity GetProcessingTime()
        {
            ProcessingTimeEntity processingTime = new ProcessingTimeEntity
            {
                Id = processingTimeId,
                InitialDate = initialDate,
                Replay = ProcessingDateReplay.Daily
            };

            return processingTime;
        }
    }
}