﻿using System.ComponentModel.DataAnnotations;

namespace HouseholdPlan.Data.Entities.Work
{
    /// <summary>
    /// Gibt eine Aufgabe an.
    /// </summary>
    public class HouseholdTaskEntity
    {
        /// <summary>
        /// Gibt die eindeutige Nummer der Aufgabe an.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gibt den Titel einer Aufgabe an
        /// </summary>
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title Required!")]
        public string Title { get; set; }

        /// <summary>
        /// Gibt die Beschreibung der Aufgabe an
        /// </summary>
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description Required!")]
        public string Description { get; set; }

        /// <summary>
        /// Beschreibt die Bearbeitungszeit der Aufgabe
        /// </summary>
        public int ProcessingTimeId { get; set; }

        /// <summary>
        /// Gibt die Id der Ersteller/in an.
        /// </summary>
        public string CreatorId { get; set; }
    }
}