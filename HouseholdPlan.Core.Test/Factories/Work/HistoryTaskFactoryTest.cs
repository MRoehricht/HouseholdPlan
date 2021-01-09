using HouseholdPlan.Core.Factories.Work;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Core.Test.Factories.People;
using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdPlan.Core.Test.Factories.Work
{
    public class HistoryTaskFactoryTest : FactoryTestBase
    {
        public static readonly DateTime Time1 = DateTime.Parse("01.01.2020 12:00:00");
        public static readonly ProcessingDateReplay Time1Reply = ProcessingDateReplay.Weekly;

        public static readonly DateTime Time2 = DateTime.Parse("01.01.2020 13:00:00");
        public static readonly ProcessingDateReplay Time2Reply = ProcessingDateReplay.Monthly;

        [Test]
        public void LoadTest()
        {
            CreateHistoryTasks();

            var tasks = access.GetHouseholdTasks();

            Assert.AreEqual(2, tasks.Count);

            HouseholdTaskEntity household2 = tasks.Last();

            HistoryTaskFactory historyTaskFactory = new HistoryTaskFactory(access);

            List<HistoryTask> historyTasks1 = historyTaskFactory.LoadHistoryTasks(tasks.First());
            Assert.AreEqual(1, historyTasks1.Count);
            HistoryTask historyTask1 = historyTasks1.First();

            List<HistoryTask> historyTasks2 = historyTaskFactory.LoadHistoryTasks(tasks.Last());
            var historyTask2 = historyTasks1.First();
        }

        [Test]
        public void CreateHistoryTasks()
        {
            var list = access.GetUsers();

            UserFactoryTest userFactoryTest = new UserFactoryTest();
            userFactoryTest.CreateUsers(access);

            HouseholdTaskFactoryTest householdTaskFactoryTest = new HouseholdTaskFactoryTest();
            householdTaskFactoryTest.CreateHouseholdTask(access);

            HistoryTask historyTask1 = new HistoryTask
            {
                Date = Time1,
                EditorId = "1",
                Status = Data.Constants.ProcessingStatus.NotDone,
                TaskId = 3
            };

            access.CreateOrUpdate(historyTask1);

            HistoryTask historyTask2 = new HistoryTask
            {
                Date = Time2,
                EditorId = "2",
                Status = Data.Constants.ProcessingStatus.NotDone,
                TaskId = 4
            };

            access.CreateOrUpdate(historyTask2);
        }
    }
}