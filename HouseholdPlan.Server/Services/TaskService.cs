﻿using HouseholdPlan.Core.Factories.Work;
using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Core.Models.Work;
using HouseholdPlan.Data.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseholdPlan.Server.Services
{
    public class TaskService : ITaskService
    {
        private readonly HouseholdTaskFactory householdTaskFactory;
        private readonly SQLiteAccess access;

        public TaskService()
        {
            access = new SQLiteAccess();
            householdTaskFactory = new HouseholdTaskFactory(access);
        }

        public void CreateOrUpdateTask(HouseholdTask task)
        {
            access.CreateOrUpdate(task);
        }

        public List<HouseholdTask> GetUserTasks(User user)
        {
            List<HouseholdTask> householdTasks = new List<HouseholdTask>();
            List<int> taskIds = access.GetHouseholdTasks().Where(h => h.CreatorId == user.Id).Select(h => h.Id).ToList();

            foreach (var taskId in taskIds)
            {
                householdTasks.Add(householdTaskFactory.LoadHouseholdTask(taskId));
            }

            return householdTasks;
        }
    }
}
