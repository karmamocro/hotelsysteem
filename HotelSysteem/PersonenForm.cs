using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSysteem
{
    public partial class PersonenForm : Form
    {
        List<Persoon> CheckedPersonen = new List<Persoon>();
        bool NewCheckedPersoon = false;
        public PersonenForm()
        {
           
            InitializeComponent();
            foreach (Persoon Gast in Global.Gasten)
            {
                ListViewItem Persoon = new ListViewItem(new[] { "", Gast.Voornaam, Gast.Achternaam, Gast.Woonplaats }) { Tag = Gast};
                LvPersonen.Items.Add(Persoon);
            }
        }

        private void BtnKlaar_Click(object sender, EventArgs e)
        {
            foreach (Persoon persoon in CheckedPersonen)
            {
                // Global.GeselecteerdeGasten worden toegevoegd in de desbetreffende ListView in het MainForm
                Global.GeslecteerdeGasten.Add(persoon);
            }

            // De geselecteerde personen worden alleen toegevoegd als het DialogResult OK is
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnVoegPersoonToe_Click(object sender, EventArgs e)
        {
            // Store inputs for visibility
            string Voornaam = TbVoornaam.Text;
            string Achternaam = TbAchternaam.Text;
            string Straatnaam = TbStraatNaam.Text;
            int Huisnummer = Convert.ToInt32(NudHuisnummer.Value);
            string Toevoeging = TbToevoeging.Text;
            string Woonplaats = TbWoonplaats.Text;
            string Land = TbLand.Text;
            int Leeftijd = Convert.ToInt32(NudLeeftijd.Value);
            int Geslacht = CbGeslacht.SelectedIndex;

            // Valideer invoervelden
            if (Voornaam == "" || Achternaam == "" || Straatnaam == "" || Huisnummer == 0 || Woonplaats == "" || Land == "" || Leeftijd == 0 || Geslacht == -1) { return; }

            Persoon NieuwPersoon = new Persoon(Voornaam, Achternaam, Straatnaam, Huisnummer, Toevoeging, Woonplaats, Land, Leeftijd, (Persoon.GeslachtType)Geslacht);

            Global.Gasten.Add(NieuwPersoon);

            ListViewItem NieuweItem = new ListViewItem(new[] { "", NieuwPersoon.Voornaam, NieuwPersoon.Achternaam, NieuwPersoon.Woonplaats })
            {
                Tag = NieuwPersoon,
            };
            LvPersonen.Items.Add(NieuweItem);
        }

        private void LvPersonen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvPersonen.SelectedItems.Count > 0)
            {
                if(TbcPersonen.SelectedIndex != 1) { TbcPersonen.SelectedIndex = 1; }
                Persoon persoon = (Persoon)LvPersonen.SelectedItems[0].Tag;
                TbInfoVoornaam.Text = persoon.Voornaam;
                TbInfoAchternaam.Text = persoon.Achternaam;
                TbInfoStraatnaam.Text = persoon.Straatnaam;
                NudInfoHuisnummer.Value = persoon.Huisnummer;
                TbInfoToevoeging.Text = persoon.Toevoeging;
                TbInfoWoonplaats.Text = persoon.Woonplaats;
                TbInfoLand.Text = persoon.Land;
                NudInfoLeeftijd.Value = persoon.Leeftijd;
                CbbInfoGeslacht.SelectedIndex = (int)persoon.Geslacht;
            }
        }

        private void LvPersonen_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // Als een persoon checked is en via de code wordt toegevoegd wordt deze functie 2x aangeroepen
            if (NewCheckedPersoon == true)
            {
                NewCheckedPersoon = false;
                return;
            }

            if (CheckedPersonen.Count == Kamer.maxPersonen && e.Item.Checked == true && !CheckedPersonen.Contains((Persoon)e.Item.Tag))
            {
                e.Item.Checked = false;
                return;
            }

            Persoon _persoon = (Persoon)e.Item.Tag;
            if (e.Item.Checked == true)
            {
                if (!CheckedPersonen.Contains(_persoon))
                {
                    CheckedPersonen.Add(_persoon);
                }
            }
            else
            {
                if (CheckedPersonen.Contains(_persoon))
                {
                    CheckedPersonen.Remove(_persoon);
                }
            }
        }
    }
}
