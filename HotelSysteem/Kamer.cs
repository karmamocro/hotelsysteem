using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    public class Kamer
    {
        public  int Kamernummer { get; private set; }
        public static int maxPersonen { get { return 4; }  }

        public Kamer(int _kamernummer)
        {
            Kamernummer = _kamernummer;
        }

        public static Kamer KrijgKamerOpKamerNummer(int _KamerNummer)
        {
            return (Global.Kamers.Where(kamer => kamer.Kamernummer == _KamerNummer)).First();
        }

        public static ReserveringKamer KrijgKamerReserveringVanVandaagOpKamer(Kamer _Kamer)
        {
            return (Global.KamerReserveringen.Where(_KamerReservering => _KamerReservering.Kamer.Kamernummer == _Kamer.Kamernummer && (DateTime.Now.Date >= _KamerReservering.DatumVan.Date && DateTime.Now.Date <= _KamerReservering.DatumTot.Date))).First();
        }

        public static bool IsKamerVrij(int Kamernummer, DateTime DatumVan, DateTime DatumTot)
        {
            return !(Global.KamerReserveringen.Any(_KamerReservering => _KamerReservering.Kamer.Kamernummer == Kamernummer && !(DatumTot.Date <= _KamerReservering.DatumVan.Date) && !(DatumVan.Date >= _KamerReservering.DatumTot.Date)));
        }

        public static bool IsKamerGereserveerdVandaag(Kamer _Kamer)
        {
            return Global.KamerReserveringen.Any(_KamerReservering => _KamerReservering.Kamer.Kamernummer == _Kamer.Kamernummer && (DateTime.Now.Date >= _KamerReservering.DatumVan.Date && DateTime.Now.Date <= _KamerReservering.DatumTot.Date) && _KamerReservering.DatumCheckIn.ToString() != "1-1-0001 00:00:00" && _KamerReservering.DatumCheckOut.ToString() == "1-1-0001 00:00:00");
        }

        public override string ToString()
        {
            return "Kamer " + Kamernummer;
        }
    }
}
