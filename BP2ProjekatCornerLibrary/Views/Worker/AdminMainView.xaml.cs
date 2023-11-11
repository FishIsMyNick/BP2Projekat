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
    /// Interaction logic for AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : Window
    {
        public string Zap_Ime { get; set; }
        public string Zap_Prezime { get; set; }
        public string Zap_Username { get; set; }
        public string Zap_Tip { get; set; }

        //TODO: treba da bude lista radnika
        public List<ZapRadTemp> ListaZaposlenihRadnika { get; set; }

        public int RadnikTableImeWidth = 300;
        public AdminMainView()
		{
			InitializeComponent();

			FillAllLists();
        }

        private void FillAllLists()
        {
			FillZapRadniciList();
        }
        private void FillZapRadniciList()
		{
			ListaZaposlenihRadnika = new List<ZapRadTemp>();

			ZapRadTemp zrt = new ZapRadTemp();
			zrt.Zap_Ime = "Ime coveka";
			zrt.Zap_Prezime = "Prezime";
			zrt.Zap_Username = "username";
			zrt.Zap_Tip = "Bibliotekar";
			zrt.Zap_DatZap = "1.1.1997.";

			ListaZaposlenihRadnika.Add(zrt);

			foreach(ZapRadTemp zr in ListaZaposlenihRadnika)
			{
				ZaposleniRadnici.Items.Add(zr);
			}
		}

		private void btn_ZapRadnik_Ime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Username_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_Tip_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btn_ZapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
		{

		}
		private void ZapRadnici_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Console.WriteLine("zr click");
		}
	}
}



public class ZapRadTemp
{
	public string? Zap_Ime { get; set; }
	public string? Zap_Prezime { get; set; }
	public string? Zap_Username { get; set; }
	public string? Zap_Tip { get; set; }
	public string? Zap_DatZap { get; set; }
}