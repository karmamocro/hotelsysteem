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
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int verdieping = 1;

        public Form1()
        {
            InitializeComponent();
            initView();

            demoRooms();
        }

        /// <summary>
        /// Sets the tab control nice
        /// </summary>
        private void initView()
        {
            #region tabSection
            tbcApplicationMain.Appearance = TabAppearance.FlatButtons;
            tbcApplicationMain.ItemSize = new Size(0, 1);
            tbcApplicationMain.SizeMode = TabSizeMode.Fixed;
            #endregion

            #region Loading Rooms and people
            Global.LaadAlleKamers();
            Global.LaadPersonen();
            #endregion
        }

        /// <summary>
        /// Populates the main listview with demorooms 
        /// </summary>
        void demoRooms()
        {
            int teller = 1;
            int groupNr = 0;
            foreach(Kamer kamer in Global.Kamers)
            {
                #region progressbar handeling
                // Berekent de progresbar value
                double tempX = (double)100 / 313 * kamer.Kamernummer;
                bunifuProgressBar1.Value = Convert.ToInt32(Math.Floor(tempX));
                #endregion  

                ListViewItem NewKamer = new ListViewItem(); // instantieert een nieuwe listview item
                NewKamer.Text = kamer.ToString(); // zet de text in een listview item

                NewKamer.Group = LvKamersHuidig.Groups[groupNr]; // geeft een kamer een verdieping

                teller++;
                if (teller > 52) { teller = 1; groupNr++; } // zorgt voor de grouping 

                if (Kamer.IsKamerGereserveerdVandaag(kamer))
                {
                    NewKamer.BackColor = Color.Red;
                    NewKamer.Tag = Kamer.KrijgKamerReserveringVanVandaagOpKamer(kamer);
                }
                LvKamersHuidig.Items.Add(NewKamer);
            }
        }

        #region Some Click Handelings
        private void BtnCloseMainFrm_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnReserveringpagina_Click(object sender, EventArgs e)
        {
            tbcApplicationMain.SelectedIndex = 1;
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            tbcApplicationMain.SelectedIndex = 0;
            LaatAlleVerdiepingenZien();
        }

        private void BtnAlleVerdiepingen_Click(object sender, EventArgs e)
        {
            if (BtnAlleVerdiepingen.Text == "Verdieping overzicht")
            {
                BtnAlleVerdiepingen.Text = "Volledig overzicht";
                LaatVerdiepingZien();
            }
            else
            {
                BtnAlleVerdiepingen.Text = "Verdieping overzicht";
                LaatAlleVerdiepingenZien();
            }
        }


        private void LaatAlleVerdiepingenZien()
        {
            LvKamersHuidig.Items.Clear();
            demoRooms();
            LblReservationCount.Text = Global.KamerReserveringen.Count.ToString();
        }

        private void LaatVerdiepingZien(int _Verdieping = 1)
        {
            //throw new NotImplementedException();
            LvKamersHuidig.Items.Clear();
            int teller = 1;
            int groupNr = 0;

            foreach (Kamer kamer in Global.Kamers)
            {
                #region progressbar handeling
                // Berekent de progresbar value
                double tempX = (double)100 / 313 * kamer.Kamernummer;
                bunifuProgressBar1.Value = Convert.ToInt32(Math.Floor(tempX));
                if (groupNr == _Verdieping - 1)
                {
                    
                    #endregion

                    ListViewItem NewKamer = new ListViewItem(); // instantieert een nieuwe listview item
                    NewKamer.Text = kamer.ToString(); // zet de text in een listview item

                    NewKamer.Group = LvKamersHuidig.Groups[groupNr]; // geeft een kamer een verdieping



                    if (Kamer.IsKamerGereserveerdVandaag(kamer))
                    {
                        NewKamer.BackColor = Color.Red;
                        NewKamer.Tag = Kamer.KrijgKamerReserveringVanVandaagOpKamer(kamer);
                    }
                    LvKamersHuidig.Items.Add(NewKamer);
                }

                teller++;
                if (teller > 52) { teller = 1; groupNr++; } // zorgt voor de grouping 
            }
        }

        private void BtnVolgendeVerdieping_Click(object sender, EventArgs e)
        {
            int _ToGo = calcLevel(direction.plus);
            LaatVerdiepingZien(_ToGo);
        }

        private void BtnVorigeVerdieping_Click(object sender, EventArgs e)
        {
            int _ToGo = calcLevel(direction.min);
            LaatVerdiepingZien(_ToGo);
        }

        int calcLevel(direction dr)
        {
            switch (dr)
            {
                case direction.plus:
                    verdieping++;
                    break;
                case direction.min:
                    verdieping--;
                    break;
                default:
                    break;
            }
            if (verdieping <= 0) { verdieping = 1; }
            if (verdieping >= 7) { verdieping = 6; }
            int _ToGo = verdieping;
            return _ToGo;
        }
        
        enum direction
        {
            plus,
            min
        }

        #endregion

        private void BtnPersonen_Click(object sender, EventArgs e)
        {
            PersonenForm pForm = new PersonenForm();
            if (pForm.ShowDialog() == DialogResult.OK)
            {
                foreach (Persoon Gast in Global.GeslecteerdeGasten)
                {
                    ListViewItem NieuweGast = new ListViewItem(new[] { Gast.Voornaam, Gast.Achternaam, Gast.Straatnaam, Gast.Huisnummer.ToString(), Gast.Woonplaats, Gast.Land, Gast.Leeftijd.ToString(), Gast.Geslacht.ToString() }) { Tag = Gast };
                    LvReserveringPersonen.Items.Add(NieuweGast);
                }

                
            }
        }

        private void BtnMaakReservering_Click(object sender, EventArgs e)
        {
            int kamerNummer = (int)NudKamerNummer.Value;
            DateTime datumFrom = McFromInOutDate.SelectionStart;
            DateTime datumTo = McFromInOutDate.SelectionEnd;
            if (NudKamerNummer.Value != 13 && NudKamerNummer.Value != 0 && LvReserveringPersonen.Items.Count != 0 && McFromInOutDate.SelectionStart.Date >= DateTime.Now.Date)
            {
                Kamer kamer = Kamer.KrijgKamerOpKamerNummer(Convert.ToInt32(NudKamerNummer.Value));

                ReserveringKamer reserveringkamer = new ReserveringKamer(kamer, Global.GeslecteerdeGasten, datumFrom, datumTo);

                if (Kamer.IsKamerVrij(kamer.Kamernummer, datumFrom, datumTo))
                {
                    Global.KamerReserveringen.Add(reserveringkamer);

                    // Reset invoervelden
                    Global.GeslecteerdeGasten.Clear();
                    LvReserveringPersonen.Items.Clear();
                    NudKamerNummer.Value = 0;
                    McFromInOutDate.SetSelectionRange(DateTime.Now.Date, DateTime.Now);

                    tbcApplicationMain.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Kamer is bezet op de gekozen datum");
                }
            }
            else
            {
                MessageBox.Show("Kamer 0 en 13 zijn ongeldig. \n" +
                    "Minimaal 1 persoon. \n" +
                    "Datum moet in de toekomst.");
            }
        }

        private void BtnReserveringOverzicht_Click(object sender, EventArgs e)
        {
            tbcApplicationMain.SelectedIndex = 2;
            LvReserveringView.Items.Clear();
            LvPersonenView.Items.Clear();
            McReserveringDatumInfo.SelectionStart = DateTime.Now;
            McReserveringDatumInfo.SelectionEnd = DateTime.Now;
            foreach (ReserveringKamer _kamer in Global.KamerReserveringen)
            {
                if (_kamer.DatumVan >= DateTime.Now || _kamer.DatumVan > DateTime.Now.AddDays(-1))
                {
                    ListViewItem lvnewres = new ListViewItem(new[] { _kamer.Kamer.Kamernummer.ToString(), _kamer.DatumAangemaakt.ToString(), _kamer.DatumVan.ToString(), _kamer.DatumTot.ToString() }) { Tag = _kamer };
                    if (_kamer.isCheckedIn()) { lvnewres.BackColor = Color.Orange; }
                    if (_kamer.isCheckedOut()) { lvnewres.BackColor = Color.OrangeRed; }
                    LvReserveringView.Items.Add(lvnewres);
                }
            }
        }



        private void BtnDashboardR_Click(object sender, EventArgs e)
        {
            LaatAlleVerdiepingenZien();
            tbcApplicationMain.SelectedIndex = 0;
        }

        private void LvReserveringView_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (LvReserveringView.SelectedItems.Count > 0)
            {
                ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                ReserveringKamer cItem = (ReserveringKamer) lvitemers.Tag;
                foreach (Persoon gast in cItem.Personen)
                {
                    ListViewItem lvitemPersoon = new ListViewItem(new[] { gast.Voornaam, gast.Achternaam, gast.Leeftijd.ToString(),gast.Woonplaats, gast.Straatnaam, gast.Huisnummer.ToString(),gast.Land,gast.Geslacht.ToString() });
                    LvPersonenView.Items.Add(lvitemPersoon);
                }

                McReserveringDatumInfo.SelectionStart = cItem.DatumVan;
                McReserveringDatumInfo.SelectionEnd = cItem.DatumTot;

                LblKamerNummer.Text = cItem.Kamer.Kamernummer.ToString();
               

                if (cItem.isCheckedOut())
                {
                    CbCheckOut.Checked = true;
                    CmsCheckOut.Enabled = false;
                    CmsCheckIn.Enabled = false;
                }
                else
                {
                    CbCheckOut.Checked = false;
                    CmsCheckOut.Enabled = true;
                    CmsCheckIn.Enabled = true;
                }

                if (cItem.isCheckedIn())
                {
                    CbCheckIn.Checked = true;
                    CmsCheckIn.Enabled = false;
                    CmsCheckOut.Enabled = true;
                    CbCheckOut.Checked = false;
                }
                else
                {
                    CbCheckIn.Checked = false;
                    CmsCheckIn.Enabled = true;
                    CmsCheckOut.Enabled = true;
                    CbCheckOut.Checked = false;
                }
            }
            
        }

        private void BtnNieuweReserveringR_Click(object sender, EventArgs e)
        {
            tbcApplicationMain.SelectedIndex = 1;
        }

        private void CmsDelete_Click(object sender, EventArgs e)
        {
            if (LvReserveringView.SelectedItems.Count > 0)
            {
                ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                ReserveringKamer cItem = (ReserveringKamer)lvitemers.Tag;

                Global.KamerReserveringen.Remove(cItem);
            }
        }

        private void CmsCheckIn_Click(object sender, EventArgs e)
        {
            if (LvReserveringView.SelectedItems.Count > 0)
            {
                ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                ReserveringKamer cItem = (ReserveringKamer)lvitemers.Tag;

                cItem.checkIn();
                CbCheckIn.Checked = true;
                lvitemers.BackColor = Color.Orange;
            }
        }

        private void CmsCheckOut_Click(object sender, EventArgs e)
        {
            if (LvReserveringView.SelectedItems.Count > 0)
            {
                ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                ReserveringKamer cItem = (ReserveringKamer)lvitemers.Tag;

                cItem.checkOut();
                CbCheckOut.Checked = true;
                lvitemers.BackColor = Color.OrangeRed;
            }
        }

        private void CbCheckIn_CheckedChanged(object sender, EventArgs e)
        {
            if (CbCheckIn.Checked)
            {
                if (LvReserveringView.SelectedItems.Count > 0)
                {
                    ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                    ReserveringKamer cItem = (ReserveringKamer)lvitemers.Tag;

                    cItem.checkIn();
                    CbCheckIn.Checked = true;
                    lvitemers.BackColor = Color.Orange;
                }
            }
        }

        private void CbCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            if (CbCheckOut.Checked)
            {
                if (LvReserveringView.SelectedItems.Count > 0)
                {
                    ListViewItem lvitemers = LvReserveringView.SelectedItems[0];
                    ReserveringKamer cItem = (ReserveringKamer)lvitemers.Tag;

                    cItem.checkOut();
                    CbCheckOut.Checked = true;
                    lvitemers.BackColor = Color.OrangeRed;
                }
            }
        }


    }
}
