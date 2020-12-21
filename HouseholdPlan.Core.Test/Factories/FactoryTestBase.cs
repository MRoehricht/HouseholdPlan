using HouseholdPlan.Data.Access;
using NUnit.Framework;
using System.IO;

namespace HouseholdPlan.Core.Test.Factories
{
    public class FactoryTestBase
    {
        private const string TestDataBaseName = "Test.db";
        protected SQLiteAccess access;

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

            access.InitDataBase();
        }
    }
}