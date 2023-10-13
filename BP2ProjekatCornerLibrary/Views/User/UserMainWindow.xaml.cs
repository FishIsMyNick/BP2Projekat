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

namespace BP2ProjekatCornerLibrary.Views.User
{
	/// <summary>
	/// Interaction logic for UserMainWindow.xaml
	/// </summary>
	public partial class UserMainWindow : Window
	{
		// Bindings
		//		Collections
		private List<Grid> Views;
		private Grid CurrentView;
		private List<StackPanel> Lists;
		private StackPanel CurrentPanel;

		public List<KnjigaRezView> KnjigeCollection;
		public List<IstorijaKnjigaView> IstorijaKnjigeCollection;
		public List<LokacijaView> LokacijaCollection;
		public List<KatalogKnjigaView> KatalogKnjigaCollection;

		public List<KnjigaULokaluView> KnjigeULokaluCollection;
		public List<NovineULokaluView> NovineULokaluCollection;
		public List<MagazinULokaluView> MagazinULokaluCollection;
		//		Selections
		public Clan CurrentUser;
		public ClanView CurrentUserView;
		public LokacijaView SelectedLocation;


		private string arrowUpImg = "pack://application:,,,/Images/arrow_up.png";
		private string arrowDownImg = "pack://application:,,,/Images/arrow_down.png";
		private int editionLimit = 3;

		//private MockDB mockDB;

		public UserMainWindow(Clan clan)
		{
			//mockDB = new MockDB();

			InitializeComponent();


			Views = new List<Grid>
			{
				viewHomePage,
				viewKnjigeNaLokaciji
			};
			CurrentView = viewHomePage;
			CurrentPanel = kulSpKnjige;
			CurrentUser = clan;


			#region Test Data
			#region Main View
			//List<Rezervacija> rezervacije = (List<Rezervacija>)DBHelper.ExecuteQuery($"SELECT * FROM KNJIGA WHERE Clan = {clan.IDClan};");
			List<Rezervacija> rezervacije = new List<Rezervacija>();

			rezervacije.AddRange(DBHelper.GetReservations(clan.Idclan));

			foreach(Rezervacija rez in rezervacije)
			{
				List<Autor> k = DBHelper.GetBookAuthors(rez.Idknjiga);
			}

			KnjigeCollection = new List<KnjigaRezView>();
			foreach (Rezervacija re in rezervacije)
			{
				//KnjigeCollection.Add(new KnjigaRezView(DBHelper.GetBook(re.IDKnjiga)));
			}

			//List<Rezervacija> istorijarezervacija = (List<Rezervacija>)DBHelper.ExecuteQuery($"SELECT * FROM REZERVACIJA WHERE Clan = {clan.Idclan};");
			//{
			//	new KnjigaRezView(),
			//	new KnjigaRezView("7 Krugova Pakla", "Dante Aligieri", "1.2.2022.", 5),
			//	new KnjigaRezView("Srecni V Ljubezni", "Ko Da Znam", "1.2.2012.", 13),
			//	new KnjigaRezView("Inferno", "Den Braun", "10.7.2018.", 14),
			//	new KnjigaRezView("Digitalna Tvrdjava", "Den Braun", "11.7.2018.", 2),
			//	new KnjigaRezView("Andjeli i Demoni", "Den Braun", "10.6.2018.", 111)
			//};

			//IstorijaKnjigeCollection = new List<IstorijaKnjigaView>
			//{
			//	new IstorijaKnjigaView(),
			//	new IstorijaKnjigaView("Meditacije", "Marko Aurelije", "3.6.2019.", "11.12.2019.", 1),
			//	new IstorijaKnjigaView("Na Klancu", "Ivan Cankar", "3.7.2019.", "11.12.2020.", 33),
			//	new IstorijaKnjigaView("Naziv", "Autor", "3.7.2019.", "11.12.2020.", 11),
			//	new IstorijaKnjigaView("Neko Cudno Delo", "Arto Paasilinna", "9.4.2017.", "11.12.2021.", 22)
			//};

			//LokacijaCollection = new List<LokacijaView>
			//{
			//	new LokacijaView(),
			//	new LokacijaView("Blagoja Parovica 9a", "Novi Sad", "Srbija", 14),
			//	new LokacijaView("Tolstojeva 4", "Novi Sad", "Srbija", 13),
			//	new LokacijaView("Jurija Gagarina 239", "Beograd", "Srbija", 12),
			//	new LokacijaView("Jurija Gagarina 4", "Beograd", "Srbija", 11),
			//	new LokacijaView("Gandijeva 44", "Beograd", "Srbija", 10),
			//	new LokacijaView("Cara Lazara 180", "Beograd", "Srbija", 9),
			//	new LokacijaView("Bogdana Garabantina 3", "Novi Sad", "Srbija", 8),
			//	new LokacijaView("Zaucerjeva 2a", "Ljubljana", "Slovenija", 7),
			//	new LokacijaView("Cesta na Brdo 44", "Ljubljana", "Slovenija", 6),
			//	new LokacijaView("Ljubljanska 33", "Kranj", "Slovenija", 5),
			//	new LokacijaView("Zagrebacka Cesta 13", "Zagreb", "Hrvatska", 4),
			//	new LokacijaView("Slovenska Cesta 73", "Zagreb", "Hrvatska", 3),
			//	new LokacijaView("Temerinska 33", "Novi Sad", "Srbija", 2),
			//	new LokacijaView("Champs Elyse 2", "Pariz", "Francuska", 1)
			//};
			#endregion
			#region Knjige U Lokalu View
			//KnjigeULokaluCollection = new List<KnjigaULokaluView>
			//{
			//	new KnjigaULokaluView("Naziv", "Autor", "IzdKuca", "1.1.1999.", "Zanr", "Jezik", true, "3", 1),
			//	new KnjigaULokaluView("Zovem se crveno", "Orhan Pamuk", "Pingvin", "12.2.2002.", "Drama", "Srpski", false, "3", 2),
			//	new KnjigaULokaluView("Hobbit", "J.R.R. Martin", "Pingvin", "1.1.1992.", "Fantazija", "Srpski", false, "2", 3),
			//	new KnjigaULokaluView("Parfem", "Patrick Suskind", "Pingvin", "1.1.1981.", "Drama", "Srpski", false, "4", 4),
			//	new KnjigaULokaluView("This Book Loves You", "Pewdiepie", "Pewdiepie", "1.1.2018.", "Self help", "Engleski", true, "1", 5),
			//	new KnjigaULokaluView("Starac i More", "Ernest Hemingvej", "Vulkan", "1.2.1980.", "Drama", "Jezik", false, "1", 6),
			//	new KnjigaULokaluView("1001 Recipe", "Gordon Ramsey", "Glok", "3.10.2014.", "Cook Book", "Engleski", false, "3", 7)
			//};
			//NovineULokaluCollection = new List<NovineULokaluView>
			//{
			//	new NovineULokaluView(),
			//	new NovineULokaluView("Kurir", iPeriod.Dnevni, 150, 3, 1),
			//	new NovineULokaluView("Blic", iPeriod.Dnevni, 150, 5, 2),
			//	new NovineULokaluView("Nin", iPeriod.Nedeljni, 200, 3, 3)
			//};
			//MagazinULokaluCollection = new List<MagazinULokaluView>
			//{
			//	new MagazinULokaluView(),
			//	new MagazinULokaluView("Science Monthly", iPeriod.Mesecni, 300, 1, 1),
			//	new MagazinULokaluView("Politikin Zabavnik", iPeriod.Nedeljni, 200, 2, 2),
			//	new MagazinULokaluView("National Geographic", iPeriod.Mesecni, 500, 3, 3),
			//	new MagazinULokaluView("Cars", iPeriod.Godisnji, 1000, 4, 4)
			//};

			#endregion
			////
			#endregion
			//// SelectedLocation = LokacijaCollection[3];

			//// Get Locals
			//LokacijaCollection = new List<LokacijaView>();
			//foreach (var lokacija in DBHelper.GetAllStores())
			//{
			//	LokacijaCollection.Add(new LokacijaView(lokacija));
			//}

			//// Get User Reservations
			//KnjigeCollection = new List<KnjigaRezView>();
			//foreach (var rez in DBHelper.GetReservations(CurrentUser.Id))
			//{
			//	KnjigeCollection.Add(new KnjigaRezView(rez));
			//}

			//// Get User Reservation History
			//IstorijaKnjigeCollection = new List<IstorijaKnjigaView>();
			//foreach (Istorijarezervacija i in DBHelper.GetReservationHistory(CurrentUser.Id))
			//{
			//	IstorijaKnjigeCollection.Add(new IstorijaKnjigaView(i));
			//}

			UpdateViews();


			GoToMainView();

			//GoToKnjigeULokaluView(1);
			//FillBooksOnLocationList(SortBooksOnLocationByName());
		}
		private void UpdateViews()
		{
			// Main view
			//lbUsername.Content = CurrentUser?.Username;
			//lbKredit.Content = CurrentUser?.Kredit;

			//KnjigeCollection.Clear();
			//foreach(Rezervacija rez in DBHelper.GetReservations(CurrentUser.Id))
			//{
			//	KnjigeCollection.Add(new KnjigaRezView(rez));
			//}
			//IstorijaKnjigeCollection.Clear();
			//foreach(Istorijarezervacija ist in DBHelper.GetReservationHistory(CurrentUser.Id))
			//{
			//	IstorijaKnjigeCollection.Add(new IstorijaKnjigaView(ist));
			//}

			//FillReservationList(SortBookReservationsByDate());
			//FillReservationHistoryList(SortBooksHistoryByDateCheckOut());
			//FillLocationList(SortLocationByCountry());
			//// Lokal view
			//FillLocationComboBox(cbZkLokalKnjige);
			//FillLocationComboBox(cbZkLokalNovine);
			//FillLocationComboBox(cbZmLokalMagazin);
		}

		#region List population
		//private void FillLocationComboBox(ComboBox comboBox)
		//{
		//	//comboBox.Items.Clear();
		//	//foreach (LokacijaView l in SortLocationByCountry())
		//	//{
		//	//	comboBox.Items.Add(l);
		//	//}
		//}

		//private void FillReservationList(List<KnjigaRezView> list)
		//{
		//	RezervacijeList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		RezervacijeList.Items.Add(new KnjigaRezView("Trenutno nemate rezervacija...", "", ""));
		//	}
		//	else
		//	{
		//		foreach (KnjigaRezView k in list)
		//		{
		//			RezervacijeList.Items.Add(k);
		//		}
		//	}
		//}
		//private void FillReservationHistoryList(List<IstorijaKnjigaView> list)
		//{
		//	IstorijaRezervacijaList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		IstorijaRezervacijaList.Items.Add(new IstorijaKnjigaView("Niste zavrsili ni jednu rezervaciju...", "", "", ""));
		//	}
		//	else
		//	{
		//		foreach (IstorijaKnjigaView k in list)
		//		{
		//			IstorijaRezervacijaList.Items.Add(k);
		//		}
		//	}
		//}
		//private void FillLocationList(List<LokacijaView> list)
		//{
		//	Lokacije.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		Lokacije.Items.Add(new LokacijaView("Nema registrovanih lokacija...", "", ""));
		//	}
		//	else
		//	{
		//		foreach (LokacijaView l in list)
		//		{
		//			Lokacije.Items.Add(l);
		//		}
		//	}
		//}
		//private void FillBooksOnLocationList(List<KnjigaULokaluView> list)
		//{
		//	KnjigeULokaluList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		KnjigeULokaluList.Items.Add(new KnjigaULokaluView("Nema knjiga na lokaciji...", "", "", "", "", "", false, ""));
		//	}
		//	else
		//	{
		//		foreach (KnjigaULokaluView l in list)
		//		{
		//			KnjigeULokaluList.Items.Add(l);
		//		}
		//	}
		//}
		//private void FillNewsOnLocationList(List<NovineULokaluView> list)
		//{
		//	NovineULokaluList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		NovineULokaluList.Items.Add(new NovineULokaluView("Nema novina na lokaciji...", iPeriod.NA, 0, 0));
		//	}
		//	else
		//	{
		//		foreach (NovineULokaluView l in list)
		//		{
		//			NovineULokaluList.Items.Add(l);
		//		}
		//	}
		//}
		//private void FillMagazinesOnLocationList(List<MagazinULokaluView> list)
		//{
		//	MagazinULokaluList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		MagazinULokaluList.Items.Add(new MagazinULokaluView("Nema magazina na lokaciji...", iPeriod.NA, 0, 0));
		//	}
		//	else
		//	{
		//		foreach (MagazinULokaluView l in list)
		//		{
		//			MagazinULokaluList.Items.Add(l);
		//		}
		//	}
		//}
		//private void FillKatalogKnjigaList(List<KatalogKnjigaView> list)
		//{
		//	KatalogKnjigaList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		KatalogKnjigaList.Items.Add(new KatalogKnjigaView(-1, -1, "Nema knjiga u katalogu..."));
		//	}
		//	else
		//	{
		//		foreach (KatalogKnjigaView k in list)
		//		{
		//			KatalogKnjigaList.Items.Add(k);
		//		}
		//	}
		//}
		//private void FillKatalogNovinaList(List<KatalogNovineMagazinView> list)
		//{
		//	KatalogNovinaList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		KatalogNovinaList.Items.Add(new KatalogNovineMagazinView(-1, -1, "Nema novina u katalogu..."));
		//	}
		//	else
		//	{
		//		foreach(KatalogNovineMagazinView k in list)
		//		{
		//			KatalogNovinaList.Items.Add(k);
		//		}
		//	}
		//}
		//private void FillKatalogMagazinaList(List<KatalogNovineMagazinView> list)
		//{
		//	KatalogMagazinaList.Items.Clear();
		//	if (list.Count == 0)
		//	{
		//		KatalogMagazinaList.Items.Add(new KatalogNovineMagazinView(-1, -1, "Nema magazina u katalogu..."));
		//	}
		//	else
		//	{
		//		foreach (KatalogNovineMagazinView k in list)
		//		{
		//			KatalogMagazinaList.Items.Add(k);
		//		}
		//	}
		//}
		#endregion




		#region Sorting Controls
		#region Event Handlers
		#region Main view
		private bool rezKnjigaSortAscending = false;
		private bool rezAutorSortAscending = false;
		private bool rezDatumSortAscending = true;
		private void btnKnjiga_Click(object sender, RoutedEventArgs e)
		{
			rezKnjigaSortAscending = !rezKnjigaSortAscending;

			imgKnjigaSort.Source = rezKnjigaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationList(SortBookReservationsByName(rezKnjigaSortAscending));
		}
		private void btnAutor_Click(object sender, RoutedEventArgs e)
		{
			rezAutorSortAscending = !rezAutorSortAscending;

			imgAutorSort.Source = rezAutorSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationList(SortBookReservationsByAuthor(rezAutorSortAscending));
		}
		private void btnDatum_Click(object sender, RoutedEventArgs e)
		{
			rezDatumSortAscending = !rezDatumSortAscending;

			imgDatumSort.Source = rezDatumSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationList(SortBookReservationsByDate(rezDatumSortAscending));
		}

		private bool irzKnjigaSortAscending = false;
		private bool irzAutorSortAscending = false;
		private bool irzDatumUzeoSortAscending = true;
		private bool irzDatumVratioSortAscending = false;
		private void btnIstorijaKnjiga_Click(object sender, RoutedEventArgs e)
		{
			irzKnjigaSortAscending = !irzKnjigaSortAscending;

			imgIstorijaKnjigaSort.Source = irzKnjigaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationHistoryList(SortBooksHistoryByName(irzKnjigaSortAscending));
		}
		private void btnIstorijaAutor_Click(object sender, RoutedEventArgs e)
		{
			irzAutorSortAscending = !irzAutorSortAscending;

			imgIstorijaAutorSort.Source = irzAutorSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationHistoryList(SortBooksHistoryByAuthor(irzAutorSortAscending));
		}
		private void btnIstorijaDatumUzeo_Click(object sender, RoutedEventArgs e)
		{
			irzDatumUzeoSortAscending = !irzDatumUzeoSortAscending;

			imgIstorijaDatumUzeoSort.Source = irzDatumUzeoSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationHistoryList(SortBooksHistoryByDateCheckOut(irzDatumUzeoSortAscending));
		}
		private void btnIstorijaDatumVratio_Click(object sender, RoutedEventArgs e)
		{
			irzDatumVratioSortAscending = !irzDatumVratioSortAscending;

			imgIstorijaDatumVratioSort.Source = irzDatumVratioSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillReservationHistoryList(SortBooksHistoryByDateCheckIn(irzDatumVratioSortAscending));
		}

		private bool lokAdresaSortAscending = false;
		private bool lokGradSortAscending = false;
		private bool lokDrzavaSortAscending = true;

		private void btnAdresa_Click(object sender, RoutedEventArgs e)
		{
			lokAdresaSortAscending = !lokAdresaSortAscending;

			imgAdresaSort.Source = lokAdresaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillLocationList(SortLocationByAddress(lokAdresaSortAscending));
		}
		private void btnGrad_Click(object sender, RoutedEventArgs e)
		{
			lokGradSortAscending = !lokGradSortAscending;

			imgGradSort.Source = lokGradSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillLocationList(SortLocationByCity(lokGradSortAscending));
		}
		private void btnDrzava_Click(object sender, RoutedEventArgs e)
		{
			lokDrzavaSortAscending = !lokDrzavaSortAscending;

			imgDrzavaSort.Source = lokDrzavaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillLocationList(SortLocationByCountry(lokDrzavaSortAscending));
		}
		#endregion
		#region Knjige U Lokalu View
		private bool kulKnjigaSortAscending = true;
		private bool kulAutorSortAscending = false;
		private bool kulIzdKucaaSortAscending = false;
		private bool kulDatIzdSortAscending = false;
		private bool kulZanrSortAscending = false;
		private bool kulJezikSortAscending = false;
		private bool kulOgrSortAscending = false;

		private void btnKULKnjiga_Click(object sender, RoutedEventArgs e)
		{
			kulKnjigaSortAscending = !kulKnjigaSortAscending;

			imgKULKnjigaSort.Source = kulKnjigaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByName(kulKnjigaSortAscending));
		}

		private void btnKULAutor_Click(object sender, RoutedEventArgs e)
		{
			kulAutorSortAscending = !kulAutorSortAscending;

			imgKULAutorSort.Source = kulAutorSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByAuthor(kulAutorSortAscending));
		}

		private void btnKULIzdKuca_Click(object sender, RoutedEventArgs e)
		{
			kulIzdKucaaSortAscending = !kulIzdKucaaSortAscending;

			imgKULIzdKucaSort.Source = kulIzdKucaaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByPublisher(kulIzdKucaaSortAscending));
		}

		private void btnKULDatIzd_Click(object sender, RoutedEventArgs e)
		{
			kulDatIzdSortAscending = !kulDatIzdSortAscending;

			imgKULDatIzdSort.Source = kulDatIzdSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByPublishDate(kulDatIzdSortAscending));
		}

		private void btnKULZanr_Click(object sender, RoutedEventArgs e)
		{
			kulZanrSortAscending = !kulZanrSortAscending;

			imgKULZanrSort.Source = kulZanrSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByGenra(kulZanrSortAscending));
		}

		private void btnKULJezik_Click(object sender, RoutedEventArgs e)
		{
			kulJezikSortAscending = !kulJezikSortAscending;

			imgKULJezikSort.Source = kulJezikSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByLanguage(kulJezikSortAscending));
		}

		private void btnKULOgr_Click(object sender, RoutedEventArgs e)
		{
			kulOgrSortAscending = !kulOgrSortAscending;

			imgKULOgrSort.Source = kulOgrSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillBooksOnLocationList(SortBooksOnLocationByLimited(kulOgrSortAscending));
		}
		private bool kulNovineSortAscending = true;
		private bool kulNovinePeriodSortAscending = false;
		private bool kulNovineCenaSortAscending = false;
		private bool kulNovineKolicinaSortAscending = false;

		private void btnKULNovine_Click(object sender, RoutedEventArgs e)
		{
			kulNovineSortAscending = !kulNovineSortAscending;

			imgKULNovineSort.Source = kulNovineSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillNewsOnLocationList(SortNewsOnLocationByName(kulNovineSortAscending));
		}

		private void btnKULNovinePeriod_Click(object sender, RoutedEventArgs e)
		{
			kulNovinePeriodSortAscending = !kulNovinePeriodSortAscending;

			imgKULNovinePeriodSort.Source = kulNovinePeriodSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillNewsOnLocationList(SortNewsOnLocationByPeriod(kulNovinePeriodSortAscending));
		}

		private void btnKULNovineCena_Click(object sender, RoutedEventArgs e)
		{
			kulNovineCenaSortAscending = !kulNovineCenaSortAscending;

			imgKULNovineCenaSort.Source = kulNovineCenaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillNewsOnLocationList(SortNewsOnLocationByPrice(kulNovineCenaSortAscending));
		}

		private void btnKULNovineKolicina_Click(object sender, RoutedEventArgs e)
		{
			kulNovineKolicinaSortAscending = !kulNovineKolicinaSortAscending;

			imgKULNovineKolicinaSort.Source = kulNovineKolicinaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillNewsOnLocationList(SortNewsOnLocationByAmount(kulNovineKolicinaSortAscending));
		}

		private bool kulMagazinSortAscending = true;
		private bool kulMagazinPeriodSortAscending = false;
		private bool kulMagazinCenaSortAscending = false;
		private bool kulMagazinKolicinaSortAscending = false;

		private void btnKULMagazin_Click(object sender, RoutedEventArgs e)
		{
			kulMagazinSortAscending = !kulMagazinSortAscending;

			imgKULMagazinSort.Source = kulMagazinSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillMagazinesOnLocationList(SortMagazinesOnLocationByName(kulMagazinSortAscending));
		}

		private void btnKULMagazinPeriod_Click(object sender, RoutedEventArgs e)
		{
			kulMagazinPeriodSortAscending = !kulMagazinPeriodSortAscending;

			imgKULMagazinPeriodSort.Source = kulMagazinPeriodSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillMagazinesOnLocationList(SortMagazinesOnLocationByPeriod(kulMagazinPeriodSortAscending));
		}

		private void btnKULMagazinCena_Click(object sender, RoutedEventArgs e)
		{
			kulMagazinCenaSortAscending = !kulMagazinCenaSortAscending;

			imgKULMagazinCenaSort.Source = kulMagazinCenaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillMagazinesOnLocationList(SortMagazinesOnLocationByPrice(kulMagazinCenaSortAscending));
		}

		private void btnKULMagazinKolicina_Click(object sender, RoutedEventArgs e)
		{
			kulMagazinKolicinaSortAscending = !kulMagazinKolicinaSortAscending;

			imgKULMagazinKolicinaSort.Source = kulMagazinKolicinaSortAscending ? new BitmapImage(new Uri(arrowDownImg)) : new BitmapImage(new Uri(arrowUpImg));

			//FillMagazinesOnLocationList(SortMagazinesOnLocationByAmount(kulMagazinKolicinaSortAscending));
		}
		#endregion
		#endregion
		#region Main View
		//private List<KnjigaRezView> SortBookReservationsByName(bool ascending = true)
		//{
		//	List<KnjigaRezView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<KnjigaRezView>();
		//	else
		//		sorted = new List<KnjigaRezView>(KnjigeCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaRezView temp = new KnjigaRezView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaRezView> SortBookReservationsByAuthor(bool ascending = true)
		//{
		//	List<KnjigaRezView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<KnjigaRezView>();
		//	else
		//		sorted = new List<KnjigaRezView>(KnjigeCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaRezView temp = new KnjigaRezView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaRezView> SortBookReservationsByDate(bool ascending = true)
		//{
		//	List<KnjigaRezView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<KnjigaRezView>();
		//	else
		//		sorted = new List<KnjigaRezView>(KnjigeCollection);


		//	if (sorted.Count > 1)
		//	{
		//		KnjigaRezView temp = new KnjigaRezView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].Datum) > DateConverter.ToDateTime(sorted[j].Datum))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].Datum) < DateConverter.ToDateTime(sorted[j].Datum))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}

		//private List<IstorijaKnjigaView> SortBooksHistoryByName(bool ascending = true)
		//{
		//	List<IstorijaKnjigaView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<IstorijaKnjigaView>();
		//	else
		//		sorted = new List<IstorijaKnjigaView>(IstorijaKnjigeCollection);

		//	if (sorted.Count > 1)
		//	{
		//		IstorijaKnjigaView temp = new IstorijaKnjigaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<IstorijaKnjigaView> SortBooksHistoryByAuthor(bool ascending = true)
		//{
		//	List<IstorijaKnjigaView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<IstorijaKnjigaView>();
		//	else
		//		sorted = new List<IstorijaKnjigaView>(IstorijaKnjigeCollection);

		//	if (sorted.Count > 1)
		//	{
		//		IstorijaKnjigaView temp = new IstorijaKnjigaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<IstorijaKnjigaView> SortBooksHistoryByDateCheckOut(bool ascending = true)
		//{
		//	List<IstorijaKnjigaView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<IstorijaKnjigaView>();
		//	else
		//		sorted = new List<IstorijaKnjigaView>(IstorijaKnjigeCollection);


		//	if (sorted.Count > 1)
		//	{
		//		IstorijaKnjigaView temp = new IstorijaKnjigaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatumUzeo) > DateConverter.ToDateTime(sorted[j].DatumUzeo))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatumUzeo) < DateConverter.ToDateTime(sorted[j].DatumUzeo))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<IstorijaKnjigaView> SortBooksHistoryByDateCheckIn(bool ascending = true)
		//{
		//	List<IstorijaKnjigaView> sorted;
		//	if (KnjigeCollection == null)
		//		sorted = new List<IstorijaKnjigaView>();
		//	else
		//		sorted = new List<IstorijaKnjigaView>(IstorijaKnjigeCollection);


		//	if (sorted.Count > 1)
		//	{
		//		IstorijaKnjigaView temp = new IstorijaKnjigaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatumVratio) > DateConverter.ToDateTime(sorted[j].DatumVratio))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatumVratio) < DateConverter.ToDateTime(sorted[j].DatumVratio))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}

		//private List<LokacijaView> SortLocationByAddress(bool ascending = true)
		//{
		//	List<LokacijaView> sorted;
		//	if (LokacijaCollection == null)
		//		sorted = new List<LokacijaView>();
		//	else
		//		sorted = new List<LokacijaView>(LokacijaCollection);

		//	if (sorted.Count > 1)
		//	{
		//		LokacijaView temp = new LokacijaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Adresa.Normalize(), sorted[j].Adresa.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Adresa.Normalize(), sorted[j].Adresa.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<LokacijaView> SortLocationByCity(bool ascending = true)
		//{
		//	List<LokacijaView> sorted;
		//	if (LokacijaCollection == null)
		//		sorted = new List<LokacijaView>();
		//	else
		//		sorted = new List<LokacijaView>(LokacijaCollection);

		//	if (sorted.Count > 1)
		//	{
		//		LokacijaView temp = new LokacijaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Grad.Normalize(), sorted[j].Grad.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Grad.Normalize(), sorted[j].Grad.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<LokacijaView> SortLocationByCountry(bool ascending = true)
		//{
		//	List<LokacijaView> sorted;
		//	if (LokacijaCollection == null)
		//		sorted = new List<LokacijaView>();
		//	else
		//		sorted = new List<LokacijaView>(LokacijaCollection);

		//	if (sorted.Count > 1)
		//	{
		//		LokacijaView temp = new LokacijaView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Drzava.Normalize(), sorted[j].Drzava.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Drzava.Normalize(), sorted[j].Drzava.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//#endregion
		//#region Knjige U Lokalu View
		//// KNJIGE
		//private List<KnjigaULokaluView> SortBooksOnLocationByName(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByAuthor(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Autor.Normalize(), sorted[j].Autor.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByPublisher(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].IzdKuca.Normalize(), sorted[j].IzdKuca.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].IzdKuca.Normalize(), sorted[j].IzdKuca.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByPublishDate(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatIzd) > DateConverter.ToDateTime(sorted[j].DatIzd))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (DateConverter.ToDateTime(sorted[i].DatIzd) < DateConverter.ToDateTime(sorted[j].DatIzd))
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByGenra(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Zanr.Normalize(), sorted[j].Zanr.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Zanr.Normalize(), sorted[j].Zanr.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByLanguage(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Jezik.Normalize(), sorted[j].Jezik.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Jezik.Normalize(), sorted[j].Jezik.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<KnjigaULokaluView> SortBooksOnLocationByLimited(bool ascending = true)
		//{
		//	List<KnjigaULokaluView> sorted;
		//	if (KnjigeULokaluCollection == null)
		//		sorted = new List<KnjigaULokaluView>();
		//	else
		//		sorted = new List<KnjigaULokaluView>(KnjigeULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		KnjigaULokaluView temp = new KnjigaULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				if (sorted[i].Ogr) continue;
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (sorted[j].Ogr)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//						break;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				if (!sorted[i].Ogr) continue;
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (!sorted[j].Ogr)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//						break;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}

		////NOVINE
		//private List<NovineULokaluView> SortNewsOnLocationByName(bool ascending = true)
		//{
		//	List<NovineULokaluView> sorted;
		//	if (NovineULokaluCollection == null)
		//		sorted = new List<NovineULokaluView>();
		//	else
		//		sorted = new List<NovineULokaluView>(NovineULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		NovineULokaluView temp = new NovineULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<NovineULokaluView> SortNewsOnLocationByPeriod(bool ascending = true)
		//{
		//	List<NovineULokaluView> sorted;
		//	if (NovineULokaluCollection == null)
		//		sorted = new List<NovineULokaluView>();
		//	else
		//		sorted = new List<NovineULokaluView>(NovineULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		NovineULokaluView temp = new NovineULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (sorted[i].PerInt > sorted[j].PerInt)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (sorted[i].PerInt < sorted[j].PerInt)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<NovineULokaluView> SortNewsOnLocationByPrice(bool ascending = true)
		//{
		//	List<NovineULokaluView> sorted;
		//	if (NovineULokaluCollection == null)
		//		sorted = new List<NovineULokaluView>();
		//	else
		//		sorted = new List<NovineULokaluView>(NovineULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		NovineULokaluView temp = new NovineULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].CenaView.Normalize(), sorted[j].CenaView.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].CenaView.Normalize(), sorted[j].CenaView.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<NovineULokaluView> SortNewsOnLocationByAmount(bool ascending = true)
		//{
		//	List<NovineULokaluView> sorted;
		//	if (NovineULokaluCollection == null)
		//		sorted = new List<NovineULokaluView>();
		//	else
		//		sorted = new List<NovineULokaluView>(NovineULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		NovineULokaluView temp = new NovineULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Kolicina.Normalize(), sorted[j].Kolicina.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Kolicina.Normalize(), sorted[j].Kolicina.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}

		////MAGAZINI
		//private List<MagazinULokaluView> SortMagazinesOnLocationByName(bool ascending = true)
		//{
		//	List<MagazinULokaluView> sorted;
		//	if (MagazinULokaluCollection == null)
		//		sorted = new List<MagazinULokaluView>();
		//	else
		//		sorted = new List<MagazinULokaluView>(MagazinULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		MagazinULokaluView temp = new MagazinULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Naziv.Normalize(), sorted[j].Naziv.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<MagazinULokaluView> SortMagazinesOnLocationByPeriod(bool ascending = true)
		//{
		//	List<MagazinULokaluView> sorted;
		//	if (MagazinULokaluCollection == null)
		//		sorted = new List<MagazinULokaluView>();
		//	else
		//		sorted = new List<MagazinULokaluView>(MagazinULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		MagazinULokaluView temp = new MagazinULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (sorted[i].PerInt > sorted[j].PerInt)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (sorted[i].PerInt < sorted[j].PerInt)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<MagazinULokaluView> SortMagazinesOnLocationByPrice(bool ascending = true)
		//{
		//	List<MagazinULokaluView> sorted;
		//	if (MagazinULokaluCollection == null)
		//		sorted = new List<MagazinULokaluView>();
		//	else
		//		sorted = new List<MagazinULokaluView>(MagazinULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		MagazinULokaluView temp = new MagazinULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].CenaView.Normalize(), sorted[j].CenaView.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].CenaView.Normalize(), sorted[j].CenaView.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}
		//private List<MagazinULokaluView> SortMagazinesOnLocationByAmount(bool ascending = true)
		//{
		//	List<MagazinULokaluView> sorted;
		//	if (MagazinULokaluCollection == null)
		//		sorted = new List<MagazinULokaluView>();
		//	else
		//		sorted = new List<MagazinULokaluView>(MagazinULokaluCollection);

		//	if (sorted.Count > 1)
		//	{
		//		MagazinULokaluView temp = new MagazinULokaluView();
		//		if (ascending)
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Kolicina.Normalize(), sorted[j].Kolicina.Normalize()) > 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			for (int i = 0; i < sorted.Count - 1; i++)
		//			{
		//				for (int j = i + 1; j < sorted.Count; j++)
		//				{
		//					if (string.Compare(sorted[i].Kolicina.Normalize(), sorted[j].Kolicina.Normalize()) < 0)
		//					{
		//						temp = sorted[i];
		//						sorted[i] = sorted[j];
		//						sorted[j] = temp;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return sorted;
		//}


		#endregion
		#endregion




		#region Navigation Controls
		private void BackClick(object sender, RoutedEventArgs e)
		{
			GoToMainView();
		}
		#region Event Handlers
		#region Main View
		// LISTE
		private void Lokacije_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as LokacijaView;

			//if (item != null)
			//{
			//	SelectedLocation = item;
			//	GoToLokalView(item.ID);
			//}
		}
		private void IstorijaRezervacijaList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as IstorijaKnjigaView;

			//if (item != null)
			//{
			//	MessageBox.Show($"Item's ID is {item.ID}");
			//}
		}
		private void RezervacijeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as KnjigaRezView;

			//if (item != null)
			//{
			//	MessageBox.Show($"Item's ID is {item.ID}");
			//}
		}

		// DUGMICI
		private void mvBtnZatraziKnjigu_Click(object sender, RoutedEventArgs e)
		{
			//GoToZatraziKnjiguView();
		}
		private void mvBtnZatraziNovine_Click(object sender, RoutedEventArgs e)
		{
			//GoToZatraziNovineView();
		}
		private void mvBtnZatraziMagazin_Click(object sender, RoutedEventArgs e)
		{
			//GoToZatraziMagazinView();
		}
		#endregion
		#region Knjige u Lokalu View
		private void kulBtnBack_Click(object sender, RoutedEventArgs e)
		{
			GoToMainView();
		}
		private void kulCbListSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var item = ((FrameworkElement)e.OriginalSource) as ComboBox;


			if (item != null)
			{
				ComboBoxItem selItem = item.SelectedItem as ComboBoxItem;
				//if (selItem != null && SelectedLocation != null)
				//{
				//	switch (selItem.Tag.ToString())
				//	{
				//		case "knjiga":
				//			GoToLokalViewBooks(SelectedLocation.ID); break;
				//		case "novine":
				//			GoToLokalViewNewspapers(SelectedLocation.ID); break;
				//		case "magazin":
				//			GoToLokalViewMagazines(SelectedLocation.ID); break;
				//		default: break;
				//	}
				//}
			}
		}
		private void btnNotifications_Click(object sender, RoutedEventArgs e)
		{
			//GoToNotificationView();
		}
		#endregion
		#region Zatrazi View

		#endregion
		#endregion




		#region View Navigation
		private void TurnOnView(Grid view)
		{
			//if (CurrentView == view) return;
			CurrentView.Visibility = Visibility.Collapsed;
			view.Visibility = Visibility.Visible;
			CurrentView = view;
		}
		private void TurnOnStackPanel(StackPanel panel)
		{
			//if (CurrentPanel == panel) return;
			CurrentPanel.Visibility = Visibility.Collapsed;
			panel.Visibility = Visibility.Visible;
			CurrentPanel = panel;
		}

		#region Switch Views
		private void GoToMainView()
		{
			UpdateViews();

			TurnOnView(viewHomePage);
		}
		//private void GoToKatalogKnjigaView()
		//{
		//	List<Knjigaulokalu> knjigaulokalus = new List<Knjigaulokalu>();
		//	foreach (Cornerlibrary l in DBHelper.GetAllStores())
		//	{
		//		knjigaulokalus.AddRange(DBHelper.GetBooksInStore(l.Id));
		//	}

		//	List<KatalogKnjigaView> katalogKnjigaViews = new List<KatalogKnjigaView>();

		//	foreach (Knjigaulokalu kul in knjigaulokalus)
		//	{
		//		Knjiga k = DBHelper.GetBook(kul.Idk);
		//		Cornerlibrary l = DBHelper.GetStore(kul.Idl);

		//		katalogKnjigaViews.Add(new KatalogKnjigaView(k.Id, l.Id, k.Naziv, k.Autor, k.Jezik, k.Zanr, l.Adresa, l.Grad, l.Drzava));
		//	}

		//	FillKatalogKnjigaList(katalogKnjigaViews);

		//	TurnOnView(viewKatalogKnjiga);
		//}
		//private void GoToKatalogNovinaView()
		//{
		//	List<Novineulokalu> novineulokalus = new List<Novineulokalu>();
		//	foreach (Cornerlibrary l in DBHelper.GetAllStores())
		//	{
		//		novineulokalus.AddRange(DBHelper.GetNewsInStore(l.Id));
		//	}

		//	List<KatalogNovineMagazinView> katalogView = new List<KatalogNovineMagazinView>();
		//	foreach(Novineulokalu nul in novineulokalus)
		//	{
		//		Novine n = DBHelper.GetNews(nul.Idn);
		//		Cornerlibrary l = DBHelper.GetStore(nul.Idl);

		//		katalogView.Add(new KatalogNovineMagazinView(n.Id, l.Id, n.NazivNov, n.Cena, l.Adresa, l.Grad, l.Drzava));
		//	}

		//	FillKatalogNovinaList(katalogView);

		//	TurnOnView(viewKatalogNovina);
		//}
		//private void GoToKatalogMagazinaView()
		//{
		//	List<Magazinulokalu> magazinulokalus = new List<Magazinulokalu>();
		//	foreach (Cornerlibrary l in DBHelper.GetAllStores())
		//	{
		//		magazinulokalus.AddRange(DBHelper.GetMagazinesInStore(l.Id));
		//	}

		//	List<KatalogNovineMagazinView> katalogView = new List<KatalogNovineMagazinView>();
		//	foreach (Magazinulokalu mul in magazinulokalus)
		//	{
		//		Magazin n = DBHelper.GetMagazin(mul.Idm);
		//		Cornerlibrary l = DBHelper.GetStore(mul.Idl);

		//		katalogView.Add(new KatalogNovineMagazinView(n.Id, l.Id, n.NazivMag, n.Cena, l.Adresa, l.Grad, l.Drzava));
		//	}

		//	FillKatalogMagazinaList(katalogView);

		//	TurnOnView(viewKatalogMagazina);
		//}
		//private void GoToLokalView(int lokalID)
		//{
		//	kulAdresa.Content = SelectedLocation.Adresa;
		//	kulGrad.Content = SelectedLocation.Grad;
		//	kulDrzava.Content = SelectedLocation.Drzava;

		//	// TODO: Get list of books for selected location
		//	List<Knjigaulokalu> kul = DBHelper.GetBooksInStore(lokalID);
		//	KnjigeULokaluCollection.Clear();
		//	foreach (Knjigaulokalu k in kul)
		//	{
		//		KnjigeULokaluCollection.Add(new KnjigaULokaluView(k));
		//	}

		//	GoToLokalViewBooks(lokalID);
		//	kulCbListSelection.SelectedIndex = 0;

		//	UpdateViews();

		//	TurnOnView(viewKnjigeNaLokaciji);
		//}
		//private void GoToNotificationView()
		//{
		//	NotificationWindow notificationWindow = new NotificationWindow(this);

		//	Nullable<bool> dialogResult = notificationWindow.ShowDialog();
		//}

		//private void GoToZatraziKnjiguView()
		//{
		//	tbZkAutorKnjige.Text = string.Empty;
		//	tbZkNazivKnjige.Text = string.Empty;
		//	cbZkLokalKnjige.SelectedIndex = 0;

		//	TurnOnView(viewZatraziKnjigu);
		//}
		//private void GoToZatraziNovineView()
		//{
		//	tbZkNazivNovina.Text = string.Empty;
		//	cbZkLokalNovine.SelectedIndex = 0;

		//	TurnOnView(viewZatraziNovine);
		//}
		//private void GoToZatraziMagazinView()
		//{
		//	tbZmNazivMagazina.Text = string.Empty;
		//	cbZmLokalMagazin.SelectedIndex = 0;

		//	TurnOnView(viewZatraziMagazin);
		//}
		//#endregion

		//#region Interview navigation
		//private void GoToLokalViewBooks(int lokalID)
		//{
		//	KnjigeULokaluCollection.Clear();
		//	foreach(Knjigaulokalu kul in DBHelper.GetBooksInStore(lokalID))
		//	{
		//		KnjigeULokaluCollection.Add(new KnjigaULokaluView(kul));
		//	}
		//	FillBooksOnLocationList(SortBooksOnLocationByName());
		//	TurnOnStackPanel(kulSpKnjige);
		//}
		//private void GoToLokalViewNewspapers(int lokalID)
		//{
		//	NovineULokaluCollection.Clear();
		//	foreach(Novineulokalu nul in DBHelper.GetNewsInStore(lokalID))
		//	{
		//		NovineULokaluCollection.Add(new NovineULokaluView(nul));
		//	}
		//	FillNewsOnLocationList(SortNewsOnLocationByName());
		//	TurnOnStackPanel(kulSpNovine);
		//}
		//private void GoToLokalViewMagazines(int lokalID)
		//{
		//	MagazinULokaluCollection.Clear();
		//	foreach(Magazinulokalu mul in DBHelper.GetMagazinesInStore(lokalID))
		//	{
		//		MagazinULokaluCollection.Add(new MagazinULokaluView(mul));
		//	}
		//	FillMagazinesOnLocationList(SortMagazinesOnLocationByName());
		//	TurnOnStackPanel(kulSpMagazini);
		//}
		#endregion
		#endregion


		#endregion


		#region DB Controls
		#region Zatrazi
		//Zatrazi Knjigu
		private void btnZkZatrazi_Click(object sender, RoutedEventArgs e)
		{
			//LokacijaView lokal = cbZkLokalKnjige.SelectedItem as LokacijaView;

			//if (tbZkNazivKnjige.Text == "")
			//{
			//	MessageBox.Show("Morate izabrati koju knjigu želite da dobijete!");
			//	return;
			//}
			//if (tbZkAutorKnjige.Text == "")
			//{
			//	MessageBox.Show("Morate izabrati autora knjige koju želite da dobijete!");
			//	return;
			//}
			//if (lokal == null)
			//{
			//	MessageBox.Show("Morate izabrati u kom lokalu želite da dobijete knjigu!");
			//	return;
			//}

			//Zahtevzaknjigu zahtevZaKnjigu = new Zahtevzaknjigu(tbZkNazivKnjige.Text, tbZkAutorKnjige.Text, lokal.ID);
			//Clanzahtevaknjigu clanzahtevaknjigu = new Clanzahtevaknjigu(CurrentUser.Id, zahtevZaKnjigu.NazK, zahtevZaKnjigu.Autor, zahtevZaKnjigu.Lokal);
			////TODO: Posalji zahtevZaKnjigu i clanZahtevaKnjigu u BP
			//// Proveri da li zahtevZaKnjigu vec postoji
			//// Proveri da li clanZahtevaKnjigu vec postoji

			//MessageBox.Show($"Uspesno ste poslali zahtev!");
			//GoToMainView();
		}
		//Zatrazi Novine
		private void btnZnZatrazi_Click(object sender, RoutedEventArgs e)
		{
			//LokacijaView lokal = cbZkLokalNovine.SelectedItem as LokacijaView;

			//if (tbZkNazivNovina.Text == "")
			//{
			//	MessageBox.Show("Morate izabrati koje novine želite da dobijete!");
			//	return;
			//}
			//if (lokal == null)
			//{
			//	MessageBox.Show("Morate izabrati u kom lokalu želite da dobijete novine!");
			//	return;
			//}

			//Zahtevzanovine zahtevzanovine = new Zahtevzanovine(tbZkNazivNovina.Text, lokal.ID);
			//Clanzahtevanovine clanzahtevanovine = new Clanzahtevanovine(CurrentUser.Id, zahtevzanovine.NazN, zahtevzanovine.Lokal);
			////TODO: Posalji zahtevZaKnjigu i clanZahtevaKnjigu u BP
			//// Proveri da li zahtevZaKnjigu vec postoji
			//// Proveri da li clanZahtevaKnjigu vec postoji

			//MessageBox.Show($"Uspesno ste poslali zahtev!");
			//GoToMainView();
		}
		//Zatrazi Magazin
		private void btnZmZatrazi_Click(object sender, RoutedEventArgs e)
		{
			//LokacijaView lokal = cbZmLokalMagazin.SelectedItem as LokacijaView;

			//if (tbZmNazivMagazina.Text == "")
			//{
			//	MessageBox.Show("Morate izabrati koji magazin želite da dobijete!");
			//	return;
			//}
			//if (lokal == null)
			//{
			//	MessageBox.Show("Morate izabrati u kom lokalu želite da dobijete magazin!");
			//	return;
			//}

			//Zahtevzamagazin zahtevzamagazin = new Zahtevzamagazin(tbZmNazivMagazina.Text, lokal.ID);
			//Clanzahtevamagazin clanzahtevanovine = new Clanzahtevamagazin(CurrentUser.Id, zahtevzamagazin.NazM, zahtevzamagazin.Lokal);
			////TODO: Posalji zahtevZaKnjigu i clanZahtevaKnjigu u BP
			//// Proveri da li zahtevZaKnjigu vec postoji
			//// Proveri da li clanZahtevaKnjigu vec postoji

			//MessageBox.Show($"Uspesno ste poslali zahtev!");
			//GoToMainView();
		}

		private void mvBtnKatalogKnjiga_Click(object sender, RoutedEventArgs e)
		{
			//GoToKatalogKnjigaView();
		}

		private void mvBtnKatalogNovina_Click(object sender, RoutedEventArgs e)
		{
			//GoToKatalogNovinaView();
		}

		private void mvBtnKatalogMagazina_Click(object sender, RoutedEventArgs e)
		{
			//GoToKatalogMagazinaView();
		}
		#endregion


		#region Knjige u lokalu
		private void KnjigeULokaluList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as KnjigaULokaluView;

			//if (item != null)
			//{
			//	if (int.Parse(item.Kolicina) > 0)
			//	{
			//		// Instantiate window
			//		ConfirmBookRezervationWindow dialogBox = new ConfirmBookRezervationWindow(item.Naziv, item.Autor, SelectedLocation.Adresa, SelectedLocation.Grad, SelectedLocation.Drzava, item.ID, SelectedLocation.ID, this);

			//		// Show window modally
			//		// NOTE: Returns only when window is closed
			//		Nullable<bool> dialogResult = dialogBox.ShowDialog();
			//	}
			//	else
			//	{
			//		MessageBox.Show("Trenutno nema ove knjige na stanju.");
			//	}
			//}
		}
		public void ConfirmBookRezervation(int bookID, int lokalID)
		{
			//Knjiga knjiga = DBHelper.GetBook(bookID);

			//float cena;
			//// Get edition price
			//if (knjiga.Ogr)
			//{
			//	cena = DBHelper.GetEditionPrice(iIzdanje.Ograniceno);
			//}
			//else
			//{
			//	DateTime newEdditionLimit = DateTime.Now.AddYears(-editionLimit);
			//	double dateDifference = (knjiga.DatIzd - newEdditionLimit).TotalSeconds;

			//	if (dateDifference > 0)
			//		cena = DBHelper.GetEditionPrice(iIzdanje.Novo);
			//	else
			//		cena = DBHelper.GetEditionPrice(iIzdanje.Staro);
			//}

			////Check credit
			//if (CurrentUser.Kredit >= cena)
			//{
			//	Rezervacija rez = new Rezervacija(CurrentUser.Id, bookID, lokalID);

			//	iDbResult result = DBHelper.AddReservation(rez);

			//	if (result == iDbResult.Success)
			//	{
			//		Knjigaulokalu kul = DBHelper.GetBookInStore(bookID, lokalID);
			//		kul.Kolicina--;
			//		DBHelper.ModifyBookInStoreAmount(kul);

			//		MessageBox.Show($"Uspesno ste rezervisali knjigu id:{bookID}, u lokalu: {lokalID}");
			//	}
			//	else if (result == iDbResult.Error)
			//	{
			//		MessageBox.Show($"Došlo je do neočekivane greške...");
			//	}
			//	else if (result == iDbResult.Duplicate)
			//	{
			//		MessageBox.Show("Već ste rezervisali ovu knjigu!");
			//	}

			//}
			//else
			//{
			//	MessageBox.Show("Nemate dovoljno sredstava za rezervaciju.");
			//}
		}
		private void NovineULokaluList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as NovineULokaluView;

			//if (item != null)
			//{
			//	if (int.Parse(item.Kolicina) > 0)
			//	{
			//		// Instantiate window
			//		ConfirmNewsRezervationWindow dialogBox = new ConfirmNewsRezervationWindow(item.Naziv, SelectedLocation.Adresa, SelectedLocation.Grad, SelectedLocation.Drzava, item.Cena, item.ID, SelectedLocation.ID, this);

			//		// Show window modally
			//		// NOTE: Returns only when window is closed
			//		Nullable<bool> dialogResult = dialogBox.ShowDialog();
			//	}
			//	else
			//	{
			//		MessageBox.Show("Trenutno nema ovih novina na stanju.");
			//	}
			//}
		}
		public void ConfirmNewsRezervation(int novineID, int lokalID)
		{
			//Novine novine = DBHelper.GetNews(novineID);

			//float cena = novine.Cena;

			////Check credit
			//if (CurrentUser.Kredit >= cena)
			//{
			//	Novineulokalu nul = DBHelper.GetNewsInStore(novineID, lokalID);
			//	nul.Kolicina--;
			//	iDbResult result = DBHelper.ModifyNewsInStoreAmount(nul);

			//	if (result == iDbResult.Success)
			//	{
			//		CurrentUser.Kredit -= cena;
			//		if (DBHelper.ModifyUserCredit(CurrentUser) == iDbResult.Success)
			//		{
			//			DBHelper.AddNewsPurchase(CurrentUser.Id, novineID);
			//			MessageBox.Show("Uspešno ste kupili novine!");
			//		}
			//		else
			//		{
			//			// Rollback
			//			CurrentUser.Kredit += cena;
			//			nul.Kolicina++;
			//			DBHelper.ModifyNewsInStoreAmount(nul);
			//			DBHelper.ModifyUserCredit(CurrentUser);
			//			//Notify
			//			MessageBox.Show($"Došlo je do neočekivane greške...");
			//		}
			//	}
			//	else
			//	{
			//		MessageBox.Show($"Došlo je do neočekivane greške...");
			//	}
			//}
			//else
			//{
			//	MessageBox.Show("Nemate dovoljno sredstava za kupovinu.");
			//}
		}
		private void MagazinULokaluList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as MagazinULokaluView;

			//if (item != null)
			//{
			//	if (int.Parse(item.Kolicina) > 0)
			//	{
			//		// Instantiate window
			//		ConfirmMagazineRezervationWindow dialogBox = new ConfirmMagazineRezervationWindow(item.Naziv, SelectedLocation.Adresa, SelectedLocation.Grad, SelectedLocation.Drzava, item.Cena, item.ID, SelectedLocation.ID, this);

			//		// Show window modally
			//		// NOTE: Returns only when window is closed
			//		Nullable<bool> dialogResult = dialogBox.ShowDialog();
			//	}
			//	else
			//	{
			//		MessageBox.Show("Trenutno nema ovog magazina na stanju.");
			//	}
			//}
		}
		public void ConfirmMagazineRezervation(int magazinID, int lokalID)
		{
			//Magazin magazin = DBHelper.GetMagazin(magazinID);

			//float cena = magazin.Cena;

			////Check credit
			//if (CurrentUser.Kredit >= cena)
			//{
			//	Magazinulokalu mul = DBHelper.GetMagazineInStore(magazinID, lokalID);
			//	mul.Kolicina--;
			//	iDbResult result = DBHelper.ModifyMagazineInStoreAmount(mul);

			//	if (result == iDbResult.Success)
			//	{
			//		CurrentUser.Kredit -= cena;
			//		if (DBHelper.ModifyUserCredit(CurrentUser) == iDbResult.Success)
			//		{
			//			DBHelper.AddMagazinePurchase(CurrentUser.Id, magazinID);
			//			MessageBox.Show("Uspešno ste kupili magazin!");
			//		}
			//		else
			//		{
			//			// Rollback
			//			CurrentUser.Kredit += cena;
			//			mul.Kolicina++;
			//			DBHelper.ModifyMagazineInStoreAmount(mul);
			//			DBHelper.ModifyUserCredit(CurrentUser);
			//			//Notify
			//			MessageBox.Show($"Došlo je do neočekivane greške...");
			//		}
			//	}
			//	else
			//	{
			//		MessageBox.Show($"Došlo je do neočekivane greške...");
			//	}
			//}
			//else
			//{
			//	MessageBox.Show("Nemate dovoljno sredstava za kupovinu.");
			//}
		}
		#endregion

		#endregion

		#region Katalog Sorting
		private void btnKatalogKnjiga_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogAutor_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogJezik_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogZanr_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogAdresa_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogGrad_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogDrzava_Click(object sender, RoutedEventArgs e)
		{

		}
		private void btnKatalogNovine_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogNovCena_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogNovAdresa_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogNovGrad_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogNovDrzava_Click(object sender, RoutedEventArgs e)
		{

		}


		private void btnKatalogMagazin_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogMagCena_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogMagAdresa_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogMagGrad_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnKatalogMagDrzava_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion

		private void KatalogKnjigaList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//var item = ((FrameworkElement)e.OriginalSource).DataContext as KatalogKnjigaView;

			//if (item != null)
			//{
			//	if (DBHelper.GetBookInStore(item.KnjigaID, item.LokalID).Kolicina <= 0)
			//	{
			//		MessageBox.Show("Trenutno nema ove knjige na stanju.");
			//	}
			//	else if(DBHelper.GetReservation(CurrentUser.Id, item.KnjigaID, item.LokalID) != null)
			//	{
			//		MessageBox.Show("Već ste rezervisali ovu knjigu.");
			//	}
			//	else
			//	{
			//		// Instantiate window
			//		ConfirmBookRezervationWindow dialogBox = new ConfirmBookRezervationWindow(item.Naziv, item.Autor, item.Adresa, item.Grad, item.Drzava, item.KnjigaID, item.LokalID, this);

			//		// Show window modally
			//		// NOTE: Returns only when window is closed
			//		Nullable<bool> dialogResult = dialogBox.ShowDialog();
			//	}
			//}
		}

		private void KatalogNovinaList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void KatalogMagazinaList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void btnCallCourier_Click(object sender, RoutedEventArgs e)
		{

        }
    }
}
#region Junk

#endregion