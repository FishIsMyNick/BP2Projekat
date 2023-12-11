using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BP2ProjekatCornerLibrary.Views.Worker
{
    /// <summary>
    /// Interaction logic for AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : Window, iDynamicListView
    {
		private Admin _currentUser;
		public static AdminMainView Instance { get; set; }
        public string Zap_Ime { get; set; }
        public string Zap_Prezime { get; set; }
        public string Zap_Username { get; set; }
        public string Zap_Tip { get; set; }

        //TODO: treba da bude lista radnika
        public List<ZapRadView> ListaZaposlenihRadnika { get; set; }
        public List<OtpRadView> ListaOtpustenihRadnika { get; set; }
		public List<OtvFilView> ListaOtvorenihFilijala { get; set; }
		public List<ZatFilView> ListaZatvorenihFilijala { get; set; }

		public int RadnikTableImeWidth = 300;
        public AdminMainView(Admin admin)
		{
			if (Instance == null)
				Instance = this;

			_currentUser = admin;

			InitializeComponent();

			FillAllLists();
        }

        #region List population
        public void RefreshLists()
        {
			FillAllLists();
        }
        private void FillAllLists()
        {
			FillZapRadniciList();
			FillOtpRadniciList();
			FillOtvFilList();
			FillZatvFilList();
			//Knjiga k = DBHelper.GetBook(1);
		}
        private void FillZapRadniciList()
		{
			ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllEmployedWorkers();
			foreach(Radnik radnik in radniks)
			{
				KorisnickiNalog nalog = new KorisnickiNalog();
				if(radnik.GetType() == typeof(Bibliotekar))
					nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
				else if(radnik.GetType() == typeof(Kurir))
					nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
				ZaposleniRadnici.Items.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
			}
		}
		private void FillOtpRadniciList()
		{
			NezaposleniRadnici.Items.Clear();

			List<Radnik> radniks = DBHelper.GetAllUnemployedWorkers();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
				NezaposleniRadnici.Items.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
		private void FillOtvFilList()
		{
			OtvoreneFilijale.Items.Clear();

			List<Biblikutak> lokali = DBHelper.GetOpenLokals();
			foreach(Biblikutak biblikutak in lokali)
			{
				Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
				Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
				OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
			}
		}
		private void FillZatvFilList()
        {
			ZatvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                ZatvoreneFilijale.Items.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
		}
		#endregion

		#region Zaposleni radnici
		private void btn_ZapRadnik_Ime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Username_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Tip_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		#region Nezaposleni radnici
		private void btn_NezapRadnik_Ime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_NezapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_NezapRadnik_Username_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_NezapRadnik_Tip_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_NezapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
		{

		}
		private void NezapRadnici_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("zr click");
		}
		#endregion

		#region Otvorene filijale
		private void btn_OFil_Naziv_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_OFil_Adresa_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_OFil_Grad_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_OFil_Drzava_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_OFil_DatOtv_Click(object sender, RoutedEventArgs e)
		{

		}
        #endregion

        #region LIST INTERACTIONS
		private void DeselectFromAllLists()
		{
			OtvoreneFilijale.SelectedItem = null;
			ZatvoreneFilijale.SelectedItem = null;
			ZaposleniRadnici.SelectedItem = null;
			NezaposleniRadnici.SelectedItem = null;
		}
        private void OtvoreneFilijale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			OtvFilView ofv = ((ListView)sender).SelectedItem as OtvFilView;
			if (ofv == null) return;
			Window editFilWindow = new AdminEditStoreWindow(caller:this, selectedID:ofv.IDBK);
			editFilWindow.ShowDialog();
		}
        private void OtvoreneFilijale_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        private void OtvoreneFilijale_MouseDown(object sender, MouseButtonEventArgs e)
        {
			DeselectFromAllLists();
        }

		private void ZaposleniRadnici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ZapRadView zrv = ((ListView)sender).SelectedItem as ZapRadView;
			if(zrv == null) return;
			Window editWorkerWindow = new AdminEditWorkerWindow(caller: this, selectedID: zrv.IDRadnik, tip: EnumsHelper.GetTipRadnika(zrv.Tip));
			editWorkerWindow.ShowDialog();
		}
        private void ZaposleniRadnici_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void ZaposleniRadnici_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }
        private void NezaposleniRadnici_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }

        private void ZatvoreneFilijale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }
		#endregion

		private void btn_Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		#region Zatvorene filijale
		private void btn_ZFil_Naziv_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZFil_Adresa_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZFil_Grad_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZFil_Drzava_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZFil_PerOtv_Click(object sender, RoutedEventArgs e)
		{

		}
		private void ZFil_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("zr click");
		}
		#endregion

		#region Buttons
		private void btn_AddWorker_Click(object sender, RoutedEventArgs e)
		{
			Window addWorkerWindow = new AdminAddWorkerWindow(this, _currentUser.IDRadnik);
			addWorkerWindow.ShowDialog();
		}

		private void btn_EditWorker_Click(object sender, RoutedEventArgs e)
		{
			Window editWorkerWindow = new AdminEditWorkerWindow();
			editWorkerWindow.ShowDialog();
		}

		private void btn_AddStore_Click(object sender, RoutedEventArgs e)
		{
			Window addStoreWindow = new AdminAddStoreWindow(this);
			addStoreWindow.ShowDialog();
		}

		private void btn_EditStore_Click(object sender, RoutedEventArgs e)
		{
			Window editStoreWindow = new AdminEditStoreWindow();
			editStoreWindow.ShowDialog();
		}

		private void btn_EditLanguage_Click(object sender, RoutedEventArgs e)
		{
			Window editLanguageWindow = new AdminEditLanguageWindow();
			editLanguageWindow.ShowDialog();
		}

		private void btn_EditZanr_Click(object sender, RoutedEventArgs e)
        {
            Window editZanrWindow = new AdminEditZanrWindow();
            editZanrWindow.ShowDialog();
        }

		private void btn_EditFormat_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_EditPeriod_Click(object sender, RoutedEventArgs e)
		{

		}

        #endregion

    }
}


