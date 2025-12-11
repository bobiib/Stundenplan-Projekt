using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Exportiert den Stundenplan als CSV-Datei für Excel.
    /// </summary>
    public class CsvExporter : IScheduleExporter
    {
        public void ExportierePlan(List<StundenplanEintrag> eintraege, string dateiPfad)
        {
            // Wir nutzen StringBuilder für bessere Performance beim Text-Zusammenbauen
            StringBuilder csv = new StringBuilder();

            // 1. Die Überschriften-Zeile schreiben
            csv.AppendLine("Klasse;Tag;Stunde;Fach;Lehrer;Raum");

            // 2. Alle Einträge durchgehen und als Zeile hinzufügen
            foreach (StundenplanEintrag e in eintraege)
            {
                // Wir bauen eine Zeile: "3A;Montag;08:00;Mathe;Herr Meier;101"
                string zeile = $"{e.Klasse};{e.Tag};{e.ZeitVon};{e.Fach};{e.Lehrer};{e.Raum}";

                csv.AppendLine(zeile);
            }

            // 3. Datei speichern (Encoding.UTF8 ist wichtig für Umlaute wie 'ä')
            File.WriteAllText(dateiPfad, csv.ToString(), Encoding.UTF8);

            Console.WriteLine("Excel-Datei (CSV) erfolgreich erstellt: " + dateiPfad);
        }
    }
}