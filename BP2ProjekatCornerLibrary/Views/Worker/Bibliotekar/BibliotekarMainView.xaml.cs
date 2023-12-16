﻿using BP2ProjekatCornerLibrary.Helpers;
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
		private int _currentUser;
		public BibliotekarMainView(Bibliotekar bibliotekar)
		{

			//mockDB = new MockDB();

			InitializeComponent();

			_currentUser = bibliotekar.IDRadnik;
			lbUsername.Content = DBHelper.GetBibNalog(_currentUser).KorisnickoIme;

			//lbUsername.Content = bibliotekar.KorisnickoIme;
            //lbUsername.Content = "placeholder";
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
			BibAddBookWindow bibAddBookWindow = new BibAddBookWindow(_currentUser);
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