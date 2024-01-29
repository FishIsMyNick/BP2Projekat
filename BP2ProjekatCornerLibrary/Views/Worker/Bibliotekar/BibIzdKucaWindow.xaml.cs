using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Shared;
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
    /// Interaction logic for BibIzdKucaWindow.xaml
    /// </summary>
    public partial class BibIzdKucaWindow : Window, iDynamicListView, iSortedListView
    {
        private int _currentUser;
        private bool _blockEvents = false;
        private bool _quitAfterSave;
        private iDynamicListView _caller;


        private IzdKuca _selectedIK;
        private Mesto _selectedMesto;
        private Drzava _selectedDrzava;

        private string GetNaziv { get => tb_Naziv.Text.Trim(); }
        private string GetUlica { get => tb_Ulica.Text.Trim(); }
        private string GetBroj { get => tb_Broj.Text.Trim(); }
        private string GetMesto { get => cb_Mesto.Text.Trim(); }
        private string GetDrzava { get => cb_Drzava.Text.Trim(); }
        private int GetPosBr { get => (cb_Mesto.SelectedValue as Mesto).PosBr; }
        private string GetOZND { get => (cb_Drzava.SelectedValue as Drzava).OZND; }
        public List<Image> Arrows { get; set; }

        public BibIzdKucaWindow(int currentUser, int selectedID = -1, bool toAdd = false, iDynamicListView caller = null)
        {
            _currentUser = currentUser;
            _quitAfterSave = toAdd;
            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_adresa,
                img_sort_mesto,
                img_sort_naziv,
                img_sort_drzava
             };
            DisableAllArrows();

            RefreshLists();

            if (toAdd)
            {
                _caller = caller;
                SetAddView();
            }
            else if (selectedID > 0)
            {
                _selectedIK = DBHelper.GetIzdKuca(selectedID);
                SetEditView(_selectedIK);
            }
            else
            {
                SetSelectView();
            }
        }
        private void SetSelectView()
        {
            view_select.Visibility = Visibility.Visible;
            view_edit.Visibility = Visibility.Hidden;
        }
        private void SetEditView(IzdKuca sel)
        {
            FillInputFields(sel);

            lb_Add_IK.Visibility = Visibility.Collapsed;
            lb_Edit_IK.Visibility = Visibility.Visible;

            view_select.Visibility = Visibility.Hidden;
            view_edit.Visibility = Visibility.Visible;

            grd_Add_Btns.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Visible;
        }
        private void SetAddView()
        {
            ClearInputFields();

            lb_Add_IK.Visibility = Visibility.Visible;
            lb_Edit_IK.Visibility = Visibility.Collapsed;

            view_select.Visibility = Visibility.Hidden;
            view_edit.Visibility = Visibility.Visible;

            grd_Add_Btns.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;
        }

        public void RefreshLists()
        {
            _blockEvents = true;
            InitDrzavaCB();
            InitMestoCB();
            FillIKList();
            _blockEvents = false;
        }
        private void InitDrzavaCB()
        {
            cb_Drzava.Items.Clear();
            cb_Drzava.Items.Add(new Drzava("0000", "+ Dodaj novu državu"));
            foreach (Drzava dr in DBHelper.GetAllDrzave())
                cb_Drzava.Items.Add(dr);
        }

        private void InitMestoCB()
        {
            cb_Mesto.Items.Clear();
            cb_Mesto.Items.Add(new Mesto(0, "+ Dodaj novo mesto"));
            foreach (Mesto m in DBHelper.GetAllMesta())
                cb_Mesto.Items.Add(m);
        }
        private void FillIKList()
        {
            IzdKuce.Items.Clear();
            foreach (IzdKuca ik in DBHelper.GetAllIzdKucas())
            {
                IzdKuce.Items.Add(ik);
            }
        }
        #region Sorting
        private List<IzdKuca> GetAllIKsFromList()
        {
            List<IzdKuca> ret = new List<IzdKuca>();
            foreach (var ik in IzdKuce.Items)
            {
                ret.Add(ik as IzdKuca);
            }
            return ret;
        }
        private void SortIK(string propName, bool ascending)
        {
            List<IzdKuca> sorted = Sorter.SortText<IzdKuca>(GetAllIKsFromList(), propName, ascending);
            IzdKuce.Items.Clear();
            foreach (IzdKuca ik in sorted)
            {
                IzdKuce.Items.Add(ik);
            }
        }
        private bool s_naz = false;
        private void btn_sort_naziv_Click(object sender, RoutedEventArgs e)
        {
            s_naz = !s_naz;
            SetArrow(img_sort_naziv, s_naz);
            SortIK("Naziv", s_naz);
        }
        private bool s_adr = false;
        private void btn_sort_adresa_Click(object sender, RoutedEventArgs e)
        {
            s_adr = !s_adr;
            SetArrow(img_sort_adresa, s_adr);
            SortIK("Adresa", s_adr);
        }
        private bool s_mes = false;
        private void btn_sort_mesto_Click(object sender, RoutedEventArgs e)
        {
            s_mes = !s_mes;
            SetArrow(img_sort_mesto, s_mes);
            SortIK("DispMesto", s_mes);
        }
        private bool s_drz = false;
        private void btn_sort_drzava_Click(object sender, RoutedEventArgs e)
        {
            s_drz = !s_drz;
            SetArrow(img_sort_drzava, s_drz);
            SortIK("DispDrzava", s_drz);
        }

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


        private void btn_Add_IK_Click(object sender, RoutedEventArgs e)
        {
            SetAddView();
        }
        #region BUTTONS
        private bool ValidateInputFields()
        {
            return (Validator.CompanyName(GetNaziv)
                && Validator.StreetName(GetUlica)
                && Validator.StreetNumber(GetBroj)
                && Validator.City(cb_Mesto.SelectedItem as Mesto)
                && Validator.Country(cb_Drzava.SelectedItem as Drzava)
                );
        }
        #region EDIT
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Validations
            if (!ValidateInputFields()) return;

            if (cb_Mesto.SelectedIndex == 0)
            {
                MessageBox.Show("Morate izabrati mesto!");
                return;
            }
            if (cb_Drzava.SelectedIndex == 0)
            {
                MessageBox.Show("Morate izabrati državu!");
                return;
            }

            string naziv = GetNaziv.Trim();
            string ulica = GetUlica.Trim();
            string broj = GetBroj.Trim();
            int posBr = GetPosBr;
            string mesto = GetMesto;
            string oznd = GetOZND;
            string drzava = GetDrzava;

            //Add new location if needed
            Adresa a = new Adresa(ulica, broj);
            Mesto m = new Mesto(posBr, mesto);
            Drzava d = new Drzava(oznd, drzava);
            bool temp = DBHelper.AddLokacija(a, m, d);

            if (!DBHelper.UpdateItemWithSQL<IzdKuca>(new IzdKuca(_selectedIK.IDIK, naziv, ulica, broj, posBr, oznd)))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                return;
            }

            MessageBox.Show("Uspešno ste izmenili podatke o izdavačkoj kući!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IzdKuce.SelectedItem = null;
            SetSelectView();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteIzdKuca(_selectedIK))
            {
                MessageBox.Show("Došlo je do greške pri brisanju izdavačke kuće!");
                return;
            }

            MessageBox.Show("Uspešno ste obrisali izdavačku kuću!");
            RefreshLists();
            SetSelectView();
        }
        #endregion
        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Validations
            if (!ValidateInputFields()) return;

            if (cb_Mesto.SelectedIndex == 0 || cb_Mesto.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati mesto!");
                return;
            }
            if (cb_Drzava.SelectedIndex == 0 || cb_Drzava.SelectedItem == null)
            {
                MessageBox.Show("Morate izabrati državu!");
                return;
            }

            string naziv = GetNaziv;
            string ulica = GetUlica;
            string broj = GetBroj;
            int posBr = GetPosBr;
            string mesto = GetMesto;
            string oznd = GetOZND;
            string drzava = GetDrzava;

            //Add new location if needed
            Adresa a = new Adresa(ulica, broj);
            Mesto m = new Mesto(posBr, mesto);
            Drzava d = new Drzava(oznd, drzava);
            bool temp = DBHelper.AddLokacija(a, m, d);

            int idIk = 0;
            if ((idIk = DBHelper.AddIzdKuca(new IzdKuca(naziv, ulica, broj, posBr, oznd))) == 0)
            {
                MessageBox.Show("Došlo je do greške pri dodavanju izdavačke kuće!");
                return;
            }

            MessageBox.Show("Uspešno ste dodali izdavačku kuću!");
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
            else
            {
                IzdKuce.SelectedItem = null;
                SetSelectView();
            }
        }
        #endregion
        #endregion

        #region LIST INTERACTIONS
        private void IzdKuce_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selItem = IzdKuce.SelectedItem as IzdKuca;
            if (selItem == null) return;

            _selectedIK = selItem as IzdKuca;
            SetEditView(_selectedIK);
        }

        private void FillInputFields(IzdKuca ik)
        {
            if (ik == null) return;

            tb_Naziv.Text = ik.Naziv;
            tb_Ulica.Text = ik.Ulica;
            tb_Broj.Text = ik.Broj;
            cb_Mesto.Text = DBHelper.GetMesto(ik.PosBr).NazivMesta;
            cb_Drzava.Text = DBHelper.GetDrzava(ik.OZND).NazivDrzave;
        }
        private void ClearInputFields()
        {
            tb_Naziv.Text = "";
            tb_Ulica.Text = "";
            tb_Broj.Text = "";
            cb_Mesto.SelectedItem = null;
            cb_Drzava.SelectedItem = null;
        }

        private void cb_Mesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;

            Mesto sel = ((ComboBox)sender).SelectedValue as Mesto;
            if (sel == null) return;

            if (sel.PosBr == 0)
            {
                AddMestoWindow amw = new AddMestoWindow(this);
                amw.ShowDialog();
                cb_Mesto.SelectedItem = null;
            }
            else
            {
                _selectedMesto = sel;
            }
        }

        private void cb_Drzava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;

            Drzava sel = ((ComboBox)sender).SelectedValue as Drzava;
            if (sel == null) return;

            if (sel.OZND == "0000")
            {
                AddDrzavaWindow adw = new AddDrzavaWindow(this);
                adw.ShowDialog();
                cb_Drzava.SelectedItem = null;
            }
            else
            {
                _selectedDrzava = sel;
            }
        }

        #endregion

    }
}
