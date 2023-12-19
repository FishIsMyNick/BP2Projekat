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
	/// Interaction logic for BibliotekarMainView.xaml
	/// </summary>
	public partial class BibliotekarMainView : Window
	{
		//private MockDB mockDB;
		private int _currentUser;
		public BibliotekarMainView(Bibliotekar bibliotekar)
		{
			InitializeComponent();

			_currentUser = bibliotekar.IDRadnik;
			lbUsername.Content = DBHelper.GetBibNalog(_currentUser).KorisnickoIme;

			//lbUsername.Content = bibliotekar.KorisnickoIme;
            //lbUsername.Content = "placeholder";
		}
				
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
			Close();
        }

        #region BUTTONS
        private void btn_Add_Book_Click(object sender, RoutedEventArgs e)
        {
            Window BookWindow = new BibBookWindow(_currentUser);
            BookWindow.Show();
        }

        private void btn_Edit_Book_Click(object sender, RoutedEventArgs e)
        {
            Window BookWindow = new BibBookWindow(_currentUser);
            BookWindow.Show();
        }

        private void btn_Add_News_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_News_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Magazin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Magazin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Izd_Nov_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Izd_Nov_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Izd_Mag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Izd_Mag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Autor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Autor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Izd_Kuca_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Remove_SuL_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Izd_Kuca_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
