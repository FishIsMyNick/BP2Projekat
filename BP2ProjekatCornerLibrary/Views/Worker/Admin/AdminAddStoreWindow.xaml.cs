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
	/// Interaction logic for AdminAddStoreWindow.xaml
	/// </summary>
	public partial class AdminAddStoreWindow : Window
	{
		public AdminAddStoreWindow()
		{
			InitializeComponent();
		}

		#region Buttons
		private void btn_Confirm_Click(object sender, RoutedEventArgs e)
		{
			string naziv = Naziv.Text;
			string ulica = Ulica.Text;
			string broj = Broj.Text;
			string mesto = Mesto.Text;
			string drzava = Drzava.Text;

			if (naziv == "" || ulica == "" || broj == "" || mesto == "" || drzava == "")
			{
				MessageBox.Show("Sva polja moraju biti popunjena!");
				return;
			}

			FilijalaMake filijalaView = new FilijalaMake(naziv, DateTime.Now);
			AddressView av = new AddressView(ulica, broj, mesto, drzava);

			//TODO: Add to DB

			MessageBox.Show("Uspešno ste dodali novu filijalu!");
			Close();
        }

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		#endregion
	}
}

public class FilijalaMake
{
	public string Naziv { get; set; }
	public string DatOtv { get; set; }
	public string DatZat { get; set; }

    public FilijalaMake(string naziv, DateTime datOtv)
    {
        Naziv = naziv;
		DatOtv = DateConverter.ToString(datOtv);
		DatZat = "";
	}
	public FilijalaMake(string naziv, DateTime datOtv, DateTime datZat)
	{
		Naziv = naziv;
		DatOtv = DateConverter.ToString(datOtv);
		DatZat = DateConverter.ToString(datZat);
	}
}
