using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Diese Klasse speichert Daten zu einer Klasse oder einem Schüler.
    /// </summary>
    internal class Schueler
    {
        private string _name;
        private string _klasse;
        private List<Fach> _faecher = new List<Fach>();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Klasse
        {
            get { return _klasse; }
            set { _klasse = value; }
        }

        public List<Fach> Faecher
        {
            get { return _faecher; }
        }

        public Schueler(string name, string klasse)
        {
            Name = name;
            Klasse = klasse;
        }

        /// <summary>
        /// Fügt ein Fach zur Liste der Fächer hinzu.
        /// </summary>
        public void fuegeFachHinzu(Fach fach)
        {
            _faecher.Add(fach);
        }
    }
}