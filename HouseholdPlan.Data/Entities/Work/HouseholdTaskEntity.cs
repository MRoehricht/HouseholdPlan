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
        public string Title { get; set; }

        /// <summary>
        /// Gibt die Beschreibung der Aufgabe an
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Beschreibt die Bearbeitungszeit der Aufgabe
        /// </summary>
        public int ProcessingTimeId { get; set; }

        /// <summary>
        /// Gibt die Id der Ersteller/in an.
        /// </summary>
        public int CreatorId { get; set; }
    }
}