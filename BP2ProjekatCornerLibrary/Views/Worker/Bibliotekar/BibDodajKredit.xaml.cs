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
	/// Interaction logic for BibDodajKredit.xaml
	/// </summary>
	public partial class BibDodajKredit : Window
	{
		//private Clan clan;
		public BibDodajKredit()
		{
			//this.clan = clan;
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			double amount;
			if (tbAmount.Text == "")
			{
				MessageBox.Show("Morate uneti neki iznos!");
			}
			else if (double.TryParse(tbAmount.Text, out amount))
			{
				if (amount <= 0)
				{
					MessageBox.Show("Iznos ne moze biti manji od 0!");
				}
				else
				{
					//if (DBHelper.AddUserCredit(clan, amount) == iDbResult.Success)
					//{
					//	MessageBox.Show("Uspesno uplaćen kredit!");
					//	Close();
					//}
					//else
					//{
					//	MessageBox.Show("Došlo je do neočekivane greške.");
					//}
				}
			}
			else
			{
				MessageBox.Show("Unet iznos nije u dobrom formatu!");
			}
		}
	}
}
