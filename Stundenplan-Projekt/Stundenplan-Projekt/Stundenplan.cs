using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Stundenplan
    {
        private int _woche;
        private DateTime _date;

        public int Woche
        {
            get { return _woche; }
            set { _woche = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        

        public Stundenplan(int woche, DateTime date)
        {
            Woche = woche;
            Date = date;
        }

        public void generierePlan()
        {

        }

        public void speichereDaten()
        {

        }

        public void ladeDaten()
        {

        }
    }
}
