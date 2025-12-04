using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Stellt ein Unterrichtsfach dar mit Name und Stundenanzahl.
    /// </summary>
    public class Fach
    {
        private string _name;
        private int _stundenProWoche;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int StundenProWoche
        {
            get { return _stundenProWoche; }
            set { _stundenProWoche = value; }
        }

        /// <summary>
        /// Erstellt ein neues Fach.
        /// </summary>
        /// <param name="name">Der Name des Fachs (z.B. Mathe).</param>
        /// <param name="stundenProWoche">Wie oft das Fach pro Woche stattfindet.</param>
        public Fach(string name, int stundenProWoche)
        {
            Name = name;
            StundenProWoche = stundenProWoche;
        }
    }
}