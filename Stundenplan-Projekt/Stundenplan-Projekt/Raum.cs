using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Ein Schulzimmer mit Nummer und Platzanzahl.
    /// </summary>
    internal class Raum
    {
        private string _nummer;
        private int _kapazitaet;

        public string Nummer { get => _nummer; set => _nummer = value; }

        /// <summary>
        /// Wie viele Schüler maximal in den Raum passen.
        /// </summary>
        public int Kapazitaet { get => _kapazitaet; set => _kapazitaet = value; }

        public Raum(string nummer, int kapazitaet)
        {
            _nummer = nummer;
            _kapazitaet = kapazitaet;
        }
    }
}