using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP2ProjekatCornerLibrary.Helpers
{
	public enum iZanr
	{
		Komedija,
		Drama,
		Horor,
		SciFi,
		SelfHelp,
		CookBook,
		Triler,
		Fantazija,
		Filozofija
	}
	public enum iTip
	{
		Djak,
		Student,
		Odrastao
	}
	public enum iPeriod
	{
		Dnevni = 0,
		Nedeljni,
		Dvonedeljni,
		Mesecni,
		Polugodisnji,
		Godisnji,
		Specijalni,
		NA
	}
	public enum iIzdanje
	{
		Novo,
		Staro,
		Ograniceno
	}
	public class ClanView
	{
		public int ID { get; set; }
		public string Username { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public double Kredit { get; set; }
		public DateTime DatRodj { get; set; }
		public string BrTel { get; set; }
		public int Tip { get; set; }
		public float Prosek { get; set; }
		public bool Budzet { get; set; }
		public bool Zaposlen { get; set; }
		public string Ulica { get; set; }
		public string Broj { get; set; }
		public string Mesto { get; set; }
		public string Drzava { get; set; }
		public ClanView(int id, string username, string ime, string prezime, double kredit, DateTime datRodj, string brTel, int tip, float prosek, bool budzet, bool zaposlen, string ulica, string broj, string mesto)
		{
			ID = id;
			Username = username;
			Ime = ime;
			Prezime = prezime;
			Kredit = kredit;
			DatRodj = datRodj;
			BrTel = brTel;
			Tip = tip;
			Prosek = prosek;
			Budzet = budzet;
			Zaposlen = zaposlen;
			Ulica = ulica;
			Broj = broj;
			Mesto = mesto;
		}
		//public ClanView(Clan clan)
		//{
		//	ID = clan.Id;
		//	Username = clan.Username;
		//	Ime = clan.Ime;
		//	Prezime = clan.Prezime;
		//	Kredit = clan.Kredit;
		//	DatRodj = (DateTime)clan.DatRodj;
		//	BrTel = (string)clan.BrTel;
		//}
	}
	public class KnjigaRezView
	{
		public int? ID { get; set; }
		public string Naziv { get; set; }
		public string Autor { get; set; }
		public string Datum { get; set; }

		public KnjigaRezView()
		{
			ID = -1;
			Naziv = "Naslov knjige";
			Autor = "Autor knjige";
			Datum = "1.1.2023.";
		}
        public KnjigaRezView(int IDKnjiga)
        {
			Knjiga k = DBHelper.GetBook(IDKnjiga);
			//Autor a = DBHelper.GetAutor()
        }
        public KnjigaRezView(string Naziv, string Autor, string Datum, int? id = -1)
		{
			ID = id;
			this.Naziv = Naziv;
			this.Autor = Autor;
			this.Datum = Datum;
		}
		public KnjigaRezView(Rezervacija rez)
		{
			Knjiga k = DBHelper.GetBook(rez.Idknjiga);
			ID = k.IDKnjiga;
			Naziv = k.Naziv;
			Autor = "Autor";
			Datum = DateConverter.ToString((DateTime)rez.DatVrPot);
		}
	}
	public class KnjigaULokaluView
	{
		public int ID { get; set; }
		public string Naziv { get; set; }
		public string Autor { get; set; }
		public string IzdKuca { get; set; }
		public string DatIzd { get; set; }
		public string Zanr { get; set; }
		public string Jezik { get; set; }
		public bool Ogr { get; set; }
		public string Ograniceno { get { return Ogr ? "Ograniceno" : ""; } }
		public string Kolicina { get; set; }
		public KnjigaULokaluView()
		{
			ID = -1;
			Naziv = "Naziv";
			Autor = "Autor";
			IzdKuca = "Izdavacka Kuca";
			DatIzd = "1.1.1970.";
			Zanr = "Zanr";
			Jezik = "Jezik";
			Kolicina = "1";
			Ogr = false;
		}
		public KnjigaULokaluView(Knjigaulokalu kul)
		{
			ID = 1;
			Knjiga k = DBHelper.GetBook(kul.Idknjiga);
			Naziv = k.Naziv;
			Autor = "Autor";
			IzdKuca = "IzdKuca";
			DatIzd = k.GodIzd.ToString();
			Zanr = "Zanr";
			Jezik = "Jezik";
			Kolicina = kul.Kolicina.ToString();
			Ogr = false;
		}
		public KnjigaULokaluView(string naziv, string autor, string izdKuce, string datIzd, string zanr, string jezik, bool ogr, string kol, int id = -1)
		{
			ID = id;
			Naziv = naziv;
			Autor = autor;
			IzdKuca = izdKuce;
			DatIzd = datIzd;
			Zanr = zanr;
			Jezik = jezik;
			Ogr = ogr;
			Kolicina = kol;
		}
		public KnjigaULokaluView(string naziv, string autor, string izdKuce, DateTime datIzd, string zanr, string jezik, bool ogr, int kol, int id = -1)
		{
			ID = id;
			Naziv = naziv;
			Autor = autor;
			IzdKuca = izdKuce;
			DatIzd = DateConverter.ToString(datIzd);
			Zanr = zanr;
			Jezik = jezik;
			Ogr = ogr;
			Kolicina = kol.ToString();
		}
	}
	public class NovineULokaluView
	{
		public int ID { get; set; }
		public string Naziv { get; set; }
		public string Period { get { if (PerInt == iPeriod.NA) return ""; return PerInt.ToString(); } }
		public iPeriod PerInt;
		public double Cena { get; set; }
		public string CenaView { get => Cena.ToString(); }
		public string Kolicina { get; set; }
		public NovineULokaluView()
		{
			ID = 0;
			Naziv = "Naziv";
			PerInt = 0;
			Cena = 0;
			Kolicina = "1";
		}
		public NovineULokaluView(string naziv, iPeriod period, double cena, int kolicina, int id = -1)
		{
			ID = id;
			Naziv = naziv;
			PerInt = period;
			Cena = cena;
			Kolicina = kolicina.ToString();
		}
		//public NovineULokaluView(Novineulokalu nul)
		//{
		//	Novine n = DBHelper.GetNews(nul.Idn);
		//	ID = n.Id;
		//	Naziv = n.NazivNov;
		//	switch (n.PeriodIzd)
		//	{
		//		case "Dnevni":
		//			PerInt = iPeriod.Dnevni; break;
		//		case "Nedeljni":
		//			PerInt = iPeriod.Nedeljni; break;
		//		case "Mesecni":
		//			PerInt = iPeriod.Mesecni; break;
		//		default:
		//			PerInt = iPeriod.Godisnji; break;
		//	}
		//	Cena = n.Cena;
		//	Kolicina = nul.Kolicina.ToString();
		//}
	}
	public class MagazinULokaluView
	{
		public int ID { get; set; }
		public string Naziv { get; set; }
		public iPeriod PerInt { get; set; }
		public string Period { get { if (PerInt == iPeriod.NA) return ""; return PerInt.ToString(); } }
		public double Cena { get; set; } = 0;
		public string CenaView { get => Cena.ToString(); }
		public string Kolicina { get; set; }
		public MagazinULokaluView()
		{
			ID = 0;
			Naziv = "Naziv";
			PerInt = 0;
			Cena = 0;
			Kolicina = "1";
		}
		public MagazinULokaluView(string naziv, iPeriod period, double cena, int kolicina, int id = -1)
		{
			ID = id;
			Naziv = naziv;
			PerInt = period;
			Cena = cena;
			Kolicina = kolicina.ToString();
		}
		//public MagazinULokaluView(Magazinulokalu mul)
		//{
		//	Magazin m = DBHelper.GetMagazin(mul.Idm);
		//	ID = m.Id;
		//	Naziv = m.NazivMag;
		//	switch (m.PeriodIzd)
		//	{
		//		case "Dnevni":
		//			PerInt = iPeriod.Dnevni; break;
		//		case "Nedeljni":
		//			PerInt = iPeriod.Nedeljni; break;
		//		case "Mesecni":
		//			PerInt = iPeriod.Mesecni; break;
		//		default:
		//			PerInt = iPeriod.Godisnji; break;
		//	}
		//	Cena = m.Cena;
		//	Kolicina = mul.Kolicina.ToString();
		//}
	}

	public class IstorijaKnjigaView
	{
		public int ID { get; set; } = -1;
		public string Naziv { get; set; }
		public string Autor { get; set; }
		public string DatumUzeo { get; set; }
		public string DatumVratio { get; set; }

		public IstorijaKnjigaView()
		{
			Naziv = "Naslov knjige";
			Autor = "Autor knjige";
			DatumUzeo = "1.1.2023.";
			DatumVratio = "1.2.2023.";
		}
		public IstorijaKnjigaView(string Naziv, string Autor, string DatumUzeo, string DatumVratio, int id = -1)
		{
			this.Naziv = Naziv;
			this.Autor = Autor;
			this.DatumUzeo = DatumUzeo;
			this.DatumVratio = DatumVratio;
			ID = id;
		}
		public IstorijaKnjigaView(Rezervacija ir)
		{
			Knjiga k = DBHelper.GetBook(ir.Idknjiga);
			Naziv = k.Naziv;
			Autor = "Autor";
			DatumUzeo = DateConverter.ToString((DateTime)ir.DatVrPot);
			DatumVratio = DateConverter.ToString((DateTime)ir.DatVrZak);
		}
	}
	public class LokacijaView
	{
		public int ID { get; set; }
		public string Adresa { get; set; }
		public string Grad { get; set; }
		public string Drzava { get; set; }
		public LokacijaView()
		{
			ID = -1;
			Adresa = "Adresa";
			Grad = "Grad";
			Drzava = "Drzava";
		}
		public LokacijaView(string adresa, string grad, string drzava, int id = -1)
		{
			ID = id;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;
		}
		public LokacijaView(Biblikutak l)
		{
			ID = l.Idbk;
			Adresa = "Adresa";
			Grad = "Grad";
			Drzava = "Drzava";
		}
	}
	public class KatalogNovineMagazinView
	{
		public int NovineID { get; set; }
		public int LokalID { get; set; }
		public string Naziv { get; set; }
		public double Cena { get; set; }
		public string Adresa { get; set; }
		public string Grad { get; set; }
		public string Drzava { get; set; }
		public KatalogNovineMagazinView(int novID, int lokID, string naziv, double cena = 0, string adresa = "", string grad = "", string drzava = "")
		{
			NovineID = novID;
			LokalID = lokID;
			Naziv = naziv;
			Cena = cena;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;
		}
	}
	public class KatalogKnjigaView
	{
		public int KnjigaID { get; set; }
		public int LokalID { get; set; }
		public string Naziv { get; set; }
		public string Autor { get; set; }
		public string Jezik { get; set; }
		public string Zanr { get; set; }
		public string Adresa { get; set; }
		public string Grad { get; set; }
		public string Drzava { get; set; }
		public KatalogKnjigaView(int bookID, int lokID, string naziv, string autor = "", string jezik = "", string zanr = "", string adresa = "", string grad = "", string drzava = "")
		{
			KnjigaID = bookID;
			LokalID = lokID;
			Naziv = naziv;
			Autor = autor;
			Jezik = jezik;
			Zanr = zanr;
			Adresa = adresa;
			Grad = grad;
			Drzava = drzava;
		}
	}
}
