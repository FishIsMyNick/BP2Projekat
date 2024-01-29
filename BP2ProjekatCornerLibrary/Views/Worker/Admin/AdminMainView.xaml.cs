using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.ViewModel;
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
    public partial class AdminMainView : Window, iDynamicListView, iSortedListView
    {
        private Models.Admin _currentUser;
        
        public static AdminMainView Instance { get; set; }
        public string Zap_Ime { get; set; }
        public string Zap_Prezime { get; set; }
        public string Zap_Username { get; set; }
        public string Zap_Tip { get; set; }

        //TODO: treba da bude lista radnika
        public List<ViewZapRad> ListaZaposlenihRadnika { get; set; }
        public List<ViewOtpRad> ListaOtpustenihRadnika { get; set; }
        public List<ViewOtvFil> ListaOtvorenihFilijala { get; set; }
        public List<ViewZatFil> ListaZatvorenihFilijala { get; set; }

        public int RadnikTableImeWidth = 300;
        public AdminMainView(Models.Admin admin)
        {
            if (Instance == null)
                Instance = this;

            _currentUser = admin;
            DBHelper._currentUserID = admin.IDRadnik;

            InitializeComponent();

            Arrows = new List<Image>
            {
                img_NezapRad_Ime_Sort,
                img_NezapRad_PerZap_Sort,
                img_NezapRad_Prezime_Sort,
                img_NezapRad_Username_Sort,
                img_NezapRad_Tip_Sort,
                img_ZapRad_Ime_Sort,
                img_ZapRad_Prezime_Sort,
                img_ZapRad_Username_Sort,
                img_ZapRad_Tip_Sort,
                img_ZapRad_DatZap_Sort,
                img_OFil_Naziv_Sort,
                img_OFil_Ulica_Sort,
                img_OFil_Grad_Sort,
                img_OFil_Drzava_Sort,
                img_OFil_DatOtv_Sort,
                img_ZFil_Naziv_Sort,
                img_ZFil_Ulica_Sort,
                img_ZFil_Grad_Sort,
                img_ZFil_Drzava_Sort,
                img_ZFil_PerOtv_Sort
            };

            FillAllLists();
        }

        #region List population
        public void RefreshLists()
        {
            FillAllLists();
        }
        private void FillAllLists()
        {
            DisableAllArrows();
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
                ZaposleniRadnici.Items.Add(new ViewZapRad(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
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
                NezaposleniRadnici.Items.Add(new ViewOtpRad(radnik.IDRadnik, radnik.Ime, radnik.Prezime, radnik.DatZap, radnik.DatOtp ?? radnik.DatRodj, nalog.KorisnickoIme, EnumsHelper.GetTipRadnikaString(nalog.TipNaloga)));
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
                OtvoreneFilijale.Items.Add(new ViewOtvFil(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
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
                ZatvoreneFilijale.Items.Add(new ViewZatFil(biblikutak.IDBK, biblikutak.Naziv, biblikutak.DatOtv, biblikutak.DatZat, biblikutak.Ulica, biblikutak.Broj, m.PosBr, d.OZND));
            }
        }
        #endregion

        #region SORTING
        public List<Image> Arrows { get; set; }
        public void DisableAllArrows()
        {
            ArrowHelper.DisableAllArrows(Arrows);
        }

        public void SetArrow(Image arrow, bool ascending)
        {
            DisableAllArrows();
            ArrowHelper.SetArrow(arrow, ascending);
        }


        #region Zaposleni radnici
        private List<ViewZapRad> GetAllZapRadFromList()
        {
            List<ViewZapRad> ret = new List<ViewZapRad>();
            foreach (var j in ZaposleniRadnici.Items) ret.Add(j as ViewZapRad);
            return ret;
        }
        private void SortZapRadString(string param, bool ascending)
        {
            List<ViewZapRad> toSort = GetAllZapRadFromList();
            ZaposleniRadnici.Items.Clear();
            foreach (ViewZapRad j in Sorter.SortText<ViewZapRad>(toSort, param, ascending)) ZaposleniRadnici.Items.Add(j);
        }
        private void SortZapRadDate(string param, bool ascending)
        {
            List<ViewZapRad> toSort = GetAllZapRadFromList();
            ZaposleniRadnici.Items.Clear();
            foreach (ViewZapRad j in Sorter.SortDateString<ViewZapRad>(toSort, param, ascending)) ZaposleniRadnici.Items.Add(j);
        }
        private bool s_zr_ime = false;
        private void btn_ZapRadnik_Ime_Click(object sender, RoutedEventArgs e)
        {
            s_zr_ime = !s_zr_ime;
            SetArrow(img_ZapRad_Ime_Sort, s_zr_ime);
            SortZapRadString("Ime", s_zr_ime);
        }
        private bool s_zr_prez = false;
        private void btn_ZapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
        {
            s_zr_prez = !s_zr_prez;
            SetArrow(img_ZapRad_Prezime_Sort, s_zr_prez);
            SortZapRadString("Prezime", s_zr_prez);
        }
        private bool s_zr_user = false;
        private void btn_ZapRadnik_Username_Click(object sender, RoutedEventArgs e)
        {
            s_zr_user = !s_zr_user;
            SetArrow(img_ZapRad_Username_Sort, s_zr_user);
            SortZapRadString("KorisnickoIme", s_zr_user);
        }
        private bool s_zr_tip = false;
        private void btn_ZapRadnik_Tip_Click(object sender, RoutedEventArgs e)
        {
            s_zr_tip = !s_zr_tip;
            SetArrow(img_ZapRad_Tip_Sort, s_zr_tip);
            SortZapRadString("Tip", s_zr_tip);
        }
        private bool s_zr_datZ = false;
        private void btn_ZapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
        {
            s_zr_datZ = !s_zr_datZ;
            SetArrow(img_ZapRad_DatZap_Sort, s_zr_datZ);
            SortZapRadDate("DatZapStr", s_zr_datZ);
        }
        #endregion

        #region Nezaposleni radnici
        private List<ViewOtpRad> GetAllOtpRadFromList()
        {
            List<ViewOtpRad> ret = new List<ViewOtpRad>();
            foreach (var j in NezaposleniRadnici.Items) ret.Add(j as ViewOtpRad);
            return ret;
        }
        private void SortOtpRadString(string param, bool ascending)
        {
            List<ViewOtpRad> toSort = GetAllOtpRadFromList();
            NezaposleniRadnici.Items.Clear();
            foreach (ViewOtpRad j in Sorter.SortText<ViewOtpRad>(toSort, param, ascending)) NezaposleniRadnici.Items.Add(j);
        }
        private void SortOtpRadDate(string param, bool ascending)
        {
            List<ViewOtpRad> toSort = GetAllOtpRadFromList();
            NezaposleniRadnici.Items.Clear();
            foreach (ViewOtpRad j in Sorter.SortDateString<ViewOtpRad>(toSort, param, ascending)) NezaposleniRadnici.Items.Add(j);
        }
        private bool s_nzr_ime = false;
        private void btn_NezapRadnik_Ime_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_ime = !s_nzr_ime;
            SetArrow(img_NezapRad_Ime_Sort, s_nzr_ime);
            SortOtpRadString("Ime", s_nzr_ime);
        }
        private bool s_nzr_prez = false;
        private void btn_NezapRadnik_Prezime_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_prez = !s_nzr_prez;
            SetArrow(img_NezapRad_Prezime_Sort, s_nzr_prez);
            SortOtpRadString("Prezime", s_nzr_prez);
        }
        private bool s_nzr_user = false;
        private void btn_NezapRadnik_Username_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_user = !s_nzr_user;
            SetArrow(img_NezapRad_Username_Sort, s_nzr_user);
            SortOtpRadString("KorisnickoIme", s_nzr_user);
        }
        private bool s_nzr_tip = false;
        private void btn_NezapRadnik_Tip_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_tip = !s_nzr_tip;
            SetArrow(img_NezapRad_Tip_Sort, s_nzr_tip);
            SortOtpRadString("Tip", s_nzr_tip);
        }
        private bool s_nzr_datZ = false;
        private void btn_NezapRadnik_DatZap_Click(object sender, RoutedEventArgs e)
        {
            s_nzr_datZ = !s_nzr_datZ;
            SetArrow(img_NezapRad_PerZap_Sort, s_nzr_datZ);
            SortOtpRadDate("DatZapStr", s_nzr_datZ);
        }
        private void NezapRadnici_List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("nzr click");
        }
        #endregion

        #region Otvorene filijale
        private List<ViewOtvFil> GetAllOtvFilFromList()
        {
            List<ViewOtvFil> ret = new List<ViewOtvFil>();
            foreach (var j in OtvoreneFilijale.Items) ret.Add(j as ViewOtvFil);
            return ret;
        }
        private void SortOtvFilString(string param, bool ascending)
        {
            List<ViewOtvFil> toSort = GetAllOtvFilFromList();
            OtvoreneFilijale.Items.Clear();
            foreach (ViewOtvFil j in Sorter.SortText<ViewOtvFil>(toSort, param, ascending)) OtvoreneFilijale.Items.Add(j);
        }
        private void SortOtvFilDate(string param, bool ascending)
        {
            List<ViewOtvFil> toSort = GetAllOtvFilFromList();
            OtvoreneFilijale.Items.Clear();
            foreach (ViewOtvFil j in Sorter.SortDateString<ViewOtvFil>(toSort, param, ascending)) OtvoreneFilijale.Items.Add(j);
        }
        private bool s_otf_naz = false;
        private void btn_OFil_Naziv_Click(object sender, RoutedEventArgs e)
        {
            s_otf_naz = ! s_otf_naz;
            SetArrow(img_OFil_Naziv_Sort, s_otf_naz);
            SortOtvFilString("Naziv", s_otf_naz);
        }
        private bool s_otf_adr = false;
        private void btn_OFil_Adresa_Click(object sender, RoutedEventArgs e)
        {
            s_otf_adr = !s_otf_adr;
            SetArrow(img_OFil_Ulica_Sort, s_otf_adr);
            SortOtvFilString("Adresa", s_otf_adr);
        }
        private bool s_otf_mesto = false;
        private void btn_OFil_Grad_Click(object sender, RoutedEventArgs e)
        {
            s_otf_mesto = !s_otf_mesto;
            SetArrow(img_OFil_Grad_Sort, s_otf_mesto);
            SortOtvFilString("GetMesto", s_otf_mesto);
        }
        private bool s_otf_drz = false;
        private void btn_OFil_Drzava_Click(object sender, RoutedEventArgs e)
        {
            s_otf_drz = !s_otf_drz;
            SetArrow(img_OFil_Drzava_Sort, s_otf_drz);
            SortOtvFilString("GetDrzava", s_otf_drz);
        }
        private bool s_otf_dat = false;
        private void btn_OFil_DatOtv_Click(object sender, RoutedEventArgs e)
        {
            s_otf_dat = !s_otf_dat;
            SetArrow(img_OFil_DatOtv_Sort, s_otf_dat);
            SortOtvFilDate("DatOtvStr", s_otf_dat);
        }
        #endregion

        #region Zatvorene filijale
        private List<ViewZatFil> GetAllZatFilFromList()
        {
            List<ViewZatFil> ret = new List<ViewZatFil>();
            foreach (var j in ZatvoreneFilijale.Items) ret.Add(j as ViewZatFil);
            return ret;
        }
        private void SortZatFilString(string param, bool ascending)
        {
            List<ViewZatFil> toSort = GetAllZatFilFromList();
            ZatvoreneFilijale.Items.Clear();
            foreach (ViewZatFil j in Sorter.SortText<ViewZatFil>(toSort, param, ascending)) ZatvoreneFilijale.Items.Add(j);
        }
        private void SortZatFilDate(string param, bool ascending)
        {
            List<ViewZatFil> toSort = GetAllZatFilFromList();
            ZatvoreneFilijale.Items.Clear();
            foreach (ViewZatFil j in Sorter.SortDateString<ViewZatFil>(toSort, param, ascending)) ZatvoreneFilijale.Items.Add(j);
        }
        private bool s_ztf_naz = false;
        private void btn_ZFil_Naziv_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_naz = ! s_ztf_naz;
            SetArrow(img_ZFil_Naziv_Sort, s_ztf_naz);
            SortZatFilString("Naziv", s_ztf_naz);
        }
        private bool s_ztf_adr = false;
        private void btn_ZFil_Adresa_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_adr = !s_ztf_adr;
            SetArrow(img_ZFil_Ulica_Sort, s_ztf_adr);
            SortZatFilString("Adresa", s_ztf_adr);
        }
        private bool s_ztf_grad = false;
        private void btn_ZFil_Grad_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_grad = !s_ztf_grad;
            SetArrow(img_ZFil_Grad_Sort, s_ztf_grad);
            SortZatFilString("GetMesto", s_ztf_grad);
        }
        private bool s_ztf_drz = false;
        private void btn_ZFil_Drzava_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_drz = !s_ztf_drz;
            SetArrow(img_ZFil_Drzava_Sort, s_ztf_drz);
            SortZatFilString("GetDrzava", s_ztf_drz);
        }
        private bool s_ztf_dat = false;
        private void btn_ZFil_PerOtv_Click(object sender, RoutedEventArgs e)
        {
            s_ztf_dat = !s_ztf_dat;
            SetArrow(img_ZFil_PerOtv_Sort, s_ztf_dat);
            SortZatFilDate("DatOtvStr", s_ztf_dat);
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
            ViewOtvFil ofv = ((ListView)sender).SelectedItem as ViewOtvFil;
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
            ViewZapRad zrv = ((ListView)sender).SelectedItem as ViewZapRad;
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


