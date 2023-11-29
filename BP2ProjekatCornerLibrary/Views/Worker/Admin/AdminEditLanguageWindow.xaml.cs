using BP2ProjekatCornerLibrary.Helpers;
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
	/// Interaction logic for AdminEditLanguageWindow.xaml
	/// </summary>
	public partial class AdminEditLanguageWindow : Window
	{
		public List<JezikView> ListaJezika { get; set; }
		private JezikView selectedLang;
		public AdminEditLanguageWindow()
		{
			InitializeComponent();

			FillLanguageList();
		}

		private void FillLanguageList()
		{
			Jezici.Items.Clear();

			JezikView jv = new JezikView("SRB", "Srpski");

			Jezici.Items.Add(jv);
		}

		#region Editing
		private void Jezici_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			selectedLang = (JezikView)Jezici.SelectedItem;

			SetEditLanguageFields();
		}
		private void SetEditLanguageFields()
		{
			if (selectedLang == null) return;

			tb_Edit_OZNJ.Text = selectedLang.OZNJ;
			tb_Edit_Naziv.Text = selectedLang.Naziv;
		}
		private void ClearEditLanguageFields()
		{
			tb_Edit_OZNJ.Text = "";
			tb_Edit_Naziv.Text = "";
			selectedLang = null;
			Jezici.SelectedItem = null;
		}
		private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Update in DB

			MessageBox.Show("Uspešno ste izmenili jezik!");
			ClearEditLanguageFields();
		}

		private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
		{
			ClearEditLanguageFields();
		}

		private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
		{
			//TODO: Delete from DB

			MessageBox.Show("Uspešno ste obrisali jezik");
			ClearEditLanguageFields();
		}
		#endregion

		#region Add
		private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
		{
			string oznj = tb_Add_OZNJ.Text;
			string naziv = tb_Add_Naziv.Text;

			if (oznj == "" || naziv == "")
			{
				MessageBox.Show("Sva polja moraju biti popunjena!");
				return;
			}
			else if(oznj.Length > DBHelper.MAX_OZNJ_LENGTH)
			{
				MessageBox.Show("Oznaka jezika može biti najviše 3 karaktera dugo!");
				return;
			}

			JezikView toAdd = new JezikView(oznj, naziv);
			//TODO: Add to DB

			MessageBox.Show("Uspešno ste dodali nov jezik!");
			ClearAddLangFields();
		}

		private void ClearAddLangFields()
		{
			tb_Add_OZNJ.Text = "";
			tb_Add_Naziv.Text = "";
		}
		#endregion
		#region Sorting
		private void btn_OZN_Sort_Click(object sender, RoutedEventArgs e)
		{

        }

		private void btn_Naziv_Sort_Click(object sender, RoutedEventArgs e)
		{

        }
		#endregion


	}
}
