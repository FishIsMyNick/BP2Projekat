using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar;
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
        public BibliotekarMainView(Models.Bibliotekar bibliotekar)
        {
            InitializeComponent();

            _currentUser = bibliotekar.IDRadnik;
            DBHelper._currentUserID = _currentUser;
            lbUsername.Content = DBHelper.GetBibNalog(_currentUser).KorisnickoIme;

            //lbUsername.Content = bibliotekar.KorisnickoIme;
            //lbUsername.Content = "placeholder";
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region BUTTONS
        private void btn_Book_Click(object sender, RoutedEventArgs e)
        {
            Window BookWindow = new BibBookWindow(_currentUser);
            BookWindow.ShowDialog();
        }

        private void btn_News_Click(object sender, RoutedEventArgs e)
        {
            Window newsWindow = new BibNewsWindow(_currentUser);
            newsWindow.ShowDialog();
        }

        private void btn_Autor_Click(object sender, RoutedEventArgs e)
        {
            Window autorWindow = new BibAutorWindow(_currentUser);
            autorWindow.ShowDialog();
        }

        private void btn_Magazine_Click(object sender, RoutedEventArgs e)
        {
            Window magWindow = new BibMagazineWindow(_currentUser);
            magWindow.ShowDialog();
        }

        private void btn_IzdKuca_Click(object sender, RoutedEventArgs e)
        {
            Window ikWindow = new BibIzdKucaWindow(_currentUser); 
            ikWindow.ShowDialog();
        }

        private void btn_Izd_Nov_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_SuL_Click(object sender, RoutedEventArgs e)
        {
            Window sulWindow = new BibSuLWindow(_currentUser);
            sulWindow.ShowDialog();
        }

        private void btn_Izd_Mag_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

    }
}
