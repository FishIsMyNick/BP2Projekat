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
    public partial class AdminEditZanrWindow : Window, iDynamicListView, iSortedListView
    {
        private Zanr selectedZanr;
        public AdminEditZanrWindow()
        {
            InitializeComponent();

            RefreshLists();
        }
        public void RefreshLists()
        {
            FillZanrList();
        }
        private void FillZanrList()
        {
            Zanrovi.Items.Clear();

            foreach(Zanr z in DBHelper.GetAllZanrs())
            {
                Zanrovi.Items.Add(z);
            }
        }

        #region Editing
        private void Zanrovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedZanr = (Zanr)Zanrovi.SelectedItem;

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
            tb_Edit_Naziv.Text = selectedZanr.NazivZanra;
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
            if (!ValidateEditInputFields()) return;

            Zanr z = new Zanr(tb_Edit_OZNZ.Text.Trim(), tb_Edit_Naziv.Text.Trim());

            if (DBHelper.UpdateZanr(z))
            {
                MessageBox.Show("Uspešno ste izmenili žanr!");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri čuvanju žanra!");
            }
            ClearEditZanrFields();
            RefreshLists();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearEditZanrFields();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            Zanr z = new Zanr(tb_Edit_OZNZ.Text.Trim(), tb_Edit_Naziv.Text.Trim());

            if (DBHelper.DeleteZanr(z))
            {
                MessageBox.Show("Uspešno ste obrisali žanr");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri brisanju žanra!");
            }
            ClearEditZanrFields();
            RefreshLists();
        }
        private bool ValidateEditInputFields()
        {
            return Validator.Oznaka(tb_Edit_OZNZ.Text.Trim()) && Validator.Naziv(tb_Edit_Naziv.Text.Trim());
        }
        #endregion

        #region Add
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateAddInputFields()) return;

            Zanr zanr = new Zanr(tb_Add_OZNZ.Text.Trim(), tb_Add_Naziv.Text.Trim());

            if (DBHelper.CheckEntityExists<Zanr>(zanr))
            {
                MessageBox.Show("Žanr sa unetom oznakom već postoji!");
                return;
            }

            if (DBHelper.AddZanr(zanr))
            {
                MessageBox.Show("Uspešno ste dodali nov žanr!");
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri dodavanju novog žanra!");
            }
            ClearAddZanrFields();
            RefreshLists();
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearAddZanrFields();
        }
        private bool ValidateAddInputFields()
        {
            return (Validator.Oznaka(tb_Add_OZNZ.Text.Trim()) && Validator.Naziv(tb_Add_Naziv.Text.Trim()));
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
        private List<Zanr> GetAllZanroviFromList()
        {
            List<Zanr> ret = new List<Zanr>();
            foreach (var j in Zanrovi.Items) ret.Add(j as Zanr);
            return ret;
        }
        private void SortZanrString(string param, bool ascending)
        {
            List<Zanr> toSort = GetAllZanroviFromList();
            Zanrovi.Items.Clear();
            foreach (Zanr j in Sorter.SortText<Zanr>(toSort, param, ascending)) Zanrovi.Items.Add(j);
        }


        private bool s_ozn_asc = false;
        private void btn_OZN_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_ozn_asc = !s_ozn_asc;
            SetArrow(img_OZN_Sort, s_ozn_asc);
            SortZanrString("OZNZ", s_ozn_asc);
        }
        private bool s_naz_asc = false;
        private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_naz_asc = !s_naz_asc;
            SetArrow(img_Naziv_Sort, s_naz_asc);
            SortZanrString("NazivZanra", s_naz_asc);
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
