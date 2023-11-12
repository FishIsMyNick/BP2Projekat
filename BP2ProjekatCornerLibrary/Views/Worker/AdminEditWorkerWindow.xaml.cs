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
    /// Interaction logic for AdminEditWorkerWindow.xaml
    /// </summary>
    public partial class AdminEditWorkerWindow : Window
    {
		private RadnikView selectedWorker;
        public AdminEditWorkerWindow()
        {
            InitializeComponent();

			FillWorkerList();
        }

		private void FillWorkerList()
		{
			ZaposleniRadnici.Items.Clear();

			RadnikView rv = new RadnikView("Vladimir", "Dragomirovic", "CovekOdIstine", "11.11.1111.", "11.11.1111.", "Bibliotekar", "Vladimira Jevrejevica", "133", "Novi Sad", "Srbija");

			ZaposleniRadnici.Items.Add(rv);
		}

		#region Buttons
		private void btn_Confirm_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Save to DB
			MessageBox.Show("Uspešno ste izmenili radnika!");
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
			MessageBox.Show("Uspešno ste obrisali radnika!");
			Close();
		}
		#endregion
		private void ZaposleniRadnici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			selectedWorker = ZaposleniRadnici.SelectedItem as RadnikView;
			Ime.Text = selectedWorker.Ime;
			Prezime.Text = selectedWorker.Prezime;
			Username.Text = selectedWorker.Username;
			DatRodj.Text = selectedWorker.DatRodj;
			switch (selectedWorker.Tip)
			{
				case "Bibliotekar":
					TipRad.SelectedIndex = 0; break;
				case "Kurir":
					TipRad.SelectedIndex = 1; break;
				default: break;
			}
			Ulica.Text = selectedWorker.Adresa.Ulica;
			Broj.Text = selectedWorker.Adresa.Broj; 
			Mesto.Text = selectedWorker.Adresa.Mesto; 
			Drzava.Text = selectedWorker.Adresa.Drzava;


			SelectView.Visibility = Visibility.Hidden;
			EditView.Visibility = Visibility.Visible;
		}

		#region Sorting
		private void btn_Ime_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_Prezime_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_Username_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_Tip_Sort_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btn_DatZap_Sort_Click(object sender, RoutedEventArgs e)
		{

		}
#endregion
	}
}
