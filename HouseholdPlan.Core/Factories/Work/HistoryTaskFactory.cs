using HouseholdPlan.Core.Factories.People;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Access;
using HouseholdPlan.Data.Entities.Work;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdPlan.Core.Factories.Work
{
    public class HistoryTaskFactory : Factory
    {
        private readonly UserFactory userFactory;

        public HistoryTaskFactory(SQLiteAccess access) : base(access)
        {
            userFactory = new UserFactory(access);
        }

        public List<HistoryTask> LoadHistoryTasks(HouseholdTaskEntity task)
        {
            List<HistoryTask> output = new List<HistoryTask>();

            List<HistoryTaskEntity> historyTaskEntities = _access.GetHistoryTasks().Where(t => t.TaskId == task.Id).ToList();

            foreach (var historyTaskEntity in historyTaskEntities)
            {
                HistoryTask historyTask = new HistoryTask
                {
                    Id = historyTaskEntity.Id,
                    TaskId = historyTaskEntity.TaskId,
                    EditorId = historyTaskEntity.EditorId,
                    Date = historyTaskEntity.Date,
                    Status = historyTaskEntity.Status,
                    Task = task,
                    Editor = userFactory.LoadUser(historyTaskEntity.EditorId)
                };

                output.Add(historyTask);
            }

            return output;
        }
    }
}