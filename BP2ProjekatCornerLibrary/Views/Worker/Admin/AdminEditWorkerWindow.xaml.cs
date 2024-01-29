using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.ViewModel;
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
    public partial class AdminEditWorkerWindow : Window, iDynamicListView, iSortedListView
    {
        private iDynamicListView _caller;
        private bool _quitAfterSave;
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
        public List<Image> Arrows { get; set; }

        private bool _passwordChanged = false;
        private bool _usernameChanged = false;

        public AdminEditWorkerWindow(iDynamicListView caller = null, int selectedID = -1, iTipRadnika tip = iTipRadnika.Bibliotekar, bool quitAfterSave = false)
        {
            _caller = caller;
            _quitAfterSave = quitAfterSave;
            InitializeComponent();
            Arrows = new List<Image>
            {
                img_Ime_Sort,
                img_Prezime_Sort,
                img_Username_Sort,
                img_DatZap_Sort,
                img_Tip_Sort
            };
            RefreshLists();

            if (selectedID > 0)
            {
                if (tip == iTipRadnika.Bibliotekar)
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
            _quitAfterSave = quitAfterSave;
        }
        #region Common
        public void RefreshLists()
        {
            _blockEvents = true;
            DisableAllArrows();
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

        private void SetUpInputFields(ViewRadnik rv)
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

            foreach (Radnik r in DBHelper.GetAllEmployedWorkers())
            {
                ZaposleniRadnici.Items.Add(MakeRadnikView(r));
            }
        }
        private ViewRadnik MakeRadnikView(Radnik r)
        {
            KorisnickiNalog nalog = null;
            if (r.GetType() == typeof(Models.Bibliotekar))
                nalog = DBHelper.GetBibNalog(r.IDRadnik);
            else if (r.GetType() == typeof(Kurir))
                nalog = DBHelper.GetKurirNalog(r.IDRadnik);

            return new ViewRadnik(r.IDRadnik, r.Ime, r.Prezime, nalog.KorisnickoIme, r.DatRodj, r.DatZap, nalog.TipNaloga, r.Ulica, r.Broj, r.PosBr, r.OZND);
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
                return;
            }
            if (_usernameChanged && nalog.KorisnickoIme != GetUsername)
            {
                if (!DBHelper.UpdateUsername(nalog.KorisnickoIme, GetUsername))
                {
                    MessageBox.Show("Došlo je do greške pri ažuriranju korisničkog imena!");
                    return;
                }
            }
            if (!DBHelper.UpdateAccount(nalog))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju naloga!");
                return;
            }
            if (!DBHelper.UpdateWorker(_selectedWorker))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju korisnika!");
                return;
            }

            MessageBox.Show("Uspešno ste izmenili radnika!");
            _caller.RefreshLists();

            if (_quitAfterSave) Close();
            else SetSelect();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_quitAfterSave) Close();
            else SetSelect();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            KorisnickiNalog nalog = DBHelper.GetKorisnickiNalog(_selectedWorker.IDRadnik, EnumsHelper.GetTipRadnika(_selectedWorker));

            if (DBHelper.DeleteWorkerWithAccount(_selectedWorker, nalog))
            {
                MessageBox.Show("Uspešno ste obrisali radnika!");
                _caller.RefreshLists();

                if (_quitAfterSave) Close();
                else SetSelect();
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                
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
                Validator.StreetName(GetUlica) &&
                Validator.StreetNumber(GetBroj) &&
                Validator.Date(GetDan, GetMesec, GetGodina)
                );
        }
        #endregion

        #endregion

        #region SELECT VIEW
        private void ZaposleniRadnici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewRadnik rv = ZaposleniRadnici.SelectedItem as ViewRadnik;

            SetUpInputFields(rv);

            _selectedWorker = DBHelper.GetWorker(rv.IDRadnik, EnumsHelper.GetTipRadnika(rv.Tip));

            SetEdit();
        }

        #region Sorting
        private List<ViewRadnik> GetAllRadniksFromList()
        {
            List<ViewRadnik> ret = new List<ViewRadnik>();
            foreach (var j in ZaposleniRadnici.Items) ret.Add(j as ViewRadnik);
            return ret;
        }
        private void SortRadnikString(string param, bool ascending)
        {
            List<ViewRadnik> toSort = GetAllRadniksFromList();
            ZaposleniRadnici.Items.Clear();
            foreach (ViewRadnik j in Sorter.SortText<ViewRadnik>(toSort, param, ascending)) ZaposleniRadnici.Items.Add(j);
        }
        private void SortRadnikDate(string param, bool ascending)
        {
            List<ViewRadnik> toSort = GetAllRadniksFromList();
            ZaposleniRadnici.Items.Clear();
            foreach (ViewRadnik j in Sorter.SortDateString<ViewRadnik>(toSort, param, ascending)) ZaposleniRadnici.Items.Add(j);
        }
        private bool s_ime = false;
        private void btn_Ime_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_ime = !s_ime;
            SetArrow(img_Ime_Sort, s_ime);
            SortRadnikString("Ime", s_ime);
        }
        private bool s_prez = false;
        private void btn_Prezime_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_prez = !s_prez;
            SetArrow(img_Prezime_Sort, s_prez);
            SortRadnikString("Prezime", s_prez);
        }
        private bool s_user = false;
        private void btn_Username_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_user = !s_user;
            SetArrow(img_Username_Sort, s_user);
            SortRadnikString("Username", s_user);
        }
        private bool s_tip = false;
        private void btn_Tip_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_tip = !s_tip;
            SetArrow(img_Tip_Sort, s_tip);
            SortRadnikString("TipStr", s_tip);
        }
        private bool s_dat = false;
        private void btn_DatZap_Sort_Click(object sender, RoutedEventArgs e)
        {
            s_dat = !s_dat;
            SetArrow(img_DatZap_Sort, s_dat);
            SortRadnikDate("DatZapStr", s_dat);
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

        #endregion

    }
}
