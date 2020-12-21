namespace HouseholdPlan.Data.Constants
{
    public enum ProcessingDateReplay
    {
        /// <summary>
        /// einmalig
        /// </summary>
        Once,

        /// <summary>
        /// täglich
        /// </summary>
        Daily,

        /// <summary>
        /// wöchentlich
        /// </summary>
        Weekly,

        /// <summary>
        /// montlich
        /// </summary>
        Monthly,

        /// <summary>
        /// jährlich
        /// </summary>
        Yearly
    }

    public static class ProcessingDateReplayHelper
    {
        public static int ProcessingDateReplayToId(ProcessingDateReplay status)
        {
            int output = status switch
            {
                ProcessingDateReplay.Once => 1,
                ProcessingDateReplay.Daily => 2,
                ProcessingDateReplay.Weekly => 3,
                ProcessingDateReplay.Monthly => 4,
                ProcessingDateReplay.Yearly => 5,
                _ => throw new System.Exception("ProcessingDateReplay ist unbekannt"),
            };
            return output;
        }

        public static ProcessingDateReplay IdToProcessingDateReplay(int id)
        {
            ProcessingDateReplay output = id switch
            {
                1 => ProcessingDateReplay.Once,
                2 => ProcessingDateReplay.Daily,
                3 => ProcessingDateReplay.Weekly,
                4 => ProcessingDateReplay.Monthly,
                5 => ProcessingDateReplay.Yearly,
                _ => throw new System.Exception("ProcessingDateReplay ist unbekannt"),
            };
            return output;
        }
    }
}