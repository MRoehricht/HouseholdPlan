using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Data.Access;
using System.Linq;

namespace HouseholdPlan.Core.Factories.People
{
    public class UserFactory : Factory
    {
        public UserFactory(SQLiteAccess access) : base(access)
        {
        }

        public User LoadUser(string id)
        {
            User output = null;

            var userEntity = _access.GetUsers().FirstOrDefault(t => t.Id == id);
            if (userEntity != null)
            {
                output = new User
                {
                    Id = userEntity.Id,
                    Name = userEntity.Name
                };
            }

            return output;
        }
    }
}