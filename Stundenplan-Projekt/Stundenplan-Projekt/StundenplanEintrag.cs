namespace Stundenplan_Projekt
{
    internal class StundenplanEintrag
    {
        public string Klasse { get; set; }
        public string Fach { get; set; }
        public string Lehrer { get; set; }
        public string Raum { get; set; }
        public string Tag { get; set; }
        public string ZeitVon { get; set; }
        public string ZeitBis { get; set; }
        public int PeriodeIndex { get; set; }
    }
}
