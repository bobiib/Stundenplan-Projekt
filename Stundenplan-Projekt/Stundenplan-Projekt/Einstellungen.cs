using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan_Projekt
{
    internal class Planungseinstellungen
    {
        // Hier speichern wir die Punktzahlen für die Bewertung
        public int StrafeRandstunde;
        public int StrafeZwischenstunde;

        public Planungseinstellungen(int strafeRand, int strafeLuecke)
        {
            StrafeRandstunde = strafeRand;
            StrafeZwischenstunde = strafeLuecke;
        }
    }
}
