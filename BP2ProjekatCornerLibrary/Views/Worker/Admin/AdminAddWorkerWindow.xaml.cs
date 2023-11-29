using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
	/// Interaction logic for AdminAddWorkerWindow.xaml
	/// </summary>
	public partial class AdminAddWorkerWindow : Window
	{
		public AdminAddWorkerWindow()
		{
			InitializeComponent();
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void btn_Confirm_Click(object sender, RoutedEventArgs e)
		{
			string ime = Ime.Text;
			string prezime = Prezime.Text;
			string username = Username.Text;
			string pass = HashHelper.ComputeSha256Hash(Pass.Password);
			string ulica = Ulica.Text;
			string broj = Broj.Text;
			string mesto = Mesto.Text;
			string drzava = Drzava.Text;
			string tip = TipRad.SelectedItem.ToString();

			if (ime == "" || prezime == "" || username == "" || Pass.Password.Length == 0 || ulica == "" || broj == "" || mesto == "" || drzava == "")
			{
				MessageBox.Show("Sva polja moraju biti popunjena!");
				return;
			}

			//TODO: Add to DB
			MessageBox.Show("Uspešno ste dodali novog radnika!");
			Close();
		}
	}
}
