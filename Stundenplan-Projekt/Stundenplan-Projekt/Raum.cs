using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Raum
    {
        private string _nummer;
        private int _kapazitaet;

        public string Nummer
        {
            get { return _nummer; }
            set { _nummer = value; }
        }

        public int Kapazitaet
        {
            get { return _kapazitaet; }
            set { _kapazitaet = value; }
        }

        public string getNummer()
        {
            return _nummer;
        }
       public int getKapazitaet()
        {
            return _kapazitaet;
        }
    }
}
