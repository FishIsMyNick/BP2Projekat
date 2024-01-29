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
    public partial class BibAutorWindow : Window, iDynamicListView, iSortedListView
    {
        private Autor _selectedAutor;
        private int _lokalID;
        private int _currentUser;
        private bool _quitAfterSave = false;
        private iDynamicListView _caller;
        public BibAutorWindow(int currentUser, Autor toEdit = null, bool toAdd = false, iDynamicListView caller = null)
        {
            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_datRodj,
                img_sort_ime,
                img_sort_drzava,
                img_sort_prezime
            };
            DisableAllArrows();

            _lokalID = DBHelper.GetLatestRasporedjenBibliotekar(currentUser).IDBK;
            _currentUser = currentUser;
            _quitAfterSave = toAdd;

            RefreshLists();
            if (toAdd)
            {
                _caller = caller;
                SetAddView();
            }
            else if (toEdit != null)
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
            cb_Drzava.Items.Add(new Drzava("XXX", "Nepoznato"));
            foreach (Drzava d in DBHelper.GetAllDrzave())
            {
                if (d.OZND != "XXX")
                    cb_Drzava.Items.Add(d);
            }
        }
        private void Autori_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewAutor sel = ((ListView)sender).SelectedItem as ViewAutor;
            if (sel != null)
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
            tb_Biografija.Text = a.Biografija != null ? a.Biografija : "";
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

            lb_Add_Autor.Visibility = Visibility.Visible;
            lb_Edit_Autor.Visibility = Visibility.Collapsed;

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;

            grd_Add_Btns.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;
        }
        private void SetEditView(Autor a)
        {
            FillInputFields(new ViewAutor(a));

            lb_Add_Autor.Visibility = Visibility.Collapsed;
            lb_Edit_Autor.Visibility = Visibility.Visible;
            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
            grd_Add_Btns.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Visible;
        }
        private void SetSelectView()
        {
            Autori.SelectedItem = null;
            view_edit.Visibility = Visibility.Collapsed;
            view_select.Visibility = Visibility.Visible;
        }
        #endregion

        #region SORTING
        private List<ViewAutor> GetAllAutorsFromList()
        {
            List<ViewAutor> ret = new List<ViewAutor>();
            foreach (var autor in Autori.Items)
            {
                ret.Add(autor as ViewAutor);
            }
            return ret;
        }
        private void SortAutorsText(string propName, bool ascending)
        {
            List<ViewAutor> sorted = Sorter.SortText<ViewAutor>(GetAllAutorsFromList(), propName, ascending);
            Autori.Items.Clear();
            foreach (ViewAutor autor in sorted)
            {
                Autori.Items.Add(autor);
            }
        }
        private void SortAutorsDate(string propName, bool ascending)
        {
            List<ViewAutor> sorted = Sorter.SortDateString<ViewAutor>(GetAllAutorsFromList(), propName, ascending);
            Autori.Items.Clear();
            foreach (ViewAutor autor in sorted)
            {
                Autori.Items.Add(autor);
            }
        }
        private bool s_ime = false;
        private void btn_sort_ime_Click(object sender, RoutedEventArgs e)
        {
            s_ime = !s_ime;
            SetArrow(img_sort_ime, s_ime);
            SortAutorsText("Ime", s_ime);
        }
        private bool s_prezime = false;
        private void btn_sort_prezime_Click(object sender, RoutedEventArgs e)
        {
            s_prezime = !s_prezime;
            SetArrow(img_sort_prezime, s_prezime);
            SortAutorsText("GetPrezime", s_prezime);
        }
        private bool s_dat = false;
        private void btn_sort_datRodj_Click(object sender, RoutedEventArgs e)
        {
            s_dat = !s_dat;
            SetArrow(img_sort_datRodj, s_dat);
            SortAutorsDate("DispDatRodj", s_dat);
        }
        private bool s_drz = false;
        private void btn_sort_drzava_Click(object sender, RoutedEventArgs e)
        {
            s_drz = !s_drz;
            SetArrow(img_sort_drzava, s_drz);
            SortAutorsText("DispDrzava", s_drz);
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

        #region BUTTONS
        private bool ValidateInputFields()
        {
            return Validator.Name(tb_Ime.Text)
                && (tb_Prezime.Text != "" ? Validator.LastName(tb_Prezime.Text) : true)
                && ((tb_Dan.Text != "" || tb_Mesec.Text != "" || tb_Godina.Text != "") ? Validator.Date(tb_Dan.Text, tb_Mesec.Text, tb_Godina.Text) : true);
        }
        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            Autor toAdd = new Autor();
            toAdd.Ime = tb_Ime.Text;
            toAdd.Prezime = tb_Prezime.Text != "" ? tb_Prezime.Text : null;
            toAdd.DatRodj = (tb_Dan.Text != "" && tb_Mesec.Text != "" & tb_Godina.Text != "") ? new DateTime(int.Parse(tb_Godina.Text), int.Parse(tb_Mesec.Text), int.Parse(tb_Dan.Text)) : null;
            toAdd.Biografija = tb_Biografija.Text != "" ? tb_Biografija.Text : null;
            toAdd.Drzava = cb_Drzava.SelectedIndex != 0 ? ((Drzava)cb_Drzava.SelectedItem).OZND : null;

            int idAutor = 0;
            if ((idAutor = DBHelper.AddItemWithSQLWithIdentity<Autor>(toAdd)) == 0)
            {
                MessageBox.Show("Došlo je do greške pri dodavanju autora!");
                return;
            }

            MessageBox.Show("Uspešno ste dodali novog autora!");
            if (_quitAfterSave)
            {
                _caller?.RefreshLists();
                Close();
            }
            else
            {
                RefreshLists();
                SetSelectView();
            }
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_quitAfterSave) Close();
            else SetSelectView();
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
                MessageBox.Show("Došlo je do greške pri izmeni podataka!");
                return;
            }

            MessageBox.Show("Uspešno ste izmenili podatke!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteAutor(_selectedAutor))
            {
                MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali podatke!");
            RefreshLists();
            SetSelectView();
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
