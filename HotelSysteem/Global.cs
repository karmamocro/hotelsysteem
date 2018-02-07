using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunifu.Framework.UI;
using WindowsFormsControlLibrary1;

namespace HotelSysteem
{
    public class Global
    {
        public static List<Persoon> Gasten = new List<Persoon>();
        public static List<Persoon> GeslecteerdeGasten = new List<Persoon>();

        public static List<Kamer> Kamers = new List<Kamer>();
        public static List<ReserveringKamer> KamerReserveringen = new List<ReserveringKamer>();

        public static void LaadPersonen()
        {
            Persoon x = new Persoon("Hansen", "van Meester", "Boskoop", 27, "", "Asten", "Nederland", 20, (Persoon.GeslachtType)0);
            Persoon y = new Persoon("Ries", "van Geffen", "Boskoop", 27, "", "Asten", "Nederland", 20, (Persoon.GeslachtType)0);
            Persoon z = new Persoon("Mona", "Dawaf", "Boskoop", 27, "", "Asten", "Nederland", 20, (Persoon.GeslachtType)1);
            Persoon i = new Persoon("Oussama", "El Hajoui", "Boskoop", 27, "", "Asten", "Nederland", 20, (Persoon.GeslachtType)0);
            Persoon o = new Persoon("Erick", "Sheizen", "Boskoop", 27, "", "Asten", "Nederland", 20,0);
            Global.Gasten.Add(x);
            Global.Gasten.Add(y);
            Global.Gasten.Add(z);
            Global.Gasten.Add(i);
            Global.Gasten.Add(o);
        }
        public static void LaadAlleKamers()
        {
            for (int i = 1; i < 314; i++)
            {
                if(i == 13) { continue; }
                Kamer kamer = new Kamer(i);
                Kamers.Add(kamer);
            }
        }




    }
}
