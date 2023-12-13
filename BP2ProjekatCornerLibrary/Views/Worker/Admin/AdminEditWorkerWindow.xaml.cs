using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AdminEditWorkerWindow.xaml
    /// </summary>
    public partial class AdminEditWorkerWindow : Window, iDynamicListView
    {
        private iDynamicListView _caller;
        private bool _blockEvents = false;
        private Radnik _selectedWorker;
        private Mesto _selectedMesto;
        private Drzava _selectedDrzava;

        private string GetIme { get => tbIme.Text.Trim(); }
        private string GetPrezime { get => tbPrezime.Text.Trim(); }
        private string GetDan { get => tbDanRodj.Text.Trim(); }
        private string GetMesec { get => tbMesecRodj.Text.Trim(); }
        private string GetGodina { get => tbGodinaRodj.Text.Trim(); }
        private string GetUsername { get => tbUsername.Text.Trim(); }
        private string GetPassword { get => pbPass.Password; }
        private string GetUlica { get => tbUlica.Text.Trim(); }
        private string GetBroj { get => tbBroj.Text.Trim(); }
        private int GetPosBr { get => (cbMesto.SelectedItem as Mesto).PosBr; }
        private string GetMesto { get => cbMesto.Text; }
        private string GetOZND { get => (cbDrzava.SelectedItem as Drzava).OZND; }
        private string GetDrzava { get => cbDrzava.Text; }
        private bool _passwordChanged = false;
        private bool _usernameChanged = false;

        public AdminEditWorkerWindow(iDynamicListView caller = null, int selectedID = -1, TipRadnika tip = TipRadnika.Bibliotekar)
        {
            _caller = caller;
            InitializeComponent();

            RefreshLists();

            if (selectedID > 0)
            {
                if (tip == TipRadnika.Bibliotekar)
                {
                    _selectedWorker = DBHelper.GetBibliotekar(selectedID);
                    SetUpInputFields(MakeRadnikView(_selectedWorker));
                }
                else
                {
                    _selectedWorker = DBHelper.GetKurir(selectedID);
                    SetUpInputFields(MakeRadnikView(_selectedWorker));
                }
                SetEdit();
            }
            else
            {
                SetSelect();
            }
        }
        #region Common
        public void RefreshLists()
        {
            _blockEvents = true;
            FillWorkerList();
            InitDrzavaCB();
            InitMestoCB();
            _blockEvents = false;
        }
        private void SetEdit()
        {
            EditView.Visibility = Visibility.Visible;
            SelectView.Visibility = Visibility.Hidden;
            pbPass.Password = "00000000";
            _passwordChanged = false;
        }
        private void SetSelect()
        {
            EditView.Visibility = Visibility.Hidden;
            SelectView.Visibility = Visibility.Visible;
        }

        private void SetUpInputFields(RadnikView rv)
        {
            if (rv == null) return;

            tbIme.Text = rv.Ime;
            tbPrezime.Text = rv.Prezime;
            tbDanRodj.Text = rv.DatRodj.Day.ToString();
            tbMesecRodj.Text = rv.DatRodj.Month.ToString();
            tbGodinaRodj.Text = rv.DatRodj.Year.ToString();
            tbUsername.Text = rv.Username;
            tbUlica.Text = rv.Lok.Ulica;
            tbBroj.Text = rv.Lok.Broj;
            cbMesto.Text = rv.GetMesto;
            cbDrzava.Text = rv.GetDrzava;
        }
        private void InitDrzavaCB()
        {
            List<Drzava> _drzave = DBHelper.GetAllDrzave();
            cbDrzava.Items.Clear();
            cbDrzava.Items.Add(new Drzava("0000", "+ Dodaj novu državu"));
            foreach (Drzava dr in _drzave)
                cbDrzava.Items.Add(dr);
        }

        private void InitMestoCB()
        {
            List<Mesto> _mesta = DBHelper.GetAllMesta();
            cbMesto.Items.Clear();
            cbMesto.Items.Add(new Mesto(0, "+ Dodaj novo mesto"));
            foreach (Mesto m in _mesta)
                cbMesto.Items.Add(m);
        }
        private void FillWorkerList()
        {
            ZaposleniRadnici.Items.Clear();

            foreach (Radnik r in DBHelper.GetAllWorkers())
            {
                ZaposleniRadnici.Items.Add(MakeRadnikView(r));
            }
        }
        private RadnikView MakeRadnikView(Radnik r)
        {
            KorisnickiNalog nalog = null;
            if (r.GetType() == typeof(Bibliotekar))
                nalog = DBHelper.GetBibNalog(r.IDRadnik);
            else if (r.GetType() == typeof(Kurir))
                nalog = DBHelper.GetKurirNalog(r.IDRadnik);

            return new RadnikView(r.IDRadnik, r.Ime, r.Prezime, nalog.KorisnickoIme, r.DatRodj, r.DatZap, nalog.TipNaloga, r.Ulica, r.Broj, r.PosBr, r.OZND);
        }
        #endregion

        #region EDIT VIEW
        private void pbPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!_passwordChanged)
            {
                _passwordChanged = true;
                pbPass.Password = "";
            }
        }
        private void tbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_usernameChanged)
            {
                _usernameChanged = true;
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

        #region Buttons
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            _selectedWorker.Ime = GetIme;
            _selectedWorker.Prezime = GetPrezime;
            _selectedWorker.DatRodj = DateConverter.ToDateTime(GetDan, GetMesec, GetGodina);
            _selectedWorker.Ulica = GetUlica;
            _selectedWorker.Broj = GetBroj;
            _selectedWorker.PosBr = GetPosBr;
            _selectedWorker.OZND = GetOZND;


            KorisnickiNalog nalog = DBHelper.GetKorisnickiNalog(_selectedWorker.IDRadnik, EnumsHelper.GetTipRadnika(_selectedWorker));
            if (_passwordChanged)
            {
                nalog.Sifra = HashHelper.ComputeSha256Hash(GetPassword);
            }

            if (!DBHelper.AddLokacija(new Adresa(GetUlica, GetBroj), new Mesto(GetPosBr, GetMesto), new Drzava(GetOZND, GetDrzava)))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju lokacije!");
                Close();
                return;
            }
            if (_usernameChanged && nalog.KorisnickoIme != GetUsername)
            {
                if(!DBHelper.UpdateUsername(nalog.KorisnickoIme, GetUsername))
                {
                    MessageBox.Show("Došlo je do greške pri ažuriranju korisničkog imena!");
                    Close();
                    return;
                }
            }
            if (!DBHelper.UpdateAccount(nalog))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju naloga!");
                Close();
                return;
            }
            if (!DBHelper.UpdateWorker(_selectedWorker))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju korisnika!");
                Close();
                return;
            }

            MessageBox.Show("Uspešno ste izmenili radnika!");
            _caller.RefreshLists();
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelect();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            KorisnickiNalog nalog = DBHelper.GetKorisnickiNalog(_selectedWorker.IDRadnik, EnumsHelper.GetTipRadnika(_selectedWorker));

            if (DBHelper.DeleteWorkerWithAccount(_selectedWorker, nalog))
            {
                MessageBox.Show("Uspešno ste obrisali radnika!");
                _caller.RefreshLists();
                Close();
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                Close();
            }
        }
        private bool ValidateInputFields()
        {
            return (
                Validator.Name(GetIme) &&
                Validator.LastName(GetPrezime) &&
                Validator.Day(GetDan) &&
                Validator.Month(GetMesec) &&
                Validator.Year(GetGodina) &&
                Validator.StringNumber(GetUlica) &&
                Validator.StreetNumber(GetBroj) &&
                Validator.Date(GetDan,GetMesec, GetGodina)
                );
        }
        #endregion

        #endregion

        #region SELECT VIEW
        private void ZaposleniRadnici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RadnikView rv = ZaposleniRadnici.SelectedItem as RadnikView;

            SetUpInputFields(rv);

            _selectedWorker = DBHelper.GetWorker(rv.IDRadnik, EnumsHelper.GetTipRadnika(rv.Tip));

            SetEdit();
        }

        #region Sorting
        private void btn_Ime_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Prezime_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Username_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Tip_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_DatZap_Sort_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion

    }
}
