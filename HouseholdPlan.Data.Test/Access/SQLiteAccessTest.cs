using HouseholdPlan.Data.Access;
using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.People;
using HouseholdPlan.Data.Entities.Work;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HouseholdPlan.Data.Test.Access
{
    public class SQLiteAccessTest
    {
        public const string TestDataBaseName = "Test.db";
        public SQLiteAccess access;

        [SetUp]
        public void SetUpTest()
        {
            if (File.Exists(TestDataBaseName))
            {
                File.Delete(TestDataBaseName);
            }

            access = new SQLiteAccess
            {
                DataBaseName = TestDataBaseName
            };
        }

        [Test]
        public void CreateGlobalCounterTest()
        {
            access.CreateGlobalCounter();

            var version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(1, version);

            version = access.GetCounter(CounterNames.UserVersion);
            Assert.AreEqual(0, version);

            version = access.GetCounter(CounterNames.HistoryTasksVersion);
            Assert.AreEqual(0, version);

            version = access.GetCounter(CounterNames.HouseholdTasksVersion);
            Assert.AreEqual(0, version);

            version = access.GetCounter(CounterNames.UserVersion);
            Assert.AreEqual(0, version);
        }

        [Test]
        public void GetCounterTest()
        {
            CreateGlobalCounterTest();

            int currentCounterValue = access.GetGlobalCounter();
            Assert.AreEqual(1, currentCounterValue);

            currentCounterValue = access.GetGlobalCounter();
            Assert.AreEqual(2, currentCounterValue);

            currentCounterValue = access.GetGlobalCounter();
            Assert.AreEqual(3, currentCounterValue);

            currentCounterValue = access.GetGlobalCounter();
            Assert.AreEqual(4, currentCounterValue);
        }

        [Test]
        public void CreateUsersTableTest()
        {
            access.CreateGlobalCounter();

            var version = access.GetCounter(CounterNames.UserVersion);
            Assert.AreEqual(0, version);

            access.CreateUsersTable();

            version = access.GetCounter(CounterNames.UserVersion);
            Assert.AreEqual(1, version);
        }

        [Test]
        public void CreateHistoryTasksTableTest()
        {
            access.CreateGlobalCounter();

            var version = access.GetCounter(CounterNames.HistoryTasksVersion);
            Assert.AreEqual(0, version);

            access.CreateHistoryTasksTable();

            version = access.GetCounter(CounterNames.HistoryTasksVersion);
            Assert.AreEqual(1, version);
        }

        [Test]
        public void CreateHouseholdTasksTableTest()
        {
            access.CreateGlobalCounter();

            var version = access.GetCounter(CounterNames.HouseholdTasksVersion);
            Assert.AreEqual(0, version);

            access.CreateHouseholdTasksTable();

            version = access.GetCounter(CounterNames.HouseholdTasksVersion);
            Assert.AreEqual(1, version);
        }

        [Test]
        public void CreateProcessingTimesTableTest()
        {
            access.CreateGlobalCounter();

            var version = access.GetCounter(CounterNames.ProcessingTimesVersion);
            Assert.AreEqual(0, version);

            access.CreateProcessingTimesTable();

            version = access.GetCounter(CounterNames.ProcessingTimesVersion);
            Assert.AreEqual(1, version);
        }

        [Test]
        public void InitDataBaseTest()
        {
            access.InitDataBase();

            int version = access.GetCounter(CounterNames.UserVersion);
            Assert.AreEqual(1, version);

            version = access.GetCounter(CounterNames.HistoryTasksVersion);
            Assert.AreEqual(1, version);

            version = access.GetCounter(CounterNames.HouseholdTasksVersion);
            Assert.AreEqual(1, version);

            version = access.GetCounter(CounterNames.ProcessingTimesVersion);
            Assert.AreEqual(1, version);
        }

        [Test]
        public void CreateOrUpdateUserTest()
        {
            access.InitDataBase();

            int version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(1, version);

            UserEntity user = new UserEntity
            {
                Name = "Test Nutzer"
            };

            access.CreateOrUpdate(user);

            Assert.AreEqual(1, user.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(user.Id + 1, version);

            user.Name = "Test Nutzer1";
            access.CreateOrUpdate(user);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(user.Id + 1, version);

            UserEntity user2 = new UserEntity
            {
                Name = "Test Nutzer2"
            };

            access.CreateOrUpdate(user2);

            Assert.AreEqual(2, user2.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(user2.Id + 1, version);
        }

        [Test]
        public void GetUsersTest()
        {
            CreateOrUpdateUserTest();

            List<UserEntity> users = access.GetUsers();

            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);

            UserEntity user1 = users.First();

            Assert.AreEqual(1, user1.Id);
            Assert.AreEqual("Test Nutzer1", user1.Name);

            UserEntity user2 = users.Last();

            Assert.AreEqual(2, user2.Id);
            Assert.AreEqual("Test Nutzer2", user2.Name);
        }

        [Test]
        public void CreateOrUpdateHistoryTaskTest()
        {
            access.InitDataBase();

            int version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(1, version);

            HistoryTaskEntity task = new HistoryTaskEntity
            {
                TaskId = 1,
                EditorId = 2,
                Date = DateTime.Parse("01.01.2020 12:00:00"),
                Status = ProcessingStatus.InProgress
            };

            access.CreateOrUpdate(task);

            Assert.AreEqual(1, task.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(task.Id + 1, version);

            task.Status = ProcessingStatus.Done;
            access.CreateOrUpdate(task);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(task.Id + 1, version);

            HistoryTaskEntity task2 = new HistoryTaskEntity
            {
                TaskId = 2,
                EditorId = 3,
                Date = DateTime.Parse("01.01.2020 13:00:00"),
                Status = ProcessingStatus.NotDone
            };

            access.CreateOrUpdate(task2);

            Assert.AreEqual(2, task2.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(task2.Id + 1, version);
        }

        [Test]
        public void GetHistoryTasksTest()
        {
            CreateOrUpdateHistoryTaskTest();

            List<HistoryTaskEntity> tasks = access.GetHistoryTasks();

            Assert.IsNotNull(tasks);
            Assert.AreEqual(2, tasks.Count);

            HistoryTaskEntity task1 = tasks.First();

            Assert.AreEqual(1, task1.Id);
            Assert.AreEqual(1, task1.TaskId);
            Assert.AreEqual(2, task1.EditorId);
            Assert.AreEqual(DateTime.Parse("01.01.2020 12:00:00"), task1.Date);
            Assert.AreEqual(ProcessingStatus.Done, task1.Status);

            HistoryTaskEntity task2 = tasks.Last();

            Assert.AreEqual(2, task2.Id);
            Assert.AreEqual(2, task2.TaskId);
            Assert.AreEqual(3, task2.EditorId);
            Assert.AreEqual(DateTime.Parse("01.01.2020 13:00:00"), task2.Date);
            Assert.AreEqual(ProcessingStatus.NotDone, task2.Status);
        }

        [Test]
        public void CreateOrUpdateProcessingTimeTest()
        {
            access.InitDataBase();

            int version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(1, version);

            ProcessingTimeEntity time1 = new ProcessingTimeEntity
            {
                InitialDate = DateTime.Parse("01.01.2020 12:00:00"),
                Replay = ProcessingDateReplay.Once
            };

            access.CreateOrUpdate(time1);

            Assert.AreEqual(1, time1.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(time1.Id + 1, version);

            time1.Replay = ProcessingDateReplay.Weekly;
            access.CreateOrUpdate(time1);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(time1.Id + 1, version);

            ProcessingTimeEntity time2 = new ProcessingTimeEntity
            {
                InitialDate = DateTime.Parse("01.01.2020 13:00:00"),
                Replay = ProcessingDateReplay.Once
            };

            access.CreateOrUpdate(time2);

            Assert.AreEqual(2, time2.Id);

            version = access.GetCounter(CounterNames.GlobalCounter);
            Assert.AreEqual(time2.Id + 1, version);
        }

        [Test]
        public void GetCreateOrUpdateProcessingTimesTest()
        {
            CreateOrUpdateProcessingTimeTest();

            List<ProcessingTimeEntity> times = access.GetProcessingTimes();

            Assert.IsNotNull(times);
            Assert.AreEqual(2, times.Count);

            ProcessingTimeEntity time1 = times.First();

            Assert.AreEqual(1, time1.Id);
            Assert.AreEqual(DateTime.Parse("01.01.2020 12:00:00"), time1.InitialDate);
            Assert.AreEqual(ProcessingDateReplay.Weekly, time1.Replay);

            ProcessingTimeEntity time2 = times.Last();

            Assert.AreEqual(2, time2.Id);
            Assert.AreEqual(DateTime.Parse("01.01.2020 13:00:00"), time2.InitialDate);
            Assert.AreEqual(ProcessingDateReplay.Once, time2.Replay);
        }
    }
}