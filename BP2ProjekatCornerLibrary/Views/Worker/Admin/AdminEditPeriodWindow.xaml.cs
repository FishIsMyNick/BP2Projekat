using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AdminEditPeriodWindow.xaml
    /// </summary>
    public partial class AdminEditPeriodWindow : Window, iDynamicListView
    {
        private Periodicnost _selectedPeriod;
        public AdminEditPeriodWindow()
        {
            InitializeComponent();

            RefreshLists();
        }
        public void RefreshLists()
        {
            FillPeriodList();
        }
        private void FillPeriodList()
        {
            Periodi.Items.Clear();
            foreach (Periodicnost p in DBHelper.GetAllPeriod())
            {
                Periodi.Items.Add(p);
            }
        }
        #region LIST
        private void Periodi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPeriod = ((ListView)sender).SelectedItem as Periodicnost;

            SetEditFields();
        }
        #endregion


        #region EDIT

        private void SetEditFields()
        {
            if(_selectedPeriod != null)
            {
                tb_Edit_Period.Text = _selectedPeriod.PeriodIzd;
                tb_Edit_Ucestalost.Text = _selectedPeriod.Ucestalost.ToString();
            }
        }
        private void ClearEditFields()
        {
            tb_Edit_Period.Text = "";
            tb_Edit_Ucestalost.Text = "";
        }

        #region BUTTONS
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateEditFields()) return;

            if (!DBHelper.UpdatePeriodIzd(_selectedPeriod.PeriodIzd, new Periodicnost(tb_Edit_Period.Text, int.Parse(tb_Edit_Ucestalost.Text))))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju perioda!");
                return;
            }
            else
            {
                MessageBox.Show("Uspešno ste izmenili period!");
            }

            RefreshLists();
            ClearEditFields();
            Periodi.SelectedItem = null;
            _selectedPeriod = null;
            return;
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditFields();
            Periodi.SelectedItem = null;
            _selectedPeriod = null;
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region TEXT CHANGED
        private void tb_Edit_Period_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetEditButtons(CheckEditFields());
        }

        private void tb_Edit_Ucestalost_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetEditButtons(CheckEditFields());
        }
        private bool CheckEditFields()
        {
            return tb_Edit_Period.Text.Length > 0 && tb_Edit_Ucestalost.Text.Length > 0;
        }
        private void SetEditButtons(bool on)
        {
            btn_Edit_Confirm.IsEnabled = on;
            btn_Edit_Delete.IsEnabled = on;
        }
        #endregion

        private bool ValidateEditFields()
        {
            return Validator.Naziv(tb_Edit_Period.Text) && Validator.Ucestalost(tb_Edit_Ucestalost.Text);
        }

        #endregion

        #region ADD

        #region BUTTONS
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TEXT CHANGED
        private void tb_Add_Period_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tb_Add_Ucestalost_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #endregion

        #endregion


        #region SORTING
        private void btn_Ucestalost_Sort_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Period_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

    }
}
