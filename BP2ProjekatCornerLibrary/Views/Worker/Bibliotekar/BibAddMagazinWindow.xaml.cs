using BP2ProjekatCornerLibrary.Helpers;
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

namespace BP2ProjekatCornerLibrary.Views.Worker
{
	/// <summary>
	/// Interaction logic for BibAddMagazinWindow.xaml
	/// </summary>
	public partial class BibAddMagazinWindow : Window
	{
		iDbResult result;
		public BibAddMagazinWindow()
		{
			InitializeComponent();
		}
		private void btnGenerisi_Click(object sender, RoutedEventArgs e)
		{
			//tbID.Text = DBHelper.GetFirstFreeMagazinID().ToString();
		}

		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{

			if (tbID.Text != "" &&
				tbNaziv.Text != "" &&
				tbCena.Text != "")
			{

				int id;
				if (!int.TryParse(tbID.Text, out id))
				{
					MessageBox.Show("Uneta vrednost za ID magazina nije validna!");
					return;
				}
				if (id < 0)
				{
					MessageBox.Show("Uneta vrednost za ID magazina nije validna!");
					return;
				}
				int cena;
				if (!int.TryParse(tbCena.Text, out cena))
				{
					MessageBox.Show("Uneta vrednost za cenu nije validna!");
					return;
				}
				if (cena < 0)
				{
					MessageBox.Show("Uneta vrednost za cenu nije validna!");
					return;
				}
				string naziv = tbNaziv.Text;
				string period = cbPeriod.SelectedValue.ToString();
				if (period == "Mesečni")
				{
					period = "Mesecni";
				}
				else if (period == "Godišnji")
				{
					period = "Godisnji";
				}


				//iDbResult result = DBHelper.AddMagazin(new Magazin(id, naziv, period, cena));
				if (result == iDbResult.Success)
				{
					MessageBox.Show("Uspešno dodat magazin!");
					Close();
				}
				else if (result == iDbResult.Duplicate)
				{
					MessageBox.Show("Magazin sa ovim ID-om ili nazivom već postoje!");
				}
				else
				{
					MessageBox.Show("Došlo je do neočekivane greške");
				}
			}
			else
			{
				MessageBox.Show("Sva polja moraju biti popunjena!");
			}
		}
	}
}
