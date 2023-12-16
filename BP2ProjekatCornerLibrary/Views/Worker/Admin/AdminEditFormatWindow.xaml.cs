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

namespace BP2ProjekatCornerLibrary.Views.Worker.Admin
{
    /// <summary>
    /// Interaction logic for AdminEditFormatWindow.xaml
    /// </summary>
    public partial class AdminEditFormatWindow : Window, iDynamicListView
    {
        private Format _selectedFormat;
        private bool _blockEvents;
        public AdminEditFormatWindow()
        {
            InitializeComponent();

            RefreshLists();
        }
        public void RefreshLists()
        {
            _blockEvents = true;
            FillFormatList();
            _blockEvents = false;
        }
        private void FillFormatList()
        {
            Formati.Items.Clear();
            foreach (Format f in DBHelper.GetAllFormats())
            {
                Formati.Items.Add(f);
            }
        }

        private void ClearInputFields()
        {
            ClearEditField();
            ClearAddField();
        }

        #region EDIT
        private void SetEditButtons(bool on)
        {
            btn_Edit_Delete.IsEnabled = _selectedFormat != null;
            btn_Edit_Confirm.IsEnabled = on;
        }
        private bool CheckEditField()
        {
            return tb_Edit_Format.Text != _selectedFormat.NazivFormata && tb_Edit_Format.Text != "";
        }
        private void SetEditField()
        {
            tb_Edit_Format.Text = _selectedFormat.NazivFormata;
        }
        private void ClearEditField()
        {
            tb_Edit_Format.Text = "";
        }
        private void tb_Edit_Format_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_blockEvents) return;
            SetEditButtons(CheckEditField());
        }
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Edit_Format.Text == "")
            {
                MessageBox.Show("Polje sa nazivom formata mora biti popunjeno!");
                return;
            }

            if (!DBHelper.UpdateFormat(_selectedFormat.NazivFormata, new Format(tb_Edit_Format.Text)))
            {
                MessageBox.Show("Došlo je do greške pri izmeni formata!");
                return;
            }
            MessageBox.Show("Uspešno ste izmenili format!");
            ClearEditField();
            RefreshLists();
        }
        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditField();
            _blockEvents = true;
            Formati.SelectedItem = null;
            _blockEvents = false;
        }
        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteFormat(_selectedFormat))
            {
                MessageBox.Show("Došlo je do greške pri brisanju formata!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali format!");
            ClearEditField() ; RefreshLists();
        }
        #endregion

        #region ADD
        private void SetAddButtons(bool on)
        {
            btn_Add_Confirm.IsEnabled = on;
        }
        private bool CheckAddField()
        {
            return tb_Add_Format.Text != "";
        }
        private void ClearAddField()
        {
            tb_Add_Format.Text = "";
        }
        private void tb_Add_Format_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_blockEvents) return;
            SetAddButtons(CheckAddField());
        }
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.AddFormat(new Format(tb_Add_Format.Text)))
            {
                MessageBox.Show("Došlo je do greške pri dodavanju novog formata!");
                return;
            }
            ClearAddField();
            MessageBox.Show("Uspešno ste dodali novi format!");
            RefreshLists();
        }
        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAddField();
        }
        #endregion

        #region SORTING
        private void btn_Format_Sort_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region LISTE
        private void Formati_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;

            _selectedFormat = ((ListView)sender).SelectedItem as Format;
            SetEditField();
        }
        #endregion

    }
}
