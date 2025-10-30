using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Schueler
    {
        private string _name;
        private string _klasse;

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

        public string getName()
        {
            return _name;
        }
        public string getKlasse()
        {
            return _klasse;
        }
    }
}
