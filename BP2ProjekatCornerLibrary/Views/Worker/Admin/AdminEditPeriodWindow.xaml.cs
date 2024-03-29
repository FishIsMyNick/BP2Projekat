﻿using BP2ProjekatCornerLibrary.Helpers;
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
    public partial class AdminEditPeriodWindow : Window, iDynamicListView, iSortedListView
    {
        private Periodicnost _selectedPeriod;
        public AdminEditPeriodWindow()
        {
            InitializeComponent();
            Arrows = new List<Image> { img_Period_Sort, img_Ucestalost_Sort };

            RefreshLists();
        }
        public void RefreshLists()
        {
            FillPeriodList();
        }
        private void FillPeriodList()
        {
            DisableAllArrows();
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
            if (_selectedPeriod != null)
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
            if (!ValidateEditFields()) return;

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
            if (!DBHelper.DeletePeriod(_selectedPeriod))
            {
                MessageBox.Show("Došlo je do greške pri brisanju perioda!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali period!");
            RefreshLists();
            ClearEditFields();
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
            if (!ValidateAddFields()) return;

            if (!DBHelper.AddPeriod(new Periodicnost(tb_Add_Period.Text, int.Parse(tb_Add_Ucestalost.Text))))
            {
                MessageBox.Show("Došlo je do greške pri dodavanju perioda!");
                return;
            }
            MessageBox.Show("Uspešno ste dodali nov period!");
            RefreshLists();
            ClearAddFields();
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAddFields();
        }

        #endregion

        #region TEXT CHANGED
        private void tb_Add_Period_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetAddButtons(CheckAddFields());
        }

        private void tb_Add_Ucestalost_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetAddButtons(CheckAddFields());
        }
        private bool CheckAddFields()
        {
            return tb_Add_Period.Text != "" && tb_Add_Ucestalost.Text != null;
        }
        private void SetAddButtons(bool on)
        {
            btn_Add_Confirm.IsEnabled = on;
        }
        private void ClearAddFields()
        {
            tb_Add_Period.Text = "";
            tb_Add_Ucestalost.Text = "";
        }
        #endregion
        private bool ValidateAddFields()
        {
            return Validator.Naziv(tb_Add_Period.Text) && Validator.Ucestalost(tb_Add_Ucestalost.Text);
        }
        #endregion


        #region SORTING
        private List<Periodicnost> GetAllPeriodsFromList()
        {
            List<Periodicnost> ret = new List<Periodicnost>();
            foreach (var j in Periodi.Items) ret.Add(j as Periodicnost);
            return ret;
        }
        private void SortPeriodString(string param, bool ascending)
        {
            List<Periodicnost> toSort = GetAllPeriodsFromList();
            Periodi.Items.Clear();
            foreach (Periodicnost j in Sorter.SortText<Periodicnost>(toSort, param, ascending)) Periodi.Items.Add(j);
        }
        private void SortPeriodInt(string param, bool ascending)
        {
            List<Periodicnost> toSort = GetAllPeriodsFromList();
            Periodi.Items.Clear();
            foreach (Periodicnost j in Sorter.SortInt<Periodicnost>(toSort, param, ascending)) Periodi.Items.Add(j);
        }
        private bool s_uce_asc = false;
        private void btn_Ucestalost_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_uce_asc = !s_uce_asc;
            SetArrow(img_Ucestalost_Sort, s_uce_asc);
            SortPeriodInt("Ucestalost", s_uce_asc);
        }
        private bool s_per_asc = false;

        private void btn_Period_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_per_asc = !s_per_asc;
           SetArrow(img_Period_Sort, s_per_asc);
            SortPeriodString("PeriodIzd", s_per_asc);
        }

        public List<Image> Arrows { get; set; }

        public void DisableAllArrows()
        {
            ArrowHelper.DisableAllArrows(Arrows);
        }

        public void SetArrow(Image arrow, bool ascending)
        {
            DisableAllArrows();
            ArrowHelper.SetArrow(arrow, ascending);
        }

        #endregion

    }
}
