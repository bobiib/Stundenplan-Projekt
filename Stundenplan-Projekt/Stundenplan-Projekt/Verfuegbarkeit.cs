using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Verfuegbarkeit
    {
        private string _tag;
        private string _zeitVon;
        private string _zeitBis;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public string ZeitVon
        {
            get { return _zeitVon; }
            set { _zeitVon = value; }
        }

        public string ZeitBis
        {
            get { return _zeitBis; }
            set { _zeitBis = value; }
        }

        public bool istVerfuegbar(string Tag, string ZeitVon, string ZeitBis)
        {
         
        }
    }
}
