using HouseholdPlan.Data.Constants;
using System;

namespace HouseholdPlan.Data.Entities.Work
{
    public class HistoryTaskEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int EditorId { get; set; }
        public DateTime Date { get; set; }
        public ProcessingStatus Status { get; set; }
    }
}