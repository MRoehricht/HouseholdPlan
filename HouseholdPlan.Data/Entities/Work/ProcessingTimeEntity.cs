using HouseholdPlan.Data.Constants;
using System;

namespace HouseholdPlan.Data.Entities.Work
{
    public class ProcessingTimeEntity
    {
        /// <summary>
        /// Eindeutige Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gibt eine Anzahl an welche gewartet wird.
        /// Zum Beispiel alle 3 Tage (Every = 3 Replay Daily)
        /// </summary>
        public int Every { get; set; }

        /// <summary>
        /// Wiederholung
        /// </summary>
        public ProcessingDateReplay Replay { get; set; }

        /// <summary>
        /// Startzeitpunkt
        /// </summary>
        public DateTime InitialDate { get; set; }
    }
}