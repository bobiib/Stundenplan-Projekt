using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Eine einzelne Stunde im fertigen Stundenplan.
    /// </summary>
    internal class StundenplanEintrag
    {
        public string Klasse { get; set; }
        public string Fach { get; set; }
        public string Lehrer { get; set; }
        public string Raum { get; set; }
        public string Tag { get; set; }
        public string ZeitVon { get; set; }
        public string ZeitBis { get; set; }

        /// <summary>
        /// Die Nummer der Lektion (0 = erste Stunde, 1 = zweite Stunde)
        /// </summary>
        public int PeriodeIndex { get; set; }
    }
}