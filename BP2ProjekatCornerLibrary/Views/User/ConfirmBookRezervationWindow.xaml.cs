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

namespace BP2ProjekatCornerLibrary.Views.User
{
	/// <summary>
	/// Interaction logic for ConfirmBookRezervationWindow.xaml
	/// </summary>
	public partial class ConfirmBookRezervationWindow : Window
	{
		public string Knjiga { get; set; } = "";
		public string Autor { get; set; } = "";

		public string Adresa { get; set; } = "";
		public string Grad { get; set; } = "";
		public string Drzava { get; set; } = "";

		private int BookID;
		private int LokalID;
		private UserMainWindow mainWindow;

		public ConfirmBookRezervationWindow(string knjiga, string autor, string adresa, string grad, string drzava, int bookID, int lokalID, UserMainWindow refCaller)
		{
			Knjiga = knjiga;
			Autor = autor;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;

			BookID = bookID;
			LokalID = lokalID;
			mainWindow = refCaller;

			InitializeComponent();

			lbBookName.Content = knjiga;
			lbBookAuthor.Content = autor;
			lbAdresa.Content = adresa;
			lbGrad.Content = grad;
			lbDrzava.Content = drzava;

		}

		private void btnConfirm_Click(object sender, RoutedEventArgs e)
		{
			mainWindow.ConfirmBookRezervation(BookID, LokalID);
			Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
