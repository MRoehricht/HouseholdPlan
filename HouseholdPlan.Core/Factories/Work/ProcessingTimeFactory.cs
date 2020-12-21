using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Access;
using System.Linq;

namespace HouseholdPlan.Core.Factories.Work
{
    public class ProcessingTimeFactory : Factory
    {
        public ProcessingTimeFactory(SQLiteAccess access) : base(access)
        {
        }

        public ProcessingTime LoadProcessingTime(int id)
        {
            ProcessingTime output = null;
            var processEntity = _access.GetProcessingTimes().FirstOrDefault(t => t.Id == id);
            if (processEntity != null)
            {
                output = new ProcessingTime
                {
                    Id = processEntity.Id,
                    Every = processEntity.Every,
                    InitialDate = processEntity.InitialDate,
                    Replay = processEntity.Replay
                };
            }

            return output;
        }
    }
}