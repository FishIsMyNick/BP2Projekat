using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
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

namespace BP2ProjekatCornerLibrary.Views.Worker
{
    /// <summary>
    /// Interaction logic for AdminEditStoreWindow.xaml
    /// </summary>
    public partial class AdminEditStoreWindow : Window, iDynamicListView, iSortedListView
    {
        private iDynamicListView _caller;
        private bool _blockEvents = false;
        private bool _quitAfterSave;

        private List<Mesto> _mesta;
        private List<Drzava> _drzave;
        private List<Biblikutak> _filijale;

        private Biblikutak _selectedFilijala;
        private Mesto _selectedMesto;
        private Drzava _selectedDrzava;

        private string GetNaziv { get => cbNaziv.Text; }
        private string GetUlica { get => cbUlica.Text; }
        private string GetBroj { get => cbBroj.Text; }
        private string GetMesto { get => cbMesto.Text; }
        private string GetDrzava { get => cbDrzava.Text; }
        private int GetPosBr { get => (cbMesto.SelectedValue as Mesto).PosBr; }
        private string GetOZND { get => (cbDrzava.SelectedValue as Drzava).OZND; }
        public List<Image> Arrows { get; set; }

        public AdminEditStoreWindow(iDynamicListView caller = null, int selectedID = -1, bool quitAfterSave = false)
        {
            _caller = caller;
            _quitAfterSave = quitAfterSave;
            InitializeComponent();
            Arrows = new List<Image>
            {
                img_Naziv_Sort,
                img_Ulica_Sort,
                img_Grad_Sort,
                img_Drzava_Sort,
                img_DatOtv_Sort
            };
            RefreshLists();


            if (selectedID > 0)
            {
                _selectedFilijala = DBHelper.GetLokal(selectedID);
                SetUpEditView(_selectedFilijala);
                ShowEdit();
            }
        }
        private void ShowList()
        {
            SelectView.Visibility = Visibility.Visible;
            EditView.Visibility = Visibility.Hidden;
        }
        private void ShowEdit()
        {
            SelectView.Visibility = Visibility.Hidden;
            EditView.Visibility = Visibility.Visible;
        }

        public void RefreshLists()
        {
            _blockEvents = true;
            InitDrzavaCB();
            InitMestoCB();
            FillFilijaleList();
            DisableAllArrows();
            _blockEvents = false;
        }
        private void InitDrzavaCB()
        {
            _drzave = DBHelper.GetAllDrzave();
            cbDrzava.Items.Clear();
            cbDrzava.Items.Add(new Drzava("0000", "+ Dodaj novu državu"));
            foreach (Drzava dr in _drzave)
                cbDrzava.Items.Add(dr);
        }

        private void InitMestoCB()
        {
            _mesta = DBHelper.GetAllMesta();
            cbMesto.Items.Clear();
            cbMesto.Items.Add(new Mesto(0, "+ Dodaj novo mesto"));
            foreach (Mesto m in _mesta)
                cbMesto.Items.Add(m);
        }
        private void FillFilijaleList()
        {
            OtvoreneFilijale.Items.Clear();
            List<Biblikutak> lokali = DBHelper.GetOpenLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, null, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        #region Sorting
        private List<OtvFilView> GetAllFilijaleFromList()
        {
            List<OtvFilView> ret = new List<OtvFilView>();
            foreach (var j in OtvoreneFilijale.Items) ret.Add(j as OtvFilView);
            return ret;
        }
        private void SortFilijalaString(string param, bool ascending)
        {
            List<OtvFilView> toSort = GetAllFilijaleFromList();
            OtvoreneFilijale.Items.Clear();
            foreach (OtvFilView j in Sorter.SortText<OtvFilView>(toSort, param, ascending)) OtvoreneFilijale.Items.Add(j);
        }
        private void SortFilijalaDate(string param, bool ascending)
        {
            List<OtvFilView> toSort = GetAllFilijaleFromList();
            OtvoreneFilijale.Items.Clear();
            foreach (OtvFilView j in Sorter.SortDateString<OtvFilView>(toSort, param, ascending)) OtvoreneFilijale.Items.Add(j);
        }
        private bool s_naz_asc = false;
        private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_naz_asc = !s_naz_asc;
            SetArrow(img_Naziv_Sort, s_naz_asc);
            SortFilijalaString("Naziv", s_naz_asc);
        }

        private bool s_adr_asc = false;
        private void btn_Adresa_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_adr_asc = !s_adr_asc;
            SetArrow(img_Ulica_Sort, s_adr_asc);
            SortFilijalaString("Adresa", s_adr_asc);
        }
        private bool s_mesto_asc = false;
        private void btn_Mesto_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_mesto_asc = !s_mesto_asc;
            SetArrow(img_Grad_Sort, s_mesto_asc);
            SortFilijalaString("GetMesto", s_mesto_asc);
        }
        private bool s_drz_asc = false;
        private void btn_Drzava_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_drz_asc = !s_drz_asc;
            SetArrow(img_Drzava_Sort, s_drz_asc);
            SortFilijalaString("GetDrzava", s_drz_asc);
        }
        private bool s_dat_asc = false;
        private void btn_DatOtv_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_dat_asc = !s_dat_asc;
            SetArrow(img_DatOtv_Sort, s_dat_asc);
            SortFilijalaDate("DatOtvStr", s_dat_asc);
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


        #region BUTTONS
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Validations
            if (!ValidateInputFields()) return;

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
            DBHelper.AddLokacija(a, m, d);

            DBHelper.UpdateItemWithSQL<Biblikutak>(new Biblikutak(_selectedFilijala.IDBK, naziv, _selectedFilijala.DatOtv, _selectedFilijala.DatZat, ulica, broj, posBr, oznd));

            MessageBox.Show("Uspešno ste izmenili podatke o filijali!");
            _caller?.RefreshLists();

            if (_quitAfterSave) Close();
            else ShowList();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_quitAfterSave) Close();
            else ShowList();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            _selectedFilijala.DatZat = DateTime.Now;
            if (!DBHelper.UpdateItemWithSQL<Biblikutak>(_selectedFilijala))
            {
                MessageBox.Show("Došlo je do greške pri brisanju filijale.");
                return;
            }

            MessageBox.Show("Uspešno ste obrisali filijalu.");
            _caller?.RefreshLists();

            if (_quitAfterSave) Close();
            else ShowList();
        }
        private bool ValidateInputFields()
        {
            return (
                Validator.StreetName(GetUlica) &&
                Validator.StreetNumber(GetBroj) &&
                Validator.City(_selectedMesto) &&
                Validator.Country(_selectedDrzava)
                );
        }
        #endregion

        #region LIST INTERACTIONS
        private void OtvoreneFilijale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selItem = OtvoreneFilijale.SelectedItem as OtvFilView;
            if (selItem == null) return;

            SetUpEditView(selItem);

            ShowEdit();

            return;
        }

        private void SetUpEditView(Biblikutak biblikutak)
        {
            var selItem = new OtvFilView(biblikutak);
            if (selItem == null) return;

            //_selectedFilijala = selItem;

            cbNaziv.Text = selItem.Naziv;
            cbUlica.Text = selItem.Ulica;
            cbBroj.Text = selItem.Broj;
            cbMesto.Text = selItem.GetMesto;
            cbDrzava.Text = selItem.GetDrzava;
        }

        private void cbMesto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;

            Mesto sel = ((ComboBox)sender).SelectedValue as Mesto;

            if (sel.PosBr == 0)
            {
                AddMestoWindow amw = new AddMestoWindow(this);
                amw.ShowDialog();
            }
            else
            {
                _selectedMesto = sel;
            }
        }

        private void cbDrzava_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;

            Drzava sel = ((ComboBox)sender).SelectedValue as Drzava;

            if (sel.OZND == "0000")
            {
                AddDrzavaWindow adw = new AddDrzavaWindow(this);
                adw.ShowDialog();
            }
            else
            {
                _selectedDrzava = sel;
            }
        }

        #endregion
    }
}
