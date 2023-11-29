using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
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
    /// Interaction logic for AdminEditStoreWindow.xaml
    /// </summary>
    public partial class AdminEditStoreWindow : Window
    {
		private FilijalaMake selectedFilijala;
		private List<Mesto> mesta;
		private List<Drzava> drzave;

		private string GetNaziv { get => cbNaziv.Text; }
        private string  GetUlica { get => cbUlica.Text; }
		private string GetBroj { get => cbBroj.Text; }
		private int GetMesto { get; }
		private string GetDrzava { get; }


        public AdminEditStoreWindow(int selectedID = -1)
        {
            InitializeComponent();

			mesta = DBHelper.GetAllMesta();
			drzave = DBHelper.GetAllDrzave();

			cbMesto.Items.Clear();
			cbMesto.Items.Add("Dodaj novo mesto");
			foreach(Mesto m in mesta)
				cbMesto.Items.Add(m.NazivMesta.ToString());

			cbDrzava.Items.Clear();
			cbDrzava.Items.Add("Dodaj novu državu");
			foreach(Drzava dr in drzave)
				cbDrzava.Items.Add(dr.NazivDrzave.ToString());

			FillFilijaleList();

			if(selectedID > 0)
			{

			}
        }
		private void FillFilijaleList()
		{
            List<Biblikutak> lokali = DBHelper.GetOpenLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.Ulica, biblikutak.Broj, m.NazivMesta, d.NazivDrzave, biblikutak.DatOtv));
            }
        }
		#region Sorting
		private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
		{

        }

		private void btn_Adresa_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_Mesto_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_Drzava_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_DatOtv_Sort_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		private void OtvoreneFilijale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var selItem = OtvoreneFilijale.SelectedItem as OtvFilView;

			cbNaziv.Text = selItem.OF_Naziv;
			cbUlica.Text = selItem.OF_Ulica;
			cbBroj.Text = selItem.OF_Broj;
			cbMesto.Text = selItem.OF_Grad;
			cbDrzava.Text = selItem.OF_Drzava;

			SelectView.Visibility = Visibility.Hidden;
			EditView.Visibility = Visibility.Visible;

			return;
		}

		private void btn_Confirm_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Update in DB
			if(!DBHelper.CheckIfLocationExists(GetUlica, GetBroj, GetMesto, GetDrzava))

			MessageBox.Show("Uspešno ste izmenili podatke o filijali!");
			Close();
		}

		private void btn_Cancel_Click(object sender, RoutedEventArgs e)
		{
			SelectView.Visibility = Visibility.Visible;
			EditView.Visibility = Visibility.Hidden;
		}

		private void btn_Delete_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Delete from DB

			MessageBox.Show("Uspešno ste obrisali filijalu.");
			Close();
		}
	}
}
