using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar
{
    /// <summary>
    /// Interaction logic for BibSuLWindow.xaml
    /// </summary>
    public partial class BibSuLWindow : Window, iDynamicListView, iSortedListView
    {
        private int _currentUser;
        private int _lokalId;
        private iStivoView _stivoView;
        private SuLView _selectedSuL;
        private bool _blockEvents;

        private bool _prikaziSvoStivo => cb_Prikaz.SelectedIndex == 0;

        public List<Image> Arrows { get; set; }

        public BibSuLWindow(int currentUser)
        {
            _blockEvents = true;
            _currentUser = currentUser;
            _lokalId = DBHelper.GetLatestRasporedjenBibliotekar(currentUser).IDBK;
            _stivoView = iStivoView.Knjiga;

            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_autori,
                img_sort_cenaSS,
                img_sort_brIzd,
                img_sort_brIzdSS,
                img_sort_id,
                img_sort_idSS,
                img_sort_izdKuce,
                img_sort_izdKuceSS,
                img_sort_kolicina,
                img_sort_kolicinaSS,
                img_sort_naziv,
                img_sort_nazivSS,
                img_sort_vrIzd,
                img_sort_vrIzdSS
            };
            DisableAllArrows();

            RefreshLists();
            SetSelectView(iStivoView.Knjiga);
            cb_TipStiva.SelectedIndex = 0;
            cb_Prikaz.SelectedIndex = 0;
            _blockEvents = false;
        }

        private void ReloadStivoList()
        {
            DisableAllArrows();
            switch (cb_TipStiva.SelectedIndex)
            {
                case 0: SetSelectView(iStivoView.Knjiga); break;
                case 1: SetSelectView(iStivoView.Novine); break;
                case 2: SetSelectView(iStivoView.Magazin); break;
            }
        }
        private void cb_TipStiva_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;
            ReloadStivoList();
        }
        private void cb_Prikaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_blockEvents) return;
            ReloadStivoList();
        }
        private void SetEditView(iStivoView tip)
        {
            view_select.Visibility = Visibility.Collapsed;
            view_edit.Visibility = Visibility.Visible;

            switch (tip)
            {
                case iStivoView.Knjiga:
                    grd_edit_knjiga.Visibility = Visibility.Visible;
                    grd_edit_sstivo.Visibility = Visibility.Collapsed;
                    FillBookTextFields();
                    break;
                case iStivoView.Novine:
                case iStivoView.Magazin:
                    grd_edit_knjiga.Visibility = Visibility.Collapsed;
                    grd_edit_sstivo.Visibility = Visibility.Visible;
                    FillSStivoTextFields();
                    break;
            }
        }
        private void SetSelectView(iStivoView tip = iStivoView.Knjiga)
        {
            view_select.Visibility = Visibility.Visible;
            view_edit.Visibility = Visibility.Collapsed;

            switch (tip)
            {
                case iStivoView.Knjiga:
                    sp_ListaKnjiga.Visibility = Visibility.Visible;
                    sp_ListaSStiva.Visibility = Visibility.Collapsed;
                    RefreshKnjigeList();
                    break;
                case iStivoView.Novine:
                    sp_ListaKnjiga.Visibility = Visibility.Collapsed;
                    sp_ListaSStiva.Visibility = Visibility.Visible;
                    RefreshNovineList();
                    break;
                case iStivoView.Magazin:
                    sp_ListaKnjiga.Visibility = Visibility.Collapsed;
                    sp_ListaSStiva.Visibility = Visibility.Visible;
                    RefreshMagazinList();
                    break;
            }
        }

        #region LIST POPULATION
        public void RefreshLists()
        {
            switch (cb_TipStiva.SelectedIndex)
            {
                case 0: _stivoView = iStivoView.Knjiga; break;
                case 1: _stivoView = iStivoView.Novine; break;
                case 2: _stivoView = iStivoView.Magazin; break;
            }
            switch (_stivoView)
            {
                case iStivoView.Knjiga:
                    RefreshKnjigeList(); break;
                case iStivoView.Novine:
                    RefreshNovineList(); break;
                case iStivoView.Magazin:
                    RefreshMagazinList(); break;
                default: break;
            }
        }
        private void RefreshKnjigeList()
        {
            Knjige.Items.Clear();
            List<KnjigaULokalu> kkk = DBHelper.GetAllLatestKnjigeULokalu(_lokalId);
            List<int> idsInLokal = new List<int>();
            foreach (KnjigaULokalu kul in kkk)
            {
                if (kul.DatBrisanja == null)
                    idsInLokal.Add(kul.IDKnjiga);
            }
            foreach (KnjigaULokalu kul in kkk)
            {
                if (idsInLokal.Contains(kul.IDKnjiga))
                    Knjige.Items.Add(new SuLView(DBHelper.GetKnjiga(kul.IDKnjiga), _lokalId, kul.Kolicina));
            }
            if (_prikaziSvoStivo)
            {
                foreach (Knjiga k in DBHelper.GetAllKnjigas())
                {
                    if (!idsInLokal.Contains(k.IDKnjiga))
                        Knjige.Items.Add(new SuLView(k, _lokalId, 0, false));
                }
            }
        }
        private void RefreshNovineList()
        {
            RefreshSStivoList(1);
        }
        private void RefreshMagazinList()
        {
            RefreshSStivoList(2);
        }
        private void RefreshSStivoList(int tipSS)
        {
            SStivo.Items.Clear();
            List<IzdSStivoULokalu> sss = DBHelper.GetAllLatestIzdSStivoULokalu(_lokalId);
            List<Tuple<int, int>> idsInLokal = new List<Tuple<int, int>>();
            foreach (IzdSStivoULokalu sul in sss)
            {
                if (sul.DatBrisanja == null)
                    idsInLokal.Add(new Tuple<int, int>(sul.IDSStivo, sul.BrIzd));
            }
            foreach (IzdSStivoULokalu issul in sss)
            {
                if (idsInLokal.Contains(new Tuple<int, int>(issul.IDSStivo, issul.BrIzd)))
                {
                    SerijskoStivo ss = DBHelper.GetSerijskoStivo(issul.IDSStivo);
                    if (ss.TipStiva == tipSS)
                        SStivo.Items.Add(new SuLView(ss, DBHelper.GetIzdanjeSStiva(issul.IDSStivo, issul.BrIzd), _lokalId, issul.Kolicina));
                }
            }
            if (_prikaziSvoStivo)
            {
                foreach (IzdanjeSStiva iss in DBHelper.GetAllIzdanjeSStiva())
                {
                    SerijskoStivo ss = DBHelper.GetSerijskoStivo(iss.IDSStivo);
                    if (ss.TipStiva != tipSS) continue;
                    Tuple<int, int> toAdd = new Tuple<int, int>(iss.IDSStivo, iss.BrIzd);
                    if (!idsInLokal.Contains(toAdd))
                        SStivo.Items.Add(new SuLView(DBHelper.GetSerijskoStivo(ss.IDSStivo), iss, _lokalId, 0, false));
                }
            }
        }

        /// <summary>
        /// Fill the text fields for the edit view. MUST SET _selectedSuL BEFORE CALL
        /// </summary>
        private void FillBookTextFields()
        {
            lb_IdStivo.Content = _selectedSuL.GetID;

            tb_Name.Text = _selectedSuL.GetNaziv;
            tb_BrIzd.Text = _selectedSuL.GetBrIzdanja;
            tb_VrIzd.Text = _selectedSuL.GetVrIzd;
            tb_BrStr.Text = _selectedSuL.GetBrStrana;
            tb_VelFont.Text = _selectedSuL.GetVelFonta;
            tb_Korice.Text = _selectedSuL.GetKorice;
            tb_Format.Text = _selectedSuL.GetFormat;
            cb_Ogr.IsChecked = _selectedSuL.GetOgraniceno;
            tb_Kolicina.Text = _selectedSuL._Kolicina.ToString();
            btn_Delete.IsEnabled = _selectedSuL._IsInStore;

            lbx_Autori.Items.Clear();
            foreach (Autor a in DBHelper.GetAllBookAuthors(_selectedSuL._Knjiga))
            {
                lbx_Autori.Items.Add(a.GetFullName);
            }
            lbx_Jezici.Items.Clear();
            foreach (Jezik j in DBHelper.GetAllBookJeziks(_selectedSuL._Knjiga))
            {
                lbx_Jezici.Items.Add(j.NazivJezika);
            }
            lbx_Zanrovi.Items.Clear();
            foreach (Zanr z in DBHelper.GetAllBookZanrs(_selectedSuL._Knjiga))
            {
                lbx_Zanrovi.Items.Add(z.NazivZanra);
            }
            lbx_IzdKuce.Items.Clear();
            foreach (IzdKuca ik in DBHelper.GetAllBookIzdKucas(_selectedSuL._Knjiga))
            {
                lbx_IzdKuce.Items.Add(ik.Naziv);
            }
        }
        /// <summary>
        /// Fill the text fields for the edit view. MUST SET _selectedSuL BEFORE CALL
        /// </summary>
        private void FillSStivoTextFields()
        {
            lb_IdStivo.Content = _selectedSuL.GetID;

            tb_NameSS.Text = _selectedSuL.GetNaziv;
            tb_BrIzdSS.Text = _selectedSuL.GetBrIzdanja;
            tb_VrIzdSS.Text = _selectedSuL.GetVrIzd;
            tb_PeriodSS.Text = _selectedSuL.GetPeriod;
            tb_FormatSS.Text = _selectedSuL.GetFormat;
            tb_CenaSS.Text = _selectedSuL.GetCena;
            tb_KolicinaSS.Text = _selectedSuL._Kolicina.ToString();
            btn_Delete.IsEnabled = _selectedSuL._IsInStore;

            lbx_JeziciSS.Items.Clear();
            foreach (Jezik j in DBHelper.GetAllSStivoJeziks(_selectedSuL._SStivo))
            {
                lbx_JeziciSS.Items.Add(j.NazivJezika);
            }
            lbx_IzdKuceSS.Items.Clear();
            foreach (IzdKuca ik in DBHelper.GetAllSStivoIzdKucas(_selectedSuL._SStivo))
            {
                lbx_IzdKuceSS.Items.Add(ik.Naziv);
            }
        }

        private void ClearSelectedItems()
        {
            Knjige.SelectedItem = null;
            SStivo.SelectedItem = null;
        }
        #endregion

        #region BUTTONS
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            switch (_selectedSuL.GetTip)
            {
                case 0:
                    if (!Validator.PozNumber0(tb_Kolicina.Text)) return;

                    if (!DBHelper.AddKnjigaULokalu(_selectedSuL._Knjiga.IDKnjiga, _lokalId, int.Parse(tb_Kolicina.Text)))
                    {
                        MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                        return;
                    }
                    MessageBox.Show("Uspešno ste izmenili podatke");
                    break;
                case 1:
                case 2:
                    if (!Validator.PozNumber0(tb_Kolicina.Text)) return;

                    if (!DBHelper.AddIzdSStivoULokalu(_selectedSuL._IzdSStiva.IDSStivo, _selectedSuL._IzdSStiva.BrIzd, _lokalId, int.Parse(tb_KolicinaSS.Text)))
                    {
                        MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                        return;
                    }
                    MessageBox.Show("Uspešno ste izmenili podatke");
                    break;
            }
            RefreshLists();
            ClearSelectedItems();
            SetSelectView(_selectedSuL.GetTipEnum);
            return;

        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            switch (_selectedSuL.GetTip)
            {
                case 0:
                    if (!DBHelper.DeleteKuL(new KnjigaULokalu(_selectedSuL._Knjiga.IDKnjiga, _lokalId, 0)))
                    {
                        MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                        return;
                    }
                    break;
                case 1:
                case 2:
                    if (!DBHelper.DeleteIzdSSuL(new IzdSStivoULokalu(_selectedSuL._IzdSStiva.IDSStivo, _selectedSuL._IzdSStiva.BrIzd, _lokalId, 0)))
                    {
                        MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                        return;
                    }
                    break;
            }
            MessageBox.Show("Uspešno ste obrisali podatke!");
            RefreshLists();
            ClearSelectedItems();
            SetSelectView(_selectedSuL.GetTipEnum);
        }
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearSelectedItems();
            SetSelectView(_selectedSuL.GetTipEnum);
        }
        #endregion
        #region LIST CONTROL

        private void Knjige_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SuLView sel = ((ListView)sender).SelectedItem as SuLView;
            if (sel != null)
            {
                _selectedSuL = sel;
                SetEditView(iStivoView.Knjiga);
            }
        }
        private void SStivo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SuLView sel = ((ListView)sender).SelectedItem as SuLView;
            if (sel != null)
            {
                _selectedSuL = sel;
                if (cb_TipStiva.SelectedValue == "Novine")
                    SetEditView(iStivoView.Novine);
                else
                    SetEditView(iStivoView.Magazin);
            }
        }
        #endregion

        #region SORTING

        #region KNJIGE
        private List<SuLView> GetAllKnjigeFromList()
        {
            List<SuLView> ret = new List<SuLView>();
            foreach (var izm in Knjige.Items)
            {
                ret.Add(izm as SuLView);
            }
            return ret;
        }
        private void SortKnjigeText(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortText<SuLView>(GetAllKnjigeFromList(), propName, ascending);
            Knjige.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                Knjige.Items.Add(izd);
            }
        }
        private void SortKnjigeDate(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortGodina<SuLView>(GetAllKnjigeFromList(), propName, ascending);
            Knjige.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                Knjige.Items.Add(izd);
            }
        }
        private void SortKnjigeKolicina(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortKolicina<SuLView>(GetAllKnjigeFromList(), propName, ascending);
            Knjige.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                Knjige.Items.Add(izd);
            }
        }

        private bool s_id_k = false;
        private void btn_sort_id_Click(object sender, RoutedEventArgs e)
        {
            s_id_k = !s_id_k;
            SetArrow(img_sort_id, s_id_k);
            SortKnjigeText("GetID", s_id_k);
        }
        private bool s_naz_k = false;
        private void btn_sort_naziv_Click(object sender, RoutedEventArgs e)
        {
            s_naz_k = !s_naz_k;
            SetArrow(img_sort_naziv, s_naz_k);
            SortKnjigeText("GetNaziv", s_naz_k);
        }
        private bool s_aut_k = false;
        private void btn_sort_autori_Click(object sender, RoutedEventArgs e)
        {
            s_aut_k = !s_aut_k;
            SetArrow(img_sort_autori, s_aut_k);
            SortKnjigeText("GetAutori", s_aut_k);
        }
        private bool s_bri_k = false;
        private void btn_sort_brIzd_Click(object sender, RoutedEventArgs e)
        {
            s_bri_k = !s_bri_k;
            SetArrow(img_sort_brIzd, s_bri_k);
            SortKnjigeText("GetBrIzdanja", s_bri_k);
        }
        private bool s_ik_k = false;
        private void btn_sort_izdKuce_Click(object sender, RoutedEventArgs e)
        {
            s_ik_k = !s_ik_k;
            SetArrow(img_sort_izdKuce, s_ik_k);
            SortKnjigeText("GetIzdKuce", s_ik_k);
        }
        private bool s_vrI_k = false;
        private void btn_sort_vrIzd_Click(object sender, RoutedEventArgs e)
        {
            s_vrI_k = !s_vrI_k;
            SetArrow(img_sort_vrIzd, s_vrI_k);
            SortKnjigeDate("GetVrIzd", s_vrI_k);
        }
        private bool s_kol_k = false;
        private void btn_sort_kolicina_Click(object sender, RoutedEventArgs e)
        {
            s_kol_k = !s_kol_k;
            SetArrow(img_sort_kolicina, s_kol_k);
            SortKnjigeKolicina("GetKolicina", s_kol_k);
        }
        #endregion
        #region SSTIVO
        private List<SuLView> GetAllSStivoFromList()
        {
            List<SuLView> ret = new List<SuLView>();
            foreach (var izm in SStivo.Items)
            {
                ret.Add(izm as SuLView);
            }
            return ret;
        }
        private void SortSStivoText(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortText<SuLView>(GetAllSStivoFromList(), propName, ascending);
            SStivo.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                SStivo.Items.Add(izd);
            }
        }
        private void SortSStivoDate(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortDateString<SuLView>(GetAllSStivoFromList(), propName, ascending);
            SStivo.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                SStivo.Items.Add(izd);
            }
        }
        private void SortSStivoKolicina(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortKolicina<SuLView>(GetAllSStivoFromList(), propName, ascending);
            SStivo.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                SStivo.Items.Add(izd);
            }
        }
        private void SortSStivoDecimal(string propName, bool ascending)
        {
            List<SuLView> sorted = Sorter.SortDecimal<SuLView>(GetAllSStivoFromList(), propName, ascending);
            SStivo.Items.Clear();
            foreach (SuLView izd in sorted)
            {
                SStivo.Items.Add(izd);
            }
        }


        private bool s_id_s = false;
        private void btn_sort_idSS_Click(object sender, RoutedEventArgs e)
        {
            s_id_s = !s_id_s;
            SetArrow(img_sort_idSS, s_id_s);
            SortSStivoText("GetID", s_id_s);
        }
        private bool s_naz_s = false;
        private void btn_sort_nazivSS_Click(object sender, RoutedEventArgs e)
        {
            s_naz_s = !s_naz_s;
            SetArrow(img_sort_nazivSS, s_naz_s);
            SortSStivoText("GetNaziv", s_naz_s);
        }
        private bool s_brI_s = false;
        private void btn_sort_brIzdSS_Click(object sender, RoutedEventArgs e)
        {
            s_brI_s = !s_brI_s;
            SetArrow(img_sort_brIzdSS, s_brI_s);
            SortSStivoText("GetBrIzdanja", s_brI_s);
        }
        private bool s_vrI_s = false;
        private void btn_sort_vrIzdSS_Click(object sender, RoutedEventArgs e)
        {
            s_vrI_s = !s_vrI_s;
            SetArrow(img_sort_vrIzdSS, s_vrI_s);
            SortSStivoDate("GetVrIzd", s_vrI_s);
        }
        private bool s_ik_s = false;
        private void btn_sort_izdKuceSS_Click(object sender, RoutedEventArgs e)
        {
            s_ik_s = !s_ik_s;
            SetArrow(img_sort_izdKuceSS, s_ik_s);
            SortSStivoText("GetIzdKuce", s_ik_s);
        }
        private bool s_cen_s = false;
        private void btn_sort_cenaSS_Click(object sender, RoutedEventArgs e)
        {
            s_cen_s = !s_cen_s;
            SetArrow(img_sort_cenaSS, s_cen_s);
            SortSStivoDecimal("GetCenaDec", s_cen_s);
        }
        private bool s_kol_s = false;
        private void btn_sort_kolicinaSS_Click(object sender, RoutedEventArgs e)
        {
            s_kol_s = !s_kol_s;
            SetArrow(img_sort_kolicinaSS, s_kol_s);
            SortSStivoKolicina("GetKolicina", s_kol_s);
        }

        public void DisableAllArrows()
        {
            ArrowHelper.DisableAllArrows(Arrows);
        }

        public void SetArrow(Image arrow, bool ascending)
        {
            DisableAllArrows();
            ArrowHelper.SetArrow(arrow, ascending);
        }


        #endregion

        #endregion
        private void ApplyFilters()
        {
            if (cb_TipStiva.SelectedIndex == 0)
            {
                List<SuLView> toShow = new List<SuLView>();
                List<SuLView> inList = new List<SuLView>();

                List<KnjigaULokalu> kkk = DBHelper.GetAllLatestKnjigeULokalu(_lokalId);
                List<int> idsInLokal = new List<int>();
                foreach (KnjigaULokalu kul in kkk)
                {
                    if (kul.DatBrisanja == null)
                        idsInLokal.Add(kul.IDKnjiga);
                }
                foreach (KnjigaULokalu kul in kkk)
                {
                    if (idsInLokal.Contains(kul.IDKnjiga))
                        inList.Add(new SuLView(DBHelper.GetKnjiga(kul.IDKnjiga), _lokalId, kul.Kolicina));
                }
                if (_prikaziSvoStivo)
                {
                    foreach (Knjiga k in DBHelper.GetAllKnjigas())
                    {
                        if (!idsInLokal.Contains(k.IDKnjiga))
                            inList.Add(new SuLView(k, _lokalId, 0, false));
                    }
                }

                foreach (SuLView sul in inList)
                {
                    if (sul.GetNaziv.ToLower().Contains(tb_f_k_naz.Text.ToLower())
                    && sul.GetID.ToLower().Contains(tb_f_k_id.Text.ToLower())
                    && sul.GetAutori.ToLower().Contains(tb_f_k_aut.Text.ToLower())
                    && sul.GetBrIzdanja.ToLower().Contains(tb_f_k_brI.Text.ToLower())
                    && sul.GetIzdKuce.ToLower().Contains(tb_f_k_ik.Text.ToLower())
                    && sul.GetVrIzd.ToLower().Contains(tb_f_k_vrI.Text.ToLower()))
                        toShow.Add(sul);
                }
                Knjige.Items.Clear();
                foreach(SuLView sul in toShow) Knjige.Items.Add(sul);
            }
            else if (cb_TipStiva.SelectedIndex == 1)
            {
                List<SuLView> toShow = new List<SuLView>();
                List<SuLView> inList = new List<SuLView>();

                List<IzdSStivoULokalu> sss = DBHelper.GetAllLatestIzdSStivoULokalu(_lokalId);
                List<Tuple<int, int>> idsInLokal = new List<Tuple<int, int>>();
                foreach (IzdSStivoULokalu sul in sss)
                {
                    if (sul.DatBrisanja == null)
                        idsInLokal.Add(new Tuple<int, int>(sul.IDSStivo, sul.BrIzd));
                }
                foreach (IzdSStivoULokalu issul in sss)
                {
                    if (idsInLokal.Contains(new Tuple<int, int>(issul.IDSStivo, issul.BrIzd)))
                    {
                        SerijskoStivo ss = DBHelper.GetSerijskoStivo(issul.IDSStivo);
                        if (ss.TipStiva == 1)
                            inList.Add(new SuLView(ss, DBHelper.GetIzdanjeSStiva(issul.IDSStivo, issul.BrIzd), _lokalId, issul.Kolicina));
                    }
                }
                if (_prikaziSvoStivo)
                {
                    foreach (IzdanjeSStiva iss in DBHelper.GetAllIzdanjeSStiva())
                    {
                        SerijskoStivo ss = DBHelper.GetSerijskoStivo(iss.IDSStivo);
                        if (ss.TipStiva != 1) continue;
                        Tuple<int, int> toAdd = new Tuple<int, int>(iss.IDSStivo, iss.BrIzd);
                        if (!idsInLokal.Contains(toAdd))
                            inList.Add(new SuLView(DBHelper.GetSerijskoStivo(ss.IDSStivo), iss, _lokalId, 0, false));
                    }
                }

                foreach (SuLView sul in inList)
                {
                    if (sul.GetNaziv.ToLower().Contains(tb_f_ss_naz.Text.ToLower())
                    && sul.GetID.ToLower().Contains(tb_f_ss_id.Text.ToLower())
                    && sul.GetBrIzdanja.ToLower().Contains(tb_f_ss_brI.Text.ToLower())
                    && sul.GetVrIzd.ToLower().Contains(tb_f_ss_dat.Text.ToLower())
                    && sul.GetIzdKuce.ToLower().Contains(tb_f_ss_ik.Text.ToLower())
                    && sul.GetCena.ToLower().Contains(tb_f_ss_cen.Text.ToLower()))
                        toShow.Add(sul);
                }
                SStivo.Items.Clear();
                foreach (SuLView sul in toShow) SStivo.Items.Add(sul);
            }
            else if (cb_TipStiva.SelectedIndex == 2)
            {
                List<SuLView> toShow = new List<SuLView>();
                List<SuLView> inList = new List<SuLView>();

                List<IzdSStivoULokalu> sss = DBHelper.GetAllLatestIzdSStivoULokalu(_lokalId);
                List<Tuple<int, int>> idsInLokal = new List<Tuple<int, int>>();
                foreach (IzdSStivoULokalu sul in sss)
                {
                    if (sul.DatBrisanja == null)
                        idsInLokal.Add(new Tuple<int, int>(sul.IDSStivo, sul.BrIzd));
                }
                foreach (IzdSStivoULokalu issul in sss)
                {
                    if (idsInLokal.Contains(new Tuple<int, int>(issul.IDSStivo, issul.BrIzd)))
                    {
                        SerijskoStivo ss = DBHelper.GetSerijskoStivo(issul.IDSStivo);
                        if (ss.TipStiva == 2)
                            inList.Add(new SuLView(ss, DBHelper.GetIzdanjeSStiva(issul.IDSStivo, issul.BrIzd), _lokalId, issul.Kolicina));
                    }
                }
                if (_prikaziSvoStivo)
                {
                    foreach (IzdanjeSStiva iss in DBHelper.GetAllIzdanjeSStiva())
                    {
                        SerijskoStivo ss = DBHelper.GetSerijskoStivo(iss.IDSStivo);
                        if (ss.TipStiva != 2) continue;
                        Tuple<int, int> toAdd = new Tuple<int, int>(iss.IDSStivo, iss.BrIzd);
                        if (!idsInLokal.Contains(toAdd))
                            inList.Add(new SuLView(DBHelper.GetSerijskoStivo(ss.IDSStivo), iss, _lokalId, 0, false));
                    }
                }

                foreach (SuLView sul in inList)
                {
                    if (sul.GetNaziv.ToLower().Contains(tb_f_ss_naz.Text.ToLower())
                    && sul.GetID.ToLower().Contains(tb_f_ss_id.Text.ToLower())
                    && sul.GetBrIzdanja.ToLower().Contains(tb_f_ss_brI.Text.ToLower())
                    && sul.GetVrIzd.ToLower().Contains(tb_f_ss_dat.Text.ToLower())
                    && sul.GetIzdKuce.ToLower().Contains(tb_f_ss_ik.Text.ToLower())
                    && sul.GetCena.ToLower().Contains(tb_f_ss_cen.Text.ToLower()))
                        toShow.Add(sul);
                }
                SStivo.Items.Clear();
                foreach (SuLView sul in toShow) SStivo.Items.Add(sul);
            }
        }
        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void btn_cl_filter_Click(object sender, RoutedEventArgs e)
        {
            tb_f_ss_naz.Text = "";
            tb_f_ss_id.Text = "";
            tb_f_ss_brI.Text = "";
            tb_f_ss_dat.Text = "";
            tb_f_ss_ik.Text = "";
            tb_f_ss_cen.Text = "";

            tb_f_k_naz.Text = "";
            tb_f_k_id.Text = "";
            tb_f_k_aut.Text = "";
            tb_f_k_brI.Text = "";
            tb_f_k_ik.Text = "";
            tb_f_k_vrI.Text = "";

            ApplyFilters();
        }
    }

    public enum iStivoView
    {
        Knjiga,
        Novine,
        Magazin
    }
    public class SuLView
    {
        #region CORE PROPS
        public int _Tip { get; set; }
        public Knjiga _Knjiga { get; set; }
        public SerijskoStivo _SStivo { get; set; }
        public IzdanjeSStiva _IzdSStiva { get; set; }
        public int _Lokal { get; set; }
        public int _Kolicina { get; set; }
        public bool _IsInStore { get; set; }

        private List<Autor> _autori;
        private List<Jezik> _jezici;
        private List<Zanr> _znarovi;
        private List<IzdKuca> _iks;
        #endregion

        #region DISPLAY PROPS
        public string GetID
        {
            get
            {
                switch (_Tip)
                {
                    case 0:
                        return _Knjiga.IDKnjiga.ToString();
                    case 1:
                    case 2:
                        return _SStivo.IDSStivo.ToString();
                    default: return "";
                }
            }
        }
        public string GetNaziv
        {
            get
            {
                switch (_Tip)
                {
                    case 0:
                        return _Knjiga.Naziv;
                    case 1:
                    case 2:
                        return _SStivo.Naziv;
                    default: return "";
                }
            }
        }
        public string GetAutori
        {
            get
            {
                string ret = "";
                for (int i = 0; i < _autori.Count; i++)
                {
                    ret += _autori[i].GetFullName;
                    if (i < _autori.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string GetJezici
        {
            get
            {
                string ret = "";
                for (int i = 0; i < _jezici.Count; i++)
                {
                    ret += _jezici[i].NazivJezika;
                    if (i < _jezici.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string GetZanrovi
        {
            get
            {
                string ret = "";
                for (int i = 0; i < _znarovi.Count; i++)
                {
                    ret += _znarovi[i].NazivZanra;
                    if (i < _znarovi.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string GetIzdKuce
        {
            get
            {
                string ret = "";
                for (int i = 0; i < _iks.Count; i++)
                {
                    ret += _iks[i].Naziv;
                    if (i < _iks.Count - 1) ret += ", ";
                }
                return ret;
            }
        }
        public string GetBrIzdanja
        {
            get
            {
                switch (_Tip)
                {
                    case 0:
                        return _Knjiga.BrIzd.ToString();
                    case 1:
                    case 2:
                        return _IzdSStiva.BrIzd.ToString();
                    default: return "";
                }
            }
        }
        public string GetVrIzd
        {
            get
            {
                switch (_Tip)
                {
                    case 0:
                        if (_Knjiga.GodIzd == null)
                        {
                            if (_Knjiga.VrIzd == null) return "Nepoznato";
                            else return _Knjiga.VrIzd;
                        }
                        else return _Knjiga.GodIzd.ToString() + ".";
                    case 1:
                    case 2:
                        return DateConverter.ToString(_IzdSStiva.DatIzd);
                    default: return "";
                }
            }
        }
        public string GetBrStrana => _Knjiga.BrStrana.ToString();
        public string GetVelFonta => _Knjiga.VelicinaFonta.ToString();
        public string GetKorice
        {
            get
            {
                if (_Knjiga.Korice == 0) return "Meke";
                else if (_Knjiga.Korice == 1) return "Tvrde";
                else return "";
            }
        }
        public string GetFormat
        {
            get
            {
                switch (_Tip)
                {
                    case 0:
                        return _Knjiga.Format;
                    case 1:
                    case 2:
                        return _SStivo.Format;
                    default: return "";
                }
            }
        }
        public bool GetOgraniceno => _Knjiga.Ograniceno == 0 ? false : true;
        public string GetKolicina
        {
            get
            {
                if (!_IsInStore) return "Nema u filijali";
                switch (_Tip)
                {
                    case 0:
                        return _Knjiga.DatBrisanja != null ? "Nema u filijali" : _Kolicina.ToString();
                    case 1:
                    case 2:
                        return _IzdSStiva.DatBrisanja != null ? "Nema u filijali" : _Kolicina.ToString();
                    default: return "Nema u filijali";
                }
            }
        }
        public string GetPeriod => _SStivo.Period;
        public string GetCena => _IzdSStiva.Cena.ToString();
        public decimal GetCenaDec => _IzdSStiva.Cena;
        public int GetTip => _Tip;
        public iStivoView GetTipEnum
        {
            get
            {
                switch (_Tip)
                {
                    case 0: return iStivoView.Knjiga;
                    case 1: return iStivoView.Novine;
                    case 2: return iStivoView.Magazin;
                    default: return iStivoView.Knjiga;
                }
            }
        }
        #endregion

        #region CTORS
        public SuLView(Knjiga k, int l, int kol, bool isInStore = true)
        {
            _Tip = 0;
            _Knjiga = k;
            _Lokal = l;
            _Kolicina = kol;
            _IsInStore = isInStore;
            List<Autor> a = DBHelper.GetAllBookAuthors(k);
            _autori = a != null ? a : new List<Autor>();
            List<Jezik> j = DBHelper.GetAllBookJeziks(k);
            _jezici = j != null ? j : new List<Jezik>();
            List<Zanr> z = DBHelper.GetAllBookZanrs(k);
            _znarovi = z != null ? z : new List<Zanr>();
            List<IzdKuca> i = DBHelper.GetAllBookIzdKucas(k);
            _iks = i != null ? i : new List<IzdKuca>();
        }
        public SuLView(SerijskoStivo ss, IzdanjeSStiva iss, int l, int kol, bool isInStore = true)
        {
            _Tip = ss.TipStiva;
            _SStivo = ss;
            _IzdSStiva = iss;
            _Lokal = l;
            _Kolicina = kol;
            _IsInStore = isInStore;
            _autori = new List<Autor>();
            _znarovi = new List<Zanr>();
            List<Jezik> j = DBHelper.GetAllSStivoJeziks(ss);
            _jezici = j != null ? j : new List<Jezik>();
            List<IzdKuca> i = DBHelper.GetAllSStivoIzdKucas(ss);
            _iks = i != null ? i : new List<IzdKuca>();
        }
        #endregion
    }
}
