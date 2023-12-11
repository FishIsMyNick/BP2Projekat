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
    /// Interaction logic for AdminAddStoreWindow.xaml
    /// </summary>
    public partial class AdminAddStoreWindow : Window, iDynamicListView
    {
        private iDynamicListView _caller;
        private List<Mesto> _mesta;
        private List<Drzava> _drzave;
        private bool _blockEvents = false;
        private Mesto _selectedMesto;
        private Drzava _selectedDrzava;

        private string GetNaziv { get => cbNaziv.Text; }
        private string GetUlica { get => cbUlica.Text; }
        private string GetBroj { get => cbBroj.Text; }
        private string GetMesto { get => cbMesto.Text; }
        private string GetDrzava { get => cbDrzava.Text; }
        private int GetPosBr { get => (cbMesto.SelectedValue as Mesto).PosBr; }
        private string GetOZND { get => (cbDrzava.SelectedValue as Drzava).OZND; }
        public AdminAddStoreWindow(iDynamicListView caller)
        {
            InitializeComponent();
            RefreshLists();
            _caller = caller;
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


        #region Buttons
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

            DBHelper.AddStore(new Biblikutak(-1, naziv, DateTime.Now, null, ulica, broj, posBr, oznd));

            MessageBox.Show("Uspešno ste dodali novu filijalu!");
            _caller.RefreshLists();
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateInputFields()
        {
            if (GetUlica.Length <= 0 || GetBroj.Length <= 0 || GetNaziv.Length <= 0)
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
                return false;
            }
            if (GetUlica.Contains(' '))
            {
                string[] s = GetUlica.Split(' ');
                if (!string.Concat(s).All(char.IsLetterOrDigit))
                {
                    MessageBox.Show("Naziv ulice ne sme sadržati specijalne karaktere!");
                    return false;
                }
            }
            else if (!GetUlica.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("Naziv ulice ne sme sadržati specijalne karaktere!");
                return false;
            }
            if (_selectedDrzava == null || _selectedMesto == null)
            {
                MessageBox.Show("Mesto i država moraju biti izabrani!");
                return false;
            }
            return true;
        }
        #endregion

        #region List Interacions
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

public class FilijalaMake
{
	public string Naziv { get; set; }
	public string DatOtv { get; set; }
	public string DatZat { get; set; }

    public FilijalaMake(string naziv, DateTime datOtv)
    {
        Naziv = naziv;
		DatOtv = DateConverter.ToString(datOtv);
		DatZat = "";
	}
	public FilijalaMake(string naziv, DateTime datOtv, DateTime datZat)
	{
		Naziv = naziv;
		DatOtv = DateConverter.ToString(datOtv);
		DatZat = DateConverter.ToString(datZat);
	}
}
