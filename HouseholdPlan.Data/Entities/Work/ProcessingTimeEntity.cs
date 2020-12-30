using HouseholdPlan.Data.Constants;
using System;
using System.ComponentModel.DataAnnotations;

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
        public int Every { get; set; } = 1;

        /// <summary>
        /// Wiederholung
        /// </summary>
        public ProcessingDateReplay Replay { get; set; } = ProcessingDateReplay.Daily;

        /// <summary>
        /// Startzeitpunkt
        /// </summary>
        [Display(Name = "Initial Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "g")]
        public DateTime InitialDate { get; set; } = DateTime.Now;
    }
}