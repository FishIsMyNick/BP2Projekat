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
    /// Interaction logic for AddMestoWindow.xaml
    /// </summary>
    public partial class AddMestoWindow : Window
    {
        private iDynamicListView _caller;
        public AddMestoWindow(iDynamicListView caller = null)
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
            string posBrStr = tbPosBr.Text;
            string naziv = tbNaziv.Text;

            if (!(posBrStr.Length > 0 && naziv.Length > 0))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return;
            }

            int posBr;
            if (!int.TryParse(posBrStr, out posBr))
            {
                MessageBox.Show("Poštanski broj nije unet u dobrom formatu!");
                return;
            }
            if(posBr < 0)
            {
                MessageBox.Show("Poštanski broj nije unet u dobrom formatu!");
                return;
            }
            if (naziv.Any(char.IsDigit))
            {
                MessageBox.Show("Naziv mesta ne sme sardžati brojeve!");
                return;
            }
            if(!naziv.All(char.IsLetter))
            {
                MessageBox.Show("Naziv mesta ne sme sardžati specijalne karaktere!");
                return;
            }

            Mesto toAdd = new Mesto(posBr, naziv);
            if (DBHelper.CheckEntityExists<Mesto>(toAdd))
            {
                MessageBox.Show("Mesto sa ovim poštanskim brojem već postoji!");
                return;
            }

            if (!DBHelper.AddItemWithSQL<Mesto>(toAdd))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                return;
            }

            _caller?.RefreshLists();
            Close();
        }
    }
}
