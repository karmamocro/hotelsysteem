using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    public class ReserveringKamer
    {
        public Kamer Kamer { get; private set; }
        private List<Persoon> _Personen = new List<Persoon>();
        public List<Persoon> Personen { get { return _Personen; } }
        public DateTime DatumCheckIn { get; private set; }
        public DateTime DatumCheckOut { get; private set; }
        public DateTime DatumAangemaakt { get; private set; }
        public DateTime DatumVan { get; private set; }
        public DateTime DatumTot { get; private set; }

        public ReserveringKamer(Kamer _kamer, List<Persoon> _personen, DateTime _datumVan, DateTime _datumTot)
        {
            Kamer = _kamer;
            _Personen.AddRange(_personen);
            DatumVan = _datumVan;
            DatumTot = _datumTot;
            DatumAangemaakt = DateTime.Now;
        }

        public void checkIn()
        {
            DatumCheckIn = DateTime.Now;
        }

        public void checkOut()
        {
            DatumCheckOut = DateTime.Now;
        }

        public bool isCheckedIn()
        {
            return (DatumCheckIn.ToString() != "1-1-0001 00:00:00") ? true : false;
        }
        public bool isCheckedOut()
        {
            return (DatumCheckOut.ToString() != "1-1-0001 00:00:00") ? true : false;
        }

    }
}
