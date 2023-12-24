using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Worker;
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

namespace BP2ProjekatCornerLibrary.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static bool _testing = false;
        //private MockDB Mock;
        public Login()
        {

            DBHelper.InitializeConnection();


            //Mock = new MockDB();
            InitializeComponent();

            if(_testing)
            {
                Window window = new BibBookWindow(1);
                window.Show();
                Close();
                return;
            }
            TryLogin();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }
        private void TryLogin()
        {

            string user = tbUsername.Text;
            string hashedPassword = HashHelper.ComputeSha256Hash(pbPasswordHidden.Password);

            if (false)
            {
                if (tbUsername.Text == "" || pbPasswordHidden.Password == "")
                {
                    MessageBox.Show("Sva polja moraju biti popunjena!");
                    return;
                }
            }
            else
            {
                //test
                user = "bib";
                hashedPassword = HashHelper.ComputeSha256Hash("pass");
            }

            string message;


            Zaposleni r = DBHelper.TryLoginUser(user, hashedPassword);

            if (r == null)
            {
                MessageBox.Show("Pogrešna kombinacija korisničkog imena i šifre!");
                return;
            }

            Type workerType = r.GetType();
            if (workerType == typeof(Bibliotekar))
            {
                BibliotekarMainView bibMainView = new BibliotekarMainView((Bibliotekar)r);
                bibMainView.Show();
                Close();
            }
            else if (workerType == typeof(Admin))
            {
                AdminMainView bibMainView = new AdminMainView((Admin)r);
                bibMainView.Show();
                Close();
            }
            else if (workerType == typeof(Kurir))
            {
                MessageBox.Show("Trenutno ne postoji podrška za kurira!");
            }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TryLogin();
            }
        }


        #region Username
        private void tbUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUsername.Text == "Username")
            {
                tbUsername.Text = "";
                tbUsername.Foreground = Brushes.Black;
            }
        }

        private void tbUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbUsername.Text == "")
            {
                tbUsername.Text = "Username";
                tbUsername.Foreground = Brushes.Gray;
            }
        }
        #endregion



        #region Password
        private void tbPasswordUnmasked_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPasswordUnmasked.Visibility = Visibility.Hidden;
            pbPasswordHidden.Visibility = Visibility.Visible;
            pbPasswordHidden.Focus();
        }
        private void pbPasswordHidden_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbPasswordHidden.Password == "")
            {
                tbPasswordUnmasked.Foreground = Brushes.Gray;
                tbPasswordUnmasked.Text = "Password";
                tbPasswordUnmasked.Visibility = Visibility.Visible;
                pbPasswordHidden.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        //#region Show password
        //private void revealModeCheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //	tbPasswordUnmasked.Text = pbPasswordHidden.Password;
        //	tbPasswordUnmasked.Foreground = Brushes.Black;
        //	tbPasswordUnmasked.Visibility = Visibility.Visible;
        //	pbPasswordHidden.Visibility = Visibility.Hidden;
        //	tbPasswordUnmasked.Focus();
        //}

        //private void revealModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //	tbPasswordUnmasked.Visibility = Visibility.Hidden;
        //	pbPasswordHidden.Visibility = Visibility.Visible;
        //	pbPasswordHidden.Focus();
        //}
        //#endregion

    }
}
