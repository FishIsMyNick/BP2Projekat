using BP2ProjekatCornerLibrary.Helpers;
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
    public partial class AdminMainView : Window
    {
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
        public AdminMainView()
		{
			InitializeComponent();

			FillAllLists();
        }

		#region List population
		private void FillAllLists()
        {
			FillZapRadniciList();
			FillOtpRadniciList();
			FillOtvFilList();
			FillZatvFilList();
		}
        private void FillZapRadniciList()
		{
			ListaZaposlenihRadnika = new List<ZapRadView>();

			ZapRadView zrt = new ZapRadView("Ime coveka", "Prezime", "username", TipRadnika.Bibliotekar, new DateTime(1997, 1, 1));

			ListaZaposlenihRadnika.Add(zrt);

			foreach(ZapRadView zr in ListaZaposlenihRadnika)
			{
				ZaposleniRadnici.Items.Add(zr);
			}
		}
		private void FillOtpRadniciList()
		{
			ListaOtpustenihRadnika = new List<OtpRadView>();

			OtpRadView nzrt = new OtpRadView("Ime coveka", "Prezime", "username", TipRadnika.Bibliotekar, new DateTime(1997, 1, 1), new DateTime(2000, 1, 1)); 

			ListaOtpustenihRadnika.Add(nzrt);

			foreach (OtpRadView nzr in ListaOtpustenihRadnika)
			{
				NezaposleniRadnici.Items.Add(nzr);
			}
		}
		private void FillOtvFilList()
		{
			ListaOtvorenihFilijala = new List<OtvFilView>();

			OtvFilView oft = new OtvFilView("1", "Naziv filijale", "Domagoja Stankovica", "133", "Velegrad", "Državetina", new DateTime(1997, 1, 1));

			ListaOtvorenihFilijala.Add(oft);

			foreach (OtvFilView of in ListaOtvorenihFilijala)
			{
				OtvoreneFilijale.Items.Add(of);
			}
		}
		private void FillZatvFilList()
		{
			ListaZatvorenihFilijala = new List<ZatFilView>();

			ZatFilView zft = new ZatFilView("1", "Naziv filijale", "Ulice Ulicićevića", "123", "Velegrad", "Državetina", new DateTime(1997, 1, 1), new DateTime(2000, 1, 1));

			ListaZatvorenihFilijala.Add(zft);

			foreach (ZatFilView zf in ListaZatvorenihFilijala)
			{
				ZatvoreneFilijale.Items.Add(zf);
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
		private void ZapRadnici_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("zr click");
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
		private void OFil_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("zr click");
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
			Window addWorkerWindow = new AdminAddWorkerWindow();
			addWorkerWindow.ShowDialog();
		}

		private void btn_EditWorker_Click(object sender, RoutedEventArgs e)
		{
			Window editWorkerWindow = new AdminEditWorkerWindow();
			editWorkerWindow.ShowDialog();
		}

		private void btn_AddStore_Click(object sender, RoutedEventArgs e)
		{
			Window addStoreWindow = new AdminAddStoreWindow();
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


