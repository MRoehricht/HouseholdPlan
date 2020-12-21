using HouseholdPlan.Core.Factories.People;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Access;
using System.Linq;

namespace HouseholdPlan.Core.Factories.Work
{
    public class HouseholdTaskFactory : Factory
    {
        private readonly HistoryTaskFactory historyTaskFactory;
        private readonly ProcessingTimeFactory processingTimeFactory;
        private readonly UserFactory userFactory;

        public HouseholdTaskFactory(SQLiteAccess access) : base(access)
        {
            historyTaskFactory = new HistoryTaskFactory(access);
            processingTimeFactory = new ProcessingTimeFactory(access);
            userFactory = new UserFactory(access);
        }

        public HouseholdTask LoadHouseholdTask(int id)
        {
            HouseholdTask output = null;

            var householdTaskEntity = _access.GetHouseholdTasks().FirstOrDefault(t => t.Id == id);

            if (householdTaskEntity != null)
            {
                output = new HouseholdTask
                {
                    Id = id,
                    Creator = userFactory.LoadUser(householdTaskEntity.CreatorId),
                    CreatorId = householdTaskEntity.CreatorId,
                    Description = householdTaskEntity.Description,
                    HistoryTasks = historyTaskFactory.LoadHistoryTasks(householdTaskEntity),
                    ProcessingTime = processingTimeFactory.LoadProcessingTime(householdTaskEntity.ProcessingTimeId),
                    ProcessingTimeId = householdTaskEntity.ProcessingTimeId,
                    Title = householdTaskEntity.Title
                };
            }

            return output;
        }
    }
}