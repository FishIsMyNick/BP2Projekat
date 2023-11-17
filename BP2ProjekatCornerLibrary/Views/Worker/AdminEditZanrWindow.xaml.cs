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
    /// Interaction logic for AdminEditZanrWindow.xaml
    /// </summary>
    public partial class AdminEditZanrWindow : Window
    {
        public List<ZanrView> ListaZanrova { get; set; }
        private ZanrView selectedZanr;
        public AdminEditZanrWindow()
        {
            InitializeComponent();

            FillZanrList();
        }

        private void FillZanrList()
        {
            Zanrovi.Items.Clear();

            ZanrView jv = new ZanrView("SRB", "Srpski");

            Zanrovi.Items.Add(jv);
        }

        #region Editing
        private void Zanrovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedZanr = (ZanrView)Zanrovi.SelectedItem;

            SetEditZanreFields();
        }
        private void tb_Edit_Naziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Edit_Naziv.Text.Length > 0)
            {
                EnableEditBtns();
            }
            else
            {
                DisableEditBtns();
            }
        }

        private void EnableEditBtns()
        {
            btn_Edit_Confirm.IsEnabled = true;
            btn_Edit_Delete.IsEnabled = true;
        }
        private void DisableEditBtns()
        {
            btn_Edit_Confirm.IsEnabled = false;
            btn_Edit_Delete.IsEnabled = false;
        }
        private void SetEditZanreFields()
        {
            if (selectedZanr == null) return;

            tb_Edit_OZNZ.Text = selectedZanr.OZNZ;
            tb_Edit_Naziv.Text = selectedZanr.Naziv;
        }
        private void ClearEditZanrFields()
        {
            tb_Edit_OZNZ.Text = "";
            tb_Edit_Naziv.Text = "";
            selectedZanr = null;
            Zanrovi.SelectedItem = null;
        }
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Update in DB

            MessageBox.Show("Uspešno ste izmenili žanr!");
            ClearEditZanrFields();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditZanrFields();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Delete from DB

            MessageBox.Show("Uspešno ste obrisali žanr");
            ClearEditZanrFields();
        }
        #endregion

        #region Add
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            string oznz = tb_Add_OZNZ.Text;
            string naziv = tb_Add_Naziv.Text;

            if (oznz == "" || naziv == "")
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return;
            }

            ZanrView toAdd = new ZanrView(oznz, naziv);
            //TODO: Add to DB

            MessageBox.Show("Uspešno ste dodali nov žanr!");
            ClearAddZanrFields();
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAddZanrFields();
        }
        private void ClearAddZanrFields()
        {
            tb_Add_OZNZ.Text = "";
            tb_Add_Naziv.Text = "";
        }

        private void SetAddBtns()
        {
            if (CheckAddFields())
            {
                EnableAddBtns();
            }
            else
            {
                DisableAddBtns();
            }
        }
        private void tb_Add_OZNZ_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetAddBtns();
        }
        private void EnableAddBtns()
        {
            btn_Add_Confirm.IsEnabled = true;
        }
        private void DisableAddBtns()
        {
            btn_Add_Confirm.IsEnabled = false;
        }

        private void tb_Add_Naziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetAddBtns();
        }
        private bool CheckAddFields()
        {
            if (tb_Add_Naziv.Text.Length > 0 && tb_Add_OZNZ.Text.Length > 0)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region Sorting
        private void btn_OZN_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
