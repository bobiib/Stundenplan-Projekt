using System.Collections.Generic;

namespace Stundenplan_Projekt
{
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

        public void AddFach(string fach)
        {
            if (Faecher.Contains(fach) == false)
            {
                Faecher.Add(fach);
            }
        }

        public void AddVerfuegbarkeit(string tag, string von, string bis)
        {
            Verfuegbarkeit v = new Verfuegbarkeit(tag, von, bis);
            Verfuegbarkeiten.Add(v);
        }

        public bool KannUnterrichten(string fach)
        {
            return Faecher.Contains(fach);
        }

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