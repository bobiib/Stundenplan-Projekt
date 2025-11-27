using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json; // Das brauchst du für das Speichern

namespace Stundenplan_Projekt
{
    internal class Stundenplan
    {
        // Hier speichern wir alle fertigen Stunden
        public List<StundenplanEintrag> Eintraege = new List<StundenplanEintrag>();

        // Hilfs-Arrays für Tage und Zeiten
        private string[] Tage = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag" };
        private string[] StartZeiten = { "08:00", "08:50", "09:50", "10:40", "11:30", "13:15", "14:05" };
        private string[] EndZeiten = { "08:45", "09:35", "10:35", "11:25", "12:15", "14:00", "14:50" };

        public void GenerierePlan(
            List<string> klassen,
            Dictionary<string, List<Fach>> klassenFaecher,
            List<Lehrperson> lehrer,
            List<Raum> raeume,
            Planungseinstellungen settings)
        {
            Eintraege.Clear(); // Alten Plan löschen
            Random wuerfel = new Random();

            // 1. Schleife: Jede Klasse durchgehen
            foreach (string klasse in klassen)
            {
                List<Fach> faecherListe = klassenFaecher[klasse];

                // 2. Schleife: Jedes Fach der Klasse durchgehen
                foreach (Fach fach in faecherListe)
                {
                    // Wenn ein Fach 4 Stunden hat, müssen wir 4 Plätze finden
                    for (int i = 0; i < fach.StundenProWoche; i++)
                    {
                        StundenplanEintrag besterEintrag = null;
                        int wenigsteStrafpunkte = 10000; // Startwert sehr hoch setzen

                        // Wir versuchen 50 mal, einen guten Platz zu finden
                        for (int versuch = 0; versuch < 50; versuch++)
                        {
                            // Zufälligen Tag und Stunde wählen
                            int tagIndex = wuerfel.Next(0, 5); // 0 bis 4 (Mo-Fr)
                            int stundenIndex = wuerfel.Next(0, 7); // 0 bis 6 (Lektionen)

                            string tag = Tage[tagIndex];
                            string start = StartZeiten[stundenIndex];
                            string ende = EndZeiten[stundenIndex];

                            // --- PRÜFUNG 1: Ist die Klasse da schon besetzt? ---
                            if (IstKlasseBesetzt(klasse, tag, stundenIndex) == true)
                            {
                                continue; // Neuer Versuch
                            }

                            // --- PRÜFUNG 2: Einen passenden Lehrer suchen ---
                            Lehrperson gewaehlterLehrer = null;
                            foreach (Lehrperson l in lehrer)
                            {
                                // Kann er das Fach? UND Hat er Zeit (Verfügbarkeit)? UND Ist er noch frei?
                                if (l.KannUnterrichten(fach.Name) == true &&
                                    l.IstVerfuegbar(tag, start, ende) == true &&
                                    IstLehrerBesetzt(l.Name, tag, stundenIndex) == false)
                                {
                                    gewaehlterLehrer = l;
                                    break; // Lehrer gefunden -> Suche abbrechen
                                }
                            }

                            if (gewaehlterLehrer == null) continue; // Keinen Lehrer gefunden -> Neuer Versuch

                            // --- PRÜFUNG 3: Einen freien Raum suchen ---
                            Raum gewaehlterRaum = null;
                            foreach (Raum r in raeume)
                            {
                                if (IstRaumBesetzt(r.Nummer, tag, stundenIndex) == false)
                                {
                                    gewaehlterRaum = r;
                                    break; // Raum gefunden -> Suche abbrechen
                                }
                            }

                            if (gewaehlterRaum == null) continue; // Keinen Raum gefunden -> Neuer Versuch

                            // --- BEWERTUNG: Wie "gut" ist dieser Termin? ---
                            int strafPunkte = BerechnePunkte(klasse, tag, stundenIndex, settings);

                            // Wenn dieser Versuch besser ist als der bisher beste (weniger Punkte)
                            if (strafPunkte < wenigsteStrafpunkte)
                            {
                                wenigsteStrafpunkte = strafPunkte;

                                // Wir merken uns diesen Eintrag als "Besten Kandidaten"
                                besterEintrag = new StundenplanEintrag();
                                besterEintrag.Klasse = klasse;
                                besterEintrag.Fach = fach.Name;
                                besterEintrag.Lehrer = gewaehlterLehrer.Name;
                                besterEintrag.Raum = gewaehlterRaum.Nummer;
                                besterEintrag.Tag = tag;
                                besterEintrag.ZeitVon = start;
                                besterEintrag.ZeitBis = ende;
                                besterEintrag.PeriodeIndex = stundenIndex;
                            }
                        }

                        // Nach 50 Versuchen nehmen wir den besten, den wir gefunden haben
                        if (besterEintrag != null)
                        {
                            Eintraege.Add(besterEintrag);
                        }
                        else
                        {
                            Console.WriteLine("Warnung: Konnte keine Stunde finden für " + fach.Name + " in Klasse " + klasse);
                        }
                    }
                }
            }

            // Am Ende alles schön sortieren
            SortiereListe();
        }

        // --- HILFSMETHODEN FÜR DIE PRÜFUNGEN ---

        private bool IstKlasseBesetzt(string klasse, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Klasse == klasse && e.Tag == tag && e.PeriodeIndex == stunde)
                {
                    return true; // Ja, besetzt
                }
            }
            return false; // Nein, frei
        }

        private bool IstLehrerBesetzt(string name, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Lehrer == name && e.Tag == tag && e.PeriodeIndex == stunde)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IstRaumBesetzt(string nummer, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Raum == nummer && e.Tag == tag && e.PeriodeIndex == stunde)
                {
                    return true;
                }
            }
            return false;
        }

        // --- BEWERTUNGSLOGIK (Einfach gehalten) ---

        private int BerechnePunkte(string klasse, string tag, int stunde, Planungseinstellungen settings)
        {
            int punkte = 0;

            // Regel 1: Randstunden (erste oder letzte Stunde des Tagesrasters) vermeiden
            if (stunde == 0 || stunde == 6)
            {
                punkte = punkte + settings.StrafeRandstunde;
            }

            // Regel 2: Zwischenstunden vermeiden
            // Wir prüfen, ob die Klasse an diesem Tag schon andere Stunden hat.
            bool hatStundenAmTag = false;
            bool hatAnschluss = false;

            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Klasse == klasse && e.Tag == tag)
                {
                    hatStundenAmTag = true;

                    // Ist die Stunde direkt davor oder danach?
                    if (e.PeriodeIndex == stunde - 1 || e.PeriodeIndex == stunde + 1)
                    {
                        hatAnschluss = true;
                    }
                }
            }

            // Wenn schon Stunden da sind, aber keine direkt angrenzt -> Wahrscheinlich Lücke
            if (hatStundenAmTag == true && hatAnschluss == false)
            {
                punkte = punkte + settings.StrafeZwischenstunde;
            }

            return punkte;
        }

        // --- SORTIERUNG ---

        private void SortiereListe()
        {
            // Wir rufen die Sortier-Funktion auf und geben unsere eigene Vergleichs-Regel mit
            Eintraege.Sort(VergleicheEintraege);
        }

        private int VergleicheEintraege(StundenplanEintrag a, StundenplanEintrag b)
        {
            // 1. Zuerst nach KLASSE sortieren (Wichtig, damit 3A und 3B getrennt sind)
            if (a.Klasse != b.Klasse)
            {
                return string.Compare(a.Klasse, b.Klasse);
            }

            // 2. Dann nach TAG sortieren (Montag vor Dienstag...)
            int indexA = Array.IndexOf(Tage, a.Tag);
            int indexB = Array.IndexOf(Tage, b.Tag);

            if (indexA != indexB)
            {
                return indexA - indexB;
            }

            // 3. Dann nach ZEIT sortieren (08:00 vor 09:00...)
            return a.PeriodeIndex - b.PeriodeIndex;
        }

        // --- SPEICHERN ---

        public void SaveJson(string pfad)
        {
            string jsonText = JsonConvert.SerializeObject(Eintraege, Formatting.Indented);
            File.WriteAllText(pfad, jsonText);
            Console.WriteLine("Gespeichert in: " + pfad);
        }
    }
}