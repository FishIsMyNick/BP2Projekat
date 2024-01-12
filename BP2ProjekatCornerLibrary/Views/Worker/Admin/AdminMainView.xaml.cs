using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Worker.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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
using Admin = BP2ProjekatCornerLibrary.Models.Admin;

namespace BP2ProjekatCornerLibrary.Views.Worker
{
    /// <summary>
    /// Interaction logic for AdminMainView.xaml
    /// </summary>
    public partial class AdminMainView : Window, iDynamicListView
    {
        private Models.Admin _currentUser;
        
        public static AdminMainView Instance { get; set; }
        public string Zap_Ime { get; set; }
        public string Zap_Prezime { get; set; }
        public string Zap_Username { get; set; }
        public string Zap_Tip { get; set; }

        //TODO: treba da bude lista radnika
        public List<ZapRadView> ListaZaposlenihRadnika { get; set; }
        public List<OtpRadView> ListaOtpustenihRadnika { get; set; }
        public List<OtvFilView> ListaOtvorenihFilijala { get; set; }
        public List<ZatFilView> ListaZatvorenihFilijala { get; set; }

        public int RadnikTableImeWidth = 300;
        public AdminMainView(Models.Admin admin)
        {
            if (Instance == null)
                Instance = this;

            _currentUser = admin;
            DBHelper._currentUserID = admin.IDRadnik;

            InitializeComponent();

            FillAllLists();
        }

        #region List population
        public void RefreshLists()
        {
            FillAllLists();
        }
        private void FillAllLists()
        {
            FillZapRadniciList();
            FillOtpRadniciList();
            FillOtvFilList();
            FillZatvFilList();
            //Knjiga k = DBHelper.GetBook(1);
        }
        private void FillZapRadniciList()
        {
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllEmployedWorkers();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                ZaposleniRadnici.Items.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private void FillOtpRadniciList()
        {
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllUnemployedWorkers();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                NezaposleniRadnici.Items.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private void FillOtvFilList()
        {
            OtvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = DBHelper.GetOpenLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        private void FillZatvFilList()
        {
            ZatvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                ZatvoreneFilijale.Items.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        #endregion

        #region SORTING
        #region Zaposleni radnici
        private bool s_zr_ime = false;
        private void btn_ZapRadnik_Ime_Click(object sender, RoutedEventArgs e)
        {
            s_zr_ime = !s_zr_ime;
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = Sorter.SortText<Radnik>(DBHelper.GetAllEmployedWorkers(), "Ime", s_zr_ime);
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                ZaposleniRadnici.Items.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private bool s_zr_prez = false;
        private void btn_ZapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
        {
            s_zr_prez = !s_zr_prez;
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = Sorter.SortText<Radnik>(DBHelper.GetAllEmployedWorkers(), "Prezime", s_zr_prez);
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                ZaposleniRadnici.Items.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private bool s_zr_user = false;
        private void btn_ZapRadnik_Username_Click(object sender, RoutedEventArgs e)
        {
            s_zr_user = !s_zr_user;
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllEmployedWorkers();
            List<ZapRadView> toSort = new List<ZapRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach(ZapRadView zrv in Sorter.SortText<ZapRadView>(toSort, "KorisnickoIme", s_zr_user)) ZaposleniRadnici.Items.Add(zrv);
        }
        private bool s_zr_tip = false;
        private void btn_ZapRadnik_Tip_Click(object sender, RoutedEventArgs e)
        {
            s_zr_tip = !s_zr_tip;
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllEmployedWorkers();
            List<ZapRadView> toSort = new List<ZapRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach (ZapRadView zrv in Sorter.SortText<ZapRadView>(toSort, "Tip", s_zr_tip)) ZaposleniRadnici.Items.Add(zrv);
        }
        private bool s_zr_datZ = false;
        private void btn_ZapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
        {
            s_zr_datZ = !s_zr_datZ;
            ZaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllEmployedWorkers();
            List<ZapRadView> toSort = new List<ZapRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new ZapRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach (ZapRadView zrv in Sorter.SortDate<ZapRadView>(toSort, "DatZap", s_zr_datZ)) ZaposleniRadnici.Items.Add(zrv);
        }
        #endregion

        #region Nezaposleni radnici
        private bool s_nzr_ime = false;
        private void btn_NezapRadnik_Ime_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_ime = !s_nzr_ime;
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = Sorter.SortText<Radnik>( DBHelper.GetAllUnemployedWorkers(), "Ime", s_nzr_ime );
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                NezaposleniRadnici.Items.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private bool s_nzr_prez = false;
        private void btn_NezapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_prez = !s_nzr_prez;
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = Sorter.SortText<Radnik>(DBHelper.GetAllUnemployedWorkers(), "Prezime", s_nzr_prez);
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                NezaposleniRadnici.Items.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
        }
        private bool s_nzr_user = false;
        private void btn_NezapRadnik_Username_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_user = !s_nzr_user;
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllUnemployedWorkers();
            List<OtpRadView> toSort = new List<OtpRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach(OtpRadView orv in Sorter.SortText<OtpRadView>(toSort, "KorisnickoIme", s_nzr_user)) NezaposleniRadnici.Items.Add(orv);
        }
        private bool s_nzr_tip = false;
        private void btn_NezapRadnik_Tip_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_tip = !s_nzr_tip;
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllUnemployedWorkers();
            List<OtpRadView> toSort = new List<OtpRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach (OtpRadView orv in Sorter.SortText<OtpRadView>(toSort, "Tip", s_nzr_tip)) NezaposleniRadnici.Items.Add(orv);
        }
        private bool s_nzr_datZ = false;
        private void btn_NezapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_datZ = !s_nzr_datZ;
            NezaposleniRadnici.Items.Clear();

            List<Radnik> radniks = DBHelper.GetAllUnemployedWorkers();
            List<OtpRadView> toSort = new List<OtpRadView>();
            foreach (Radnik radnik in radniks)
            {
                KorisnickiNalog nalog = new KorisnickiNalog();
                if (radnik.GetType() == typeof(Models.Bibliotekar))
                    nalog = DBHelper.GetBibNalog(radnik.IDRadnik);
                else if (radnik.GetType() == typeof(Kurir))
                    nalog = DBHelper.GetKurirNalog(radnik.IDRadnik);
                toSort.Add(new OtpRadView(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
            }
            foreach (OtpRadView orv in Sorter.SortText<OtpRadView>(toSort, "PeriodRada", s_nzr_datZ)) NezaposleniRadnici.Items.Add(orv);
        }
        private void NezapRadnici_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("zr click");
        }
        #endregion

        #region Otvorene filijale
        private bool s_otf_naz = false;
        private void btn_OFil_Naziv_Click(object sender, RoutedEventArgs e)
        {
            s_otf_naz = ! s_otf_naz;
            OtvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = Sorter.SortText<Biblikutak>( DBHelper.GetOpenLokals(), "Naziv", s_otf_naz );
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        private bool s_otf_adr = false;
        private void btn_OFil_Adresa_Click(object sender, RoutedEventArgs e)
        {
            s_otf_adr = !s_otf_adr;
            OtvoreneFilijale.Items.Clear();

            List<OtvFilView> toSort = new List<OtvFilView>();
            foreach (Biblikutak biblikutak in DBHelper.GetOpenLokals())
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (OtvFilView ofv in Sorter.SortText<OtvFilView>(toSort, "Adresa", s_otf_adr)) OtvoreneFilijale.Items.Add(ofv);
        }
        private bool s_otf_mesto = false;
        private void btn_OFil_Grad_Click(object sender, RoutedEventArgs e)
        {
            s_otf_mesto = !s_otf_mesto;
            OtvoreneFilijale.Items.Clear();

            List<OtvFilView> toSort = new List<OtvFilView>();
            foreach (Biblikutak biblikutak in DBHelper.GetOpenLokals())
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (OtvFilView ofv in Sorter.SortText<OtvFilView>(toSort, "GetMesto", s_otf_mesto)) OtvoreneFilijale.Items.Add(ofv);
        }
        private bool s_otf_drz = false;
        private void btn_OFil_Drzava_Click(object sender, RoutedEventArgs e)
        {
            s_otf_drz = !s_otf_drz;
            OtvoreneFilijale.Items.Clear();

            List<OtvFilView> toSort = new List<OtvFilView>();
            foreach (Biblikutak biblikutak in DBHelper.GetOpenLokals())
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (OtvFilView ofv in Sorter.SortText<OtvFilView>(toSort, "GetDrzava", s_otf_drz)) OtvoreneFilijale.Items.Add(ofv);
        }
        private bool s_otf_dat = false;
        private void btn_OFil_DatOtv_Click(object sender, RoutedEventArgs e)
        {
            s_otf_dat = !s_otf_dat;
            OtvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = Sorter.SortDate<Biblikutak>(DBHelper.GetOpenLokals(), "DatOtv", s_otf_dat);
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                OtvoreneFilijale.Items.Add(new OtvFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        #endregion

        #region Zatvorene filijale
        private bool s_ztf_naz = false;
        private void btn_ZFil_Naziv_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_naz = ! s_ztf_naz;
            ZatvoreneFilijale.Items.Clear();

            List<Biblikutak> lokali = Sorter.SortText<Biblikutak>(DBHelper.GetClosedLokals(), "Naziv", s_ztf_naz);
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                ZatvoreneFilijale.Items.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        private bool s_ztf_adr = false;
        private void btn_ZFil_Adresa_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_adr = !s_ztf_adr;
            ZatvoreneFilijale.Items.Clear();

            List<ZatFilView> toSort = new List<ZatFilView>();
            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (ZatFilView zfv in Sorter.SortText<ZatFilView>(toSort, "Adresa", s_ztf_adr)) ZatvoreneFilijale.Items.Add(zfv);
        }
        private bool s_ztf_grad = false;
        private void btn_ZFil_Grad_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_grad = !s_ztf_grad;
            ZatvoreneFilijale.Items.Clear();

            List<ZatFilView> toSort = new List<ZatFilView>();
            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (ZatFilView zfv in Sorter.SortText<ZatFilView>(toSort, "GetMesto", s_ztf_grad)) ZatvoreneFilijale.Items.Add(zfv);
        }
        private bool s_ztf_drz = false;
        private void btn_ZFil_Drzava_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_drz = !s_ztf_grad;
            ZatvoreneFilijale.Items.Clear();

            List<ZatFilView> toSort = new List<ZatFilView>();
            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (ZatFilView zfv in Sorter.SortText<ZatFilView>(toSort, "GetDrzava", s_ztf_grad)) ZatvoreneFilijale.Items.Add(zfv);
        }
        private bool s_ztf_dat = false;
        private void btn_ZFil_PerOtv_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_dat = !s_ztf_dat;
            ZatvoreneFilijale.Items.Clear();

            List<ZatFilView> toSort = new List<ZatFilView>();
            List<Biblikutak> lokali = DBHelper.GetClosedLokals();
            foreach (Biblikutak biblikutak in lokali)
            {
                Mesto m = DBHelper.GetMesto(biblikutak.PosBr);
                Drzava d = DBHelper.GetDrzava(biblikutak.OZND);
                toSort.Add(new ZatFilView(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
            foreach (ZatFilView zfv in Sorter.SortText<ZatFilView>(toSort, "PeriodRada", s_ztf_dat)) ZatvoreneFilijale.Items.Add(zfv);
        }
        private void ZFil_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("zr click");
        }
        #endregion
        #endregion

        #region LIST INTERACTIONS
        private void DeselectFromAllLists()
        {
            OtvoreneFilijale.SelectedItem = null;
            ZatvoreneFilijale.SelectedItem = null;
            ZaposleniRadnici.SelectedItem = null;
            NezaposleniRadnici.SelectedItem = null;
        }
        private void OtvoreneFilijale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OtvFilView ofv = ((ListView)sender).SelectedItem as OtvFilView;
            if (ofv == null) return;
            Window editFilWindow = new AdminEditStoreWindow(caller: this, selectedID: ofv.IDBK, quitAfterSave:true);
            editFilWindow.ShowDialog();
        }
        private void OtvoreneFilijale_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
        private void OtvoreneFilijale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }

        private void ZaposleniRadnici_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ZapRadView zrv = ((ListView)sender).SelectedItem as ZapRadView;
            if (zrv == null) return;
            Window editWorkerWindow = new AdminEditWorkerWindow(caller: this, selectedID: zrv.IDRadnik, tip: EnumsHelper.GetTipRadnika(zrv.Tip), quitAfterSave:true);
            editWorkerWindow.ShowDialog();
        }
        private void ZaposleniRadnici_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void ZaposleniRadnici_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }
        private void NezaposleniRadnici_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }

        private void ZatvoreneFilijale_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeselectFromAllLists();
        }
        #endregion

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        #region Buttons
        private void btn_AddWorker_Click(object sender, RoutedEventArgs e)
        {
            Window addWorkerWindow = new AdminAddWorkerWindow(this, _currentUser.IDRadnik);
            addWorkerWindow.ShowDialog();
        }

        private void btn_EditWorker_Click(object sender, RoutedEventArgs e)
        {
            Window editWorkerWindow = new AdminEditWorkerWindow();
            editWorkerWindow.ShowDialog();
        }

        private void btn_AddStore_Click(object sender, RoutedEventArgs e)
        {
            Window addStoreWindow = new AdminAddStoreWindow(this);
            addStoreWindow.ShowDialog();
        }

        private void btn_EditStore_Click(object sender, RoutedEventArgs e)
        {
            Window editStoreWindow = new AdminEditStoreWindow();
            editStoreWindow.ShowDialog();
        }

        private void btn_EditLanguage_Click(object sender, RoutedEventArgs e)
        {
            Window editLanguageWindow = new AdminEditLanguageWindow();
            editLanguageWindow.ShowDialog();
        }

        private void btn_EditZanr_Click(object sender, RoutedEventArgs e)
        {
            Window editZanrWindow = new AdminEditZanrWindow();
            editZanrWindow.ShowDialog();
        }

        private void btn_EditFormat_Click(object sender, RoutedEventArgs e)
        {
            Window editFormat = new AdminEditFormatWindow();
            editFormat.ShowDialog();
        }

        private void btn_EditPeriod_Click(object sender, RoutedEventArgs e)
        {
            Window editPeriod = new AdminEditPeriodWindow();
            editPeriod.ShowDialog();
        }

        #endregion

    }
}


