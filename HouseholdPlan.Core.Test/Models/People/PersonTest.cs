using HouseholdPlan.Data.Entities.People;
using NUnit.Framework;

namespace HouseholdPlan.Core.Test.Models.People
{
    public class PersonTest
    {
        [Test]
        public void InitTest()
        {
            int id = 1;
            string name = "Test Name";

            UserEntity person = new UserEntity
            {
                Id = id,
                Name = name
            };

            Assert.AreEqual(id, person.Id);
            Assert.AreEqual(name, person.Name);
        }
    }
}