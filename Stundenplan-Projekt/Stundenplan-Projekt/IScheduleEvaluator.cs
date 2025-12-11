using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// INTERFACE: Definiert, wie ein Stundenplan bewertet wird.
    /// </summary>
    public interface IScheduleEvaluator
    {
        int BerechnePunkte(
            List<StundenplanEintrag> eintraege,
            string klasse,
            string tag,
            int stunde,
            Planungseinstellungen settings);
    }
}