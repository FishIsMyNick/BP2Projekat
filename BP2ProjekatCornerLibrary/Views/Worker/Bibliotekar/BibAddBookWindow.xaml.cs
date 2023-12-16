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
	/// Interaction logic for BibAddBookWindow.xaml
	/// </summary>
	public partial class BibAddBookWindow : Window
	{
		iDbResult result;
		private int currentUser;
		public BibAddBookWindow(int currentUser)
		{
			this.currentUser = currentUser;
			InitializeComponent();

			InitZanrCB();
			InitJezikCB();
			InitIzdKuca();
		}

		private void InitZanrCB()
		{
			List<Zanr> zanrovi = DBHelper.GetAllZanrs();
			zanrovi.Add(new Zanr("000", "Dodaj nov žanr"));

			cbZanr.ItemsSource = zanrovi;
			cbZanr.SelectedIndex = 0;
		}
		private void InitJezikCB()
		{
			List<Jezik> jeziks = DBHelper.GetAllJeziks();
			jeziks.Add(new Jezik("000", "Dodaj nov jezik"));

			cbJezik.ItemsSource = jeziks;
			cbJezik.SelectedIndex = 0;
		}
		private void InitIzdKuca()
		{
			List<IzdKuca> izdkucas = DBHelper.GetAllIzdKucas();
			IzdKuca ph_addNew = new IzdKuca("Dodaj novu izdavačku kuću");
			ph_addNew.IDIK = -1;
			izdkucas.Add(ph_addNew); 

			cbIK.ItemsSource = izdkucas;
			cbIK.SelectedIndex = 0;
		}

		private void btnGenerisi_Click(object sender, RoutedEventArgs e)
		{
			//tbID.Text = DBHelper.GetFirstFreeBookID().ToString();
		}

		private void btnDodaj_Click(object sender, RoutedEventArgs e)
		{
			//if (tbID.Text == "" ||
			//	tbNaziv.Text == "" ||
			//	tbAutor.Text == "" ||
			//	//tbIzdKuca.Text != "" &&
			//	tbGodIzd.Text == "")
			//{
			//	MessageBox.Show("Sva polja moraju biti popunjena!");
			//}
			////cbDan.SelectedItem != null &&
			////cbMesec.SelectedItem != null &&
			////cbGodina.SelectedItem != null)
			//else
			//{
			//	// TODO: Izbaci sve sa id, prebaci na autogenerisan
			//	int id;
			//	if (!int.TryParse(tbID.Text, out id))
			//	{
			//		MessageBox.Show("Uneta vrednost za ID knjige nije validna!");
			//		return;
			//	}
			//	if (id < 0)
			//	{
			//		MessageBox.Show("Uneta vrednost za ID knjige nije validna!");
			//		return;
			//	}

			//	// Get values
			//	string naziv = tbNaziv.Text;
			//	string autor = tbAutor.Text;
			//	string jezik = cbJezik.SelectedValue.ToString();
			//	int izdKuca = int.Parse(cbIK.SelectedValue.ToString());
			//	string godIzd = tbGodIzd.Text;
			//	string zanr = cbZanr.SelectedValue.ToString();
			//	bool ogr = (bool)chbOgraniceno.IsChecked;
			//	bool dodajOvde = (bool)chbDodajOvde.IsChecked;

			//	// Check BrIzd
			//	int brIzd;

			//	if(!int.TryParse(tbBrIzd.Text, out brIzd) || brIzd < 1)
			//	{
			//		MessageBox.Show("Uneta vrednost za broj izdanja knjige nije validna!");
			//		return;
			//	}

			//	// Get possible authors
			//	string[] aImena = autor.Split(' ');
			//	string aIme = aImena[0];
			//	string aPrezime = "";
			//	for (int i = 1; i < aImena.Length; i++)
			//	{
			//		aPrezime += aImena[i];
			//		if (i < aImena.Length - 1)
			//			aPrezime += " ";
			//	}
			//	List<Autor> autors = DBHelper.GetAutorsByName(aIme, aPrezime);

			//	int autorID = autors[0].IDAutor;
			//	if (autors.Count > 1)
			//	{
			//		// TODO: open autor selection

			//	}

			//	// Check if book exists
			//	Knjiga checkDup = DBHelper.GetExactBook(naziv, autorID, izdKuca, godIzd, brIzd, zanr, jezik, ogr);
			//	if(checkDup != null)
			//	{
			//		MessageBox.Show("Knjiga sa ovim podacima već postoji!");
			//		return;
			//	}

			//	//Check dodaj ovde
			//	int kol = 0;
			//	if (dodajOvde)
			//	{
			//		if(!int.TryParse(tbKolicina.Text.ToString(), out kol) || kol < 1)
			//		{
			//			MessageBox.Show("Količina nije uneta u ispravnom formatu!");
			//			return;
			//		}
			//	}

			//	// Try add book
			//	iDbResult result = DBHelper.AddBook(id, naziv, godIzd, brIzd, ogr, autorID, jezik, izdKuca, zanr);

			//	if (result == iDbResult.Success)
			//	{
			//		MessageBox.Show("Uspešno dodata knjiga!");

			//		if (dodajOvde)
			//		{
			//			result = DBHelper.AddKnjigaULokalu(id, (int)DBHelper.GetRadnik(currentUser).Idbk, kol);

			//			if(result == iDbResult.Success)
			//			{
			//				MessageBox.Show("Uspešno dodata knjiga u ovoj filijali!");
			//			}
			//			else
			//			{
			//				MessageBox.Show("Greška pri dodavanju knjige u ovoj filijali! Knjiga je već dodata u sistem. Ako želite da je dodate u ovu filijalu, možete to uraditi iz glavnog menija.");
			//			}
			//		}
					
			//		Close();
			//	}
			//	else if (result == iDbResult.Duplicate)
			//	{
			//		MessageBox.Show("Knjiga sa ovim ID-om ili kombinacijom naziva, autora i jezika već postoji!");
			//	}
			//	else
			//	{
			//		MessageBox.Show("Došlo je do neočekivane greške");
			//	}
			//}
		}
	}
}
