using System;

namespace Stundenplan_Projekt
{
    internal class Verfuegbarkeit
    {
        public string Tag { get; set; }
        public string ZeitVon { get; set; }
        public string ZeitBis { get; set; }

        public Verfuegbarkeit(string tag, string zeitVon, string zeitBis)
        {
            Tag = tag;
            ZeitVon = zeitVon;
            ZeitBis = zeitBis;
        }

        public bool istVerfuegbar(string tag, string zeitVon, string zeitBis)
        {
            if (Tag.Equals(tag, StringComparison.OrdinalIgnoreCase))
            {
                TimeSpan start = TimeSpan.Parse(ZeitVon);
                TimeSpan ende = TimeSpan.Parse(ZeitBis);
                TimeSpan pruefStart = TimeSpan.Parse(zeitVon);
                TimeSpan pruefEnde = TimeSpan.Parse(zeitBis);

                return (pruefStart >= start && pruefEnde <= ende);
            }
            return false;
        }
    }
}
