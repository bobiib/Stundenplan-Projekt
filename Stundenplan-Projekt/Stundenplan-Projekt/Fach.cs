using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Fach
    {
        private string _name;
        private int stundenProWoche;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int getStundenProWoche()
        {
            return stundenProWoche;
        }
    }
}
