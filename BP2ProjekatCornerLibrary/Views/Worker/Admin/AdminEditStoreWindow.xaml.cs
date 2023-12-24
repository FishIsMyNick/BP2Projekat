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
    public partial class AdminEditStoreWindow : Window, iDynamicListView
    {
        private iDynamicListView _caller;
        private bool _blockEvents = false;

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


        public AdminEditStoreWindow(iDynamicListView caller = null, int selectedID = -1)
        {
            _caller = caller;
            InitializeComponent();

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
            List<Biblikutak> lokali = DBHelper.GetOpenLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, null, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        #region Sorting
        private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Adresa_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Mesto_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Drzava_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_DatOtv_Sort_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region BUTTONS
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            // Validations
            if(!ValidateInputFields()) return;

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
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ShowList();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            _selectedFilijala.DatZat = DateTime.Now;
            DBHelper.UpdateItemWithSQL<Biblikutak>(_selectedFilijala);

            MessageBox.Show("Uspešno ste obrisali filijalu.");
            AdminMainView.Instance.RefreshLists();
            Close();
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
