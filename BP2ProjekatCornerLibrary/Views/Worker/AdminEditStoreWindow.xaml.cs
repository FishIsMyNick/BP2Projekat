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
    /// Interaction logic for AdminEditStoreWindow.xaml
    /// </summary>
    public partial class AdminEditStoreWindow : Window
    {
		private FilijalaMake selectedFilijala;
        public AdminEditStoreWindow()
        {
            InitializeComponent();

			FillFilijaleList();
        }
		private void FillFilijaleList()
		{
			OtvoreneFilijale.SelectedItem = null;
			OtvoreneFilijale.Items.Clear();

			OtvFilView ofv = new OtvFilView("1", "Naziv filijale", "Domagoja Stankovica",  "133", "Novi Sad", "Srbija", new DateTime(1997, 1, 1));

			OtvoreneFilijale.Items.Add(ofv);
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

			Naziv.Text = selItem.OF_Naziv;
			Ulica.Text = selItem.OF_Ulica;
			Broj.Text = selItem.OF_Broj;
			Mesto.Text = selItem.OF_Grad;
			Drzava.Text = selItem.OF_Drzava;

			SelectView.Visibility = Visibility.Hidden;
			EditView.Visibility = Visibility.Visible;

			return;
		}

		private void btn_Confirm_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Update in DB

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
