using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
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

namespace BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar
{
    /// <summary>
    /// Interaction logic for BibAutorWindow.xaml
    /// </summary>
    public partial class BibAutorWindow : Window, iDynamicListView
    {
        private Autor _selectedAutor;
        private int _lokalID;
        private int _currentUser;
        private bool _quitAfterSave = false;
        public BibAutorWindow(int currentUser, Autor toEdit = null)
        {
            InitializeComponent();
            _lokalID = DBHelper.GetLatestRasporedjenBibliotekar(currentUser).IDBK;
            _currentUser = currentUser;

            RefreshLists();
            
            if (toEdit != null)
            {
                _quitAfterSave = true;
                _selectedAutor = toEdit;
                SetEditView(_selectedAutor);
            }
            else
            {
                SetSelectView();
            }
        }

        #region INIT CONTROLS
        public void RefreshLists()
        {
            FillAutorList();
            InitCbDrzave();
        }
        private void FillAutorList()
        {
            Autori.Items.Clear();
            foreach (Autor a in DBHelper.GetAllAutors())
            {
                Autori.Items.Add(new ViewAutor(a));
            }
        }
        private void InitCbDrzave()
        {
            cb_Drzava.Items.Clear();
            cb_Drzava.Items.Add(new Drzava("0000", "Nepoznato"));
            foreach (Drzava d in DBHelper.GetAllDrzave())
                cb_Drzava.Items.Add(d);
        }
        private void Autori_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewAutor sel = ((ListView)sender).SelectedItem as ViewAutor;
            if(sel != null)
            {
                _selectedAutor = sel;
                SetEditView(_selectedAutor);
            }
        }

        private void ClearInputFields()
        {
            tb_Ime.Text = "";
            tb_Prezime.Text = string.Empty;
            tb_Dan.Text = string.Empty;
            tb_Mesec.Text = string.Empty;
            tb_Godina.Text = string.Empty;
            cb_Drzava.Text = string.Empty;
            tb_Biografija.Text = string.Empty;
        }
        private void FillInputFields(ViewAutor a)
        {
            tb_Ime.Text = a.Ime;
            tb_Prezime.Text = a.Prezime != null ? a.Prezime : "";
            if (a.DatRodj != null)
            {
                tb_Dan.Text = ((DateTime)a.DatRodj).Day.ToString();
                tb_Mesec.Text = ((DateTime)a.DatRodj).Month.ToString();
                tb_Godina.Text = ((DateTime)a.DatRodj).Year.ToString();
            }
            else
            {
                tb_Dan.Text = string.Empty;
                tb_Mesec.Text = string.Empty;
                tb_Godina.Text = string.Empty;
            }
            cb_Drzava.Text = a.DispDrzava;
            tb_Biografija .Text = a.Biografija != null ? a.Biografija : "";
        }
        #endregion

        #region WINDOW CONTROLS
        private void btn_Add_Autor_Click(object sender, RoutedEventArgs e)
        {
            SetAddView();
        }
        private void SetAddView()
        {
            ClearInputFields();
            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
        }
        private void SetEditView(Autor a)
        {
            FillInputFields(new ViewAutor(a));
            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
        }
        private void SetSelectView()
        {
            Autori.SelectedItem = null;
            view_edit.Visibility = Visibility.Collapsed;
            view_select.Visibility = Visibility.Visible;
        }
        #endregion

        #region SORTING

        private void btn_sort_ime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_prezime_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_datRodj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_drzava_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region BUTTONS
        private bool ValidateInputFields()
        {
            return Validator.Name(tb_Ime.Text)
                && tb_Prezime.Text != "" ? Validator.LastName(tb_Prezime.Text) : true
                && (tb_Dan.Text != "" || tb_Mesec.Text != "" || tb_Godina.Text != "") ? Validator.Date(tb_Dan.Text, tb_Mesec.Text, tb_Godina.Text) : true;
        }
        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidateInputFields()) return;

            Autor toAdd = new Autor();
            toAdd.Ime = tb_Ime.Text;
            toAdd.Prezime = tb_Prezime.Text != "" ? tb_Prezime.Text : null;
            toAdd.DatRodj = (tb_Dan.Text != "" && tb_Mesec.Text != "" & tb_Godina.Text != "") ? new DateTime(int.Parse(tb_Godina.Text), int.Parse(tb_Mesec.Text), int.Parse(tb_Dan.Text)) : null;
            toAdd.Biografija = tb_Biografija.Text != "" ? tb_Biografija.Text : null;
            toAdd.Drzava = cb_Drzava.SelectedIndex != 0 ? ((Drzava)cb_Drzava.SelectedItem).OZND : null;

            int idAutor = 0;
            if((idAutor = DBHelper.AddItemWithSQLWithIdentity<Autor>(toAdd)) == 0)
            {
                MessageBox.Show("Došlo je do greške pri dodavanju autora!");
                return;
            }

            MessageBox.Show("Uspešno ste dodali novog autora!");
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelectView();
        }
        #endregion
        #region EDIT
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            Autor toEdit = new Autor();
            toEdit.IDAutor = _selectedAutor.IDAutor;
            toEdit.Ime = tb_Ime.Text;
            toEdit.Prezime = tb_Prezime.Text != "" ? tb_Prezime.Text : null;
            toEdit.DatRodj = (tb_Dan.Text != "" && tb_Mesec.Text != "" & tb_Godina.Text != "") ? new DateTime(int.Parse(tb_Godina.Text), int.Parse(tb_Mesec.Text), int.Parse(tb_Dan.Text)) : null;
            toEdit.Biografija = tb_Biografija.Text != "" ? tb_Biografija.Text : null;
            toEdit.Drzava = cb_Drzava.SelectedIndex != 0 ? ((Drzava)cb_Drzava.SelectedItem).OZND : null;

            if (!DBHelper.UpdateAutor(toEdit))
            {

            }
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelectView();
        }
        #endregion

        #endregion

        private void Label_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
