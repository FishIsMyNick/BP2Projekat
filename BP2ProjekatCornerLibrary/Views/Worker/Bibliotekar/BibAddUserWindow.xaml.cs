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
	/// Interaction logic for BibAddUserWindow.xaml
	/// </summary>
	public partial class BibAddUserWindow : Window
	{
		iDbResult result;
		public BibAddUserWindow()
		{
			InitializeComponent();
		}

		private void Generisi(object sender, RoutedEventArgs e)
		{
			//tbBrKartice.Text = DBHelper.GetFirstFreeUserID().ToString();
		}

		private void Dodaj(object sender, RoutedEventArgs e)
		{
			if (tbBrKartice.Text != "" &&
				tbUsername.Text != "" &&
				pbPassword.Password != "" &&
				tbIme.Text != "" &&
				tbPrezime.Text != "" &&
				cbDan.SelectedItem != null &&
				cbMesec.SelectedItem != null &&
				cbGodina.SelectedItem != null &&
				cbTip.SelectedItem != null)
			{
				int brKartice;
				if (!int.TryParse(tbBrKartice.Text, out brKartice))
				{
					MessageBox.Show("Uneta vrednost za broj kartice nije validna!");
					return;
				}
				if (brKartice < 0)
				{
					MessageBox.Show("Uneta vrednost za broj kartice nije validna!");
					return;
				}
				string username = tbUsername.Text;
				string hashedPassword = HashHelper.ComputeSha256Hash(pbPassword.Password);
				string ime = tbIme.Text;
				string prezime = tbPrezime.Text;
				DateTime datRodj;
				try
				{
					datRodj = new DateTime(1950 + cbGodina.SelectedIndex, 1 + cbMesec.SelectedIndex, 1 + cbDan.SelectedIndex);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Uneti datum nije validan!");
					return;
				}
				int tip = cbTip.SelectedIndex;


				//iDbResult result = DBHelper.AddUser(new Clan(brKartice, username, hashedPassword, ime, prezime, datRodj, 0, tip));
				if (result == iDbResult.Success)
				{
					MessageBox.Show("Uspešno dodat član!");
					Close();
				}
				else if (result == iDbResult.Duplicate)
				{
					MessageBox.Show("Korisnik sa ovim brojem kartice ili korisničkim imenom već postoji!");
				}
				else
				{
					MessageBox.Show("Došlo je do neočekivane greške");
				}
			}

		}
	}
}
