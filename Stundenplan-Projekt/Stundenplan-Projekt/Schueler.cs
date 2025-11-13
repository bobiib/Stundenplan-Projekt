using System;
using System.Collections.Generic;

namespace Stundenplan_Projekt
{
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

        public void fuegeFachHinzu(Fach fach)
        {
            _faecher.Add(fach);
        }
    }
}
