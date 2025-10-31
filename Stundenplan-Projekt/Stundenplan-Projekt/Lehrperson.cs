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

        public string getName()
        {
            return _name;
        }

        public string getKuerzel()
        {
            return _kuerzel;
        }
    }
}
