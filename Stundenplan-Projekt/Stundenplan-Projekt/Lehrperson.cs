using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Lehrperson
    {
        private string _name;
        private string _kuerzel;
        private List<Fach> _faecher = new List<Fach>();


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Kuerzel
        {
            get { return _kuerzel; }
            set { _kuerzel = value; }
        }

        public Lehrperson(string name, string kuerzel)
        {
            Name = name;
            Kuerzel = kuerzel;
        }

        public List<Fach> Faecher
        {
            get { return _faecher; }
        }

        public void fuegeFachHinzu(Fach fach)
        {
            _faecher.Add(fach);
        }
    }
}