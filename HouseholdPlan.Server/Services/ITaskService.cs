using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Core.Models.Work;
using System.Collections.Generic;

namespace HouseholdPlan.Server.Services
{
    public interface ITaskService
    {
        void CreateOrUpdateTask(HouseholdTask task);
        List<HouseholdTask> GetUserTasks(User user);
        List<HouseholdTask> GetUserTasks(string userGuid);
    }
}