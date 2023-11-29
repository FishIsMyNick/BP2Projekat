using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
        private static string connString;

        #region Constraints
        public static int MAX_OZNJ_LENGTH = 3;
        #endregion
        public static void InitializeConnection()
        {
            optionBuilder = new DbContextOptionsBuilder<CornerLibraryDbContext>();
            connString = "Server=(local)\\CORNERLIBRARYDB; Database=CornerLibraryDB; Trusted_Connection=True; TrustServerCertificate=True; User Id=sa; Password=bp2sifra;Encrypt=False;";
            optionBuilder.UseSqlServer(connString);
            db = new CornerLibraryDbContext(optionBuilder.Options);
        }
        public static object ExecuteQuery(string query)
        {
            return query;
        }

        #region NEW SHIT
        #region PUBLIC
        public static bool CheckDbNotNull(object obj)
        {
            return obj.GetType() != typeof(DBNull);
        }
        #endregion
        #region SQL DIRECT EXECUTION
        public static T CreateInstance<T>(params ClassPropertyValue[] args)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            Type iType = typeof(T);

            foreach (ClassPropertyValue arg in args)
            {
                if (CheckDbNotNull(arg.Value))
                    iType.GetProperty(arg.Name).SetValue(instance, arg.Value);
            }

            return instance;
            //return (T)Activator.CreateInstance(typeof(T), args);
        }
        public static List<T> GetListFromSQL<T>(string sqlParameters = null) where T : new()
        {
            List<string> columns = new List<string>();
            List<T> ret = new List<T>();

            string sqlCommand = $"select * from {typeof(T).Name}";
            if (sqlParameters != null) sqlCommand += " where " + sqlParameters;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        ClassPropertyValue[] values = new ClassPropertyValue[reader.FieldCount];
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = new ClassPropertyValue(columns[i], reader[columns[i]]);
                        }

                        ret.Add(CreateInstance<T>(values));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return ret;
        }
        public static T GetFirstFromSQL<T>(string sqlParameters = null) where T : new()
        {
            List<string> columns = new List<string>();
            T ret = new T();
            string sqlCommand = $"select * from {typeof(T).Name}";
            if (sqlParameters != null) sqlCommand += " where " + sqlParameters;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        ClassPropertyValue[] values = new ClassPropertyValue[reader.FieldCount];
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = new ClassPropertyValue(columns[i], reader[columns[i]]);
                        }

                        ret = CreateInstance<T>(values);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return ret;
        }
        #endregion

        #region LOGIN
        public static Zaposleni TryLoginUser(string username, string password)
        {
            KorisnickiNalog nalog = GetFirstFromSQL<KorisnickiNalog>($"KorisnickoIme='{username}' and Sifra='{password}'");

            if (nalog.KorisnickoIme != null)
            {
                switch (nalog.TipNaloga)
                {
                    case (1):
                        AdminKoristiNalog akn = GetFirstFromSQL<AdminKoristiNalog>($"KorisnickoIme='{username}'");
                        return GetFirstFromSQL<Admin>($"IDRadnik='{akn.ID}'");
                    case (2):
                        BibliotekarKoristiNalog bkn = GetFirstFromSQL<BibliotekarKoristiNalog>($"KorisnickoIme='{username}'");
                        return GetFirstFromSQL<Bibliotekar>($"IDRadnik='{bkn.ID}'");
                    case (3):
                        KurirKoristiNalog kkn = GetFirstFromSQL<KurirKoristiNalog>($"KorisnickoIme='{username}'");
                        return GetFirstFromSQL<Kurir>($"IDRadnik='{kkn.ID}'");
                    default: return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GETTERS

        #region WORKERS
        // EMPLOYEES
        public static List<Zaposleni> GetAllEmployees(string args = null)
        {
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.AddRange(GetListFromSQL<Admin>(args));
            zaposleni.AddRange(GetListFromSQL<Bibliotekar>(args));
            zaposleni.AddRange(GetListFromSQL<Kurir>(args));

            return zaposleni;
        }
        public static List<Zaposleni> GetAllEmployedEmployees()
        {
            return GetAllEmployees("DatOtp IS NULL");
        }
        public static List<Zaposleni> GetAllUnemployedEmployees()
        {
            return GetAllEmployees("DatOtp IS NOT NULL");
        }
        public static Zaposleni GetEmployee(int workerId)
        {
            List<Zaposleni> workerList = new List<Zaposleni>();
            workerList.AddRange(GetListFromSQL<Admin>($"IDRadnik={workerId}"));
            if (workerList.Count == 0)
            {
                workerList.Add(GetWorker(workerId));
                if (workerList.Count == 0)
                {
                    return null;
                }
            }
            return workerList[0];
        }
        //WORKERS
        public static List<Radnik> GetAllWorkers(string args = null)
        {
            List<Radnik> radniks = new List<Radnik>();
            radniks.AddRange(GetListFromSQL<Bibliotekar>(args));
            radniks.AddRange(GetListFromSQL<Kurir>(args));

            return radniks;
        }
        public static List<Radnik> GetAllEmployedWorkers()
        {
            return GetAllWorkers("DatOtp IS NULL");
        }
        public static List<Radnik> GetAllUnemployedWorkers()
        {
            return GetAllWorkers("DatOtp IS NOT NULL");
        }
        public static Radnik GetWorker(int workerId)
        {
            List<Radnik> workerList = new List<Radnik>();
            workerList.AddRange(GetListFromSQL<Bibliotekar>($"IDRadnik={workerId}"));
            if (workerList.Count == 0)
            {
                workerList.AddRange(GetListFromSQL<Kurir>($"IDRadnik={workerId}"));
                if (workerList.Count == 0)
                {
                    return null;
                }
            }
            return workerList[0];
        }
        // BIBLIOTEKAR
        public static List<Bibliotekar> GetAllBibliotekar(string args = null)
        {
            return GetListFromSQL<Bibliotekar>(args);
        }
        public static List<Bibliotekar> GetAllEmployedBibliotekars()
        {
            return GetAllBibliotekar("DatOtp IS NULL");
        }
        public static List<Bibliotekar> GetAllUnemployedBibliotekars()
        {
            return GetAllBibliotekar("DatOtp IS NOT NULL");
        }
        public static Bibliotekar GetBibliotekar(int id)
        {
            return GetFirstFromSQL<Bibliotekar>($"IDRadnik={id}");
        }
        //KURIR
        public static List<Kurir> GetAllKurirs(string args = null)
        {
            return GetListFromSQL<Kurir>(args);
        }
        public static List<Kurir> GetAllEmployedKurirs()
        {
            return GetAllKurirs("DatOtp IS NULL");
        }
        public static List<Kurir> GetAllUnemployedKurirs()
        {
            return GetAllKurirs("DatOtp IS NOT NULL");
        }
        public static Kurir GetKurir(int id)
        {
            return GetFirstFromSQL<Kurir>($"IDRadnik={id}");
        }
        #endregion

        #region ACCOUNTS
        public static List<KorisnickiNalog> GetAllKorisnickiNalogs(string args = null)
        {
            return GetListFromSQL<KorisnickiNalog>(args);
        }
        /// <summary>
        /// Get account by the username
        /// </summary>
        /// <param name="username">KorisnickoIme</param>
        /// <returns></returns>
        public static KorisnickiNalog GetKorisnickiNalog(string username)
        {
            return GetFirstFromSQL<KorisnickiNalog>($"KorisnickoIme='{username}'");
        }
        /// <summary>
        /// Get bibliotekar account based on ID
        /// </summary>
        /// <param name="id">IDRadnik</param>
        /// <returns></returns>
        public static KorisnickiNalog GetBibNalog(int id)
        {
            return GetKorisnickiNalog(GetFirstFromSQL<BibliotekarKoristiNalog>($"ID='{id}'").KorisnickoIme);
        }
        /// <summary>
        /// Get kurir account based on ID
        /// </summary>
        /// <param name="id">IDRadnik</param>
        /// <returns></returns>
        public static KorisnickiNalog GetKurirNalog(int id)
        {
            return GetKorisnickiNalog(GetFirstFromSQL<KurirKoristiNalog>($"ID='{id}'").KorisnickoIme);
        }
        /// <summary>
        /// Get admin account based on ID
        /// </summary>
        /// <param name="id">IDRadnik</param>
        /// <returns></returns>
        public static KorisnickiNalog GetAdminNalog(int id)
        {
            return GetKorisnickiNalog(GetFirstFromSQL<AdminKoristiNalog>($"ID='{id}'").KorisnickoIme);
        }
        #endregion

        #region LOKALI
        public static List<Biblikutak> GetAllLokals(string args)
        {
            return GetListFromSQL<Biblikutak>(args);
        }
        public static List<Biblikutak> GetOpenLokals()
        {
            return GetAllLokals($"DatZat IS NULL");
        }
        public static List<Biblikutak> GetClosedLokals()
        {
            return GetAllLokals($"DatZat IS NOT NULL");
        }
        public static Biblikutak GetLokal(int id)
        {
            return GetFirstFromSQL<Biblikutak>($"IDBK='{id}'");
        }
        #endregion

        #region LOKACIJA
        //LOKACIJA
        public static List<Lokacija> GetAllLocations(string args)
        {
            return GetListFromSQL<Lokacija>(args);
        }
        public static List<Lokacija> GetAllLocationsInCountry(string country)
        {
            return GetAllLocations($"OZND='{country}'");
        }
        public static List<Lokacija> GetAllLocationsInCity(int posBr)
        {
            return GetAllLocations($"PosBr={posBr}");
        }
        public static Lokacija GetLokacija(string ulica, string broj, int posBr, string oznd)
        {
            return GetFirstFromSQL<Lokacija>($"Ulica='{ulica}' and Broj='{broj}' and PosBr={posBr} and OZND='{oznd}'");
        }
        //MESTO
        public static List<Mesto> GetAllMesta()
        {
            return GetListFromSQL<Mesto>();
        }
        public static Mesto GetMesto(int posBr)
        {
            return GetFirstFromSQL<Mesto>($"PosBr={posBr}");
        }
        //DRZAVA
        public static List<Drzava> GetAllDrzave()
        {
            return GetListFromSQL<Drzava>();
        }
        public static Drzava GetDrzava(string oznd)
        {
            return GetFirstFromSQL<Drzava>($"OZND='{oznd}'");
        }
        #endregion



        // KNJIGA
        public static Knjiga GetBook(int bookID)
        {
            return GetFirstFromSQL<Knjiga>($"IDKnjiga={bookID}");
        }
        public static Knjiga GetExactBook(string naziv, int autorID, int IDIK, string godIzd, int brIzd, string oznz, string oznj, bool ogr)
        {
            List<Knjiga> knjige = db.Knjiga.FromSql($"select * from KNJIGA where Naziv={naziv} and GodIzd={godIzd} and BrIzd={brIzd} and Ograniceno={Convert.ToInt16(ogr)}").ToList();
            if (knjige.Count == 0) return null;

            Autor autor = db.Autor.FromSql($"select * from AUTOR where IDAutor={autorID}").ToList()[0];
            if (autor == null) return null;

            IzdKuca IzdKuca = db.IzdKuca.FromSql($"select * from IzdKuca where IDIK={IDIK}").ToList()[0];
            if (IzdKuca == null) return null;


            bool allSame = true;

            foreach (Knjiga k in knjige)
            {
                List<Pise> pise = db.Pise.FromSql($"select * from PISE where IDKnjiga={k.IDKnjiga} and IDAutor={autorID}").ToList();
                if (pise.Count == 0) allSame = false;

                List<IzdajeKnjigu> ik = db.IzdajeKnjigu.FromSql($"select * from IZDAJEKNJIGU where IDIK={IDIK} and IDKnjiga={k.IDKnjiga}").ToList();
                if (ik.Count == 0) allSame = false;

                List<KnjigaNaJeziku> knj = db.KnjigaNaJeziku.FromSql($"select * from KNJIGANAJEZIKU where IDKnjiga={k.IDKnjiga} and OZNJ={oznj}").ToList();
                if (knj.Count == 0) allSame = false;

                List<PripadaZanru> pz = db.PripadaZanru.FromSql($"select * from PRIPADAZANRU where IDKnjiga={k.IDKnjiga} and OZNZ={oznz}").ToList();
                if (pz.Count == 0) allSame = false;

                if (allSame)
                    return k;
                else
                    allSame = true;
            }
            return null;
        }
        public static List<Knjiga> GetAllKnjigas()
        {
            return db.Knjiga.ToList();
        }

        // AUTOR
        public static Autor GetAutor(int autorID)
        {
            return db.Autor.FromSql($"select * from AUTOR where IDAutor={autorID}").ToList()[0];
        }
        public static List<Autor> GetAutorsByName(string name, string lastName)
        {
            return db.Autor.FromSql($"select * from AUTOR where Ime={name} and Prezime={lastName}").ToList();
        }
        public static List<Autor> GetBookAuthors(int bookID)
        {
            Knjiga k = db.Knjiga.FromSql($"select * from KNJIGA where IDKnjiga={bookID}").ToList()[0];

            List<Pise> pisu = db.Pise.FromSql($"select * from PISE where IDKnjiga={bookID}").ToList();
            //List<Pise> pisu = db.Pises.FromSql($"select * from PISE").ToList();

            HashSet<int> authors = new HashSet<int>();
            foreach (Pise p in pisu)
            {
                authors.Add(p.IDAutor);
            }

            List<Autor> ret = new List<Autor>();
            foreach (int a in authors)
                ret.Add(GetAutor(a));

            return ret;
        }
        public static List<Autor> GetAllAutors()
        {
            return db.Autor.ToList();
        }

        // JEZIK
        public static Jezik GetJezik(string oznj)
        {
            return db.Jezik.FromSql($"select * from JEZIK where OZNJ={oznj}").ToList()[0];
        }
        public static Jezik GetJezikByName(string jezik)
        {
            return db.Jezik.FromSql($"select * from JEZIK where NAZIVJEZIKA={jezik}").ToList()[0];
        }
        public static List<Jezik> GetAllJeziks()
        {
            //return db.Jeziks.FromSql($"select * from Jezik").ToList();
            return db.Jezik.ToList();
        }

        // ZANR
        public static Zanr GetZanr(string oznz)
        {
            return db.Zanr.FromSql($"select * from ZANR where OZNZ={oznz}").ToList()[0];
        }
        public static Zanr GetZanrByName(string zanr)
        {
            return db.Zanr.FromSql($"select * from ZANR where NAZIVZANRA={zanr}").ToList()[0];
        }
        public static List<Zanr> GetAllZanrs()
        {
            //return db.Zanrs.FromSql($"select * from ZANR").ToList();
            return db.Zanr.ToList();
        }

        // IZDAVACKA KUCA
        public static IzdKuca GetIzdKuca(int idik)
        {
            return db.IzdKuca.FromSql($"select * from IzdKuca where IDIK={idik}").ToList()[0];
        }
        public static List<IzdKuca> GetAllIzdKucas()
        {
            return db.IzdKuca.ToList();
        }


        #endregion

        #region ADDERS

        #region LOKACIJA
        public static bool CheckIfLocationExists(string ulica, string broj, int posBr, string oznd)
        {
            return GetAllLocations($"Ulica='{ulica}' and Broj='{broj}' and PosBr={posBr} and OZND='{oznd}'").Count() > 0;
        }
        #endregion
        // KNJIGA
        public static iDbResult AddBook(string naziv, int brIzd, int godIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int IzdKuca, string zanr)
        {
            return AddBook(naziv, brIzd, godIzd, "", brStrana, velicinaFonta, korice, format, ograniceno, autorID, jezik, IzdKuca, zanr);
        }
        public static iDbResult AddBook(string naziv, int brIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int IzdKuca, string zanr)
        {
            return AddBook(naziv, brIzd, -1, vrIzd, brStrana, velicinaFonta, korice, format, ograniceno, autorID, jezik, IzdKuca, zanr);
        }
        private static iDbResult AddBook(string naziv, int brIzd, int godIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int IzdKuca, string zanr)
        {

            Knjiga knjiga = new Knjiga(naziv, brIzd, godIzd, vrIzd, brStrana, velicinaFonta, korice, ograniceno, format);
            db.Knjiga.Add(knjiga);
            db.SaveChanges();

            int bookID = knjiga.IDKnjiga;

            Pise pise = new Pise(bookID, autorID);
            KnjigaNaJeziku knjiganajeziku = new KnjigaNaJeziku(bookID, jezik);
            PripadaZanru pripadazanru = new PripadaZanru(bookID, zanr);
            IzdajeKnjigu izdajeknjigu = new IzdajeKnjigu(bookID, IzdKuca);

            try
            {
                db.Knjiga.Add(knjiga);
                db.SaveChanges();
                db.Pise.Add(pise);
                db.KnjigaNaJeziku.Add(knjiganajeziku);
                db.PripadaZanru.Add(pripadazanru);
                db.IzdajeKnjigu.Add(izdajeknjigu);
            }
            catch (Exception ex)
            {
                return iDbResult.Error;
            }

            db.SaveChanges();

            return iDbResult.Success;
        }

        // KNJIGA U LOKALU
        public static iDbResult AddKnjigaULokalu(int knjigaID, int lokalID, int kolicina)
        {
            try
            {
                db.KnjigaUlokalu.Add(new KnjigaULokalu(knjigaID, lokalID, kolicina));
            }
            catch (Exception ex)
            {
                return iDbResult.Error;
            }

            db.SaveChanges();

            return iDbResult.Success;
        }

        #endregion

        #endregion

        #region Getters
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
        public static SerijskoStivo GetSerijskoStivo(int id)
        {
            //TODO: GET FROM DB
            //List<SerijskoStivo> sstFDB = db.Serijskostivos.ToList();

            //         foreach (SerijskoStivo l in sstFDB)
            //{
            //	if (l.IDSStivo == id)
            //	{
            //		return l;
            //	}
            //}
            return null;
        }
        // GET STORE


        public static Biblikutak GetStore(int storeID)
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
        public static List<Biblikutak> GetAllStores()
        {
            //return MockDB.Instance.Lokali;
            return null;
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

#region OLD
//public static Clan GetUser(int id)
//{
//	//TODO: Get user
//	//foreach (Clan c in MockDB.Instance.Clanovi)
//	//{
//	//	if (c.Id == id) return c;
//	//}
//	return null;
//}
//public static Clan GetUserByCard(int id)
//{
//	//TODO: Get user
//	//foreach (Clan c in MockDB.Instance.Clanovi)
//	//{
//	//	if (c.BrKartice == id) return c;
//	//}
//	return null;
//}
//public static Clan TryLoginUser(string username, string hashedPassword, out string message)
//{
//	message = "";
//	//List<Korisnik> lk = db.Korisniks.ToList();
//	//Clan c = db.Clans.FirstOrDefault(x => x.KorisnickoIme == username);
//	List<Clan> cs = db.Clans.FromSql($"select * from CLAN where KorisnickoIme={username}").ToList();
//	//Clan c = db.Clans.FirstOrDefault(x => x.KorisnickoIme == username);
//	Clan c = null;
//	if (cs.Count > 0)
//		c = cs[0];

//	if (c != null)
//	{
//		if (c.Sifra == hashedPassword)
//			return c;
//		else
//		{
//			message = "Pogrešna šifra!";
//			return null;
//		}
//	}

//	message = "Nepostojeći korisnik.";
//	return null;
//}

//public static Radnik TryLoginBibliotekar(string username, string hashedPassword, out string message)
//{
//	message = "";
//	var optionBuilder = new DbContextOptionsBuilder<CornerLibraryDbContext>();
//	optionBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CornerLibraryDbConnString"].ConnectionString);
//	using (var db = new CornerLibraryDbContext(optionBuilder.Options))
//	{
//		//List<Korisnik> lk = db.Korisniks.ToList();
//		List<Radnik> cs = db.Radniks.FromSql($"select * from RADNIK where KorisnickoIme={username}").ToList();
//		Radnik c = null;
//		if (cs.Count > 0)
//			c = cs[0];

//		if (c != null)
//		{
//			if (c.Sifra == hashedPassword)
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

//public static List<Autor> GetBookAuthors(int IDKnjiga)
//{
//	List<Pise> pises = db.Pise
//}
//public static Jezik GetJezik(string OZNJ)
//{
//	return db.Jeziks.FromSql($"select * from dbo.JEZIK where OZNJ={OZNJ}").ToList()[0];
//}
//public static Zanr GetZanr(string OZNZ)
//{
//	return db.Zanrs.FromSql($"select * from dbo.ZANR where OZNZ={OZNZ}").ToList()[0];
//}
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



// GET RESERVATIONS
//public static List<Rezervacija> GetReservations(int clanID)
//{
//	return db.Rezervacijas.FromSql($"Select * from dbo.REZERVACIJA where IDClan={clanID}").ToList();
//}
//public static Rezervacija GetReservation(int clan, int book, int lokal)
//{
//	return db.Rezervacijas.FromSql($"select * from dbo.REZERVACIJA where IDClan={clan} and IDKnjiga={book} and IDBK={lokal}").ToList()[0];

//}
//public static List<Rezervacija> GetReservationHistory(int clanID)
//{
//	List<Rezervacija> ret = new List<Rezervacija>();

//	//foreach (Istorijarezervacija i in MockDB.Instance.IstorijeRezervacija)
//	//{
//	//	if (i.Clan == clanID)
//	//		ret.Add(i);
//	//}
//	return ret;
//}

//// GET Ispunjen zahtev za knjigu
//public static List<Ispunjenzahtevknjiga> GetFulfilledBookRequests()
//{
//	// TODO: Get all fulfilled book requests
//	//return MockDB.Instance.ispunjenzahtevzaknjigus;
//	return null;
//}
//public static List<Ispunjenzahtevknjiga> GetFulfilledBookRequests(int clanID)
//{
//	List<Ispunjenzahtevknjiga> ret = new List<Ispunjenzahtevknjiga>();

//	foreach (Ispunjenzahtevknjiga izk in GetFulfilledBookRequests())
//	{
//		if (izk.Idclan == clanID)
//		{
//			ret.Add(izk);
//		}
//	}
//	return ret;
//}
//// GET Ispunjen zahtev za novine
//public static List<Ispunjenzahtevserijskostivo> GetFulfilledNewsRequests()
//{
//	// TODO: Get all fulfilled book requests
//	//return MockDB.Instance.ispunjenzahtevzanovines;
//	return null;
//}
//public static List<Ispunjenzahtevserijskostivo> GetFulfilledNewsRequests(int clanID)
//{
//	List<Ispunjenzahtevserijskostivo> ret = new List<Ispunjenzahtevserijskostivo>();

//	foreach (Ispunjenzahtevserijskostivo izk in GetFulfilledNewsRequests())
//	{
//		if (izk.Idclan == clanID)
//		{
//			ret.Add(izk);
//		}
//	}
//	return ret;
//}
//// GET Ispunjen zahtev za magazin
//public static List<Ispunjenzahtevserijskostivo> GetFulfilledMagazineRequests()
//{
//	// TODO: Get all fulfilled book requests
//	//return MockDB.Instance.ispunjenzahtevzamagazins;
//	return null;
//}
//public static List<Ispunjenzahtevserijskostivo> GetFulfilledMagazineRequests(int clanID)
//{
//	List<Ispunjenzahtevserijskostivo> ret = new List<Ispunjenzahtevserijskostivo>();

//	foreach (Ispunjenzahtevserijskostivo izk in GetFulfilledMagazineRequests())
//	{
//		if (izk.Idclan == clanID)
//		{
//			ret.Add(izk);
//		}
//	}
//	return ret;
//}

//// Modify User Credit
//public static iDbResult ModifyUserCredit(int userID, double newCredit)
//{
//	Clan clan = GetUser(userID);

//	//TODO: Try modify entry
//	//MockDB.Instance.Clanovi.Remove(clan);
//	//clan.Kredit = newCredit;
//	//MockDB.Instance.Clanovi.Add(clan);

//	iDbResult result = iDbResult.Success;
//	return result;
//}
//public static iDbResult ModifyUserCredit(Clan clan)
//{
//	//TODO: Try modify entry
//	Clan c = null;
//	//foreach (Clan clan1 in MockDB.Instance.Clanovi)
//	//{
//	//	if (clan1.Id == clan.Id)
//	//	{ c = clan1; break; }
//	//}
//	//MockDB.Instance.Clanovi.Remove(c);
//	//MockDB.Instance.Clanovi.Add(clan);

//	iDbResult result = iDbResult.Success;
//	return result;
//}
//public static iDbResult AddUserCredit(Clan clan, double amount)
//{
//	clan.Kredit += amount;
//	return ModifyUserCredit(clan);
//}
//public static iDbResult SubUserCredit(Clan clan, double amount)
//{
//	clan.Kredit -= amount;
//	return ModifyUserCredit(clan);
//}

////public static iDbResult ExtendMembership(Clanskakartica c, int years = 1)
////{
////	//Clanskakartica clanskakartica = null;
////	//foreach (Clanskakartica ck in MockDB.Instance.ClanskeKartice)
////	//{
////	//	if (ck.Id == c.Id)
////	//		clanskakartica = ck;
////	//}
////	//if (clanskakartica == null)
////	//	return iDbResult.Error;
////	//int preostalo = (int)(clanskakartica.DatVal - DateTime.Now).TotalDays;
////	//if (preostalo > 30)
////	//{
////	//	MessageBox.Show($"Trenutno nije moguće produžiti članarinu.\nPreostalo dana: {preostalo}.\nMoguće produžiti kad preostane manje od 30 dana.");
////	//	return iDbResult.Error;
////	//}

////	//MockDB.Instance.ClanskeKartice.Remove(c);
////	//clanskakartica.DatVal = DateTime.Now.AddYears(years);
////	//MockDB.Instance.ClanskeKartice.Add(clanskakartica);
////	return iDbResult.Success;
////}
////Modify Book In Store Amount
//public static iDbResult ModifyBookInStoreAmount(KnjigaULokalu kul)
//{
//	//TODO: Try update entry
//	//foreach (Knjigaulokalu k in MockDB.Instance.KUL)
//	//{
//	//	if (k.Idk == kul.Idk && k.Idl == kul.Idl)
//	//	{
//	//		MockDB.Instance.KUL.Remove(k);
//	//		MockDB.Instance.KUL.Add(kul);
//	//		return iDbResult.Success;
//	//	}
//	//}

//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult ModifyBookInStoreAmount(int bookID, int lokalID, int newAmount)
//{
//	//return ModifyBookInStoreAmount(new Knjigaulokalu(bookID, lokalID, newAmount));
//	return iDbResult.Success;
//}

////Modify News In Store Amount
//public static iDbResult ModifyNewsInStoreAmount(Novineulokalu nul)
//{
//	//TODO: Try update entry
//	//foreach (Novineulokalu k in MockDB.Instance.NUL)
//	//{
//	//	if (k.Idn == nul.Idn && k.Idl == nul.Idl)
//	//	{
//	//		MockDB.Instance.NUL.Remove(k);
//	//		MockDB.Instance.NUL.Add(nul);
//	//		return iDbResult.Success;
//	//	}
//	//}
//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult ModifyNewsInStoreAmount(int newsId, int lokalID, int newAmount)
//{
//	//return ModifyNewsInStoreAmount(new Novineulokalu(newsId, lokalID, newAmount));
//	return iDbResult.Success;
//}

////Modify Magazine In Store Amount
//public static iDbResult ModifyMagazineInStoreAmount(Magazinulokalu mul)
//{
//	//TODO: Try update entry
//	//foreach (Magazinulokalu k in MockDB.Instance.MUL)
//	//{
//	//	if (k.Idm == mul.Idm && k.Idl == mul.Idl)
//	//	{
//	//		MockDB.Instance.MUL.Remove(k);
//	//		MockDB.Instance.MUL.Add(mul);
//	//		return iDbResult.Success;
//	//	}
//	//}
//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult ModifyMagazineInStoreAmount(int newsId, int lokalID, int newAmount)
//{
//	//return ModifyMagazineInStoreAmount(new Magazinulokalu(newsId, lokalID, newAmount));
//	return iDbResult.Success;
//}
////Add User
//public static iDbResult AddUser(Clan clan)
//{
//	//db.Clans.Add(clan);
//	//db.SaveChanges();
//	//foreach (Clan c in MockDB.Instance.Clanovi)
//	//{
//	//	if (c.Id == clan.Id)
//	//		return iDbResult.Duplicate;
//	//	if (c.Username == clan.Username)
//	//		return iDbResult.Duplicate;
//	//}

//	//MockDB.Instance.Clanovi.Add(clan);
//	return iDbResult.Success;
//}
////Add Reservation
//public static iDbResult AddReservation(Rezervacija rez)
//{
//	return AddReservation(rez.Clan, rez.Knjiga, rez.Lokal);
//}
//public static iDbResult AddReservation(int userID, int bookID, int lokalID)
//{
//	//Rezervacija rez = new Rezervacija(userID, bookID, lokalID);

//	////TODO: Try add rezervation
//	//MockDB.Instance.Rezervacije.Add(rez);

//	iDbResult result = iDbResult.Success;


//	return result;
//}
////Add book
//public static iDbResult AddBook(Knjiga knjiga)
//{
//	//foreach (Knjiga c in MockDB.Instance.Knjigas)
//	//{
//	//	if (c.Id == knjiga.Id)
//	//		return iDbResult.Duplicate;
//	//	if (c.Naziv == knjiga.Naziv && c.Autor == knjiga.Autor && c.Jezik == knjiga.Jezik)
//	//		return iDbResult.Duplicate;
//	//}

//	//MockDB.Instance.Knjigas.Add(knjiga);
//	return iDbResult.Success;
//}

////Add News
//public static iDbResult AddNews(Novine novine)
//{
//	//foreach (Novine c in MockDB.Instance.Novines)
//	//{
//	//	if (c.Id == novine.Id)
//	//		return iDbResult.Duplicate;
//	//	if (c.NazivNov == novine.NazivNov)
//	//		return iDbResult.Duplicate;
//	//}

//	//MockDB.Instance.Novines.Add(novine);
//	return iDbResult.Success;
//}

////Add Reservation History
//public static iDbResult AddReservationHistory(Rezervacija rez)
//{
//	//Istorijarezervacija istorijarezervacija = new Istorijarezervacija(rez, DateTime.Now);

//	////TODO: Try add reservation history
//	//MockDB.Instance.IstorijeRezervacija.Add(istorijarezervacija);

//	iDbResult result = iDbResult.Success;

//	return result;
//}


////Add News Purchase
//public static iDbResult AddNewsPurchase(int clan, int novine)
//{
//	return AddNewsPurchase(clan, novine, DateTime.Now);
//}
//public static iDbResult AddNewsPurchase(int clan, int novine, DateTime datum)
//{
//	//Kupionovine kupionovine = new Kupionovine(clan, novine, datum);

//	////TODO: Try add news purchase
//	//MockDB.Instance.KupovineNovina.Add(kupionovine);

//	iDbResult result = iDbResult.Success;

//	return result;
//}

////Add Magazin
//public static iDbResult AddMagazin(Magazin magazin)
//{
//	//foreach (Magazin c in MockDB.Instance.Magazines)
//	//{
//	//	if (c.Id == magazin.Id)
//	//		return iDbResult.Duplicate;
//	//	if (c.NazivMag == magazin.NazivMag)
//	//		return iDbResult.Duplicate;
//	//}

//	//MockDB.Instance.Magazines.Add(magazin);
//	return iDbResult.Success;
//}


////Add Magazine Purchase
//public static iDbResult AddMagazinePurchase(int clan, int magazin)
//{
//	return AddMagazinePurchase(clan, magazin, DateTime.Now);
//}
//public static iDbResult AddMagazinePurchase(int clan, int magazin, DateTime datum)
//{
//	//Kupiomagazin kupiomagazin = new Kupiomagazin(clan, magazin, datum);

//	////TODO: Try add magazine purchase
//	//MockDB.Instance.KupovineMagazina.Add(kupiomagazin);

//	iDbResult result = iDbResult.Success;

//	return result;
//}



//// DELETE BOOK REQUEST
//public static iDbResult DeleteFulfilledBookRequest(int clan, int bookID, int lokalID)
//{
//	Ispunjenzahtevzaknjigu toDelete = null;

//	//foreach (Ispunjenzahtevzaknjigu z in MockDB.Instance.ispunjenzahtevzaknjigus)
//	//{
//	//	if (clan == z.Clan && bookID == z.Izknjiga && lokalID == z.Izlokal)
//	//	{
//	//		toDelete = z; break;
//	//	}
//	//}
//	if (toDelete == null)
//		return iDbResult.Error;

//	//MockDB.Instance.ispunjenzahtevzaknjigus.Remove(toDelete);

//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult DeleteFulfilledBookRequest(Ispunjenzahtevzaknjigu request)
//{
//	return DeleteFulfilledBookRequest(request.Clan, request.Izknjiga, request.Izlokal);
//	// TODO: Delete request
//}
//// DELETE NEWS REQUEST
//public static iDbResult DeleteFulfilledNewsRequest(int clan, int novine, int lokal)
//{
//	Ispunjenzahtevzanovine toDelete = null;
//	//foreach (Ispunjenzahtevzanovine z in MockDB.Instance.ispunjenzahtevzanovines)
//	//{
//	//	if (clan == z.Clan && novine == z.Iznovine && lokal == z.Izlokal)
//	//	{
//	//		toDelete = z; break;
//	//	}
//	//}
//	if (toDelete == null)
//		return iDbResult.Error;

//	//MockDB.Instance.ispunjenzahtevzanovines.Remove(toDelete);

//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult DeleteFulfilledNewsRequest(Ispunjenzahtevzanovine request)
//{
//	return DeleteFulfilledNewsRequest(request.Clan, request.Iznovine, request.Izlokal);
//	// TODO: Delete request
//}
//// DELETE MAGAZINE REQUEST
//public static iDbResult DeleteFulfilledMagazineRequest(int clan, int magazin, int lokal)
//{
//	Ispunjenzahtevzamagazin toDelete = null;

//	//foreach (Ispunjenzahtevzamagazin z in MockDB.Instance.ispunjenzahtevzamagazins)
//	//{
//	//	if (clan == z.Clan && magazin == z.Izmagazin && lokal == z.Izlokal)
//	//	{
//	//		toDelete = z; break;
//	//	}
//	//}
//	//if (toDelete == null)
//	//	return iDbResult.Error;

//	//MockDB.Instance.ispunjenzahtevzamagazins.Remove(toDelete);
//	iDbResult result = iDbResult.Success;

//	return result;
//}
//public static iDbResult DeleteFulfilledMagazineRequest(Ispunjenzahtevzamagazin request)
//{
//	return DeleteFulfilledMagazineRequest(request.Clan, request.Izmagazin, request.Izlokal);
//	// TODO: Delete request
//}
//// DELETE RESERVATION
//public static iDbResult DeleteReservation(Rezervacija rez)
//{
//	return DeleteReservation(rez.IDClan, rez.IDKnjiga, rez.IDBK);
//}
//public static iDbResult DeleteReservations(List<Rezervacija> rezervacije)
//{
//	foreach (Rezervacija rez in rezervacije)
//	{
//		if (DeleteReservation(rez.IDClan, rez.IDKnjiga, rez.IDBK) == iDbResult.Error)
//			return iDbResult.Error;
//	}
//	return iDbResult.Success;
//}
//public static iDbResult DeleteReservation(int clan, int knjiga, int lokal)
//{
//	Rezervacija toDelete = null;
//	//foreach (Rezervacija r in MockDB.Instance.Rezervacije)
//	//{
//	//	if (r.Clan == clan && r.Knjiga == knjiga && r.Lokal == lokal)
//	//	{
//	//		toDelete = r; break;
//	//	}
//	//}
//	//if (toDelete == null)
//	//	return iDbResult.Error;

//	//MockDB.Instance.Rezervacije.Remove(toDelete);

//	iDbResult result = iDbResult.Success;

//	return result;
//}
#endregion
