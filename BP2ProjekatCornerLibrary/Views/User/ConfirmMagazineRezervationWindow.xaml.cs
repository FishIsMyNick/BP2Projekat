using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BP2ProjekatCornerLibrary.Views.User
{
	/// <summary>
	/// Interaction logic for ConfirmMagazineRezervationWindow.xaml
	/// </summary>
	public partial class ConfirmMagazineRezervationWindow : Window
	{
		public string Magazin { get; set; } = "";
		public double Cena { get; set; } = 0;

		public string Adresa { get; set; } = "";
		public string Grad { get; set; } = "";
		public string Drzava { get; set; } = "";

		private int MagazinID;
		private int LokalID;
		private UserMainWindow mainWindow;

		public ConfirmMagazineRezervationWindow(string magazin, string adresa, string grad, string drzava, double cena, int magazinID, int lokalID, UserMainWindow refCaller)
		{
			Magazin = magazin;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;
			Cena = cena;

			MagazinID = magazinID;
			LokalID = lokalID;
			mainWindow = refCaller;

			InitializeComponent();

			lbBookName.Content = magazin;
			lbAdresa.Content = adresa;
			lbGrad.Content = grad;
			lbDrzava.Content = drzava;
			lbCena.Content = cena;
		}

		private void btnConfirm_Click(object sender, RoutedEventArgs e)
		{
			mainWindow.ConfirmNewsRezervation(MagazinID, LokalID);
			Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
