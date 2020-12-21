using HouseholdPlan.Data.Access;

namespace HouseholdPlan.Core.Factories
{
    public class Factory
    {
        protected SQLiteAccess _access;

        public Factory(SQLiteAccess access)
        {
            _access = access;
        }
    }
}