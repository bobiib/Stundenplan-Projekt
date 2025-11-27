using System;
using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Stundenplan Generator ===");

            // --- 1. Listen erstellen ---
            List<string> klassen = new List<string>();
            klassen.Add("3A");
            klassen.Add("3B");

            // Fächer für 3A
            List<Fach> faecher3A = new List<Fach>();
            faecher3A.Add(new Fach("Mathe", 4));
            faecher3A.Add(new Fach("Deutsch", 4));
            faecher3A.Add(new Fach("Englisch", 3));
            faecher3A.Add(new Fach("Informatik", 2));
            faecher3A.Add(new Fach("Biologie", 2));

            // Fächer für 3B
            List<Fach> faecher3B = new List<Fach>();
            faecher3B.Add(new Fach("Mathe", 3));
            faecher3B.Add(new Fach("Deutsch", 4));
            faecher3B.Add(new Fach("Englisch", 3));
            faecher3B.Add(new Fach("Chemie", 2));
            faecher3B.Add(new Fach("Sport", 2));

            // Zuordnung Klasse -> Fächer
            Dictionary<string, List<Fach>> alleFaecher = new Dictionary<string, List<Fach>>();
            alleFaecher.Add("3A", faecher3A);
            alleFaecher.Add("3B", faecher3B);

            // --- 2. Lehrer erstellen ---
            List<Lehrperson> lehrer = new List<Lehrperson>();

            Lehrperson l1 = new Lehrperson("Herr Meier", "MEI");
            l1.AddFach("Mathe");
            l1.AddFach("Informatik");
            // Herr Meier kann Vormittags (Mo-Fr)
            l1.AddVerfuegbarkeit("Montag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Dienstag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Mittwoch", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Donnerstag", "08:00", "12:15");
            l1.AddVerfuegbarkeit("Freitag", "08:00", "12:15");
            lehrer.Add(l1);

            Lehrperson l2 = new Lehrperson("Frau Keller", "KEL");
            l2.AddFach("Deutsch");
            l2.AddFach("Englisch");
            // Frau Keller hat Dienstags frei (also Mo, Mi, Do, Fr eintragen)
            l2.AddVerfuegbarkeit("Montag", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Mittwoch", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Donnerstag", "08:00", "17:00");
            l2.AddVerfuegbarkeit("Freitag", "08:00", "17:00");
            lehrer.Add(l2);

            Lehrperson l3 = new Lehrperson("Herr Braun", "BRA");
            l3.AddFach("Biologie");
            l3.AddFach("Chemie");
            lehrer.Add(l3); // Keine Verfügbarkeit = Immer Zeit

            Lehrperson l4 = new Lehrperson("Frau Huber", "HUB");
            l4.AddFach("Sport");
            l4.AddFach("Englisch");
            lehrer.Add(l4);

            // --- 3. Räume ---
            List<Raum> raeume = new List<Raum>();
            raeume.Add(new Raum("101", 25));
            raeume.Add(new Raum("102", 25));
            raeume.Add(new Raum("201", 30));
            raeume.Add(new Raum("Sporthalle", 200));

            // --- 4. Benutzereingabe für Gewichtung ---
            Console.WriteLine("\nBitte Einstellungen eingeben:");

            Console.Write("Straf-Punkte für Randstunden (Standard 1): ");
            string eingabe1 = Console.ReadLine();
            int strafeRand = 1;
            if (eingabe1 != "")
            {
                strafeRand = int.Parse(eingabe1);
            }

            Console.Write("Straf-Punkte für Zwischenstunden (Standard 10): ");
            string eingabe2 = Console.ReadLine();
            int strafeLuecke = 10;
            if (eingabe2 != "")
            {
                strafeLuecke = int.Parse(eingabe2);
            }

            Planungseinstellungen settings = new Planungseinstellungen(strafeRand, strafeLuecke);

            // --- 5. Generieren ---
            Stundenplan meinPlan = new Stundenplan();
            Console.WriteLine("\nBerechne Stundenplan...");

            meinPlan.GenerierePlan(klassen, alleFaecher, lehrer, raeume, settings);

            // --- 6. Anzeigen ---
            string letzteKlasse = "";
            foreach (StundenplanEintrag e in meinPlan.Eintraege)
            {
                // Wenn neue Klasse kommt, Überschrift machen
                if (e.Klasse != letzteKlasse)
                {
                    letzteKlasse = e.Klasse;
                    Console.WriteLine("\n--- Klasse " + letzteKlasse + " ---");
                }

                Console.WriteLine(e.Tag + " " + e.ZeitVon + " : " + e.Fach + " (" + e.Lehrer + ", " + e.Raum + ")");
            }

            // Speichern
            meinPlan.SaveJson("stundenplan.json");

            Console.WriteLine("\nFertig.");
            Console.ReadKey();
        }
    }
}