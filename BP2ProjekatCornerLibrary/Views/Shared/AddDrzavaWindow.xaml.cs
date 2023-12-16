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

namespace BP2ProjekatCornerLibrary.Views.Shared
{
    /// <summary>
    /// Interaction logic for AddDrzavaWindow.xaml
    /// </summary>
    public partial class AddDrzavaWindow : Window
    {
        private iDynamicListView _caller;
        public AddDrzavaWindow(iDynamicListView caller)
        {
            _caller = caller;   
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            string oznd = tbOZND.Text;
            string naziv = tbNaziv.Text;

            if (!(oznd.Length > 0 && naziv.Length > 0))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return;
            }

            if (oznd.Any(char.IsDigit))
            {
                MessageBox.Show("Oznaka države ne sme sadržati brojeve!");
                return;
            }
            if (naziv.Any(char.IsDigit))
            {
                MessageBox.Show("Naziv države ne sme sadržati brojeve!");
                return;
            }
            if (!oznd.All(char.IsLetter))
            {
                MessageBox.Show("Oznaka države ne sme sadržati specijalne znakove!");
                return;
            }
            if (!naziv.All(char.IsLetter))
            {
                MessageBox.Show("Naziv države ne sme sadržati specijalne znakove!");
                return;
            }

            if (oznd.Length > 4)
            {
                MessageBox.Show("Oznaka države može biti najviše 4 karaktera!");
                return;
            }

            Drzava toAdd = new Drzava(oznd, naziv);
            if (DBHelper.CheckEntityExists<Drzava>(toAdd))
            {
                MessageBox.Show("Država sa ovom oznakom već postoji!");
                return;
            }

            if (!DBHelper.AddItemWithSQL<Drzava>(toAdd))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                return;
            }

            _caller?.RefreshLists();
            Close();
        }
    }
}
