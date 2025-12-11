using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// INTERFACE: Definiert eine Schnittstelle, um den Stundenplan zu exportieren.
    /// </summary>
    public interface IScheduleExporter
    {
        /// <summary>
        /// Exportiert die gegebene Liste von Einträgen in eine Datei.
        /// </summary>
        void ExportierePlan(List<StundenplanEintrag> eintraege, string dateiPfad);
    }
}