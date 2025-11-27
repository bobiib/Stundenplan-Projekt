using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Container-Klasse für das Speichern und Laden aller Daten (Lehrer, Räume, Klassen).
    /// </summary>
    internal class SchulDaten
    {
        // Hier speichern wir alles drin
        public List<Lehrperson> Lehrer { get; set; } = new List<Lehrperson>();
        public List<Raum> Raeume { get; set; } = new List<Raum>();

        // Klassen-Namen (z.B. "3A", "3B")
        public List<string> KlassenNamen { get; set; } = new List<string>();

        // Welcher Klasse hat welche Fächer
        public Dictionary<string, List<Fach>> KlassenFaecher { get; set; } = new Dictionary<string, List<Fach>>();
    }
}