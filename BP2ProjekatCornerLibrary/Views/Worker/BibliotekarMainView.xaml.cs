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
	/// Interaction logic for BibliotekarMainView.xaml
	/// </summary>
	public partial class BibliotekarMainView : Window
	{
		//private MockDB mockDB;
		public BibliotekarMainView(Radnik bibliotekar)
		{

			//mockDB = new MockDB();

			InitializeComponent();

			//lbUsername.Content = bibliotekar.Username;
			lbUsername.Content = "Username";
		}
		//public BibliotekarMainView(Bibliotekar bibliotekar)
		//{

		//	mockDB = new MockDB();

		//	InitializeComponent();

		//	//lbUsername.Content = bibliotekar.Username;
		//	lbUsername.Content = "Username";
		//}

		private bool CheckCardID()
		{
			//string cardIDstring = tbBrKartice.Text;
			//clan = null;
			//if (cardIDstring == "")
			//{
			//	MessageBox.Show("Morate uneti broj kartice!");
			//	return false;
			//}
			//int cardID;
			//if (!int.TryParse(cardIDstring, out cardID))
			//{
			//	MessageBox.Show("Unet broj kartice nije u pravilnom formatu!");
			//	return false;
			//}

			//clan = DBHelper.GetUserByCard(cardID);

			//if (clan == null)
			//{
			//	MessageBox.Show("Korisnik sa ovom karticom ne postoji!");
			//	return false;
			//}
			//else
			//{
			//	if (DBHelper.GetCard(cardID).DatVal < DateTime.Now)
			//	{
			//		MessageBox.Show("Članu je istekla članarina!");
			//	}
			//	return true;
			//}
			return false;
		}

		private void btnGetReservations_Click(object sender, RoutedEventArgs e)
		{
			//Clan clan = null;
			//if (CheckCardID(out clan))
			//{
			//	BibUserReservations bibUserReservations = new BibUserReservations(clan);

			//	bibUserReservations.ShowDialog();
			//}
		}

		private void btnKredit_Click(object sender, RoutedEventArgs e)
		{
			//Clan clan = null;
			//if (CheckCardID(out clan))
			//{
			//	BibDodajKredit bibKredit = new BibDodajKredit(clan);

			//	bibKredit.ShowDialog();
			//	Console.WriteLine();
			//}
		}

		private void btnAddUser_Click(object sender, RoutedEventArgs e)
		{
			BibAddUserWindow bibAddUserWindow = new BibAddUserWindow();
			bibAddUserWindow.ShowDialog();
		}

		private void btnExtendMembership_Click(object sender, RoutedEventArgs e)
		{
			//Clan clan = null;
			//if (CheckCardID(out clan))
			//{
			//	if (DBHelper.ExtendMembership(DBHelper.GetCard((int)clan.BrKartice)) == iDbResult.Success)
			//	{
			//		MessageBox.Show("Uspešno produžena članarina!");
			//	}
			//}
		}

		private void btnRazduzi_Click(object sender, RoutedEventArgs e)
		{
			//Clan clan = null;
			//if (CheckCardID(out clan))
			//{
			//	if (DBHelper.DeleteReservations(DBHelper.GetReservations(clan.Id)) == iDbResult.Success)
			//		MessageBox.Show("Uspešno razdužen član!");
			//	else
			//		MessageBox.Show("Došlo je do neočekivane greške!");
			//}
		}

		private void btnAddBook_Click(object sender, RoutedEventArgs e)
		{
			BibAddBookWindow bibAddBookWindow = new BibAddBookWindow();
			bibAddBookWindow.ShowDialog();
		}

		private void btnAddNews_Click(object sender, RoutedEventArgs e)
		{
			BibAddNewsWindow bibAddNewsWindow = new BibAddNewsWindow();
			bibAddNewsWindow.ShowDialog();
		}

		private void btnAddMagazin_Click(object sender, RoutedEventArgs e)
		{
			BibAddMagazinWindow addMagazinWindow = new BibAddMagazinWindow();
			addMagazinWindow.ShowDialog();
		}

		private void btnAddBookInStore_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnAddNewsInStore_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnAddMagazinInStore_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnRemoveBookInStore_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnRemoveNewsInStore_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnRemoveMagazineInStore_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
