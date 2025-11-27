namespace Stundenplan_Projekt
{
    internal class Raum
    {
        private string _nummer;
        private int _kapazitaet;

        public string Nummer { get => _nummer; set => _nummer = value; }
        public int Kapazitaet { get => _kapazitaet; set => _kapazitaet = value; }

        public Raum(string nummer, int kapazitaet)
        {
            _nummer = nummer;
            _kapazitaet = kapazitaet;
        }
    }
}
