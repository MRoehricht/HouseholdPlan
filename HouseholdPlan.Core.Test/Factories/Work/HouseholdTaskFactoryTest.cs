using HouseholdPlan.Core.Factories.Work;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Core.Test.Factories.People;
using HouseholdPlan.Data.Access;
using NUnit.Framework;

namespace HouseholdPlan.Core.Test.Factories.Work
{
    public class HouseholdTaskFactoryTest : FactoryTestBase
    {
        public const string Task1Title = "Bad putzen";
        public const string Task1Description = "Klo, Wanne, Boden";

        public const string Task2Title = "Fenster putzen";
        public const string Task2Description = "Griffe, Innen, Außen";

        [Test]
        public void LoadTest()
        {
            CreateHouseholdTask();

            HouseholdTaskFactory householdTaskFactory = new HouseholdTaskFactory(access);
            var task1 = householdTaskFactory.LoadHouseholdTask(3);
            Assert.AreEqual(3, task1.Id);
            Assert.AreEqual(Task1Title, task1.Title);
            Assert.AreEqual(Task1Description, task1.Description);
            Assert.AreEqual("1", task1.Creator.Id);
            Assert.AreEqual(UserFactoryTest.UserName1, task1.Creator.Name);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time1, task1.ProcessingTime.InitialDate);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time1Reply, task1.ProcessingTime.Replay);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time1Every, task1.ProcessingTime.Every);

            var task2 = householdTaskFactory.LoadHouseholdTask(4);
            Assert.AreEqual(4, task2.Id);
            Assert.AreEqual(Task2Title, task2.Title);
            Assert.AreEqual(Task2Description, task2.Description);
            Assert.AreEqual("2", task2.Creator.Id);
            Assert.AreEqual(UserFactoryTest.UserName2, task2.Creator.Name);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time2, task2.ProcessingTime.InitialDate);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time2Reply, task2.ProcessingTime.Replay);
            Assert.AreEqual(ProcessingTimeFactoryTest.Time2Every, task2.ProcessingTime.Every);
        }

        [Test]
        public void CreateHouseholdTask()
        {
            if (access.GetUsers().Count == 0)
            {
                UserFactoryTest userFactoryTest = new UserFactoryTest();
                userFactoryTest.CreateUsers(access);
            }

            ProcessingTimeFactoryTest processingTimeFactoryTest = new ProcessingTimeFactoryTest();
            processingTimeFactoryTest.CreateProcessingTimes(access);

            HouseholdTask task1 = new HouseholdTask
            {
                CreatorId = "1",
                Title = Task1Title,
                Description = Task1Description,
                ProcessingTimeId = 1
            };

            access.CreateOrUpdate(task1);

            HouseholdTask task2 = new HouseholdTask
            {
                CreatorId = "2",
                Title = Task2Title,
                Description = Task2Description,
                ProcessingTimeId = 2
            };

            access.CreateOrUpdate(task2);
        }

        public void CreateHouseholdTask(SQLiteAccess access)
        {
            this.access = access;
            CreateHouseholdTask();
        }
    }
}