using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Data.Entities.Work;
using System.Collections.Generic;

namespace HouseholdPlan.Core.Models.Work
{
    public class HouseholdTask : HouseholdTaskEntity
    {
        private ProcessingTimeEntity processingTime = new ProcessingTimeEntity();

        public ProcessingTimeEntity ProcessingTime
        {
            get
            {
                return processingTime;
            }
            set
            {
                if (processingTime != value)
                {
                    processingTime = value;
                    ProcessingTimeId = value.Id;
                }
            }
        }

        private User creator;

        public User Creator
        {
            get
            {
                return creator;
            }
            set
            {
                if (creator != value)
                {
                    creator = value;
                    CreatorId = value.Id;
                }
            }
        }

        public List<HistoryTask> HistoryTasks { get; set; } = new List<HistoryTask>();
    }
}