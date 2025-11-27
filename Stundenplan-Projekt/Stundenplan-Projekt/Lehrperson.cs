using System.Collections.Generic;

namespace Stundenplan_Projekt
{
    /// <summary>
    /// Stellt einen Lehrer dar mit Name, Kürzel und Fächern.
    /// </summary>
    internal class Lehrperson
    {
        public string Name { get; set; }
        public string Kuerzel { get; set; }

        // Einfache Listen ohne spezielle "private" Regeln
        public List<string> Faecher = new List<string>();
        public List<Verfuegbarkeit> Verfuegbarkeiten = new List<Verfuegbarkeit>();

        public Lehrperson(string name, string kuerzel)
        {
            Name = name;
            Kuerzel = kuerzel;
        }

        /// <summary>
        /// Fügt dem Lehrer ein Fach hinzu, das er unterrichten kann.
        /// </summary>
        public void AddFach(string fach)
        {
            if (Faecher.Contains(fach) == false)
            {
                Faecher.Add(fach);
            }
        }

        /// <summary>
        /// Fügt eine Zeit hinzu, an der der Lehrer anwesend ist.
        /// </summary>
        public void AddVerfuegbarkeit(string tag, string von, string bis)
        {
            Verfuegbarkeit v = new Verfuegbarkeit(tag, von, bis);
            Verfuegbarkeiten.Add(v);
        }

        /// <summary>
        /// Prüft, ob der Lehrer dieses Fach unterrichten darf.
        /// </summary>
        public bool KannUnterrichten(string fach)
        {
            return Faecher.Contains(fach);
        }

        /// <summary>
        /// Prüft, ob der Lehrer an diesem Tag und Uhrzeit Zeit hat.
        /// </summary>
        public bool IstVerfuegbar(string tag, string zeitVon, string zeitBis)
        {
            if (Verfuegbarkeiten.Count == 0)
            {
                return true;
            }

            foreach (Verfuegbarkeit v in Verfuegbarkeiten)
            {
                if (v.IstInZeitraum(tag, zeitVon, zeitBis))
                {
                    return true;
                }
            }

            return false;
        }
    }
}