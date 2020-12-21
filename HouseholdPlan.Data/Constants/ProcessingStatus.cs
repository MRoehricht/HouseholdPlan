namespace HouseholdPlan.Data.Constants
{
    public enum ProcessingStatus
    {
        Done,
        NotDone,
        InProgress
    }

    public static class ProcessingStatusHelper
    {
        public static int ProcessingStatusToId(ProcessingStatus status)
        {
            int output = status switch
            {
                ProcessingStatus.Done => 1,
                ProcessingStatus.NotDone => 2,
                ProcessingStatus.InProgress => 3,
                _ => throw new System.Exception("ProcessingStatus ist unbekannt"),
            };
            return output;
        }

        public static ProcessingStatus IdToProcessingStatus(int id)
        {
            ProcessingStatus output = id switch
            {
                1 => ProcessingStatus.Done,
                2 => ProcessingStatus.NotDone,
                3 => ProcessingStatus.InProgress,
                _ => throw new System.Exception("ProcessingStatus ist unbekannt"),
            };
            return output;
        }
    }
}