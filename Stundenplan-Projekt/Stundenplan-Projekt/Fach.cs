using System;

namespace Stundenplan_Projekt
{
    internal class Fach
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

        public Fach(string name, int stundenProWoche)
        {
            Name = name;
            StundenProWoche = stundenProWoche;
        }
    }
}
