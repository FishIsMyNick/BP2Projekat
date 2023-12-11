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
    /// Interaction logic for AdminEditLanguageWindow.xaml
    /// </summary>
    public partial class AdminEditLanguageWindow : Window, iDynamicListView
    {
        private Jezik selectedLang;
        public AdminEditLanguageWindow()
        {
            InitializeComponent();

            RefreshLists();
        }
        public void RefreshLists()
        {
            FillLanguageList();
        }

        private void FillLanguageList()
        {
            Jezici.Items.Clear();

            foreach (Jezik j in DBHelper.GetAllJeziks())
            {
                Jezici.Items.Add(j);
            }
        }

        #region Editing
        private void Jezici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedLang = (Jezik)Jezici.SelectedItem;

            SetEditLanguageFields();
        }
        private void SetEditLanguageFields()
        {
            if (selectedLang == null) return;

            tb_Edit_OZNJ.Text = selectedLang.OZNJ;
            tb_Edit_Naziv.Text = selectedLang.NazivJezika;
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

        #region BUTTONS
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateEditInputFields()) return;

            Jezik j = new Jezik(tb_Edit_OZNJ.Text.Trim(), tb_Edit_Naziv.Text.Trim());

            if (DBHelper.UpdateJezik(j))
            {
                MessageBox.Show("Uspešno ste izmenili jezik!");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri čuvanju jezika!");
            }
            ClearEditLanguageFields();
            RefreshLists();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditLanguageFields();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            Jezik j = new Jezik(tb_Edit_OZNJ.Text.Trim(), tb_Edit_Naziv.Text.Trim());

            if (DBHelper.DeleteJezik(j))
            {
                MessageBox.Show("Uspešno ste obrisali jezik");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri brisanju jezika!");
            }
            ClearEditLanguageFields();
            RefreshLists();
        }
        private bool ValidateEditInputFields()
        {
            return (Validator.Oznaka(tb_Edit_OZNJ.Text.Trim()) && Validator.Naziv(tb_Edit_Naziv.Text.Trim()));
        }
        private void ClearEditLanguageFields()
        {
            tb_Edit_OZNJ.Text = "";
            tb_Edit_Naziv.Text = "";
            selectedLang = null;
            Jezici.SelectedItem = null;
        }
        #endregion
        #endregion

        #region Add
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateAddInputFields()) return;

            Jezik j = new Jezik(tb_Add_OZNJ.Text.Trim() , tb_Add_Naziv.Text.Trim());

            if(DBHelper.CheckEntityExists<Jezik>(j))
            {
                MessageBox.Show("Jezik sa unetom oznakom već postoji!");
                return;
            }

            if (DBHelper.AddJezik(j))
            {
                MessageBox.Show("Uspešno ste dodali nov jezik!");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri dodavanju novog jezika!");
            }
            ClearAddLangFields();
            RefreshLists();
        }
        private bool ValidateAddInputFields()
        {
            return (Validator.Oznaka(tb_Add_OZNJ.Text.Trim()) && Validator.Naziv(tb_Add_Naziv.Text.Trim()));
        }
        private void ClearAddLangFields()
        {
            tb_Add_OZNJ.Text = "";
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
        private void tb_Add_OZNJ_TextChanged(object sender, TextChangedEventArgs e)
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
            if (tb_Add_Naziv.Text.Length > 0 && tb_Add_OZNJ.Text.Length > 0)
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
