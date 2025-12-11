namespace Stundenplan_Projekt
{
    /// <summary>
    /// INTERFACE: Definiert Methoden zum Laden und Speichern.
    /// </summary>
    public interface IPersistenceService
    {
        void SaveSchulDaten(SchulDaten daten, string pfad);
        SchulDaten LoadSchulDaten(string pfad);
        void SaveStundenplan(object plan, string pfad);
    }
}