using System;

namespace Stundenplan_Projekt
{
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