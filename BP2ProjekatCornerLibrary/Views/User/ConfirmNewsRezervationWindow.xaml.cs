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
	/// Interaction logic for ConfirmNewsRezervationWindow.xaml
	/// </summary>
	public partial class ConfirmNewsRezervationWindow : Window
	{
		public string Novine { get; set; } = "";
		public double Cena { get; set; } = 0;

		public string Adresa { get; set; } = "";
		public string Grad { get; set; } = "";
		public string Drzava { get; set; } = "";

		private int NovineID;
		private int LokalID;
		private UserMainWindow mainWindow;

		public ConfirmNewsRezervationWindow(string novine, string adresa, string grad, string drzava, double cena, int novineID, int lokalID, UserMainWindow refCaller)
		{
			Novine = novine;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;
			Cena = cena;

			NovineID = novineID;
			LokalID = lokalID;
			mainWindow = refCaller;

			InitializeComponent();

			lbBookName.Content = novine;
			lbAdresa.Content = adresa;
			lbGrad.Content = grad;
			lbDrzava.Content = drzava;
			lbCena.Content = cena;
		}

		private void btnConfirm_Click(object sender, RoutedEventArgs e)
		{
			mainWindow.ConfirmNewsRezervation(NovineID, LokalID);
			Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
