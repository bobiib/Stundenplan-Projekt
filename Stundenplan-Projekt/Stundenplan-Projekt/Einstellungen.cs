using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Speichert die Regeln für die Bewertung des Stundenplans.
    /// </summary>
    public class Planungseinstellungen
    {
        // Hier speichern wir die Punktzahlen für die Bewertung
        public int StrafeRandstunde;
        public int StrafeZwischenstunde;
        public int StrafeRessourcen;

        /// <summary>
        /// Erstellt die Einstellungen.
        /// </summary>
        /// <param name="strafeRand">Punkteabzug für Randstunden.</param>
        /// <param name="strafeLuecke">Punkteabzug für Zwischenstunden.</param>
        /// <param name="strafeRessourcen">Punkteabzug für gleichzeitige Raumnutzung.</param>
        public Planungseinstellungen(int strafeRand, int strafeLuecke, int strafeRessourcen)
        {
            StrafeRandstunde = strafeRand;
            StrafeZwischenstunde = strafeLuecke;
            StrafeRessourcen = strafeRessourcen;
        }
    }
}