using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Diese Klasse erstellt den Stundenplan und prüft die Regeln.
    /// </summary>
    internal class Stundenplan
    {
        public List<StundenplanEintrag> Eintraege = new List<StundenplanEintrag>();

        private string[] Tage = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag" };
        private string[] StartZeiten = { "08:00", "08:50", "09:50", "10:40", "11:30", "13:15", "14:05" };
        private string[] EndZeiten = { "08:45", "09:35", "10:35", "11:25", "12:15", "14:00", "14:50" };

        /// <summary>
        /// Versucht für alle Fächer passende Plätze zu finden.
        /// </summary>
        public void GenerierePlan(
            List<string> klassen,
            Dictionary<string, List<Fach>> klassenFaecher,
            List<Lehrperson> lehrer,
            List<Raum> raeume,
            Planungseinstellungen settings)
        {
            Eintraege.Clear();
            Random wuerfel = new Random();

            foreach (string klasse in klassen)
            {
                List<Fach> faecherListe = klassenFaecher[klasse];

                foreach (Fach fach in faecherListe)
                {
                    // Wir müssen so viele Termine finden, wie das Fach Stunden hat
                    for (int i = 0; i < fach.StundenProWoche; i++)
                    {
                        StundenplanEintrag besterEintrag = null;
                        int wenigsteStrafpunkte = 10000;

                        // Wir probieren 50 zufällige Möglichkeiten aus
                        for (int versuch = 0; versuch < 50; versuch++)
                        {
                            int tagIndex = wuerfel.Next(0, 5);
                            int stundenIndex = wuerfel.Next(0, 7);

                            string tag = Tage[tagIndex];
                            string start = StartZeiten[stundenIndex];
                            string ende = EndZeiten[stundenIndex];

                            // Ist die Klasse da schon im Unterricht?
                            if (IstKlasseBesetzt(klasse, tag, stundenIndex) == true)
                            {
                                continue;
                            }

                            // Lehrer suchen
                            Lehrperson gewaehlterLehrer = null;
                            foreach (Lehrperson l in lehrer)
                            {
                                if (l.KannUnterrichten(fach.Name) == true &&
                                    l.IstVerfuegbar(tag, start, ende) == true &&
                                    IstLehrerBesetzt(l.Name, tag, stundenIndex) == false)
                                {
                                    gewaehlterLehrer = l;
                                    break;
                                }
                            }

                            if (gewaehlterLehrer == null) continue;

                            // Raum suchen
                            Raum gewaehlterRaum = null;

                            // Wichtig: Wir mischen die Räume, sonst wird immer Raum 1 genommen
                            var zufaelligeRaeume = raeume.OrderBy(x => wuerfel.Next()).ToList();

                            foreach (Raum r in zufaelligeRaeume)
                            {
                                // Wir prüfen ob der Raum frei ist und ob er groß genug ist (mind. 20)
                                if (IstRaumBesetzt(r.Nummer, tag, stundenIndex) == false && r.Kapazitaet >= 20)
                                {
                                    gewaehlterRaum = r;
                                    break;
                                }
                            }

                            if (gewaehlterRaum == null) continue;

                            // Punkte berechnen
                            int strafPunkte = BerechnePunkte(klasse, tag, stundenIndex, settings);

                            // Wenn das der beste Versuch bisher war, merken wir uns den
                            if (strafPunkte < wenigsteStrafpunkte)
                            {
                                wenigsteStrafpunkte = strafPunkte;

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

                        if (besterEintrag != null)
                        {
                            Eintraege.Add(besterEintrag);
                        }
                        else
                        {
                            Console.WriteLine("Warnung: Kein Platz gefunden für " + fach.Name + " (" + klasse + ")");
                        }
                    }
                }
            }

            SortiereListe();
        }

        private bool IstKlasseBesetzt(string klasse, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Klasse == klasse && e.Tag == tag && e.PeriodeIndex == stunde) return true;
            }
            return false;
        }

        private bool IstLehrerBesetzt(string name, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Lehrer == name && e.Tag == tag && e.PeriodeIndex == stunde) return true;
            }
            return false;
        }

        private bool IstRaumBesetzt(string nummer, string tag, int stunde)
        {
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Raum == nummer && e.Tag == tag && e.PeriodeIndex == stunde) return true;
            }
            return false;
        }

        /// <summary>
        /// Berechnet Strafpunkte für ungünstige Termine (Randstunden, Lücken, Raummangel).
        /// </summary>
        private int BerechnePunkte(string klasse, string tag, int stunde, Planungseinstellungen settings)
        {
            int punkte = 0;

            // Randstunden bestrafen
            if (stunde == 0 || stunde == 6)
            {
                punkte = punkte + settings.StrafeRandstunde;
            }

            // Zwischenstunden bestrafen
            bool hatStundenAmTag = false;
            bool hatAnschluss = false;

            foreach (StundenplanEintrag e in Eintraege)
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

            if (hatStundenAmTag == true && hatAnschluss == false)
            {
                punkte = punkte + settings.StrafeZwischenstunde;
            }

            // Ressourcen checken: Wie viele Räume werden parallel genutzt?
            int anzahlParallel = 0;
            foreach (StundenplanEintrag e in Eintraege)
            {
                if (e.Tag == tag && e.PeriodeIndex == stunde)
                {
                    anzahlParallel++;
                }
            }
            punkte = punkte + (anzahlParallel * settings.StrafeRessourcen);

            return punkte;
        }

        private void SortiereListe()
        {
            Eintraege.Sort(VergleicheEintraege);
        }

        private int VergleicheEintraege(StundenplanEintrag a, StundenplanEintrag b)
        {
            if (a.Klasse != b.Klasse) return string.Compare(a.Klasse, b.Klasse);

            int indexA = Array.IndexOf(Tage, a.Tag);
            int indexB = Array.IndexOf(Tage, b.Tag);

            if (indexA != indexB) return indexA - indexB;

            return a.PeriodeIndex - b.PeriodeIndex;
        }

        /// <summary>
        /// Speichert das Ergebnis in einer JSON Datei.
        /// </summary>
        public void SaveJson(string pfad)
        {
            string jsonText = JsonConvert.SerializeObject(Eintraege, Formatting.Indented);
            File.WriteAllText(pfad, jsonText);
            Console.WriteLine("Gespeichert in: " + pfad);
        }
    }
}