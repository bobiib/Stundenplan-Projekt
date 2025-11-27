using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Die Hauptklasse, die das Menü anzeigt und die Benutzereingaben steuert.
    /// </summary>
    internal class Program
    {
        // Der Pfad zur Datei, in der wir Lehrer, Räume und Klassen speichern
        static string datenPfad = "stammdaten.json";

        static void Main(string[] args)
        {
            SchulDaten daten;

            // Zuerst prüfen wir, ob wir schon eine Datei auf der Festplatte haben
            if (File.Exists(datenPfad))
            {
                Console.WriteLine("Lade vorhandene Daten aus " + datenPfad + "...");
                string json = File.ReadAllText(datenPfad);
                daten = JsonConvert.DeserializeObject<SchulDaten>(json);
            }
            else
            {
                // Wenn nicht, dann erstellen wir die Testdaten neu
                Console.WriteLine("Erste Nutzung: Erzeuge Testdaten...");
                daten = ErzeugeTestDaten();

                // Wir speichern das sofort ab, damit die Datei angelegt wird
                SpeicherDaten(daten);
                Console.WriteLine("Testdaten wurden in " + datenPfad + " gespeichert.");
            }

            // Hier startet die Endlosschleife für das Menü
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== HAUPTMENÜ ===");
                Console.WriteLine("1. Stundenplan generieren");
                Console.WriteLine("2. Neuen LEHRER anlegen");
                Console.WriteLine("3. Neuen RAUM anlegen");
                Console.WriteLine("4. Neue KLASSE anlegen");
                Console.WriteLine("5. Beenden");
                Console.Write("\nAuswahl: ");

                string wahl = Console.ReadLine();

                if (wahl == "1")
                {
                    GeneriereStundenplan(daten);
                }
                else if (wahl == "2")
                {
                    NeuerLehrer(daten);
                }
                else if (wahl == "3")
                {
                    NeuerRaum(daten);
                }
                else if (wahl == "4")
                {
                    NeueKlasse(daten);
                }
                else if (wahl == "5")
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Speichert alle aktuellen Daten in die JSON-Datei.
        /// </summary>
        static void SpeicherDaten(SchulDaten daten)
        {
            string json = JsonConvert.SerializeObject(daten, Formatting.Indented);
            File.WriteAllText(datenPfad, json);
        }

        /// <summary>
        /// Fragt den Benutzer nach Daten für einen neuen Lehrer.
        /// </summary>
        static void NeuerLehrer(SchulDaten daten)
        {
            Console.WriteLine("\n--- Neuer Lehrer ---");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Kürzel: ");
            string kuerzel = Console.ReadLine();

            Lehrperson neu = new Lehrperson(name, kuerzel);

            Console.WriteLine("Fächer eingeben (leer lassen zum Beenden):");
            while (true)
            {
                Console.Write("Fach: ");
                string fach = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fach)) break;
                neu.AddFach(fach);
            }

            // Wir setzen die Verfügbarkeit einfachheitshalber auf die ganze Woche
            neu.AddVerfuegbarkeit("Montag", "08:00", "17:00");
            neu.AddVerfuegbarkeit("Dienstag", "08:00", "17:00");
            neu.AddVerfuegbarkeit("Mittwoch", "08:00", "17:00");
            neu.AddVerfuegbarkeit("Donnerstag", "08:00", "17:00");
            neu.AddVerfuegbarkeit("Freitag", "08:00", "17:00");

            daten.Lehrer.Add(neu);
            SpeicherDaten(daten);
            Console.WriteLine("Gespeichert! Taste drücken.");
            Console.ReadKey();
        }

        /// <summary>
        /// Erstellt einen neuen Raum basierend auf Benutzereingaben.
        /// </summary>
        static void NeuerRaum(SchulDaten daten)
        {
            Console.WriteLine("\n--- Neuer Raum ---");
            Console.Write("Nummer: ");
            string nr = Console.ReadLine();
            Console.Write("Kapazität: ");
            int kap = int.Parse(Console.ReadLine());

            Raum neu = new Raum(nr, kap);
            daten.Raeume.Add(neu);

            SpeicherDaten(daten);
            Console.WriteLine("Gespeichert! Taste drücken.");
            Console.ReadKey();
        }

        /// <summary>
        /// Legt eine neue Klasse an und fragt die Fächer ab.
        /// </summary>
        static void NeueKlasse(SchulDaten daten)
        {
            Console.WriteLine("\n--- Neue Klasse ---");
            Console.Write("Name (z.B. 4C): ");
            string name = Console.ReadLine();

            if (daten.KlassenNamen.Contains(name))
            {
                Console.WriteLine("Gibt es schon!");
                Console.ReadKey();
                return;
            }

            daten.KlassenNamen.Add(name);
            List<Fach> faecher = new List<Fach>();

            Console.WriteLine("Fächer hinzufügen (leer lassen zum Beenden):");
            while (true)
            {
                Console.Write("Fach: ");
                string fName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fName)) break;

                Console.Write("Stunden pro Woche: ");
                int std = int.Parse(Console.ReadLine());

                faecher.Add(new Fach(fName, std));
            }

            daten.KlassenFaecher.Add(name, faecher);
            SpeicherDaten(daten);
            Console.WriteLine("Gespeichert! Taste drücken.");
            Console.ReadKey();
        }

        /// <summary>
        /// Startet den Prozess der Stundenplanerstellung.
        /// </summary>
        static void GeneriereStundenplan(SchulDaten daten)
        {
            Console.Clear();
            Console.WriteLine("=== EINSTELLUNGEN ===");

            Console.Write("Strafe Randstunden (Std 1): ");
            string e1 = Console.ReadLine();
            int s1 = e1 == "" ? 1 : int.Parse(e1);

            Console.Write("Strafe Zwischenstunden (Std 10): ");
            string e2 = Console.ReadLine();
            int s2 = e2 == "" ? 10 : int.Parse(e2);

            Console.Write("Strafe Ressourcen (Std 2): ");
            string e3 = Console.ReadLine();
            int s3 = e3 == "" ? 2 : int.Parse(e3);

            Planungseinstellungen settings = new Planungseinstellungen(s1, s2, s3);

            Stundenplan plan = new Stundenplan();
            Console.WriteLine("Berechne...");

            // Hier nutzen wir die Daten die wir vorher geladen haben
            plan.GenerierePlan(daten.KlassenNamen, daten.KlassenFaecher, daten.Lehrer, daten.Raeume, settings);

            string letzteKlasse = "";
            foreach (StundenplanEintrag e in plan.Eintraege)
            {
                if (e.Klasse != letzteKlasse)
                {
                    letzteKlasse = e.Klasse;
                    Console.WriteLine("\n--- Klasse " + letzteKlasse + " ---");
                }
                Console.WriteLine(e.Tag + " " + e.ZeitVon + " : " + e.Fach + " (" + e.Lehrer + ", " + e.Raum + ")");
            }

            plan.SaveJson("stundenplan_ergebnis.json");

            Console.WriteLine("\nFertig. Taste drücken.");
            Console.ReadKey();
        }

        /// <summary>
        /// Erstellt die Start-Daten falls das Programm zum ersten Mal läuft.
        /// </summary>
        static SchulDaten ErzeugeTestDaten()
        {
            SchulDaten d = new SchulDaten();

            d.KlassenNamen.Add("3A");
            d.KlassenNamen.Add("3B");

            List<Fach> f3A = new List<Fach>();
            f3A.Add(new Fach("Mathe", 4));
            f3A.Add(new Fach("Deutsch", 4));
            f3A.Add(new Fach("Englisch", 3));
            f3A.Add(new Fach("Informatik", 2));
            f3A.Add(new Fach("Biologie", 2));
            d.KlassenFaecher.Add("3A", f3A);

            List<Fach> f3B = new List<Fach>();
            f3B.Add(new Fach("Mathe", 3));
            f3B.Add(new Fach("Deutsch", 4));
            f3B.Add(new Fach("Englisch", 3));
            f3B.Add(new Fach("Chemie", 2));
            f3B.Add(new Fach("Sport", 2));
            d.KlassenFaecher.Add("3B", f3B);

            Lehrperson l1 = new Lehrperson("Herr Meier", "MEI");
            l1.AddFach("Mathe");
            l1.AddFach("Informatik");
            l1.AddVerfuegbarkeit("Montag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Dienstag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Mittwoch", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Donnerstag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Freitag", "08:00", "12:15");
            d.Lehrer.Add(l1);

            Lehrperson l2 = new Lehrperson("Frau Keller", "KEL");
            l2.AddFach("Deutsch");
            l2.AddFach("Englisch");
            l2.AddVerfuegbarkeit("Montag", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Mittwoch", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Donnerstag", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Freitag", "08:00", "17:00");
            d.Lehrer.Add(l2);

            Lehrperson l3 = new Lehrperson("Herr Braun", "BRA");
            l3.AddFach("Biologie");
            l3.AddFach("Chemie");
            l3.AddVerfuegbarkeit("Montag", "08:00", "17:00");
            l3.AddVerfuegbarkeit("Dienstag", "08:00", "17:00");
            l3.AddVerfuegbarkeit("Mittwoch", "08:00", "17:00");
            l3.AddVerfuegbarkeit("Donnerstag", "08:00", "17:00");
            l3.AddVerfuegbarkeit("Freitag", "08:00", "17:00");
            d.Lehrer.Add(l3);

            Lehrperson l4 = new Lehrperson("Frau Huber", "HUB");
            l4.AddFach("Sport");
            l4.AddFach("Englisch");
            l4.AddVerfuegbarkeit("Montag", "08:00", "17:00");
            l4.AddVerfuegbarkeit("Dienstag", "08:00", "17:00");
            l4.AddVerfuegbarkeit("Mittwoch", "08:00", "17:00");
            l4.AddVerfuegbarkeit("Donnerstag", "08:00", "17:00");
            l4.AddVerfuegbarkeit("Freitag", "08:00", "17:00");
            d.Lehrer.Add(l4);

            d.Raeume.Add(new Raum("101", 25));
            d.Raeume.Add(new Raum("102", 25));
            d.Raeume.Add(new Raum("201", 30));
            d.Raeume.Add(new Raum("Sporthalle", 200));

            return d;
        }
    }
}