using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
    /// Interaction logic for AdminAddWorkerWindow.xaml
    /// </summary>
    public partial class AdminAddWorkerWindow : Window, iDynamicListView
    {
        private iDynamicListView _caller = null;
        private bool _blockEvents = false;
        private Mesto _selectedMesto;
        private Drzava _selectedDrzava;
        private int _adminID;

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

        public AdminAddWorkerWindow(iDynamicListView caller, int AdminID)
        {
            _adminID = AdminID;
            _caller = caller;
            InitializeComponent();

            RefreshLists();
        }

        public void RefreshLists()
        {
            _blockEvents = true;
            InitDrzavaCB();
            InitMestoCB();
            _blockEvents = false;
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

        #region BUTTONS
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            KorisnickiNalog nalog = new KorisnickiNalog(GetUsername, HashHelper.ComputeSha256Hash(GetPassword), DateTime.Now, ((int)EnumsHelper.GetTipRadnika(TipRad.Text)));

            if (!DBHelper.AddLokacija(new Adresa(GetUlica, GetBroj), new Mesto(GetPosBr, GetMesto), new Drzava(GetOZND, GetDrzava)))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju lokacije!");
                Close();
                return;
            }

            switch (nalog.TipNaloga)
            {
                case 2:
                    Models.Bibliotekar b = new Models.Bibliotekar(-1, GetIme, GetPrezime, DateConverter.ToDateTime(GetDan, GetMesec, GetGodina), DateTime.Now, null, _adminID, DateTime.Now, GetUlica, GetBroj, GetPosBr, GetOZND);
                    if (!DBHelper.AddWorkerWithAccount(b, nalog))
                    {
                        MessageBox.Show("Došlo je do greške pri dodavanju korisnika!");
                        Close();
                        return;
                    }
                    break;
                case 3:
                    Kurir k = new Kurir(-1, GetIme, GetPrezime, DateConverter.ToDateTime(GetDan, GetMesec, GetGodina), DateTime.Now, null, _adminID, DateTime.Now, GetUlica, GetBroj, GetPosBr, GetOZND);
                    if (!DBHelper.AddWorkerWithAccount(k, nalog))
                    {
                        MessageBox.Show("Došlo je do greške pri dodavanju korisnika!");
                        Close();
                        return;
                    }
                    break;
                default: return;
            }

            MessageBox.Show("Uspešno ste dodali novog radnika!");
            _caller.RefreshLists();
            Close();
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

    }
}
