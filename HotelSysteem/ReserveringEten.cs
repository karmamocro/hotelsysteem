using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSysteem
{
    public class ReserveringEten
    {
        public Kamer Kamer { get; private set; }
        private List<Persoon> _Personen = new List<Persoon>();
        public List<Persoon> Personen { get { return _Personen; } }
        public SoortReservering TypeReservering { get; private set; }
        public DateTime DatumCheckIn { get; private set; }
        public DateTime DatumCheckOut { get; private set; }
        public DateTime DatumAangemaakt { get; private set; }
        public enum SoortReservering { Ontbijt, Diner }


        public ReserveringEten(Kamer _kamer, List<Persoon> _personen, SoortReservering _typeReservering)
        {
            Kamer = _kamer;
            _Personen.AddRange(Personen);
            TypeReservering = _typeReservering;
            DatumAangemaakt = DateTime.Now;
        }
    }
}
