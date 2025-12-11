using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Exportiert den Stundenplan als CSV-Datei für Excel (Optimiert).
    /// </summary>
    public class CsvExporter : IScheduleExporter
    {
        public void ExportierePlan(List<StundenplanEintrag> eintraege, string dateiPfad)
        {
            StringBuilder csv = new StringBuilder();

            // 1: Dieser Befehl in der ersten Zeile sagt Excel: 
            // "Benutze Semikolons als Trennzeichen"
            // Dadurch öffnen sich die Spalten automatisch richtig.
            csv.AppendLine("sep=;");

            // 2: Ein schöner Header mit Datum
            csv.AppendLine("STUNDENPLAN EXPORT;;;;;");
            csv.AppendLine($"Generiert am:;{DateTime.Now.ToString("dd.MM.yyyy HH:mm")};;;;");
            csv.AppendLine(); // Eine Leerzeile für die Optik

            // 3: Klare Überschriften
            csv.AppendLine("Klasse;Wochentag;Uhrzeit;Fach;Lehrer;Raum");

            foreach (StundenplanEintrag e in eintraege)
            {
                // 4: Wir fassen Start- und Endzeit zusammen (sieht besser aus)
                string zeitSpalte = $"{e.ZeitVon} - {e.ZeitBis}";

                // Zeile bauen
                string zeile = $"{e.Klasse};{e.Tag};{zeitSpalte};{e.Fach};{e.Lehrer};{e.Raum}";

                csv.AppendLine(zeile);
            }

            // 5: Encoding mit "Preamble" (BOM)
            // Das sorgt dafür, dass Excel Umlaute (ä, ö, ü) auch wirklich richtig anzeigt.
            File.WriteAllText(dateiPfad, csv.ToString(), new UTF8Encoding(true));

            Console.WriteLine("Optimierte Excel-Datei erstellt: " + dateiPfad);
        }
    }
}