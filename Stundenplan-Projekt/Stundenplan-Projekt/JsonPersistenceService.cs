using Newtonsoft.Json;
using System.IO;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// IMPLEMENTIERUNG: Speichert Daten als JSON-Dateien.
    /// </summary>
    public class JsonPersistenceService : IPersistenceService
    {
        public void SaveSchulDaten(SchulDaten daten, string pfad)
        {
            string json = JsonConvert.SerializeObject(daten, Formatting.Indented);
            File.WriteAllText(pfad, json);
        }

        public SchulDaten LoadSchulDaten(string pfad)
        {
            if (!File.Exists(pfad)) return null;
            string json = File.ReadAllText(pfad);
            return JsonConvert.DeserializeObject<SchulDaten>(json);
        }

        public void SaveStundenplan(object plan, string pfad)
        {
            string json = JsonConvert.SerializeObject(plan, Formatting.Indented);
            File.WriteAllText(pfad, json);
        }
    }
}