using Azure.Identity;
using BP2ProjekatCornerLibrary.Models;
using Format = BP2ProjekatCornerLibrary.Models.Format;
using BP2ProjekatCornerLibrary.Models.NonContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Windows.Input;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Controls.Primitives;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;


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
        public static int _currentUserID;

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
        public static T ExecuteQueryFirst<T>(string query) where T : new()
        {
            List<string> columns = new List<string>();
            T ret = new T();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(query, connection);
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
                    connection.Close();
                }
                return ret;
            }
        }
        public static List<T> ExecuteQueryList<T>(string query)
        {
            List<string> columns = new List<string>();
            List<T> ret = new List<T>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(query, connection);
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
                    connection.Close();
                }
                return ret.Count > 0 ? ret : null;
            }
        }
        #region NEW STUFF
        #region PUBLIC
        public static bool CheckDbNotNull(object obj)
        {
            return obj.GetType() != typeof(DBNull);
        }
        public static bool CheckEntityExists<T>(object obj) where T : new()
        {
            if (!obj.GetType().IsSubclassOf(typeof(_DbClass))) return false;

            _DbClass asDbClass = obj as _DbClass;
            List<ClassPropertyValue> keys = asDbClass.GetKeyProperties();
            string param = "";
            for (int i = 0; i < keys.Count; i++)
            {
                param += $"{keys[i].Name}={MakeSqlValue(keys[i].Value)}";
                if (i < keys.Count - 1) param += " and ";
            }
            return (GetListFromSQL<T>(param).Count > 0);
        }
        public static bool CheckEntityExists<T>(List<ClassPropertyValue> keys) where T : new()
        {
            return CheckEntityExists<T>(keys.ToArray());
        }
        public static bool CheckEntityExists<T>(ClassPropertyValue[] keys) where T : new()
        {
            if (!typeof(T).IsSubclassOf(typeof(_DbClass))) return false;

            _DbClass asDbClass = new T() as _DbClass;
            List<ClassPropertyValue> Tkeys = asDbClass.GetKeyProperties();

            Dictionary<string, object> dictKeys = new Dictionary<string, object>();
            foreach (ClassPropertyValue k in keys)
                dictKeys.Add(k.Name, k.Value);


            Dictionary<string, object> dictTKeys = new Dictionary<string, object>();
            foreach (ClassPropertyValue k in Tkeys)
                dictTKeys.Add(k.Name, k.Value);

            foreach (string name in dictTKeys.Keys)
            {
                if (dictKeys[name] == null)
                {
                    return false;
                }
            }

            string param = "";
            for (int i = 0; i < keys.Length; i++)
            {
                param += $"{keys[i].Name}={MakeSqlValue(keys[i].Value)}";
                if (i < keys.Length - 1) param += " and ";
            }
            return (GetListFromSQL<T>(param).Count > 0);
        }
        #endregion
        #region SQL DIRECT EXECUTION

        #region HELPERS
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
        private static string EncapsulateInSingleQuote(string inStr)
        {
            return "'" + inStr + "'";
        }
        private static string TryEncapsulateInSingleQuote(object value)
        {
            if (value.GetType() == typeof(string) || value.GetType() == typeof(DateTime))
                return "'" + value.ToString() + "'";
            else return value.ToString();
        }
        private static string MakeSqlValue(object value)
        {
            if (value == null)
                return "NULL";
            else
                return TryEncapsulateInSingleQuote(value);
        }
        private static List<ClassPropertyValue> GetDBProperties(object obj, bool withKeyIdentity = false)
        {
            _DbClass asDbClass = obj as _DbClass;
            if (asDbClass == null) return null;

            Type type = obj.GetType();
            ClassPropertyValue idKey = asDbClass.GetKeyIdentity();

            PropertyInfo[] properties = type.GetProperties();
            List<string> dbProperties = asDbClass.GetDbPropertyNames();
            List<ClassPropertyValue> propertyValues = new List<ClassPropertyValue>();

            if (withKeyIdentity)
            {
                foreach (PropertyInfo pi in properties)
                {
                    if (dbProperties.Contains(pi.Name))
                    {
                        propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(obj)));
                    }
                }
            }
            else
            {
                foreach (PropertyInfo pi in properties)
                {
                    if (!(idKey != null && pi.Name == idKey.Name))
                    {
                        if (dbProperties.Contains(pi.Name))
                        {
                            propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(obj)));
                        }
                    }
                }
            }
            return propertyValues;
        }
        #endregion
        #region GET
        //GET
        public static int GetListFromSQL<T>(out List<T> list, string sqlParameters = null) where T : new()
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
                    connection.Close();
                }
            }
            list = ret;
            return ret.Count;
        }
        public static List<T> GetListFromSQL<T>(string sqlParameters = null) where T : new()
        {
            List<T> ret = new List<T>();
            GetListFromSQL<T>(out ret, sqlParameters);
            return ret;
        }
        public static bool GetFirstFromSQL<T>(out T obj, string sqlParameters = null) where T : new()
        {
            List<string> columns = new List<string>();
            T ret = new T();
            string sqlCommand = $"select * from {typeof(T).Name}";
            if (sqlParameters != null) sqlCommand += " where " + sqlParameters;

            obj = ret;
            bool found = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sqlCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        found = true;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                        }

                        ClassPropertyValue[] values = new ClassPropertyValue[reader.FieldCount];
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = new ClassPropertyValue(columns[i], reader[columns[i]]);
                        }

                        obj = CreateInstance<T>(values);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    connection.Close();
                }
            }
            return found;
        }
        public static T GetFirstFromSQL<T>(string sqlParameters = null) where T : new()
        {
            T ret = new T();
            GetFirstFromSQL<T>(out ret, sqlParameters);
            return ret;
        }
        #endregion

        #region ADD
        //ADD
        public static int AddItemWithSQLWithIdentity<T>(T toAdd) where T : new()
        {
            if (toAdd == null || !typeof(T).IsSubclassOf(typeof(_DbClass))) return 0;

            //_DbClass asDbClass = toAdd as _DbClass;

            //if (CheckEntityExists<T>(asDbClass.GetKeyProperties()))
            //    return 0;

            //Type type = typeof(T);
            //ClassPropertyValue idKey = asDbClass.GetKeyIdentity();

            //PropertyInfo[] properties = type.GetProperties();
            //List<ClassPropertyValue> propertyValues = new List<ClassPropertyValue>();

            //foreach (PropertyInfo pi in properties)
            //{
            //    if (!(idKey != null && pi.Name == idKey.Name))
            //        propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(toAdd)));
            //}

            List<ClassPropertyValue> propertyValues = GetDBProperties(toAdd);


            string sqlInsertCommand = $"insert into {typeof(T).Name}(";
            for (int i = 0; i < propertyValues.Count; i++)
            {
                sqlInsertCommand += propertyValues[i].Name;
                if (i < propertyValues.Count - 1) sqlInsertCommand += ", ";
            }
            sqlInsertCommand += ") values (";

            for (int i = 0; i < propertyValues.Count; i++)
            {
                sqlInsertCommand += MakeSqlValue(propertyValues[i].Value);

                if (i < propertyValues.Count - 1) sqlInsertCommand += ", ";
            }
            sqlInsertCommand += ");";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlInsertCommand, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    connection.Close();
                    // Check Error
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        return 0;
                    }
                }
            }
            int ret = 0;
            string getIdCommand = $"select IDENT_CURRENT('{typeof(T).Name}')";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(getIdCommand, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader[0] != null)
                        {
                            ret = int.Parse(reader[0].ToString()); break;
                        }
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    connection.Close();
                }
            }
            return ret;
        }

        public static bool AddItemWithSQL<T>(T toAdd) where T : new()
        {
            if (toAdd == null || !typeof(T).IsSubclassOf(typeof(_DbClass))) return false;

            _DbClass asDbClass = toAdd as _DbClass;

            if (CheckEntityExists<T>(asDbClass.GetKeyProperties()))
                return false;

            //Type type = typeof(T);

            //PropertyInfo[] properties = type.GetProperties();
            //List<ClassPropertyValue> propertyValues = new List<ClassPropertyValue>();

            //foreach (PropertyInfo pi in properties)
            //{
            //    propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(toAdd)));
            //}
            List<ClassPropertyValue> propertyValues = GetDBProperties(toAdd);


            string sqlInsertCommand = $"insert into {typeof(T).Name}(";
            for (int i = 0; i < propertyValues.Count; i++)
            {
                sqlInsertCommand += propertyValues[i].Name;
                if (i < propertyValues.Count - 1) sqlInsertCommand += ", ";
            }
            sqlInsertCommand += ") values (";

            for (int i = 0; i < propertyValues.Count; i++)
            {
                sqlInsertCommand += MakeSqlValue(propertyValues[i].Value);

                if (i < propertyValues.Count - 1) sqlInsertCommand += ", ";
            }
            sqlInsertCommand += ");";

            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand command = new SqlCommand(sqlInsertCommand, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    connection.Close();
                    // Check Error
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region UPDATE
        //SET
        public static bool UpdateItemWithSQL<T>(T toUpdate) where T : new()
        {
            if (toUpdate == null || !typeof(T).IsSubclassOf(typeof(_DbClass))) return false;
            _DbClass asDbClass = toUpdate as _DbClass;
            List<ClassPropertyValue> keys = asDbClass.GetKeyProperties();
            if (!CheckEntityExists<T>(keys)) return false;

            //Type type = typeof(T);

            //PropertyInfo[] properties = type.GetProperties();
            //List<ClassPropertyValue> propertyValues = new List<ClassPropertyValue>();

            //foreach (PropertyInfo pi in properties)
            //{
            //    bool isKey = false;
            //    foreach (ClassPropertyValue kpv in keys)
            //    {
            //        if (kpv.Name == pi.Name)
            //        {
            //            isKey = true;
            //            break;
            //        }
            //    }
            //    if (!isKey)
            //        propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(toUpdate)));
            //}
            List<ClassPropertyValue> propertyValues = GetDBProperties(toUpdate);

            string keysToSQL = "";
            for (int i = 0; i < keys.Count; i++)
            {
                keysToSQL += $"{keys[i].Name}={MakeSqlValue(keys[i].Value)}";
                if (i < keys.Count - 1)
                    keysToSQL += " and ";
            }


            string propertiesToSet = "";

            for (int i = 0; i < propertyValues.Count; i++)
            {

                propertiesToSet += $"{propertyValues[i].Name}={MakeSqlValue(propertyValues[i].Value)}";
                if (i < propertyValues.Count - 1) propertiesToSet += ", ";
            }

            string sqlCommand = $"update {typeof(T).Name} set {propertiesToSet} where {keysToSQL}";


            using (SqlConnection connection = new SqlConnection(connString))
            {

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    connection.Close();
                    // Check Error
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion

        #region DELETE
        //DELETE
        public static bool DeleteItemWithSQL<T>(T toDelete)
        {
            if (toDelete == null || !typeof(T).IsSubclassOf(typeof(_DbClass))) return false;

            _DbClass asDbClass = toDelete as _DbClass;
            //Type type = typeof(T);

            //PropertyInfo[] properties = type.GetProperties();
            //List<ClassPropertyValue> keys = asDbClass.GetKeyProperties();
            //List<ClassPropertyValue> propertyValues = new List<ClassPropertyValue>();

            //foreach (PropertyInfo pi in properties)
            //{
            //    bool isKey = false;
            //    foreach (ClassPropertyValue kpv in keys)
            //    {
            //        if (kpv.Name == pi.Name)
            //        {
            //            isKey = true;
            //            break;
            //        }
            //    }
            //    if (!isKey)
            //        propertyValues.Add(new ClassPropertyValue(pi.Name, pi.GetValue(toDelete)));
            //}

            List<ClassPropertyValue> keys = asDbClass.GetKeyProperties();
            List<ClassPropertyValue> propertyValues = GetDBProperties(toDelete);

            string keysToSQL = "";
            for (int i = 0; i < keys.Count; i++)
            {
                keysToSQL += $"{keys[i].Name}={MakeSqlValue(keys[i].Value)}";
                if (i < keys.Count - 1)
                    keysToSQL += " and ";
            }

            string sqlCommand = $"delete from {typeof(T).Name} where {keysToSQL}";


            using (SqlConnection connection = new SqlConnection(connString))
            {

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    connection.Close();
                    // Check Error
                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into Database!");
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion
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
        //ADRESA
        public static List<Adresa> GetAllAdresa(string args = null)
        {
            return GetListFromSQL<Adresa>(args);
        }
        public static Adresa GetAdresa(string ulica, string broj)
        {
            return GetFirstFromSQL<Adresa>($"Ulica={MakeSqlValue(ulica)} and Broj = {MakeSqlValue(broj)}");
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

        //DONE
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
        public static Zaposleni GetEmployee(int workerId, iTipRadnika tip)
        {
            List<Zaposleni> workerList = new List<Zaposleni>();
            workerList.AddRange(GetListFromSQL<Admin>($"IDRadnik={workerId}"));
            if (workerList.Count == 0)
            {
                workerList.Add(GetWorker(workerId, tip));
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
        public static Radnik GetWorker(int workerId, iTipRadnika tip)
        {
            List<Radnik> workerList = new List<Radnik>();
            if (tip == iTipRadnika.Bibliotekar)
            {
                workerList.AddRange(GetListFromSQL<Bibliotekar>($"IDRadnik={workerId}"));
            }
            else if (tip == iTipRadnika.Kurir)
            {
                workerList.AddRange(GetListFromSQL<Kurir>($"IDRadnik={workerId}"));
                if (workerList.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                return null;
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
        public static List<RasporedjenBibliotekar> GetRasporedjenBibliotekar(string args = null)
        {
            return GetListFromSQL<RasporedjenBibliotekar>(args);
        }
        public static RasporedjenBibliotekar GetLatestRasporedjenBibliotekar(int idRadnik)
        {
            var rb = ExecuteQueryFirst<RasporedjenBibliotekar>($"SELECT * FROM RasporedjenBibliotekar WHERE DatVr IN(SELECT max(DatVr) FROM RasporedjenBibliotekar);");
            return rb as RasporedjenBibliotekar;
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
        //ADMIN
        public static Admin GetAdmin()
        {
            return GetFirstFromSQL<Admin>();
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
        public static List<IzmenaLokala> GetAllIzmenaLokala(string args = null)
        {
            return GetListFromSQL<IzmenaLokala>(args);
        }
        #endregion

        //DONE
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
        public static List<KorisnickiNalog> GetAllOpenKorisnickiNalogs()
        {
            return GetAllKorisnickiNalogs("DatZatvaranja IS NULL");
        }
        public static List<KorisnickiNalog> GetAllClosedKorisnickiNalogs()
        {
            return GetAllKorisnickiNalogs("DatZatvaranja IS NOT NULL");
        }
        public static KorisnickiNalog GetKorisnickiNalog(int id, iTipRadnika tip)
        {
            if (tip == iTipRadnika.Bibliotekar)
            {
                return GetBibNalog(id);
            }
            else if (tip == iTipRadnika.Kurir)
            {
                return GetKurirNalog(id);
            }
            else if (tip == iTipRadnika.Admin)
            {
                return GetAdminNalog(id);
            }
            return null;
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

        #region AUTOR
        public static List<Autor> GetAllAutors(bool includeDeleted = false)
        {
            if(!includeDeleted)
                return GetListFromSQL<Autor>("DatBrisanja IS NULL");
            else
                return GetListFromSQL<Autor>();
        }
        public static Autor GetAutor(int autorID)
        {
            Autor ret = null;
            GetFirstFromSQL<Autor>(out ret, $"IDAutor={autorID}");
            return ret;
        }
        public static List<Autor> GetAutorsByName(string name, string lastName)
        {
            return GetListFromSQL<Autor>($"Ime={MakeSqlValue(name)} and Prezime={MakeSqlValue(lastName)}");
        }
        public static List<IzmenaAutora> GetAllIzmeneAutora(string args = null)
        {
            return GetListFromSQL<IzmenaAutora>(args);
        }
        //RELACIJE
        public static List<Pise> GetAllPise(string args)
        {
            return GetListFromSQL<Pise>(args);
        }
        public static List<Autor> GetAllBookAuthors(Knjiga k)
        {
            List<Autor> ret = new List<Autor>();
            foreach (Pise p in GetAllPiseWithBook(k))
            {
                ret.Add(GetAutor(p.IDAutor));
            }
            return ret.Count > 0 ? ret : null;
        }
        public static List<IzKnjigeAutor> GetAllIzmenaKnjigeAutor(string args = null)
        {
            return GetListFromSQL<IzKnjigeAutor>(args);
        }
        public static List<IzmenaAutora> GetAllIzmenaAutora(string args = null)
        {
            return GetListFromSQL<IzmenaAutora>(args);
        }
        #endregion

        #region JEZIK
        public static List<Jezik> GetAllJeziks()
        {
            return GetListFromSQL<Jezik>();
        }
        public static Jezik GetJezik(string oznj)
        {
            Jezik ret = null;
            GetFirstFromSQL<Jezik>(out ret, $"OZNJ={MakeSqlValue(oznj)}");
            return ret;
        }
        public static Jezik GetJezikByName(string jezik)
        {
            Jezik ret = null;
            GetFirstFromSQL<Jezik>(out ret, $"NazivJezika={MakeSqlValue(jezik)}");
            return ret;
        }
        // RELACIJE
        public static List<Jezik> GetAllSStivoJeziks(SerijskoStivo ss)
        {
            List<Jezik> ret = new List<Jezik>();
            foreach (SStivoNaJeziku ssnj in GetAllSStivoNaJezikuWithSS(ss))
                ret.Add(GetJezik(ssnj.OZNJ));
            return ret.Count > 0 ? ret : null;
        }
        public static List<Jezik> GetAllBookJeziks(Knjiga k)
        {
            List<Jezik> ret = new List<Jezik>();
            foreach (KnjigaNaJeziku knj in GetAllKnjigaNaJezikuWithBook(k))
                ret.Add(GetJezik(knj.OZNJ));
            return ret.Count > 0 ? ret : null;
        }
        public static List<KnjigaNaJeziku> GetAllKnjigaNaJeziku(string args = null)
        {
            return GetListFromSQL<KnjigaNaJeziku>(args);
        }
        public static List<IzKnjigeJezik> GetAllIzmenaKnjigaJezik(string args = null)
        {
            return GetListFromSQL<IzKnjigeJezik>(args);
        }
        public static List<SStivoNaJeziku> GetAllSStivoNaJeziku(string args = null)
        {
            return GetListFromSQL<SStivoNaJeziku>(args);
        }
        public static List<IzSStivaJezik> GetAllIzmenaSStivaJezik(string args = null)
        {
            return GetListFromSQL<IzSStivaJezik>(args);
        }
        #endregion

        #region ZANR
        public static List<Zanr> GetAllZanrs()
        {
            return GetListFromSQL<Zanr>();
        }
        public static Zanr GetZanr(string oznz)
        {
            Zanr ret = null;
            GetFirstFromSQL<Zanr>(out ret, $"OZNZ={MakeSqlValue(oznz)}");
            return ret;
        }
        public static Zanr GetZanrByName(string zanr)
        {
            Zanr ret = null;
            GetFirstFromSQL<Zanr>(out ret, $"NazivZanra={MakeSqlValue(zanr)}");
            return ret;
        }
        // RELACIJE

        public static List<Zanr> GetAllBookZanrs(Knjiga k)
        {
            List<Zanr> ret = new List<Zanr>();
            foreach (PripadaZanru pz in GetAllPripadaZanruWithBook(k))
                ret.Add(GetZanr(pz.OZNZ));
            return ret.Count > 0 ? ret : null;
        }
        public static List<PripadaZanru> GetAllPripadaZanru(string args = null)
        {
            return GetListFromSQL<PripadaZanru>(args);
        }
        public static List<IzKnjigeZanr> GetAllIzmenaKnjigeZanr(string args = null)
        {
            return GetListFromSQL<IzKnjigeZanr>(args);
        }
        #endregion

        #region FORMAT
        // FORMAT
        public static List<Format> GetAllFormats(string args = null)
        {
            return GetListFromSQL<Format>(args);
        }
        #endregion

        #region PERIOD
        public static List<Periodicnost> GetAllPeriod(string args = null)
        {
            return GetListFromSQL<Periodicnost>(args);
        }
        public static Periodicnost GetPeriod(string args)
        {
            return GetFirstFromSQL<Periodicnost>($"PeriodIzd={MakeSqlValue(args)}");
        }
        public static List<Periodicnost> GetAllPeriodSorted()
        {
            return ExecuteQueryList<Periodicnost>($"SELECT * FROM Periodicnost ORDER BY Ucestalost ASC;") as List<Periodicnost>;
        }

        #endregion

        #region IZDAVACKA KUCA
        // IZDAVACKA KUCA
        public static IzdKuca GetIzdKuca(int idik)
        {
            return GetFirstFromSQL<IzdKuca>($"IDIK={idik}");
        }
        public static List<IzdKuca> GetAllIzdKucas(bool includeClosed = false)
        {
            string? arg = includeClosed ? null : "DatZat IS NULL";
            return GetListFromSQL<IzdKuca>(arg);
        }
        // RELACIJE
        public static List<IzdKuca> GetAllSStivoIzdKucas(SerijskoStivo ss)
        {
            List<IzdKuca> ret = new List<IzdKuca>();
            foreach (IzdajeSStivo iss in GetAllIzdajeSStivoWithSS(ss))
                ret.Add(GetIzdKuca(iss.IDIK));
            return ret.Count > 0 ? ret : null;
        }
        public static List<IzdKuca> GetAllBookIzdKucas(Knjiga k)
        {
            List<IzdKuca> ret = new List<IzdKuca>();
            foreach (IzdajeKnjigu ik in GetAllIzdajeKnjiguWithBook(k))
                ret.Add(GetIzdKuca(ik.IDIK));
            return ret.Count > 0 ? ret : null;
        }
        public static List<IzdajeKnjigu> GetAllIzdajeKnjigu(string args = null)
        {
            return GetListFromSQL<IzdajeKnjigu>(args);
        }
        public static List<IzdajeSStivo> GetAllIzdajeSStivo(string args = null)
        {
            return GetListFromSQL<IzdajeSStivo>(args);
        }
        public static List<IzmenaIzdKuce> GetAllIzmenaIzdKuce(string args = null)
        {
            return GetListFromSQL<IzmenaIzdKuce>(args);
        }
        #endregion

        #region KNJIGA
        // KNJIGA
        public static Knjiga GetKnjiga(int id)
        {
            return GetFirstFromSQL<Knjiga>($"IDKnjiga={id}");
        }
        public static List<Knjiga> GetAllKnjigas(string args = null, bool includeDeleted = false)
        {
            if (!includeDeleted)
            {
                if (args == null) args = "DatBrisanja IS NULL";
                else args += " and DatBrisanja IS NULL";
            }
            return GetListFromSQL<Knjiga>(args);
        }
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
        // RELACIJE
        public static List<KnjigaULokalu> GetAllKnjigeULokalu(string args = null)
        {
            return GetListFromSQL<KnjigaULokalu>(args);
        }
        public static List<KnjigaULokalu> GetAllLatestKnjigeULokalu(int idLokal)
        {
            List<KnjigaULokalu> allKul = ExecuteQueryList<KnjigaULokalu>($"select * from KnjigaULokalu where IDBK={idLokal}");
            List<int> idKnjigas = new List<int>();
            foreach(KnjigaULokalu kul in allKul)
            {
                if(!idKnjigas.Contains(kul.IDKnjiga)) idKnjigas.Add(kul.IDKnjiga);
            }

            List<KnjigaULokalu> ret = new List<KnjigaULokalu>();
            foreach(int k in idKnjigas)
            {
                ret.Add(ExecuteQueryFirst<KnjigaULokalu>($"select * from KnjigaULokalu where IDBK={idLokal} and IDKnjiga={k} and DatVrIzmene IN(Select max(DatVrIzmene) FROM KnjigaULokalu where IDBK={idLokal} and IDKnjiga={k});"));
            }
            return ret;
        }
        public static List<Pise> GetAllPiseWithBook(Knjiga k)
        {
            return GetAllPise($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
        }
        public static List<KnjigaNaJeziku> GetAllKnjigaNaJezikuWithBook(Knjiga k)
        {
            return GetAllKnjigaNaJeziku($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
        }
        public static List<PripadaZanru> GetAllPripadaZanruWithBook(Knjiga k)
        {
            return GetAllPripadaZanru($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
        }
        public static List<IzdajeKnjigu> GetAllIzdajeKnjiguWithBook(Knjiga k)
        {
            return GetAllIzdajeKnjigu($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
        }
        // izmnee
        public static List<IzmenaKnjige> GetAllIzmenaKnjige(string args = null)
        {
            return GetListFromSQL<IzmenaKnjige>(args);
        }
        public static List<IzKnjigeIzdKuca> GetAllIzmenaKnjigeIzdKuca(string args = null)
        {
            return GetListFromSQL<IzKnjigeIzdKuca>(args);
        }
        #endregion

        #region SERIJSKO STIVO
        //SSTIVO
        public static List<SerijskoStivo> GetAllSStivo(string args = null, bool includeDeleted = false)
        {
            if (!includeDeleted)
            {
                if (args == null) args = "DatBrisanja IS NULL";
                else args += " and DatBrisanja IS NULL";
            }
            return GetListFromSQL<SerijskoStivo>(args);
        }
        public static List<SerijskoStivo> GetAllNews(bool includeDeleted = false)
        {
            string arg = "TipStiva=1";
            if (!includeDeleted) arg += " and DatBrisanja IS NULL";
            return GetListFromSQL<SerijskoStivo>(arg);
        }
        public static List<SerijskoStivo> GetAllMagazines(bool includeDeleted = false)
        {
            string arg = "TipStiva=2";
            if (!includeDeleted) arg += " and DatBrisanja IS NULL";
            return GetListFromSQL<SerijskoStivo>(arg);
        }
        public static List<IzdanjeSStiva> GetAllIzdanjeSStiva(string args = null)
        {
            return GetListFromSQL<IzdanjeSStiva>(args);
        }
        public static List<IzdanjeSStiva> GetAllIzdanjeSStivaFromSS(SerijskoStivo ss)
        {
            return GetAllIzdanjeSStivaFromSS(ss.IDSStivo);
        }
        public static List<IzdanjeSStiva> GetAllIzdanjeSStivaFromSS(int idSS)
        {
            return GetListFromSQL<IzdanjeSStiva>($"IDSStivo={MakeSqlValue(idSS)}");
        }
        public static SerijskoStivo GetSerijskoStivo(int IDSStivo)
        {
            return GetFirstFromSQL<SerijskoStivo>($"IDSStivo={MakeSqlValue(IDSStivo)}");
        }
        public static IzdanjeSStiva GetIzdanjeSStiva(int IDSStivo, int BrIzd)
        {
            return GetFirstFromSQL<IzdanjeSStiva>($"IDSStivo={MakeSqlValue(IDSStivo)} and BrIzd={MakeSqlValue(BrIzd)}");
        }
        public static IzdanjeSStiva GetLatestIzdanjeSStiva(int IDStivo)
        {
            return (IzdanjeSStiva)ExecuteQueryFirst<IzdanjeSStiva>($"SELECT * From IzdanjeSStiva where IDSStivo={MakeSqlValue(IDStivo)} and BrIzd IN(Select max(BrIzd) FROM IzdanjeSStiva where IDSStivo={MakeSqlValue(IDStivo)});");
        }
        public static bool CheckIfEditionExists(int idStivo, int brIzd)
        {
            return GetAllIzdanjeSStiva($"IDSStivo={MakeSqlValue(idStivo)} and BrIzd={MakeSqlValue(brIzd)}").Count > 0;
        }

        public static List<IzdSStivoULokalu> GetAllLatestIzdSStivoULokalu(int idLokal)
        {
            List<IzdSStivoULokalu> allSSul = ExecuteQueryList<IzdSStivoULokalu>($"select * from IzdSStivoULokalu where IDBK={idLokal}");
            List<Tuple<int, int>> idSS = new List<Tuple<int, int>>();
            foreach (IzdSStivoULokalu ssul in allSSul)
            {
                Tuple<int, int> toAdd = new Tuple<int, int>(ssul.IDSStivo, ssul.BrIzd);
                if (!idSS.Contains(toAdd)) idSS.Add(toAdd);
            }

            List<IzdSStivoULokalu> ret = new List<IzdSStivoULokalu>();
            foreach (Tuple<int,int> iss in idSS)
            {
                ret.Add(ExecuteQueryFirst<IzdSStivoULokalu>($"select * from IzdSStivoULokalu where IDBK={idLokal} and IDSStivo={iss.Item1} and BrIzd={iss.Item2} and DatVrIzmene IN(Select max(DatVrIzmene) FROM IzdSStivoULokalu where IDBK={idLokal} and IDSStivo={iss.Item1} and BrIzd={iss.Item2});"));
            }
            return ret;
        }
        //RELACIJE
        public static List<IzdSStivoULokalu> GetAllIzdSStivoULokalu(string args = null)
        {
            return GetListFromSQL<IzdSStivoULokalu>(args);
        }
        public static IzdSStivoULokalu GetLatestIzdSStivoULokalau(int idStivo, int brIzd, int idLokal)
        {
            return (IzdSStivoULokalu)ExecuteQueryFirst<IzdSStivoULokalu>($"Select * from IzdSStivoULokalu where IDSStivo={MakeSqlValue(idStivo)} and BrIzd={MakeSqlValue(brIzd)} and IDBK={MakeSqlValue(idLokal)} and DatVrIzmene IN(Select max(DatVrIzmene) from IzdSStivoULokalu where IDSStivo={MakeSqlValue(idStivo)} and BrIzd={MakeSqlValue(brIzd)} and IDBK={MakeSqlValue(idLokal)});");
        }
        public static List<SStivoNaJeziku> GetAllSStivoNaJezikuWithSS(SerijskoStivo ss)
        {
            return GetAllSStivoNaJeziku($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
        }
        public static List<IzdajeSStivo> GetAllIzdajeSStivoWithSS(SerijskoStivo ss)
        {
            return GetAllIzdajeSStivo($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
        }
        //izmene
        public static List<IzmenaSStiva> GetAllIzmenaSStiva(string args = null)
        {
            return GetListFromSQL<IzmenaSStiva>(args);
        }
        public static List<IzmenaIzdSStiva> GetAllIzmenaIzdSStiva(string args = null)
        {
            return GetListFromSQL<IzmenaIzdSStiva>(args);
        }
        public static List<IzSStivaIzdKuca> GetAllIzmenaSStivoIzdKuca(string args = null)
        {
            return GetListFromSQL<IzSStivaIzdKuca>(args);
        }

        #endregion

        #endregion



        #region ADDERS

        #region LOKACIJA
        public static bool CheckIfLocationExists(string ulica, string broj, int posBr, string oznd)
        {
            return GetAllLocations($"Ulica='{ulica}' and Broj='{broj}' and PosBr={posBr} and OZND='{oznd}'").Count() > 0;
        }
        public static Lokacija CheckIfLocationExistsAndReturn(string ulica, string broj, int posBr, string oznd)
        {
            Lokacija ret = null;
            GetFirstFromSQL<Lokacija>(out ret, $"Ulica='{ulica}' and Broj='{broj}' and PosBr={posBr} and OZND='{oznd}'");
            return ret;
        }
        /// <summary>
        /// Dodaje adresu ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddAdresa(Adresa adresa)
        {
            return AddItemWithSQL<Adresa>(adresa);
        }
        public static bool AddAdresa(string ulica, string broj)
        {
            return AddAdresa(new Adresa(ulica, broj));
        }
        /// <summary>
        /// Dodaje mesto ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddMesto(Mesto mesto)
        {
            return AddItemWithSQL<Mesto>(mesto);
        }
        public static bool AddMesto(int posBr, string naziv)
        {
            return AddMesto(new Mesto(posBr, naziv));
        }
        /// <summary>
        /// Dodaje drzavu ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddDrzava(Drzava drzava)
        {
            return AddItemWithSQL<Drzava>(drzava);
        }
        public static bool AddDrzava(string oznd, string naziv)
        {
            return AddDrzava(new Drzava(oznd, naziv));
        }
        /// <summary>
        /// Dodaje mesto u drzavi ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddMUD(Mesto m, Drzava d)
        {
            AddMesto(m);
            AddDrzava(d);
            MestoUDrzavi mud = new MestoUDrzavi(m.PosBr, d.OZND);
            return AddItemWithSQL<MestoUDrzavi>(mud);
        }
        /// <summary>
        /// Dodaje lokaciju ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddLokacija(string ulica, string broj, int posBr, string oznd)
        {
            return AddLokacija(new Lokacija(ulica, broj, posBr, oznd));
        }
        /// <summary>
        /// Dodaje lokaciju ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddLokacija(Adresa a, Mesto m, Drzava d)
        {
            AddAdresa(a);
            AddMUD(m, d);
            Lokacija lok = new Lokacija(a.Ulica, a.Broj, m.PosBr, d.OZND);
            AddItemWithSQL<Lokacija>(lok);
            return true;
        }
        /// <summary>
        /// Dodaje lokaciju ako vec ne postoji
        /// </summary>
        /// <param name="adresa"></param>
        /// <returns>True ako je dodao, False ako vec postoji ili je doslo do greske</returns>
        public static bool AddLokacija(Lokacija lok)
        {
            Mesto m = GetMesto(lok.PosBr);
            Drzava d = GetDrzava(lok.OZND);

            if (m != null && d != null)
            {
                return AddLokacija(new Adresa(lok.Ulica, lok.Broj), m, d);
            }
            return false;
        }
        #endregion

        #region WORKER
        public static int AddWorker(Bibliotekar r)
        {
            return AddItemWithSQLWithIdentity<Bibliotekar>(r);
        }
        public static int AddWorker(Kurir r)
        {
            return AddItemWithSQLWithIdentity<Kurir>(r);
        }
        public static int AddAdmin(Admin r)
        {
            return AddItemWithSQLWithIdentity<Admin>(r);
        }
        #endregion

        #region ACCOUNTS
        public static bool AddAccount(KorisnickiNalog kn)
        {
            return AddItemWithSQL<KorisnickiNalog>(kn);
        }
        public static bool AddWorkerWithAccount(Bibliotekar r, KorisnickiNalog k)
        {
            int workerID = AddWorker(r);
            if (workerID == 0) { return false; }

            if (AddAccount(k))
            {
                return AddItemWithSQL<BibliotekarKoristiNalog>(new BibliotekarKoristiNalog(workerID, k.KorisnickoIme));
            }
            return false;
        }
        public static bool AddWorkerWithAccount(Kurir r, KorisnickiNalog k)
        {
            int workerID = AddWorker(r);
            if (workerID == 0) { return false; }

            if (AddAccount(k))
            {
                return AddItemWithSQL<AdminKoristiNalog>(new AdminKoristiNalog(workerID, k.KorisnickoIme));
            }
            return false;
        }
        public static bool AddWorkerWithAccount(Admin r, KorisnickiNalog k)
        {
            int workerID = AddAdmin(r);
            if (workerID == 0) { return false; }

            if (AddAccount(k))
            {
                return AddItemWithSQL<KurirKoristiNalog>(new KurirKoristiNalog(workerID, k.KorisnickoIme));
            }
            return false;
        }

        #endregion

        #region LOKALI
        public static bool AddStore(Biblikutak biblikutak)
        {
            return AddItemWithSQL<Biblikutak>(biblikutak);
        }
        public static bool AddStoreRange(List<Biblikutak> list)
        {
            bool ret = true;
            foreach (Biblikutak b in list)
            {
                if (!(ret = AddItemWithSQL<Biblikutak>(b)))
                    break;
            }
            return ret;
        }
        public static bool AddIzmenaLokala(IzmenaLokala izmenaLokala)
        {
            return AddItemWithSQL<IzmenaLokala>(izmenaLokala);
        }
        #endregion

        #region AUTOR
        public static int AddAutor(Autor autor)
        {
            return AddItemWithSQLWithIdentity<Autor>(autor);
        }
        // Pise
        public static bool AddPise(Pise p)
        {
            return AddItemWithSQL<Pise>(p);
        }
        public static bool AddPise(int a, int k)
        {
            return AddPise(new Pise(a, k));
        }
        // Izmena Autora
        public static bool AddIzmenaAutora(IzmenaAutora ia)
        {
            return AddItemWithSQL(ia);
        }
        //Izmena Pise
        public static bool AddIzmenaKnjigeAutor(IzKnjigeAutor ika)
        {
            return AddItemWithSQL(ika);
        }
        #endregion

        #region JEZIK
        public static bool AddJezik(Jezik j)
        {
            return AddItemWithSQL<Jezik>(j);
        }
        // Knjiga na Jeziku
        public static bool AddKnjigaNaJeziku(KnjigaNaJeziku knj)
        {
            return AddItemWithSQL<KnjigaNaJeziku>(knj);
        }
        public static bool AddKnjigaNaJeziku(Knjiga k, Jezik j)
        {
            return AddKnjigaNaJeziku(new KnjigaNaJeziku(k.IDKnjiga, j.OZNJ));
        }
        public static bool AddKnjigaNaJeziku(int k, string j)
        {
            return AddKnjigaNaJeziku(new KnjigaNaJeziku(k, j));
        }
        // SStivo na Jeziku
        public static bool AddSStivoNaJeziku(SStivoNaJeziku snj)
        {
            return AddItemWithSQL<SStivoNaJeziku>(snj);
        }
        public static bool AddSStivoNaJeziku(int s, string j)
        {
            return AddSStivoNaJeziku(new SStivoNaJeziku(s, j));
        }
        // Izmena Knjige na Jeziku
        public static bool AddIzmenaKnjigeJezik(IzKnjigeJezik ikj)
        {
            return AddItemWithSQL<IzKnjigeJezik>(ikj);
        }
        // Izmena SStiva na Jeziku
        public static bool AddIzmenaSStivaJezik(IzSStivaJezik isj)
        {
            return AddItemWithSQL(isj);
        }
        
        #endregion

        #region ZANR
        public static bool AddZanr(Zanr zanr)
        {
            return AddItemWithSQL<Zanr>(zanr);
        }
        // Pripada Zanru
        public static bool AddPripadaZanru(PripadaZanru pz)
        {
            return AddItemWithSQL<PripadaZanru>(pz);
        }
        public static bool AddPripadaZanru(int k, string z)
        {
            return AddPripadaZanru(new PripadaZanru(k, z));
        }
        // Izmena Pripada Zanru
        public static bool AddIzmenaPripadaZanru(IzKnjigeZanr ikz)
        {
            return AddItemWithSQL<IzKnjigeZanr>(ikz);
        }
        #endregion

        #region FORMAT
        public static bool AddFormat(Format format)
        {
            return AddItemWithSQL<Format>(format);
        }
        #endregion

        #region PERIOD
        public static bool AddPeriod(Periodicnost period)
        {
            return AddItemWithSQL<Periodicnost>(period);
        }
        #endregion

        #region IZD KUCA
        public static int AddIzdKuca(IzdKuca ik)
        {
            return AddItemWithSQLWithIdentity<IzdKuca>(ik);
        }
        public static bool AddIzmenaIzdKuca(IzmenaIzdKuce ik)
        {
            return AddItemWithSQL<IzmenaIzdKuce>(ik);
        }
        // Izdaje Knjigu
        public static bool AddIzdajeKnjigu(IzdajeKnjigu ik)
        {
            return AddItemWithSQL(ik);
        }
        public static bool AddIzdajeKnjigu(int k, int idik)
        {
            return AddIzdajeKnjigu(new IzdajeKnjigu(k, idik));
        }
        // Izdaje SStivo
        public static bool AddIzdajeSStivo(IzdajeSStivo iss)
        {
            return AddItemWithSQL(iss);
        }
        public static bool AddIzdajeSStivo(int s, int idik)
        {
            return AddIzdajeSStivo(new IzdajeSStivo(s, idik));
        }
        #endregion

        #region KNJIGA
        public static int AddBookOnly(Knjiga k)
        {
            return AddItemWithSQLWithIdentity(k);
        }
        public static bool AddIzmenaKnjige(IzmenaKnjige k)
        {
            return AddItemWithSQL<IzmenaKnjige>(k);
        }
        // KNJIGA
        // with Godina Izdavanja
        public static int AddBook(string naziv, int brIzd, int godIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int IzdKuca, string zanr)
        {
            return AddBook(naziv, brIzd, godIzd, "", brStrana, velicinaFonta, korice, format, ograniceno, autorID, jezik, IzdKuca, zanr);
        }
        // with Vreme Izdavanja
        public static int AddBook(string naziv, int brIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int IzdKuca, string zanr)
        {
            return AddBook(naziv, brIzd, -1, vrIzd, brStrana, velicinaFonta, korice, format, ograniceno, autorID, jezik, IzdKuca, zanr);
        }
        // with Both
        private static int AddBook(string naziv, int brIzd, int godIzd, string vrIzd, int brStrana, int velicinaFonta, int korice, string format, int ograniceno, int autorID, string jezik, int izdKuca, string zanr)
        {
            AddFormat(new Format(format));

            Knjiga knjiga = new Knjiga(naziv, brIzd, godIzd, vrIzd, brStrana, velicinaFonta, korice, ograniceno, format);

            return AddBook(knjiga, autorID, jezik, zanr, izdKuca);
        }
        public static int AddBook(Knjiga k, Autor a, Jezik j, Zanr z, IzdKuca i)
        {
            return AddBook(k, a.IDAutor, j.OZNJ, z.OZNZ, i.IDIK);
        }
        public static int AddBook(Knjiga k, int autorID, string oznj, string oznz, int idik)
        {
            int bookID = AddItemWithSQLWithIdentity<Knjiga>(k);

            if (bookID == 0)
            {
                return 0;
            }

            if (!AddPise(new Pise(bookID, autorID))) return 0;
            if (!AddKnjigaNaJeziku(new KnjigaNaJeziku(bookID, oznj))) return 0;
            if (!AddPripadaZanru(new PripadaZanru(bookID, oznz))) return 0;
            if (!AddIzdajeKnjigu(new IzdajeKnjigu(bookID, idik))) return 0;

            return bookID;
        }
        public static int AddBook(Knjiga k, List<Autor> autorList, List<Jezik> jezikList, List<Zanr> zanrList, List<IzdKuca> izdKucaList)
        {
            List<int> autorIDs = new List<int>();
            List<string> oznjs = new List<string>();
            List<string> oznzs = new List<string>();
            List<int> idiks = new List<int>();
            foreach (Autor a in autorList) autorIDs.Add(a.IDAutor);
            foreach (Jezik j in jezikList) oznjs.Add(j.OZNJ);
            foreach (Zanr z in zanrList) oznzs.Add(z.OZNZ);
            foreach (IzdKuca i in izdKucaList) idiks.Add(i.IDIK);

            return AddBook(k, autorIDs, oznjs, oznzs, idiks);
        }
        public static int AddBook(Knjiga k, List<int> autorIDs, List<string> oznjs, List<string> oznzs, List<int> idiks)
        {
            int bookID = AddItemWithSQLWithIdentity<Knjiga>(k);

            if (bookID == 0)
            {
                return 0;
            }

            foreach (int autorID in autorIDs)
                if (!AddPise(new Pise(bookID, autorID))) return 0;

            foreach (string oznj in oznjs)
                if (!AddKnjigaNaJeziku(new KnjigaNaJeziku(bookID, oznj))) return 0;

            foreach (string oznz in oznzs)
                if (!AddPripadaZanru(new PripadaZanru(bookID, oznz))) return 0;

            foreach (int idik in idiks)
                if (!AddIzdajeKnjigu(new IzdajeKnjigu(bookID, idik))) return 0;

            return bookID;
        }
        // KNJIGA U LOKALU
        public static bool AddKnjigaULokalu(int knjigaID, int lokalID, int kolicina)
        {
            KnjigaULokalu kul = new KnjigaULokalu(knjigaID, lokalID, kolicina);

            return AddItemWithSQL<KnjigaULokalu>(kul);
        }
        // Izmene
        public static bool AddIzmenaIzdajeKnjigu(IzKnjigeIzdKuca ik)
        {
            return AddItemWithSQL(ik);
        }
        #endregion

        #region SERIJSKOSTIVO
        public static int AddSStivoOnly(SerijskoStivo ss)
        {
            return AddItemWithSQLWithIdentity(ss);
        }
        public static int AddSStivo(string naziv, int tip, string format, string period, string jezik, int izdKuca)
        {
            return AddSStivo(new SerijskoStivo(naziv, tip, format, period), jezik, izdKuca);
        }
        public static int AddSStivo(SerijskoStivo s, Jezik j, IzdKuca ik)
        {
            return AddSStivo(s, j.OZNJ, ik.IDIK);
        }
        public static int AddSStivo(SerijskoStivo s, string j, int ik)
        {
            int ssId = AddItemWithSQLWithIdentity(s);
            if (ssId == 0)
            {
                return 0;
            }

            if (!AddSStivoNaJeziku(new SStivoNaJeziku(ssId, j))) return 0;
            if (!AddIzdajeSStivo(new IzdajeSStivo(ssId, ik))) return 0;

            return ssId;
        }
        public static int AddSStivo(SerijskoStivo s, List<Jezik> js, List<IzdKuca> iks)
        {
            List<string> jeziks = new List<string>();
            List<int> izdKuce = new List<int>();
            foreach (Jezik j in js) jeziks.Add(j.OZNJ);
            foreach (IzdKuca ik in iks) izdKuce.Add(ik.IDIK);

            return AddSStivo(s, jeziks, izdKuce);
        }
        public static int AddSStivo(SerijskoStivo s, List<string> js, List<int> iks)
        {
            int ssId = AddItemWithSQLWithIdentity(s);
            if (ssId == 0)
            {
                MessageBox.Show("Došlo je do greške pri dodavanju serijskog štiva!");
                return 0;
            }

            foreach (string j in js)
                if (!AddSStivoNaJeziku(new SStivoNaJeziku(ssId, j)))
                {
                    MessageBox.Show("Došlo je do greške pri dodavanju jezika serijskog štiva!");
                    return 0;
                }
            foreach (int ik in iks)
                if (!AddIzdajeSStivo(new IzdajeSStivo(ssId, ik)))
                {
                    MessageBox.Show("Došlo je do greške pri dodavanju izdavačke kuće serijskog štiva!");
                    return 0;
                }

            return ssId;
        }
        // Izdanje SStivo
        public static bool AddIzdanjeSStiva(IzdanjeSStiva s)
        {
            //if (!CheckEntityExists<IzdanjeSStiva>(new ClassPropertyValue("IDSStivo", s.IDSStivo)))
            //    return false;

            return AddItemWithSQL(s);
        }
        // Izd SStivo u Lokalu
        public static bool AddIzdSStivoULokalu(IzdSStivoULokalu isl)
        {
            return AddItemWithSQL(isl);
        }
        public static bool AddIzdSStivoULokalu(int idss, int brIzd, int lokalId, int kolicina)
        {
            return AddIzdSStivoULokalu(new IzdSStivoULokalu(idss, brIzd, lokalId, kolicina));
        }
        // Izmena SStivo
        public static bool AddIzmenaSStiva(IzmenaSStiva iss)
        {
            return AddItemWithSQL<IzmenaSStiva>(iss);
        }
        // Izmena Izd SStiva
        public static bool AddIzmenaIzdSStiva(IzmenaIzdSStiva iiss)
        {
            return AddItemWithSQL(iiss);
        }
        public static bool AddIzmenaSStivaIzdKuca(IzSStivaIzdKuca issik)
        {
            return AddItemWithSQL(issik);
        }
        #endregion

        #endregion



        #region UPDATERS

        #region LOKACIJA
        #endregion

        #region WORKERS
        public static bool UpdateWorker(Radnik selectedWorker)
        {
            if (selectedWorker.GetType() == typeof(Bibliotekar))
            {
                return UpdateItemWithSQL<Bibliotekar>(selectedWorker as Bibliotekar);
            }
            else if (selectedWorker.GetType() == typeof(Kurir))
            {
                return UpdateItemWithSQL<Kurir>(selectedWorker as Kurir);
            }
            return false;
        }
        public static bool UpdateAccount(KorisnickiNalog nalog)
        {
            return UpdateItemWithSQL<KorisnickiNalog>(nalog);
        }
        public static bool UpdateUsername(string oldUsername, string newUsername)
        {
            KorisnickiNalog nalog = GetKorisnickiNalog(oldUsername);
            switch (nalog.TipNaloga)
            {
                case 1:
                    AdminKoristiNalog akn = GetFirstFromSQL<AdminKoristiNalog>($"KorisnickoIme={MakeSqlValue(oldUsername)}");
                    DeleteItemWithSQL(akn);
                    DeleteItemWithSQL(nalog);
                    akn.KorisnickoIme = newUsername;
                    nalog.KorisnickoIme = newUsername;
                    AddItemWithSQL(nalog);
                    AddItemWithSQL(akn);
                    break;
                case 2:
                    BibliotekarKoristiNalog bkn = GetFirstFromSQL<BibliotekarKoristiNalog>($"KorisnickoIme={MakeSqlValue(oldUsername)}");
                    DeleteItemWithSQL(bkn);
                    DeleteItemWithSQL(nalog);
                    bkn.KorisnickoIme = newUsername;
                    nalog.KorisnickoIme = newUsername;
                    AddItemWithSQL(nalog);
                    AddItemWithSQL(bkn);
                    break;
                case 3:
                    KurirKoristiNalog kkn = GetFirstFromSQL<KurirKoristiNalog>($"KorisnickoIme={MakeSqlValue(oldUsername)}");
                    DeleteItemWithSQL(kkn);
                    DeleteItemWithSQL(nalog);
                    kkn.KorisnickoIme = newUsername;
                    nalog.KorisnickoIme = newUsername;
                    AddItemWithSQL(nalog);
                    AddItemWithSQL(kkn);
                    break;

            }
            return true;
        }
        #endregion

        #region ACCOUNTS
        public static bool UpateAccound(KorisnickiNalog korisnickiNalog)
        {
            return UpdateItemWithSQL<KorisnickiNalog>(korisnickiNalog);
        }
        #endregion

        #region LOKALS
        public static bool UpdateLibrary(Biblikutak b)
        {
            AddLokacija(new Lokacija(b.Ulica, b.Broj, b.PosBr, b.OZND));
            Biblikutak old = GetLokal(b.IDBK);

            if (UpdateItemWithSQL<Biblikutak>(b))
            {
                string? naziv = old.Naziv != b.Naziv ? b.Naziv : null;
                DateTime? datOtv = old.DatOtv != b.DatOtv ? b.DatOtv : null;
                DateTime? datZat = old.DatZat != b.DatZat ? b.DatZat : null;

                return AddIzmenaLokala(new IzmenaLokala(b.IDBK, _currentUserID, naziv, datOtv, datZat));
            }
            return false;
        }
        #endregion

        #region AUTOR
        public static bool UpdateAutor(Autor a)
        {
            Autor old = GetAutor(a.IDAutor);

            if (UpdateItemWithSQL<Autor>(a))
                return AddIzmenaAutora(new IzmenaAutora(old, _currentUserID));
            else return false;
        }
        #endregion

        #region JEZIK
        public static bool UpdateJezik(Jezik j)
        {
            return UpdateItemWithSQL<Jezik>(j);
        }

        #endregion

        #region ZANR
        public static bool UpdateZanr(Zanr z)
        {
            return UpdateItemWithSQL<Zanr>(z);
        }

        #endregion

        #region FORMAT
        public static bool UpdateFormat(string oldFormatKey, Format format)
        {
            if (!AddFormat(format)) return false;
            foreach (SerijskoStivo ss in GetAllSStivo($"Format={MakeSqlValue(oldFormatKey)}"))
            {
                ss.Format = format.NazivFormata;
                if (UpdateSStivo(ss) == null) return false;
            }
            foreach (Knjiga k in GetAllKnjigas($"Format={MakeSqlValue(oldFormatKey)}"))
            {
                k.Format = format.NazivFormata;
                if (UpdateKnjiga(k) == null) return false;
            }
            return DeleteItemWithSQL<Format>(new Format(oldFormatKey));
        }

        #endregion

        #region PERIOD
        public static bool UpdatePeriodIzd(string oldPeriodNaziv, Periodicnost periodicnost)
        {
            if (oldPeriodNaziv != periodicnost.PeriodIzd)
            {
                if (!AddPeriod(periodicnost)) return false;

                foreach (SerijskoStivo ss in GetAllSStivo($"Period={MakeSqlValue(oldPeriodNaziv)}"))
                {
                    ss.Period = periodicnost.PeriodIzd;
                    if (UpdateSStivo(ss) == null) return false;
                }
                foreach (IzmenaSStiva iss in GetAllIzmenaSStiva($"Period={MakeSqlValue(oldPeriodNaziv)}"))
                {
                    iss.Period = periodicnost.PeriodIzd;
                    if (!UpdateIzmenaSStiva(iss)) return false;
                }

                return DeleteItemWithSQL<Periodicnost>(new Periodicnost(oldPeriodNaziv, 0));
            }
            else
            {
                return UpdateItemWithSQL<Periodicnost>(periodicnost);
            }
        }

        #endregion

        #region IZD KUCA
        public static bool UpdateIzdKuca(IzdKuca izdKuca)
        {
            AddLokacija(izdKuca.Ulica, izdKuca.Broj, izdKuca.PosBr, izdKuca.OZND);

            if (!UpdateItemWithSQL<IzdKuca>(izdKuca)) return false;

            return AddIzmenaIzdKuca(new IzmenaIzdKuce(izdKuca, _currentUserID));
        }
        #endregion

        #region KNJIGA
        public static bool UpdateKnjiga(Knjiga knjiga, List<Autor> autori, List<IzdKuca> izdKuce, List<Zanr> zanrovi, List<Jezik> jezici)
        {
            IzmenaKnjige ik = new IzmenaKnjige();
            if ((ik = UpdateKnjiga(knjiga)) == null) return false;
            if (!UpdateKnjigaAutor(ik, autori)) return false;
            if (!UpdateKnjigaJezik(ik, jezici)) return false;
            if (!UpdateKnjigaZanr(ik, zanrovi)) return false;
            if (!UpdateKnjigaIzdKuca(ik, izdKuce)) return false;
            return true;
        }
        public static IzmenaKnjige UpdateKnjiga(Knjiga knjiga)
        {
            IzmenaKnjige ret = new IzmenaKnjige(knjiga, _currentUserID);
            if (!UpdateItemWithSQL<Knjiga>(knjiga)) return null;

            if (AddIzmenaKnjige(ret) != null) return ret;
            return null;
        }
        public static bool UpdateKnjigaAutor(IzmenaKnjige k, List<Autor> newList)
        {
            List<Pise> newAutorList = new List<Pise>();
            foreach (Autor autor in newList)
            {
                newAutorList.Add(new Pise(k.IDKnjiga, autor.IDAutor));
            }

            List<Pise> pises = GetAllPiseWithBook(GetBook(k.IDKnjiga));
            List<Pise> toRemove = new List<Pise>();
            List<Pise> toAdd = new List<Pise>();

            foreach (Pise pise in pises)
            {
                if (!newAutorList.Contains(pise)) toRemove.Add(pise);
            }
            foreach (Pise pise in newAutorList)
            {
                if (!pises.Contains(pise)) toAdd.Add(pise);
            }

            foreach (Pise pise in toRemove)
            {
                if (!DeletePise(pise)) return false;
            }
            foreach (Pise pise in toAdd)
            {
                if (!AddPise(pise)) return false;
                if (!AddIzmenaKnjigeAutor(new IzKnjigeAutor(k, pise.IDAutor))) return false;
            }
            return true;
        }
        public static bool UpdateKnjigaJezik(IzmenaKnjige k, List<Jezik> newList)
        {
            List<KnjigaNaJeziku> newKnJList = new List<KnjigaNaJeziku>();
            foreach (Jezik jezik in newList)
            {
                newKnJList.Add(new KnjigaNaJeziku(k.IDKnjiga, jezik.OZNJ));
            }

            List<KnjigaNaJeziku> jezici = GetAllKnjigaNaJezikuWithBook(GetBook(k.IDKnjiga));
            List<KnjigaNaJeziku> toRemove = new List<KnjigaNaJeziku>();
            List<KnjigaNaJeziku> toAdd = new List<KnjigaNaJeziku>();

            foreach (KnjigaNaJeziku starKnj in jezici)
            {
                if (!newKnJList.Contains(starKnj)) toRemove.Add(starKnj);
            }
            foreach (KnjigaNaJeziku novKnj in newKnJList)
            {
                if (!jezici.Contains(novKnj)) toAdd.Add(novKnj);
            }

            foreach (KnjigaNaJeziku knj in toRemove)
            {
                if (!DeleteKnjigaNaJeziku(knj)) return false;
            }
            foreach (KnjigaNaJeziku knj in toAdd)
            {
                if (!AddKnjigaNaJeziku(knj)) return false;
                if (!AddIzmenaKnjigeJezik(new IzKnjigeJezik(k, knj.OZNJ))) return false;
            }
            return true;
        }
        public static bool UpdateKnjigaZanr(IzmenaKnjige k, List<Zanr> newList)
        {
            List<PripadaZanru> newPZList = new List<PripadaZanru>();
            foreach (Zanr z in newList)
            {
                newPZList.Add(new PripadaZanru(k.IDKnjiga, z.OZNZ));
            }

            List<PripadaZanru> pripadaZanru = GetAllPripadaZanruWithBook(GetBook(k.IDKnjiga));
            List<PripadaZanru> toRemove = new List<PripadaZanru>();
            List<PripadaZanru> toAdd = new List<PripadaZanru>();

            foreach (PripadaZanru pz in pripadaZanru)
            {
                if (!newPZList.Contains(pz)) toRemove.Add(pz);
            }
            foreach (PripadaZanru pz in newPZList)
            {
                if (!pripadaZanru.Contains(pz)) toAdd.Add(pz);
            }

            foreach (PripadaZanru pz in toRemove)
            {
                if (!DeletePripadaZanru(pz)) return false;
            }
            foreach (PripadaZanru pz in toAdd)
            {
                if (!AddPripadaZanru(pz)) return false;
                if (!AddIzmenaPripadaZanru(new IzKnjigeZanr(k, pz.OZNZ))) return false;
            }
            return true;
        }
        public static bool UpdateKnjigaIzdKuca(IzmenaKnjige k, List<IzdKuca> newList)
        {
            List<IzdajeKnjigu> newIKList = new List<IzdajeKnjigu>();
            foreach (IzdKuca ik in newList)
            {
                newIKList.Add(new IzdajeKnjigu(k.IDKnjiga, ik.IDIK));
            }

            List<IzdajeKnjigu> izdaje = GetAllIzdajeKnjiguWithBook(GetBook(k.IDKnjiga));
            List<IzdajeKnjigu> toRemove = new List<IzdajeKnjigu>();
            List<IzdajeKnjigu> toAdd = new List<IzdajeKnjigu>();

            foreach (IzdajeKnjigu ik in izdaje)
            {
                if (!newIKList.Contains(ik)) toRemove.Add(ik);
            }
            foreach (IzdajeKnjigu ik in newIKList)
            {
                if (!izdaje.Contains(ik)) toAdd.Add(ik);
            }

            foreach (IzdajeKnjigu ik in toRemove)
            {
                if (!DeleteIzdajeKnjigu(ik)) return false;
            }
            foreach (IzdajeKnjigu ik in toAdd)
            {
                if (!AddIzdajeKnjigu(ik)) return false;
                if (!AddIzmenaIzdajeKnjigu(new IzKnjigeIzdKuca(k, ik.IDIK))) return false;
            }
            return true;
        }

        public static bool UpdateIzmenaKnjige(IzmenaKnjige ik)
        {
            return UpdateItemWithSQL<IzmenaKnjige>(ik);
        }
        #endregion

        #region SERIJSKO STIVO
        public static bool UpdateSStivo(SerijskoStivo sstivo, List<IzdKuca> izdKuce, List<Jezik> jezici)
        {
            IzmenaSStiva iss = new IzmenaSStiva();
            if ((iss = UpdateSStivo(sstivo)) == null) return false;
            if (!UpdateSStivoJezik(iss, jezici)) return false;
            if (!UpdateSStivoIzdKuca(iss, izdKuce)) return false;
            return true;
        }
        public static IzmenaSStiva UpdateSStivo(SerijskoStivo sstivo)
        {
            IzmenaSStiva ret = new IzmenaSStiva(sstivo, _currentUserID);
            if (!UpdateItemWithSQL<SerijskoStivo>(sstivo)) return null;
            if (AddIzmenaSStiva(ret)) return ret;
            return null;
        }
        public static bool UpdateSStivoJezik(IzmenaSStiva ss, List<Jezik> newList)
        {
            List<SStivoNaJeziku> newSSnJList = new List<SStivoNaJeziku>();
            foreach (Jezik jezik in newList)
            {
                newSSnJList.Add(new SStivoNaJeziku(ss.IDSStivo, jezik.OZNJ));
            }

            List<SStivoNaJeziku> jezici = GetAllSStivoNaJezikuWithSS(GetSerijskoStivo(ss.IDSStivo));
            List<SStivoNaJeziku> toRemove = new List<SStivoNaJeziku>();
            List<SStivoNaJeziku> toAdd = new List<SStivoNaJeziku>();

            foreach (SStivoNaJeziku ssnj in jezici)
            {
                if (!newSSnJList.Contains(ssnj)) toRemove.Add(ssnj);
            }
            foreach (SStivoNaJeziku ssnj in newSSnJList)
            {
                if (!jezici.Contains(ssnj)) toAdd.Add(ssnj);
            }

            foreach (SStivoNaJeziku ssnj in toRemove)
            {
                if (!DeleteSStivoNaJeziku(ssnj)) return false;
            }
            foreach (SStivoNaJeziku ssnj in toAdd)
            {
                if (!AddSStivoNaJeziku(ssnj)) return false;
                if (!AddIzmenaSStivaJezik(new IzSStivaJezik(ss, ssnj.OZNJ))) return false;
            }
            return true;
        }
        public static bool UpdateSStivoIzdKuca(IzmenaSStiva izss, List<IzdKuca> newList)
        {
            List<IzdajeSStivo> newISSList = new List<IzdajeSStivo>();
            foreach (IzdKuca ik in newList)
            {
                newISSList.Add(new IzdajeSStivo(izss.IDSStivo, ik.IDIK));
            }

            List<IzdajeSStivo> izdaju = GetAllIzdajeSStivoWithSS(GetSerijskoStivo(izss.IDSStivo));
            List<IzdajeSStivo> toRemove = new List<IzdajeSStivo>();
            List<IzdajeSStivo> toAdd = new List<IzdajeSStivo>();

            foreach (IzdajeSStivo iss in izdaju)
            {
                if (!newISSList.Contains(iss)) toRemove.Add(iss);
            }
            foreach (IzdajeSStivo iss in newISSList)
            {
                if (!izdaju.Contains(iss)) toAdd.Add(iss);
            }

            foreach (IzdajeSStivo iss in toRemove)
            {
                if (!DeleteIzdajeSStivo(iss)) return false;
            }
            foreach (IzdajeSStivo iss in toAdd)
            {
                if (!AddIzdajeSStivo(iss)) return false;
                if (!AddIzmenaSStivaIzdKuca(new IzSStivaIzdKuca(iss.IDSStivo, iss.IDIK, _currentUserID))) return false;
            }
            return true;
        }
        // izdanje
        public static bool UpdateIzdSStiva(IzdanjeSStiva iss)
        {
            if (!UpdateItemWithSQL<IzdanjeSStiva>(iss)) return false;
            return AddIzmenaIzdSStiva(new IzmenaIzdSStiva(iss, _currentUserID));
        }
        // izmene
        public static bool UpdateIzmenaSStiva(IzmenaSStiva iss)
        {
            return UpdateItemWithSQL<IzmenaSStiva>(iss);
        }
        public static bool UpdateIzmenaIzdSStiva(IzmenaIzdSStiva iss)
        {
            return UpdateItemWithSQL<IzmenaIzdSStiva>(iss);
        }
        #endregion


        #endregion



        #region DELETERS

        #region LOKACIJE
        #endregion

        #region WORKERS
        public static bool DeleteWorker(Radnik selectedWorker)
        {
            selectedWorker.DatOtp = DateTime.Now;
            if (selectedWorker.GetType() == typeof(Bibliotekar))
            {
                if (!DeleteBibUsingAccount(selectedWorker as Bibliotekar)) return false;
                return UpdateItemWithSQL<Bibliotekar>(selectedWorker as Bibliotekar);
            }
            else if (selectedWorker.GetType() == typeof(Kurir))
            {
                if (!DeleteKurirUsingAccount(selectedWorker as Kurir)) return false;
                return UpdateItemWithSQL<Kurir>(selectedWorker as Kurir);
            }
            return false;
        }
        public static bool DeleteBibUsingAccount(Bibliotekar selectedWorker)
        {
            return DeleteItemWithSQL<BibliotekarKoristiNalog>(GetFirstFromSQL<BibliotekarKoristiNalog>($"ID={MakeSqlValue(selectedWorker.IDRadnik)}"));
        }
        public static bool DeleteKurirUsingAccount(Kurir selectedWorker)
        {
            return DeleteItemWithSQL<KurirKoristiNalog>(GetFirstFromSQL<KurirKoristiNalog>($"ID={MakeSqlValue(selectedWorker.IDRadnik)}"));
        }
        #endregion

        #region ACCOUNTS
        public static bool DeleteAccount(KorisnickiNalog nalog)
        {
            nalog.DatZatvaranja = DateTime.Now;
            if (!DeleteAccountUsedByWorker(nalog)) return false;

            return UpdateItemWithSQL<KorisnickiNalog>(nalog);
        }
        public static bool DeleteAccountUsedByWorker(KorisnickiNalog nalog)
        {
            if (nalog.TipNaloga == 2)
            {
                return DeleteItemWithSQL<BibliotekarKoristiNalog>(GetFirstFromSQL<BibliotekarKoristiNalog>($"KorisnickoIme={MakeSqlValue(nalog.KorisnickoIme)}"));
            }
            else if (nalog.TipNaloga == 3)
            {
                return DeleteItemWithSQL<KurirKoristiNalog>(GetFirstFromSQL<KurirKoristiNalog>($"KorisnickoIme={MakeSqlValue(nalog.KorisnickoIme)}"));
            }
            return false;
        }
        public static bool DeleteWorkerWithAccount(Radnik selectedWorker, KorisnickiNalog korisnickiNalog)
        {
            if (!DeleteWorker(selectedWorker)) return false;
            return DeleteAccount(korisnickiNalog);
        }

        #endregion

        #region LOKALS
        public static bool DeleteLokal(Biblikutak b)
        {
            b.DatZat = DateTime.Now;
            if (!DeleteAllKuLFromLokal(b)) return false;
            if (!DeleteAllSSuLFromLokal(b)) return false;

            if (UpdateItemWithSQL<Biblikutak>(b))
            {
                if (DeleteAllKuLFromLokal(b) && DeleteAllSSuLFromLokal(b))
                    return AddIzmenaLokala(new IzmenaLokala(b, _currentUserID));
            }
            return false;
        }
        public static bool DeleteAllKuLFromLokal(Biblikutak b)
        {
            List<KnjigaULokalu> kul = GetAllKnjigeULokalu($"IDBK={MakeSqlValue(b.IDBK)}");
            foreach (KnjigaULokalu k in kul)
            {
                if (!DeleteKuL(k)) return false;
            }
            return true;
        }
        public static bool DeleteAllSSuLFromLokal(Biblikutak b)
        {
            List<IzdSStivoULokalu> ssul = GetAllIzdSStivoULokalu($"IDBK={MakeSqlValue(b.IDBK)}");
            foreach (IzdSStivoULokalu ss in ssul)
            {
                if (!DeleteIzdSSuL(ss)) return false;
            }
            return true;
        }
        #endregion

        #region AUTORS
        public static bool DeleteAutor(Autor a)
        {
            List<Pise> pise = GetAllPise($"IDAutor={MakeSqlValue(a.IDAutor)}");
            foreach (Pise p in pise)
            {
                if (!DeletePise(p)) return false;
            }

            a.DatBrisanja = DateTime.Now;
            return UpdateItemWithSQL<Autor>(a);
        }
        public static bool DeletePise(Pise p)
        {
            return DeleteItemWithSQL<Pise>(p);
        }
        public static bool DeleteAllIzmenaPise(IzKnjigeAutor ika)
        {
            return DeleteItemWithSQL<IzKnjigeAutor>(ika);
        }
        public static bool DeleteAllPiseWithAutor(Autor a)
        {
            List<Pise> pise = GetAllPise($"IDAutor={MakeSqlValue(a.IDAutor)}");
            foreach (Pise p in pise)
            {
                if (!DeletePise(p)) return false;
            }
            return true;
        }
        public static bool DeleteIzmenaPiseWithAutor(Autor a)
        {
            List<IzKnjigeAutor> pise = GetAllIzmenaKnjigeAutor($"IDAutor={MakeSqlValue(a.IDAutor)}");
            foreach (IzKnjigeAutor p in pise)
            {
                if (!DeleteAllIzmenaPise(p)) return false;
            }
            return true;
        }
        #endregion

        #region JEZIK
        public static bool DeleteJezik(Jezik jezik)
        {
            if (!DeleteAllIzmenaKnJWithJezik(jezik)) return false;
            if (!DeleteAllKnJWithJezik(jezik)) return false;

            if (!DeleteAllIzmenaSSnJWithJezik(jezik)) return false;
            if (!DeleteAllSSnJWithJezik(jezik)) return false;

            return DeleteItemWithSQL<Jezik>(jezik);
        }
        public static bool DeleteKnjigaNaJeziku(KnjigaNaJeziku knj)
        {
            return DeleteItemWithSQL<KnjigaNaJeziku>(knj);
        }
        public static bool DeleteSStivoNaJeziku(SStivoNaJeziku snj)
        {
            return DeleteItemWithSQL<SStivoNaJeziku>(snj);
        }
        public static bool DeleteIzmenaKnjigaNaJeziku(IzKnjigeJezik knj)
        {
            return DeleteItemWithSQL<IzKnjigeJezik>(knj);
        }
        public static bool DeleteIzmenaSStivoNaJeziku(IzSStivaJezik snj)
        {
            return DeleteItemWithSQL<IzSStivaJezik>(snj);
        }
        public static bool DeleteAllKnJWithJezik(Jezik j)
        {
            List<KnjigaNaJeziku> knjs = GetAllKnjigaNaJeziku($"OZNJ={MakeSqlValue(j.OZNJ)}");
            foreach (KnjigaNaJeziku knj in knjs)
            {
                if (!DeleteKnjigaNaJeziku(knj)) return false;
            }
            return true;
        }
        public static bool DeleteAllSSnJWithJezik(Jezik j)
        {
            List<SStivoNaJeziku> ssnjs = GetAllSStivoNaJeziku($"OZNJ={MakeSqlValue(j.OZNJ)}");
            foreach (SStivoNaJeziku snj in ssnjs)
            {
                if (!DeleteSStivoNaJeziku(snj)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaKnJWithJezik(Jezik j)
        {
            List<IzKnjigeJezik> knjs = GetAllIzmenaKnjigaJezik($"OZNJ={MakeSqlValue(j.OZNJ)}");
            foreach (IzKnjigeJezik knj in knjs)
            {
                if (!DeleteIzmenaKnjigaNaJeziku(knj)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaSSnJWithJezik(Jezik j)
        {
            List<IzSStivaJezik> ssnjs = GetAllIzmenaSStivaJezik($"OZNJ={MakeSqlValue(j.OZNJ)}");
            foreach (IzSStivaJezik snj in ssnjs)
            {
                if (!DeleteIzmenaSStivoNaJeziku(snj)) return false;
            }
            return true;
        }
        #endregion

        #region ZANR
        public static bool DeleteZanr(Zanr zanr)
        {
            if (!DeleteAllIzmenaPripadaZanruWithZanr(zanr)) return false;
            if (!DeleteAllPripadaZanruWithZanr(zanr)) return false;

            return DeleteItemWithSQL<Zanr>(zanr);
        }
        public static bool DeletePripadaZanru(PripadaZanru pz)
        {
            return DeleteItemWithSQL<PripadaZanru>(pz);
        }
        public static bool DeleteIzmenaPripadaZanru(IzKnjigeZanr izZanr)
        {
            return DeleteItemWithSQL<IzKnjigeZanr>(izZanr);
        }
        public static bool DeleteAllPripadaZanruWithZanr(Zanr z)
        {
            List<PripadaZanru> pripadaZanrus = GetAllPripadaZanru($"OZNZ={MakeSqlValue(z.OZNZ)}");
            foreach (PripadaZanru pz in pripadaZanrus)
            {
                if (!DeletePripadaZanru(pz)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaPripadaZanruWithZanr(Zanr z)
        {
            List<IzKnjigeZanr> pripadaZanrus = GetAllIzmenaKnjigeZanr($"OZNZ={MakeSqlValue(z.OZNZ)}");
            foreach (IzKnjigeZanr pz in pripadaZanrus)
            {
                if (!DeleteIzmenaPripadaZanru(pz)) return false;
            }
            return true;
        }
        #endregion

        #region FORMAT
        public static bool DeleteFormat(Format format)
        {
            foreach (Knjiga k in GetAllKnjigas($"Format={MakeSqlValue(format.NazivFormata)}"))
            {
                k.Format = null;
                if (UpdateKnjiga(k) == null) return false;
            }
            foreach (SerijskoStivo s in GetAllSStivo($"Format={MakeSqlValue(format.NazivFormata)}"))
            {
                s.Format = null;
                if (UpdateSStivo(s) == null) return false;
            }
            foreach (IzmenaKnjige ik in GetAllIzmenaKnjige($"Format={MakeSqlValue(format.NazivFormata)}"))
            {
                ik.Format = null;
                if (!UpdateIzmenaKnjige(ik)) return false;
            }
            foreach (IzmenaSStiva iss in GetAllIzmenaSStiva($"Format={MakeSqlValue(format.NazivFormata)}"))
            {
                iss.Format = null;
                if (!UpdateIzmenaSStiva(iss)) return false;
            }

            return DeleteItemWithSQL<Format>(format);
        }
        #endregion

        #region PERIOD
        public static bool DeletePeriod(Periodicnost periodicnost)
        {
            foreach (SerijskoStivo s in GetAllSStivo($"Period={MakeSqlValue(periodicnost.PeriodIzd)}"))
            {
                s.Period = null;
                if (UpdateSStivo(s) == null) return false;
            }
            foreach (IzmenaSStiva iss in GetAllIzmenaSStiva($"Period={MakeSqlValue(periodicnost.PeriodIzd)}"))
            {
                iss.Period = null;
                if (!UpdateIzmenaSStiva(iss)) return false;
            }
            return DeleteItemWithSQL<Periodicnost>(periodicnost);
        }

        #endregion

        #region IZD KUCA
        public static bool DeleteIzdKuca(IzdKuca izdKuca)
        {
            foreach (IzdajeKnjigu ik in GetAllIzdajeKnjigu($"IDIK={MakeSqlValue(izdKuca.IDIK)}"))
            {
                if (!DeleteItemWithSQL<IzdajeKnjigu>(ik)) return false;
            }
            foreach (IzdajeSStivo iss in GetAllIzdajeSStivo($"IDIK={MakeSqlValue(izdKuca.IDIK)}"))
            {
                if (!DeleteItemWithSQL<IzdajeSStivo>(iss)) return false;
            }
            izdKuca.DatZat = DateTime.Now;
            if(UpdateItemWithSQL<IzdKuca>(izdKuca))
            {
                return AddIzmenaIzdKuca(new IzmenaIzdKuce(izdKuca, _currentUserID));
            }
            return false;
        }

        public static bool DeleteIzdajeKnjigu(IzdajeKnjigu ik)
        {
            return DeleteItemWithSQL<IzdajeKnjigu>(ik);
        }
        public static bool DeleteIzdajeSStivo(IzdajeSStivo iss)
        {
            return DeleteItemWithSQL<IzdajeSStivo>(iss);
        }
        public static bool DeleteIzmenaIzdajeKnjigu(IzKnjigeIzdKuca ik)
        {
            return DeleteItemWithSQL<IzKnjigeIzdKuca>(ik);
        }
        public static bool DeleteIzmenaIzdajeSStivo(IzSStivaIzdKuca iss)
        {
            return DeleteItemWithSQL<IzSStivaIzdKuca>(iss);
        }
        public static bool DeleteAllIzdajeKnjiguWithIK(IzdKuca ik)
        {
            List<IzdajeKnjigu> izks = GetAllIzdajeKnjigu($"IDIK={MakeSqlValue(ik.IDIK)}");
            foreach (IzdajeKnjigu izk in izks)
            {
                if (!DeleteIzdajeKnjigu(izk)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzdajeSStivoWithIK(IzdKuca ik)
        {
            List<IzdajeSStivo> izsss = GetAllIzdajeSStivo($"IDIK={MakeSqlValue(ik.IDIK)}");
            foreach (IzdajeSStivo izs in izsss)
            {
                if (!DeleteIzdajeSStivo(izs)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaIzdajeKnjiguWithIK(IzdKuca ik)
        {
            List<IzKnjigeIzdKuca> izks = GetAllIzmenaKnjigeIzdKuca($"IDIK={MakeSqlValue(ik.IDIK)}");
            foreach (IzKnjigeIzdKuca izk in izks)
            {
                if (!DeleteIzmenaIzdajeKnjigu(izk)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaIzdajeSStivoWithIK(IzdKuca ik)
        {
            List<IzSStivaIzdKuca> izsss = GetAllIzmenaSStivoIzdKuca($"IDIK={MakeSqlValue(ik.IDIK)}");
            foreach (IzSStivaIzdKuca izs in izsss)
            {
                if (!DeleteIzmenaIzdajeSStivo(izs)) return false;
            }
            return true;
        }
        #endregion

        #region KNJIGA
        public static bool DeleteKnjiga(Knjiga k, int idBib)
        {
            if (!DeleteAllKuLWithBook(k)) return false;

            //if (!DeleteAllIzmenaPiseWithKnjiga(k)) return false;
            //if (!DeleteAllIzmenaKnJWithKnjiga(k)) return false;
            //if (!DeleteAllIzmenaPripadaZanruWithKnjiga(k)) return false;
            //if (!DeleteAllIzmenaIzdajeKnjiguWithKnjiga(k)) return false;

            if (!DeletePiseWithKnjiga(k)) return false;
            if (!DeleteAllKnJWithKnjiga(k)) return false;
            if (!DeleteAllPripadaZanruWithKnjiga(k)) return false;
            if (!DeleteAllIzdajeKnjiguWithKnjiga(k)) return false;

            k.DatBrisanja = DateTime.Now;
            if(UpdateItemWithSQL<Knjiga>(k))
            {
                return AddIzmenaKnjige(new IzmenaKnjige(k, idBib));
            }
            return false; 
        }
        public static bool DeleteKuL(KnjigaULokalu kul)
        {
            kul.DatBrisanja = DateTime.Now;
            kul.Kolicina = 0;
            return AddItemWithSQL<KnjigaULokalu>(kul);
        }
        public static bool DeleteAllKuLWithBook(Knjiga knjiga)
        {
            List<KnjigaULokalu> kul = GetAllKnjigeULokalu($"IDKnjiga={MakeSqlValue(knjiga.IDKnjiga)}");
            foreach (KnjigaULokalu k in kul)
            {
                if (!DeleteKuL(k)) return false;
            }
            return true;
        }

        public static bool DeletePiseWithKnjiga(Knjiga k)
        {
            List<Pise> pise = GetAllPise($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (Pise p in pise)
            {
                if (!DeletePise(p)) return false;
            }
            return true;
        }
        public static bool DeleteAllKnJWithKnjiga(Knjiga k)
        {
            List<KnjigaNaJeziku> knjs = GetAllKnjigaNaJeziku($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (KnjigaNaJeziku knj in knjs)
            {
                if (!DeleteKnjigaNaJeziku(knj)) return false;
            }
            return true;
        }
        public static bool DeleteAllPripadaZanruWithKnjiga(Knjiga k)
        {
            List<PripadaZanru> pripadaZanrus = GetAllPripadaZanru($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (PripadaZanru pz in pripadaZanrus)
            {
                if (!DeletePripadaZanru(pz)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzdajeKnjiguWithKnjiga(Knjiga k)
        {
            List<IzdajeKnjigu> izks = GetAllIzdajeKnjigu($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (IzdajeKnjigu izk in izks)
            {
                if (!DeleteIzdajeKnjigu(izk)) return false;
            }
            return true;
        }

        public static bool DeleteAllIzmenaPiseWithKnjiga(Knjiga k)
        {
            List<IzKnjigeAutor> pise = GetAllIzmenaKnjigeAutor($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (IzKnjigeAutor p in pise)
            {
                if (!DeleteAllIzmenaPise(p)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaKnJWithKnjiga(Knjiga k)
        {
            List<IzKnjigeJezik> knjs = GetAllIzmenaKnjigaJezik($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (IzKnjigeJezik knj in knjs)
            {
                if (!DeleteIzmenaKnjigaNaJeziku(knj)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaPripadaZanruWithKnjiga(Knjiga k)
        {
            List<IzKnjigeZanr> pripadaZanrus = GetAllIzmenaKnjigeZanr($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (IzKnjigeZanr pz in pripadaZanrus)
            {
                if (!DeleteIzmenaPripadaZanru(pz)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaIzdajeKnjiguWithKnjiga(Knjiga k)
        {
            List<IzKnjigeIzdKuca> izks = GetAllIzmenaKnjigeIzdKuca($"IDKnjiga={MakeSqlValue(k.IDKnjiga)}");
            foreach (IzKnjigeIzdKuca izk in izks)
            {
                if (!DeleteIzmenaIzdajeKnjigu(izk)) return false;
            }
            return true;
        }
        #endregion

        #region SSTIVO
        public static bool DeleteSStivo(SerijskoStivo ss, int idBib)
        {
            if (!DeleteAllIzdajeSStivoWithSS(ss)) return false;
            if (!DeleteAllSSnJWithSS(ss)) return false;
            if (!DeleteAllIzdSStivo(ss, idBib)) return false;

            ss.DatBrisanja = DateTime.Now;
            if (UpdateItemWithSQL<SerijskoStivo>(ss))
            {
                return AddIzmenaSStiva(new IzmenaSStiva(ss, idBib));
            }
            return false;   
        }
        public static bool DeleteIzdSStivo(IzdanjeSStiva izdss, int idBib)
        {
            if (!DeleteAllIzdSSulWithSS(izdss)) return false;
            //if (!DeleteAllIzmenaIzdSStivoWithIzdSS(izdss)) return false;
            //if (!DeleteAllIzdSSulWithSS(izdss)) return false;
            izdss.DatBrisanja = DateTime.Now;
            if(UpdateItemWithSQL<IzdanjeSStiva>(izdss))
            {
                return AddIzmenaIzdSStiva(new IzmenaIzdSStiva(izdss, idBib));
            }
            return false;
        }
        public static bool DeleteAllIzdSStivo(SerijskoStivo ss, int idBib)
        {
            List<IzdanjeSStiva> izdanja = GetAllIzdanjeSStiva($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
            foreach (IzdanjeSStiva iss in izdanja)
            {
                if (!DeleteIzdSStivo(iss, idBib)) return false;
            }
            return true;
        }

        public static bool DeleteAllSSnJWithSS(SerijskoStivo ss)
        {
            List<SStivoNaJeziku> ssnjs = GetAllSStivoNaJeziku($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
            foreach (SStivoNaJeziku ssnj in ssnjs)
            {
                if (!DeleteSStivoNaJeziku(ssnj)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzdajeSStivoWithSS(SerijskoStivo ss)
        {
            List<IzdajeSStivo> izdajeSStivos = GetAllIzdajeSStivo($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
            foreach (IzdajeSStivo izss in izdajeSStivos)
            {
                if (!DeleteIzdajeSStivo(izss)) return false;
            }
            return true;
        }

        public static bool DeleteIzdSSuL(IzdSStivoULokalu ssul)
        {
            ssul.DatBrisanja = DateTime.Now;
            ssul.Kolicina = 0;
            return AddItemWithSQL<IzdSStivoULokalu>(ssul);
        }
        public static bool DeleteAllIzdSSulWithSS(IzdanjeSStiva iss)
        {
            List<IzdSStivoULokalu> ssuls = GetAllIzdSStivoULokalu($"IDSStivo={MakeSqlValue(iss.IDSStivo)} and BrIzd={iss.BrIzd}");
            foreach (IzdSStivoULokalu issul in ssuls)
            {
                if (!DeleteIzdSSuL(issul)) return false;
            }
            return true;
        }

        // izmene
        public static bool DeleteIzmenaSStivo(IzmenaSStiva iss)
        {
            return DeleteItemWithSQL<IzmenaSStiva>(iss);
        }
        public static bool DeleteIzmenaIzdSStivo(IzmenaIzdSStiva iizdss)
        {
            return DeleteItemWithSQL<IzmenaIzdSStiva>(iizdss);
        }
        public static bool DeleteAllIzmenaSStivoWithSS(SerijskoStivo ss)
        {
            List<IzmenaSStiva> izmene = GetAllIzmenaSStiva($"IDSStivo={MakeSqlValue(ss.IDSStivo)}");
            foreach (IzmenaSStiva iss in izmene)
            {
                if (!DeleteIzmenaSStivo(iss)) return false;
            }
            return true;
        }
        public static bool DeleteAllIzmenaIzdSStivoWithIzdSS(IzdanjeSStiva izdss)
        {
            List<IzmenaIzdSStiva> izmene = GetAllIzmenaIzdSStiva($"IDSStivo={MakeSqlValue(izdss.IDSStivo)} and BrIzd={MakeSqlValue(izdss.BrIzd)}");
            foreach (IzmenaIzdSStiva izss in izmene)
            {
                if (!DeleteIzmenaIzdSStivo(izss)) return false;
            }
            return true;
        }

        public static bool DeleteIzmenaSStivoJezikWithSS(IzSStivaJezik iss)
        {
            return DeleteItemWithSQL<IzSStivaJezik>(iss);
        }
        public static bool DeleteAllIzmenaSStivoJezikWithSS(IzmenaSStiva iss)
        {
            List<IzSStivaJezik> izmene = GetAllIzmenaSStivaJezik($"IDSStivo={iss.IDSStivo}");
            foreach (IzSStivaJezik jezik in izmene)
            {
                if (!DeleteIzmenaSStivoJezikWithSS(jezik)) return false;
            }
            return true;
        }
        public static bool DeleteIzmenaSStivoIzdKucaWithSS(IzSStivaIzdKuca iss)
        {
            return DeleteItemWithSQL<IzSStivaIzdKuca>(iss);
        }
        public static bool DeleteAllIzmenaSStivoIzdKucaWithSS(IzmenaSStiva iss)
        {
            List<IzSStivaIzdKuca> izmene = GetAllIzmenaSStivoIzdKuca($"IDSStivo={MakeSqlValue(iss)}");
            foreach (IzSStivaIzdKuca izik in izmene)
            {
                if (!DeleteIzmenaSStivoIzdKucaWithSS(izik)) return false;
            }
            return true;
        }

        #endregion

        #endregion

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
//#region OLD
//#region Getters
//public static float GetEditionPrice(iIzdanje edition)
//{
//    switch (edition)
//    {
//        case iIzdanje.Ograniceno: return 3;
//        case iIzdanje.Novo: return 2;
//        case iIzdanje.Staro: return 1;
//        default: return 0;
//    }
//}


//// GET BOOK
//public static KnjigaULokalu GetBookInStore(int bookID, int lokalID)
//{
//    //TODO: get book in store
//    //foreach (Knjigaulokalu k in MockDB.Instance.KUL)
//    //{
//    //	if (k.Idk == bookID && k.Idl == lokalID)
//    //		return k;
//    //}
//    return null;
//}
//public static List<KnjigaULokalu> GetBooksInStore(int lokalID)
//{
//    List<KnjigaULokalu> ret = new List<KnjigaULokalu>();

//    //foreach (Knjigaulokalu k in MockDB.Instance.KUL)
//    //{
//    //	if (k.Idl == lokalID)
//    //	{ ret.Add(k); }
//    //}
//    return ret;
//}
//public static SerijskoStivo GetSerijskoStivo(int id)
//{
//    //TODO: GET FROM DB
//    //List<SerijskoStivo> sstFDB = db.Serijskostivos.ToList();

//    //         foreach (SerijskoStivo l in sstFDB)
//    //{
//    //	if (l.IDSStivo == id)
//    //	{
//    //		return l;
//    //	}
//    //}
//    return null;
//}
//// GET STORE


//public static Biblikutak GetStore(int storeID)
//{
//    // TODO: Get store
//    //foreach (Cornerlibrary l in MockDB.Instance.Lokali)
//    //{
//    //	if (l.Id == storeID)
//    //	{
//    //		return l;
//    //	}
//    //}
//    return null;
//}
//public static List<Biblikutak> GetAllStores()
//{
//    //return MockDB.Instance.Lokali;
//    return null;
//}

//#endregion
//#endregion
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
