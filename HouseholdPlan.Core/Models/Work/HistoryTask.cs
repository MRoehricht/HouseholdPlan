using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Data.Entities.Work;

namespace HouseholdPlan.Core.Models.Work
{
    public class HistoryTask : HistoryTaskEntity
    {
        private HouseholdTaskEntity task;

        public HouseholdTaskEntity Task
        {
            get
            {
                return task;
            }
            set
            {
                if (task != value)
                {
                    task = value;
                    TaskId = value.Id;
                }
            }
        }

        private User editor;

        public User Editor
        {
            get
            {
                return editor;
            }
            set
            {
                if (editor != value)
                {
                    editor = value;
                    EditorId = value.Id;
                }
            }
        }
    }
}