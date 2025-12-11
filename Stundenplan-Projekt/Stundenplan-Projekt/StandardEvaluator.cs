using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// IMPLEMENTIERUNG: Die Standard-Bewertung (Randstunden, Lücken etc.).
    /// </summary>
    public class StandardEvaluator : IScheduleEvaluator
    {
        public int BerechnePunkte(
            List<StundenplanEintrag> eintraege,
            string klasse,
            string tag,
            int stunde,
            Planungseinstellungen settings)
        {
            int punkte = 0;

            // Regel 1: Randstunden
            if (stunde == 0 || stunde == 6)
            {
                punkte += settings.StrafeRandstunde;
            }

            // Regel 2: Zwischenstunden
            bool hatStundenAmTag = false;
            bool hatAnschluss = false;

            foreach (StundenplanEintrag e in eintraege)
            {
                if (e.Klasse == klasse && e.Tag == tag)
                {
                    hatStundenAmTag = true;
                    if (e.PeriodeIndex == stunde - 1 || e.PeriodeIndex == stunde + 1)
                    {
                        hatAnschluss = true;
                    }
                }
            }

            if (hatStundenAmTag && !hatAnschluss)
            {
                punkte += settings.StrafeZwischenstunde;
            }

            // Regel 3: Ressourcenschonung
            int anzahlParallel = 0;
            foreach (StundenplanEintrag e in eintraege)
            {
                if (e.Tag == tag && e.PeriodeIndex == stunde)
                {
                    anzahlParallel++;
                }
            }
            punkte += (anzahlParallel * settings.StrafeRessourcen);

            return punkte;
        }
    }
}