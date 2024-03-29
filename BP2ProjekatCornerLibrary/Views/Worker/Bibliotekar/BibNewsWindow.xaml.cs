﻿using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar
{
    /// <summary>
    /// Interaction logic for BibNewsWindow.xaml
    /// </summary>
    public partial class BibNewsWindow : Window, iDynamicListView, iSortedListView
    {
        private int _currentUser;
        private int _lokalID;
        private bool _quitAfterSave = false;
        private iDynamicListView _caller;
        private SerijskoStivo _stivoToEdit;
        public List<Image> Arrows { get; set; }

        private int _tipSStiva = 1;
        public BibNewsWindow(int currentUser, SerijskoStivo toEdit = null, bool quitAfterSave = false, iDynamicListView caller = null)
        {
            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_format,
                img_sort_izdKuce,
                img_sort_jezici,
                img_sort_naziv,
                img_sort_period
            };
            DisableAllArrows();
            _stivoToEdit = toEdit;
            _caller = caller;
            _quitAfterSave = quitAfterSave;
            if (quitAfterSave)
            {
                SetAddView();
            }
            else if (_stivoToEdit == null)
            {
                SetSelectView();
            }
            else
            {
                SetEditView(toEdit);
            }

            _currentUser = currentUser;
            RasporedjenBibliotekar rasporedjenBibliotekar = DBHelper.GetLatestRasporedjenBibliotekar(_currentUser);
            _lokalID = rasporedjenBibliotekar.IDBK;


            RefreshLists();
            if (toEdit != null)
            {
                FillInputFields(toEdit);
            }
            _quitAfterSave = quitAfterSave;
            _caller = caller;
        }

        #region INIT CONTROLS
        private void SetAddView()
        {
            btn_Uredi_Izdanja.Visibility = Visibility.Collapsed;
            lb_Add_Book.Visibility = Visibility.Visible;
            grd_Add_Btns.Visibility = Visibility.Visible;

            lb_Edit_Book.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;

            btn_Uredi_Izdanja.Visibility = Visibility.Collapsed;
        }
        private void SetEditView(SerijskoStivo ss)
        {
            _stivoToEdit = ss;
            btn_Uredi_Izdanja.Visibility = Visibility.Visible;
            lb_Add_Book.Visibility = Visibility.Collapsed;
            grd_Add_Btns.Visibility = Visibility.Collapsed;

            lb_Edit_Book.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Visible;

            FillInputFields(ss);

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;

            btn_Uredi_Izdanja.Visibility = Visibility.Visible;
        }
        private void SetSelectView()
        {
            Novine.SelectedItem = null;
            view_edit.Visibility = Visibility.Collapsed;
            view_select.Visibility = Visibility.Visible;
        }
        public void RefreshLists()
        {
            FillNewsList();
            FillFormati();
            FillJeziciList();
            FillIKList();
            FillPeriodi();
        }


        private void FillNewsList()
        {
            Novine.Items.Clear();
            foreach (SerijskoStivo ss in DBHelper.GetAllNews())
            {
                List<Jezik> jezici = DBHelper.GetAllSStivoJeziks(ss);
                List<IzdKuca> iks = DBHelper.GetAllSStivoIzdKucas(ss);

                Novine.Items.Add(new ViewSStivo(ss.IDSStivo, ss.Naziv, _tipSStiva, ss.Format, ss.Period, jezici, iks));
            }
        }
        private void FillFormati()
        {
            cb_Format.Items.Clear();
            foreach (Format f in DBHelper.GetAllFormats())
            {
                cb_Format.Items.Add(f);
            }
        }
        private void FillPeriodi()
        {
            cb_Period.Items.Clear();
            foreach (Periodicnost p in DBHelper.GetAllPeriodSorted())
            {
                cb_Period.Items.Add(p);
            }
        }
        private void FillJeziciList()
        {
            lbx_Jezici.Items.Clear();
            // lbx_Jezici.Items.Add(new Jezik("0000", "+ Dodaj nov jezik"));
            foreach (Jezik j in DBHelper.GetAllJeziks())
            {
                lbx_Jezici.Items.Add(j);
            }
        }
        private void FillIKList()
        {
            lbx_IzdKuce.Items.Clear();
            foreach (IzdKuca ik in DBHelper.GetAllIzdKucas())
            {
                lbx_IzdKuce.Items.Add(ik);
            }
        }
        private void ClearInputFields()
        {
            tb_Name.Text = "";
            cb_AddHere.IsChecked = false;
            tb_Kolicina.Text = "";

            InitCbFormati();
            InitCbPeriod();
            InitLbxJezici();
            InitLbxIzdKuce();
        }
        private void FillInputFields(SerijskoStivo ss)
        {
            tb_Name.Text = ss.Naziv;

            InitCbFormati(ss);
            InitCbPeriod(ss);
            InitLbxJezici(ss);
            InitLbxIzdKuce(ss);
        }
        private void FillInputFields(ViewSStivo vk)
        {
            FillInputFields(vk as SerijskoStivo);
        }

        private void InitCbFormati(SerijskoStivo ss = null)
        {
            if (ss == null) { cb_Format.SelectedIndex = 0; return; }
            string nf = ss.Format;
            foreach (Format f in cb_Format.Items)
            {
                if (f.NazivFormata == nf)
                {
                    cb_Format.SelectedItem = f;
                    break;
                }
            }
        }
        private void InitCbPeriod(SerijskoStivo ss = null)
        {
            if (ss == null) { cb_Period.SelectedIndex = 0; return; }
            string np = ss.Period;
            foreach (Periodicnost p in cb_Period.Items)
            {
                if (p.PeriodIzd == np)
                {
                    cb_Period.SelectedItem = p;
                    break;
                }
            }
        }

        private void InitLbxJezici(SerijskoStivo ss = null)
        {
            lbx_Jezici.SelectedItems.Clear();
            if (ss == null) { return; }
            List<Jezik> jeziks = new List<Jezik>();
            List<string> oznjs = new List<string>();
            foreach (SStivoNaJeziku ssnj in DBHelper.GetAllSStivoNaJezikuWithSS(ss))
            {
                jeziks.Add(DBHelper.GetJezik(ssnj.OZNJ));
                oznjs.Add(ssnj.OZNJ);
            }

            for (int i = 0; i < lbx_Jezici.Items.Count; i++)
            {
                if (oznjs.Contains((lbx_Jezici.Items[i] as Jezik).OZNJ))
                {
                    (lbx_Jezici as ListBox).SelectedItems.Add(lbx_Jezici.Items[i]);
                }
            }
        }

        private void InitLbxIzdKuce(SerijskoStivo ss = null)
        {
            lbx_IzdKuce.SelectedItems.Clear();
            if (ss == null) { return; }
            List<IzdKuca> izdKuce = new List<IzdKuca>();
            List<int> idiks = new List<int>();
            foreach (IzdajeSStivo izss in DBHelper.GetAllIzdajeSStivoWithSS(ss))
            {
                izdKuce.Add(DBHelper.GetIzdKuca(izss.IDIK));
                idiks.Add(izss.IDIK);
            }

            for (int i = 0; i < lbx_IzdKuce.Items.Count; i++)
            {
                if (idiks.Contains((lbx_IzdKuce.Items[i] as IzdKuca).IDIK))
                {
                    (lbx_IzdKuce as ListBox).SelectedItems.Add(lbx_IzdKuce.Items[i]);
                }
            }
        }
        #endregion

        #region BUTTONS
        private bool ValidateInputFields()
        {
            return Validator.Naziv(tb_Name.Text);
        }
        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            SerijskoStivo toAdd = new SerijskoStivo(tb_Name.Text, _tipSStiva, cb_Format.Text, cb_Period.Text);
            List<Jezik> jToAdd = new List<Jezik>();
            List<IzdKuca> iToAdd = new List<IzdKuca>();
            foreach (var j in lbx_Jezici.SelectedItems)
            {
                jToAdd.Add(j as Jezik);
            }
            foreach (var i in lbx_IzdKuce.SelectedItems)
            {
                iToAdd.Add(i as IzdKuca);
            }

            if (DBHelper.AddSStivo(toAdd, jToAdd, iToAdd) != 0)
            {
                MessageBox.Show("Uspešno ste dodali nove novine!");
                if (_quitAfterSave)
                {
                    _caller?.RefreshLists();
                    Close();
                }
                else
                {
                    RefreshLists();
                    SetSelectView();
                }
            }
            else
            {
                MessageBox.Show("Došlo je do greške pri dodavanju novina!");
            }
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (_quitAfterSave)
            {
                Close();
            }
            else
            {
                ClearInputFields();
                SetSelectView();
            }
        }
        #endregion
        #region EDIT
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            SerijskoStivo toEdit = new SerijskoStivo(_stivoToEdit);
            toEdit.Naziv = tb_Name.Text;
            toEdit.Format = cb_Format.Text;
            toEdit.Period = cb_Period.Text;


            List<IzdKuca> iToEdit = new List<IzdKuca>();
            foreach (var a in lbx_IzdKuce.SelectedItems)
            {
                iToEdit.Add(a as IzdKuca);
            }
            List<Jezik> jToEdit = new List<Jezik>();
            foreach (var a in lbx_Jezici.SelectedItems)
            {
                jToEdit.Add(a as Jezik);
            }

            if (!DBHelper.UpdateSStivo(toEdit, iToEdit, jToEdit))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste izmenili podatke o novinama!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteSStivo(_stivoToEdit, _currentUser))
            {
                MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali podatke o novinama!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelectView();
        }
        #endregion
        #endregion



        private void btn_Add_News_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            SetAddView();
        }
        private void Novine_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewSStivo sel = ((ListView)sender).SelectedValue as ViewSStivo;
            if (sel != null)
            {
                FillInputFields(sel);
                SetEditView(DBHelper.GetSerijskoStivo(sel.IDSStivo));
            }
        }

        private void cb_AddHere_Click(object sender, RoutedEventArgs e)
        {
            lb_Kolicina.IsEnabled = (bool)cb_AddHere.IsChecked;
            tb_Kolicina.IsEnabled = (bool)cb_AddHere.IsChecked;
        }


        private void btn_Add_Jezik_Click(object sender, RoutedEventArgs e)
        {
            // Not enabled for this user
        }

        private void btn_Add_IK_Click(object sender, RoutedEventArgs e)
        {
            Window addIKWin = new BibIzdKucaWindow(_currentUser, toAdd: true, caller: this); addIKWin.ShowDialog();
        }
        private void btn_Uredi_Izdanja_Click(object sender, RoutedEventArgs e)
        {
            Window izdWindow = new BibIzdNewsWindow(_currentUser, _stivoToEdit);
            izdWindow.ShowDialog();
        }


        #region SORTING

        private List<ViewSStivo> GetAllNovFromList()
        {
            List<ViewSStivo> ret = new List<ViewSStivo>();
            foreach (var izm in Novine.Items)
            {
                ret.Add(izm as ViewSStivo);
            }
            return ret;
        }
        private void SortNovText(string propName, bool ascending)
        {
            List<ViewSStivo> sorted = Sorter.SortText<ViewSStivo>(GetAllNovFromList(), propName, ascending);
            Novine.Items.Clear();
            foreach (ViewSStivo izd in sorted)
            {
                Novine.Items.Add(izd);
            }
        }
        private void SortNovInt(string propName, bool ascending)
        {
            List<ViewSStivo> sorted = Sorter.SortInt<ViewSStivo>(GetAllNovFromList(), propName, ascending);
            Novine.Items.Clear();
            foreach (ViewSStivo izd in sorted)
            {
                Novine.Items.Add(izd);
            }
        }
        private bool s_naz = false;
        private void btn_sort_naziv_Click(object sender, RoutedEventArgs e)
        {
            s_naz = !s_naz;
            SetArrow(img_sort_naziv, s_naz);
            SortNovText("Naziv", s_naz);
        }
        private bool s_jez = false;
        private void btn_sort_jezici_Click(object sender, RoutedEventArgs e)
        {
            s_jez = !s_jez;
            SetArrow(img_sort_jezici, s_jez);
            SortNovText("ListJezici", s_jez);
        }
        private bool s_for = false;
        private void btn_sort_format_Click(object sender, RoutedEventArgs e)
        {
            s_for = !s_for;
            SetArrow(img_sort_format, s_for);
            SortNovText("Format", s_for);
        }
        private bool s_ik = false;
        private void btn_sort_izdKuce_Click(object sender, RoutedEventArgs e)
        {
            s_ik = !s_ik;
            SetArrow(img_sort_izdKuce, s_ik);
            SortNovText("ListIzdKuce", s_ik);
        }
        private bool s_per = false;
        private void btn_sort_period_Click(object sender, RoutedEventArgs e)
        {
            s_per = !s_per;
            SetArrow(img_sort_period, s_per);
            SortNovInt("SortPeriod", s_per);
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
        #region FILTERING
        private void ApplyFilters()
        {
            List<ViewSStivo> toShow = new List<ViewSStivo>();
            List<ViewSStivo> inList = new List<ViewSStivo>();

            foreach (SerijskoStivo ss in DBHelper.GetAllMagazines())
            {
                List<Jezik> jezici = DBHelper.GetAllSStivoJeziks(ss);
                List<IzdKuca> iks = DBHelper.GetAllSStivoIzdKucas(ss);

                inList.Add(new ViewSStivo(ss.IDSStivo, ss.Naziv, _tipSStiva, ss.Format, ss.Period, jezici, iks));
            }

            foreach (ViewSStivo ss in inList)
            {
                if (ss.Naziv.ToLower().Contains(tb_f_naz.Text.ToLower())
                && ss.ListJezici.ToLower().Contains(tb_f_jez.Text.ToLower())
                && ss.Format.ToLower().Contains(tb_f_for.Text.ToLower())
                && ss.ListIzdKuce.ToLower().Contains(tb_f_ik.Text.ToLower())
                && ss.Period.ToLower().Contains(tb_f_per.Text.ToLower()))
                    toShow.Add(ss);
            }
            Novine.Items.Clear();
            foreach (ViewSStivo ss in toShow) Novine.Items.Add(ss);
        }
        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void btn_cl_filter_Click(object sender, RoutedEventArgs e)
        {
            tb_f_naz.Text = "";
            tb_f_jez.Text = "";
            tb_f_ik.Text = "";
            tb_f_for.Text = "";
            tb_f_per.Text = "";
            ApplyFilters();
        }
        #endregion
    }
}
