using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Definiert einen Zeitrahmen, an dem eine Person verfügbar ist.
    /// </summary>
    internal class Verfuegbarkeit
    {
        public string Tag { get; set; }
        public TimeSpan Von { get; set; }
        public TimeSpan Bis { get; set; }

        public Verfuegbarkeit(string tag, string von, string bis)
        {
            Tag = tag;
            Von = TimeSpan.Parse(von);
            Bis = TimeSpan.Parse(bis);
        }

        /// <summary>
        /// Prüft, ob eine bestimmte Zeit in diesem Verfügbarkeits-Fenster liegt.
        /// </summary>
        /// <param name="tag">Der Wochentag.</param>
        /// <param name="zeitStart">Startzeit der Stunde.</param>
        /// <param name="zeitEnde">Endzeit der Stunde.</param>
        /// <returns>True, wenn die Person Zeit hat.</returns>
        public bool IstInZeitraum(string tag, string zeitStart, string zeitEnde)
        {
            // Wir prüfen, ob der Tag stimmt (Groß/Kleinschreibung egal)
            if (Tag.ToLower() != tag.ToLower())
            {
                return false;
            }

            TimeSpan start = TimeSpan.Parse(zeitStart);
            TimeSpan ende = TimeSpan.Parse(zeitEnde);

            // Prüfen: Ist der Lehrer ab 'start' da UND bis 'ende' noch da?
            if (start >= Von && ende <= Bis)
            {
                return true;
            }

            return false;
        }
    }
}