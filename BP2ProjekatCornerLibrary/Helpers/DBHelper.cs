using BP2ProjekatCornerLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BP2ProjekatCornerLibrary.Models { }
namespace BP2ProjekatCornerLibrary.Helpers
{
	public static class DBHelper
	{
		#region Test
		#endregion
		public static DbContextOptionsBuilder<CornerLibraryDbContext> optionBuilder;
		public static CornerLibraryDbContext db;
		public static void InitializeConnection()
		{
			optionBuilder = new DbContextOptionsBuilder<CornerLibraryDbContext>();
			optionBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CornerLibraryDbConnString"].ConnectionString);
			db = new CornerLibraryDbContext(optionBuilder.Options);
		}
		public static object ExecuteQuery(string query)
		{
			return query;
		}
		#region Getters
		public static int GetFirstFreeUserID()
		{
			//using (var db = new CornerLibraryDbContext(optionBuilder.Options))
			//{
			int id = db.Clans.First().IDClan;

			foreach (Clan clan in db.Clans)
			{
				if (clan.IDClan > id)
					id = clan.IDClan;
			}
			id++;
			if (id < 0)
			{
				id = db.Clans.First().IDClan;
				bool set = true;
				do
				{
					id++;
					set = true;
					foreach (Clan c in db.Clans)
					{
						if (c.IDClan == id)
						{
							set = false;
							break;
						}
					}
				} while (!set && id > 0);
			}
			return id;
			//}
		}
		public static Clan GetUser(int id)
		{
			//TODO: Get user
			//foreach (Clan c in MockDB.Instance.Clanovi)
			//{
			//	if (c.Id == id) return c;
			//}
			return null;
		}
		public static Clan GetUserByCard(int id)
		{
			//TODO: Get user
			//foreach (Clan c in MockDB.Instance.Clanovi)
			//{
			//	if (c.BrKartice == id) return c;
			//}
			return null;
		}
		public static Clan TryLoginUser(string username, string hashedPassword, out string message)
		{
			message = "";
			//List<Korisnik> lk = db.Korisniks.ToList();
			Clan c = db.Clans.FirstOrDefault(x => x.KorisnickoIme == username);
			if (c != null)
			{
				if (c.Sifra == hashedPassword)
					return c;
				else
				{
					message = "Pogrešna šifra!";
					return null;
				}
			}

			message = "Nepostojeći korisnik.";
			return null;
		}

		//public static Bibliotekar TryLoginBibliotekar(string username, string hashedPassword, out string message)
		//{
		//	message = "";
		//	var optionBuilder = new DbContextOptionsBuilder<CornerLibraryDbContext>();
		//	optionBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CornerLibraryDbConnString"].ConnectionString);
		//	using (var db = new CornerLibraryDbContext(optionBuilder.Options))
		//	{
		//		//List<Korisnik> lk = db.Korisniks.ToList();
		//		Bibliotekar c = db.Bibliotekars.FirstOrDefault(x => x.Username == username);
		//		if (c != null)
		//		{
		//			if (c.Pass == hashedPassword)
		//				return c;
		//			else
		//			{
		//				message = "Pogrešna šifra!";
		//				return null;
		//			}
		//		}
		//	}
		//	message = "Nepostojeći korisnik.";
		//	return null;
		//}



		public static float GetEditionPrice(iIzdanje edition)
		{
			switch (edition)
			{
				case iIzdanje.Ograniceno: return 3;
				case iIzdanje.Novo: return 2;
				case iIzdanje.Staro: return 1;
				default: return 0;
			}
		}

		// GET BOOK
		public static int GetFirstFreeBookID()
		{
			//int id = MockDB.Instance.Knjigas[0].Id;
			//for (int i = 1; i < MockDB.Instance.Knjigas.Count; i++)
			//{
			//	if (id < MockDB.Instance.Knjigas[i].Id)
			//		id = MockDB.Instance.Knjigas[i].Id;
			//}
			//id++;
			//if (id < 0)
			//{
			//	id = MockDB.Instance.Knjigas[0].Id;
			//	bool set = true;
			//	do
			//	{
			//		id++;
			//		set = true;
			//		foreach (Knjiga c in MockDB.Instance.Knjigas)
			//		{
			//			if (c.Id == id)
			//			{
			//				set = false;
			//				break;
			//			}
			//		}
			//	} while (!set && id > 0);
			//}
			return 1;
		}
		public static Knjiga GetBook(int bookID)
		{
			//TODO: Get book
			//foreach (Knjiga k in MockDB.Instance.Knjigas)
			//{
			//	if (k.Id == bookID)
			//		return k;
			//}
			return null;
		}
		public static KnjigaULokalu GetBookInStore(int bookID, int lokalID)
		{
			//TODO: get book in store
			//foreach (Knjigaulokalu k in MockDB.Instance.KUL)
			//{
			//	if (k.Idk == bookID && k.Idl == lokalID)
			//		return k;
			//}
			return null;
		}
		public static List<KnjigaULokalu> GetBooksInStore(int lokalID)
		{
			List<KnjigaULokalu> ret = new List<KnjigaULokalu>();

			//foreach (Knjigaulokalu k in MockDB.Instance.KUL)
			//{
			//	if (k.Idl == lokalID)
			//	{ ret.Add(k); }
			//}
			return ret;
		}

		// GET NEWS
		//public static int GetFirstFreeNewsID()
		//{
		//	//int id = MockDB.Instance.Novines[0].Id;
		//	//for (int i = 1; i < MockDB.Instance.Novines.Count; i++)
		//	//{
		//	//	if (id < MockDB.Instance.Novines[i].Id)
		//	//		id = MockDB.Instance.Novines[i].Id;
		//	//}
		//	//id++;
		//	//if (id < 0)
		//	//{
		//	//	id = MockDB.Instance.Novines[0].Id;
		//	//	bool set = true;
		//	//	do
		//	//	{
		//	//		id++;
		//	//		set = true;
		//	//		foreach (Novine c in MockDB.Instance.Novines)
		//	//		{
		//	//			if (c.Id == id)
		//	//			{
		//	//				set = false;
		//	//				break;
		//	//			}
		//	//		}
		//	//	} while (!set && id > 0);
		//	//}
		//	return 1;
		//}
		//public static Novine GetNews(int novineID)
		//{
		//	//TODO: Get news
		//	//foreach (Novine n in MockDB.Instance.Novines)
		//	//{
		//	//	if (n.Id == novineID)
		//	//		return n;
		//	//}
		//	return null;
		//}
		//public static Novineulokalu GetNewsInStore(int novineID, int lokalID)
		//{
		//	// TODO: Get news in store
		//	//foreach (Novineulokalu n in MockDB.Instance.NUL)
		//	//{
		//	//	if (n.Idn == novineID && n.Idl == lokalID)
		//	//		return n;
		//	//}
		//	return null;
		//}
		//public static List<Novineulokalu> GetNewsInStore(int lokalID)
		//{
		//	List<Novineulokalu> ret = new List<Novineulokalu>();

		//	//foreach (Novineulokalu k in MockDB.Instance.NUL)
		//	//{
		//	//	if (k.Idl == lokalID)
		//	//	{ ret.Add(k); }
		//	//}
		//	return ret;
		//}

		//// GET MAGAZINE
		//public static int GetFirstFreeMagazinID()
		//{
		//	//int id = MockDB.Instance.Magazines[0].Id;
		//	//for (int i = 1; i < MockDB.Instance.Magazines.Count; i++)
		//	//{
		//	//	if (id < MockDB.Instance.Magazines[i].Id)
		//	//		id = MockDB.Instance.Magazines[i].Id;
		//	//}
		//	//id++;
		//	//if (id < 0)
		//	//{
		//	//	id = MockDB.Instance.Magazines[0].Id;
		//	//	bool set = true;
		//	//	do
		//	//	{
		//	//		id++;
		//	//		set = true;
		//	//		foreach (Magazin c in MockDB.Instance.Magazines)
		//	//		{
		//	//			if (c.Id == id)
		//	//			{
		//	//				set = false;
		//	//				break;
		//	//			}
		//	//		}
		//	//	} while (!set && id > 0);
		//	//}
		//	return 1;
		//}
		//public static Magazin GetMagazin(int magID)
		//{
		//	// TODO: Get magazin
		//	//foreach (Magazin m in MockDB.Instance.Magazines)
		//	//{
		//	//	if (m.Id == magID)
		//	//	{ return m; }
		//	//}
		//	return null;
		//}
		//public static Magazinulokalu GetMagazineInStore(int magID, int lokalID)
		//{
		//	// TODO: Get magazine in store
		//	//foreach (Magazinulokalu m in MockDB.Instance.MUL)
		//	//{
		//	//	if (m.Idm == magID && m.Idl == lokalID)
		//	//	{
		//	//		return m;
		//	//	}
		//	//}
		//	return null;
		//}
		//public static List<Magazinulokalu> GetMagazinesInStore(int lokalID)
		//{
		//	List<Magazinulokalu> ret = new List<Magazinulokalu>();

		//	//foreach (Magazinulokalu k in MockDB.Instance.MUL)
		//	//{
		//	//	if (k.Idl == lokalID)
		//	//	{ ret.Add(k); }
		//	//}
		//	return ret;
		//}

		public static SerijskoStivo GetSerijskoStivo(int id)
		{
			//TODO: GET FROM DB
			List<SerijskoStivo> sstFDB = db.Serijskostivos.ToList();

            foreach (SerijskoStivo l in sstFDB)
			{
				if (l.IDSStivo == id)
				{
					return l;
				}
			}
			return null;
		}
		// GET STORE


        public static Cornerlibrary GetStore(int storeID)
		{
			// TODO: Get store
			//foreach (Cornerlibrary l in MockDB.Instance.Lokali)
			//{
			//	if (l.Id == storeID)
			//	{
			//		return l;
			//	}
			//}
			return null;
		}
		public static List<Cornerlibrary> GetAllStores()
		{
			//return MockDB.Instance.Lokali;
			return null;
		}

		// GET RESERVATIONS
		public static List<Rezervacija> GetReservations(int clanID)
		{
			List<Rezervacija> ret = new List<Rezervacija>();

			//foreach (Rezervacija r in MockDB.Instance.Rezervacije)
			//{
			//	if (r.Clan == clanID)
			//	{
			//		ret.Add(r);
			//	}
			//}
			return ret;
		}
		public static Rezervacija GetReservation(int clan, int book, int lokal)
		{
			//foreach (Rezervacija r in MockDB.Instance.Rezervacije)
			//{
			//	if (r.Clan == clan && r.Knjiga == book && r.Lokal == lokal)
			//		return r;
			//}
			return null;
		}
		public static List<Istorijarezervacija> GetReservationHistory(int clanID)
		{
			List<Istorijarezervacija> ret = new List<Istorijarezervacija>();

			//foreach (Istorijarezervacija i in MockDB.Instance.IstorijeRezervacija)
			//{
			//	if (i.Clan == clanID)
			//		ret.Add(i);
			//}
			return ret;
		}

		// GET Ispunjen zahtev za knjigu
		public static List<Ispunjenzahtevzaknjigu> GetFulfilledBookRequests()
		{
			// TODO: Get all fulfilled book requests
			//return MockDB.Instance.ispunjenzahtevzaknjigus;
			return null;
		}
		public static List<Ispunjenzahtevzaknjigu> GetFulfilledBookRequests(int clanID)
		{
			List<Ispunjenzahtevzaknjigu> ret = new List<Ispunjenzahtevzaknjigu>();

			foreach (Ispunjenzahtevzaknjigu izk in GetFulfilledBookRequests())
			{
				if (izk.Clan == clanID)
				{
					ret.Add(izk);
				}
			}
			return ret;
		}
		// GET Ispunjen zahtev za novine
		public static List<Ispunjenzahtevzanovine> GetFulfilledNewsRequests()
		{
			// TODO: Get all fulfilled book requests
			//return MockDB.Instance.ispunjenzahtevzanovines;
			return null;
		}
		public static List<Ispunjenzahtevzanovine> GetFulfilledNewsRequests(int clanID)
		{
			List<Ispunjenzahtevzanovine> ret = new List<Ispunjenzahtevzanovine>();

			foreach (Ispunjenzahtevzanovine izk in GetFulfilledNewsRequests())
			{
				if (izk.Clan == clanID)
				{
					ret.Add(izk);
				}
			}
			return ret;
		}
		// GET Ispunjen zahtev za magazin
		public static List<Ispunjenzahtevzamagazin> GetFulfilledMagazineRequests()
		{
			// TODO: Get all fulfilled book requests
			//return MockDB.Instance.ispunjenzahtevzamagazins;
			return null;
		}
		public static List<Ispunjenzahtevzamagazin> GetFulfilledMagazineRequests(int clanID)
		{
			List<Ispunjenzahtevzamagazin> ret = new List<Ispunjenzahtevzamagazin>();

			foreach (Ispunjenzahtevzamagazin izk in GetFulfilledMagazineRequests())
			{
				if (izk.Clan == clanID)
				{
					ret.Add(izk);
				}
			}
			return ret;
		}

		#endregion
		#region Modifiers/Setters
		// Modify User Credit
		public static iDbResult ModifyUserCredit(int userID, double newCredit)
		{
			Clan clan = GetUser(userID);

			//TODO: Try modify entry
			//MockDB.Instance.Clanovi.Remove(clan);
			//clan.Kredit = newCredit;
			//MockDB.Instance.Clanovi.Add(clan);

			iDbResult result = iDbResult.Success;
			return result;
		}
		public static iDbResult ModifyUserCredit(Clan clan)
		{
			//TODO: Try modify entry
			Clan c = null;
			//foreach (Clan clan1 in MockDB.Instance.Clanovi)
			//{
			//	if (clan1.Id == clan.Id)
			//	{ c = clan1; break; }
			//}
			//MockDB.Instance.Clanovi.Remove(c);
			//MockDB.Instance.Clanovi.Add(clan);

			iDbResult result = iDbResult.Success;
			return result;
		}
		public static iDbResult AddUserCredit(Clan clan, double amount)
		{
			clan.Kredit += amount;
			return ModifyUserCredit(clan);
		}
		public static iDbResult SubUserCredit(Clan clan, double amount)
		{
			clan.Kredit -= amount;
			return ModifyUserCredit(clan);
		}

		//public static iDbResult ExtendMembership(Clanskakartica c, int years = 1)
		//{
		//	//Clanskakartica clanskakartica = null;
		//	//foreach (Clanskakartica ck in MockDB.Instance.ClanskeKartice)
		//	//{
		//	//	if (ck.Id == c.Id)
		//	//		clanskakartica = ck;
		//	//}
		//	//if (clanskakartica == null)
		//	//	return iDbResult.Error;
		//	//int preostalo = (int)(clanskakartica.DatVal - DateTime.Now).TotalDays;
		//	//if (preostalo > 30)
		//	//{
		//	//	MessageBox.Show($"Trenutno nije moguće produžiti članarinu.\nPreostalo dana: {preostalo}.\nMoguće produžiti kad preostane manje od 30 dana.");
		//	//	return iDbResult.Error;
		//	//}

		//	//MockDB.Instance.ClanskeKartice.Remove(c);
		//	//clanskakartica.DatVal = DateTime.Now.AddYears(years);
		//	//MockDB.Instance.ClanskeKartice.Add(clanskakartica);
		//	return iDbResult.Success;
		//}
		//Modify Book In Store Amount
		public static iDbResult ModifyBookInStoreAmount(KnjigaULokalu kul)
		{
			//TODO: Try update entry
			//foreach (Knjigaulokalu k in MockDB.Instance.KUL)
			//{
			//	if (k.Idk == kul.Idk && k.Idl == kul.Idl)
			//	{
			//		MockDB.Instance.KUL.Remove(k);
			//		MockDB.Instance.KUL.Add(kul);
			//		return iDbResult.Success;
			//	}
			//}

			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult ModifyBookInStoreAmount(int bookID, int lokalID, int newAmount)
		{
			//return ModifyBookInStoreAmount(new Knjigaulokalu(bookID, lokalID, newAmount));
			return iDbResult.Success;
		}

		//Modify News In Store Amount
		public static iDbResult ModifyNewsInStoreAmount(Novineulokalu nul)
		{
			//TODO: Try update entry
			//foreach (Novineulokalu k in MockDB.Instance.NUL)
			//{
			//	if (k.Idn == nul.Idn && k.Idl == nul.Idl)
			//	{
			//		MockDB.Instance.NUL.Remove(k);
			//		MockDB.Instance.NUL.Add(nul);
			//		return iDbResult.Success;
			//	}
			//}
			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult ModifyNewsInStoreAmount(int newsId, int lokalID, int newAmount)
		{
			//return ModifyNewsInStoreAmount(new Novineulokalu(newsId, lokalID, newAmount));
			return iDbResult.Success;
		}

		//Modify Magazine In Store Amount
		public static iDbResult ModifyMagazineInStoreAmount(Magazinulokalu mul)
		{
			//TODO: Try update entry
			//foreach (Magazinulokalu k in MockDB.Instance.MUL)
			//{
			//	if (k.Idm == mul.Idm && k.Idl == mul.Idl)
			//	{
			//		MockDB.Instance.MUL.Remove(k);
			//		MockDB.Instance.MUL.Add(mul);
			//		return iDbResult.Success;
			//	}
			//}
			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult ModifyMagazineInStoreAmount(int newsId, int lokalID, int newAmount)
		{
			//return ModifyMagazineInStoreAmount(new Magazinulokalu(newsId, lokalID, newAmount));
			return iDbResult.Success;
		}
		#endregion
		#region Adders
		//Add User
		public static iDbResult AddUser(Clan clan)
		{
			//foreach (Clan c in MockDB.Instance.Clanovi)
			//{
			//	if (c.Id == clan.Id)
			//		return iDbResult.Duplicate;
			//	if (c.Username == clan.Username)
			//		return iDbResult.Duplicate;
			//}

			//MockDB.Instance.Clanovi.Add(clan);
			return iDbResult.Success;
		}
		//Add Reservation
		public static iDbResult AddReservation(Rezervacija rez)
		{
			return AddReservation(rez.Clan, rez.Knjiga, rez.Lokal);
		}
		public static iDbResult AddReservation(int userID, int bookID, int lokalID)
		{
			//Rezervacija rez = new Rezervacija(userID, bookID, lokalID);

			////TODO: Try add rezervation
			//MockDB.Instance.Rezervacije.Add(rez);

			iDbResult result = iDbResult.Success;


			return result;
		}
		//Add book
		public static iDbResult AddBook(Knjiga knjiga)
		{
			//foreach (Knjiga c in MockDB.Instance.Knjigas)
			//{
			//	if (c.Id == knjiga.Id)
			//		return iDbResult.Duplicate;
			//	if (c.Naziv == knjiga.Naziv && c.Autor == knjiga.Autor && c.Jezik == knjiga.Jezik)
			//		return iDbResult.Duplicate;
			//}

			//MockDB.Instance.Knjigas.Add(knjiga);
			return iDbResult.Success;
		}

		//Add News
		public static iDbResult AddNews(Novine novine)
		{
			//foreach (Novine c in MockDB.Instance.Novines)
			//{
			//	if (c.Id == novine.Id)
			//		return iDbResult.Duplicate;
			//	if (c.NazivNov == novine.NazivNov)
			//		return iDbResult.Duplicate;
			//}

			//MockDB.Instance.Novines.Add(novine);
			return iDbResult.Success;
		}

		//Add Reservation History
		public static iDbResult AddReservationHistory(Rezervacija rez)
		{
			//Istorijarezervacija istorijarezervacija = new Istorijarezervacija(rez, DateTime.Now);

			////TODO: Try add reservation history
			//MockDB.Instance.IstorijeRezervacija.Add(istorijarezervacija);

			iDbResult result = iDbResult.Success;

			return result;
		}


		//Add News Purchase
		public static iDbResult AddNewsPurchase(int clan, int novine)
		{
			return AddNewsPurchase(clan, novine, DateTime.Now);
		}
		public static iDbResult AddNewsPurchase(int clan, int novine, DateTime datum)
		{
			//Kupionovine kupionovine = new Kupionovine(clan, novine, datum);

			////TODO: Try add news purchase
			//MockDB.Instance.KupovineNovina.Add(kupionovine);

			iDbResult result = iDbResult.Success;

			return result;
		}

		//Add Magazin
		public static iDbResult AddMagazin(Magazin magazin)
		{
			//foreach (Magazin c in MockDB.Instance.Magazines)
			//{
			//	if (c.Id == magazin.Id)
			//		return iDbResult.Duplicate;
			//	if (c.NazivMag == magazin.NazivMag)
			//		return iDbResult.Duplicate;
			//}

			//MockDB.Instance.Magazines.Add(magazin);
			return iDbResult.Success;
		}


		//Add Magazine Purchase
		public static iDbResult AddMagazinePurchase(int clan, int magazin)
		{
			return AddMagazinePurchase(clan, magazin, DateTime.Now);
		}
		public static iDbResult AddMagazinePurchase(int clan, int magazin, DateTime datum)
		{
			//Kupiomagazin kupiomagazin = new Kupiomagazin(clan, magazin, datum);

			////TODO: Try add magazine purchase
			//MockDB.Instance.KupovineMagazina.Add(kupiomagazin);

			iDbResult result = iDbResult.Success;

			return result;
		}



		#endregion
		#region Deleters
		// DELETE BOOK REQUEST
		public static iDbResult DeleteFulfilledBookRequest(int clan, int bookID, int lokalID)
		{
			Ispunjenzahtevzaknjigu toDelete = null;

			//foreach (Ispunjenzahtevzaknjigu z in MockDB.Instance.ispunjenzahtevzaknjigus)
			//{
			//	if (clan == z.Clan && bookID == z.Izknjiga && lokalID == z.Izlokal)
			//	{
			//		toDelete = z; break;
			//	}
			//}
			if (toDelete == null)
				return iDbResult.Error;

			//MockDB.Instance.ispunjenzahtevzaknjigus.Remove(toDelete);

			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult DeleteFulfilledBookRequest(Ispunjenzahtevzaknjigu request)
		{
			return DeleteFulfilledBookRequest(request.Clan, request.Izknjiga, request.Izlokal);
			// TODO: Delete request
		}
		// DELETE NEWS REQUEST
		public static iDbResult DeleteFulfilledNewsRequest(int clan, int novine, int lokal)
		{
			Ispunjenzahtevzanovine toDelete = null;
			//foreach (Ispunjenzahtevzanovine z in MockDB.Instance.ispunjenzahtevzanovines)
			//{
			//	if (clan == z.Clan && novine == z.Iznovine && lokal == z.Izlokal)
			//	{
			//		toDelete = z; break;
			//	}
			//}
			if (toDelete == null)
				return iDbResult.Error;

			//MockDB.Instance.ispunjenzahtevzanovines.Remove(toDelete);

			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult DeleteFulfilledNewsRequest(Ispunjenzahtevzanovine request)
		{
			return DeleteFulfilledNewsRequest(request.Clan, request.Iznovine, request.Izlokal);
			// TODO: Delete request
		}
		// DELETE MAGAZINE REQUEST
		public static iDbResult DeleteFulfilledMagazineRequest(int clan, int magazin, int lokal)
		{
			Ispunjenzahtevzamagazin toDelete = null;

			//foreach (Ispunjenzahtevzamagazin z in MockDB.Instance.ispunjenzahtevzamagazins)
			//{
			//	if (clan == z.Clan && magazin == z.Izmagazin && lokal == z.Izlokal)
			//	{
			//		toDelete = z; break;
			//	}
			//}
			//if (toDelete == null)
			//	return iDbResult.Error;

			//MockDB.Instance.ispunjenzahtevzamagazins.Remove(toDelete);
			iDbResult result = iDbResult.Success;

			return result;
		}
		public static iDbResult DeleteFulfilledMagazineRequest(Ispunjenzahtevzamagazin request)
		{
			return DeleteFulfilledMagazineRequest(request.Clan, request.Izmagazin, request.Izlokal);
			// TODO: Delete request
		}
		// DELETE RESERVATION
		public static iDbResult DeleteReservation(Rezervacija rez)
		{
			return DeleteReservation(rez.IDClan, rez.IDKnjiga, rez.IDBK);
		}
		public static iDbResult DeleteReservations(List<Rezervacija> rezervacije)
		{
			foreach (Rezervacija rez in rezervacije)
			{
				if (DeleteReservation(rez.IDClan, rez.IDKnjiga, rez.IDBK) == iDbResult.Error)
					return iDbResult.Error;
			}
			return iDbResult.Success;
		}
		public static iDbResult DeleteReservation(int clan, int knjiga, int lokal)
		{
			Rezervacija toDelete = null;
			//foreach (Rezervacija r in MockDB.Instance.Rezervacije)
			//{
			//	if (r.Clan == clan && r.Knjiga == knjiga && r.Lokal == lokal)
			//	{
			//		toDelete = r; break;
			//	}
			//}
			//if (toDelete == null)
			//	return iDbResult.Error;

			//MockDB.Instance.Rezervacije.Remove(toDelete);

			iDbResult result = iDbResult.Success;

			return result;
		}
		#endregion
	}

	public enum iDbResult
	{
		Success = 0,
		Duplicate,
		Error
	}
}

