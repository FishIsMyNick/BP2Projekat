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
	/// Interaction logic for BibAddBookWindow.xaml
	/// </summary>
	public partial class BibAddBookWindow : Window
	{
		iDbResult result;
		public BibAddBookWindow()
		{
			InitializeComponent();

			List<string> zanrovi = Enum.GetValues(typeof(iZanr)).Cast<iZanr>().ToList<iZanr>().ConvertAll(f => f.ToString());

			foreach (string zanr in zanrovi)
			{
				if (zanr == "CookBook")
				{
					cbZanr.Items.Add("Cook Book");
				}
				else if (zanr == "SciFi")
				{
					cbZanr.Items.Add("Sci Fi");
				}
				else if (zanr == "SelfHelp")
				{
					cbZanr.Items.Add("Self Help");
				}
				else
				{
					cbZanr.Items.Add((string)zanr);
				}
			}
			cbZanr.SelectedIndex = 0;

			for (int i = 1900; i <= 2023; i++)
			{
				cbGodina.Items.Add(i.ToString());
			}
			cbGodina.SelectedIndex = 0;
		}

		private void btnGenerisi_Click(object sender, RoutedEventArgs e)
		{
			//tbID.Text = DBHelper.GetFirstFreeBookID().ToString();
		}

		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{
			if (tbID.Text != "" &&
				tbNaziv.Text != "" &&
				tbAutor.Text != "" &&
				tbIzdKuca.Text != "" &&
				tbJezik.Text != "" &&
				cbDan.SelectedItem != null &&
				cbMesec.SelectedItem != null &&
				cbGodina.SelectedItem != null)
			{

				int id;
				if (!int.TryParse(tbID.Text, out id))
				{
					MessageBox.Show("Uneta vrednost za ID knjige nije validna!");
					return;
				}
				if (id < 0)
				{
					MessageBox.Show("Uneta vrednost za ID knjige nije validna!");
					return;
				}
				string naziv = tbNaziv.Text;
				string autor = tbAutor.Text;
				string jezik = tbJezik.Text;
				string izdKuca = tbIzdKuca.Text;
				string zanr = cbZanr.SelectedValue.ToString();
				bool ogr = (bool)chbOgraniceno.IsChecked;
				DateTime datIzd;

				try
				{
					datIzd = new DateTime(1900 + cbGodina.SelectedIndex, 1 + cbMesec.SelectedIndex, 1 + cbDan.SelectedIndex);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Uneti datum nije validan!");
					return;
				}


				//iDbResult result = DBHelper.AddBook(new Knjiga(id, naziv, autor, izdKuca, datIzd, zanr, jezik, ogr));
				if (result == iDbResult.Success)
				{
					MessageBox.Show("Uspešno dodata knjiga!");
					Close();
				}
				else if (result == iDbResult.Duplicate)
				{
					MessageBox.Show("Knjiga sa ovim ID-om ili kombinacijom naziva, autora i jezika već postoji!");
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
