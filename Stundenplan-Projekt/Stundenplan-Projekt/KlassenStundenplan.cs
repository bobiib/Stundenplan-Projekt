using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    public class KlassenStundenplan
    {
        public string KlassenName { get; set; }
        public List<StundenplanEintrag> Eintraege { get; set; } = new List<StundenplanEintrag>();
    }
}
