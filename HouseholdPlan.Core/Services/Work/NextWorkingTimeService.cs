using HouseholdPlan.Data.Constants;
using HouseholdPlan.Data.Entities.Work;
using System;

namespace HouseholdPlan.Core.Services.Work
{
    public class NextWorkingTimeService
    {
        public ProcessingTimeEntity ProcessingTime { get; set; }

        public DateTime GetNextWorkingTime()
        {
            var date = ProcessingTime.InitialDate;

            DateTime output = ProcessingTime.Replay switch
            {
                ProcessingDateReplay.Daily => date.AddDays(1),
                ProcessingDateReplay.Weekly => date.AddDays(7),
                ProcessingDateReplay.Monthly => date.AddMonths(1),
                ProcessingDateReplay.Yearly => date.AddMonths(12),
                ProcessingDateReplay.Once => date,
                _ => throw new Exception("Der ProcessingDateReplay Wert ist unbekannt"),
            };

            return output;
        }
    }
}