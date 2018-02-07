using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    public class Persoon
    {
        
        public string Voornaam { get; private set; }
        public string Achternaam { get; private set; }
        public string Straatnaam { get; private set; }
        public int Huisnummer { get; private set; }
        public string Toevoeging { get; private set; }
        public string Woonplaats { get; private set; }
        public string Land { get; private set; }
        public int Leeftijd { get; private set; }
        public int Id { get; private set; }
        public GeslachtType Geslacht { get; set; }
        public enum GeslachtType
        {
            Man,
            Vrouw
        }

        public Persoon(string _voornaam, string _achternaam, string _straatnaam, int _huisnummer, string _toevoeging, string _woonplaats, string _land, int _leeftijd, GeslachtType _geslacht)
        {
            Voornaam = _voornaam;
            Achternaam = _achternaam;
            Straatnaam = _straatnaam;
            Leeftijd = _leeftijd;
            Huisnummer = _huisnummer;
            Toevoeging = _toevoeging;
            Woonplaats = _woonplaats;
            Land = _land;
            Geslacht = _geslacht;
        }


    }
}
