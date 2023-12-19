﻿using BP2ProjekatCornerLibrary.Helpers;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BP2ProjekatCornerLibrary.Views.Worker
{
    /// <summary>
    /// Interaction logic for BibAddBookWindow.xaml
    /// </summary>
    public partial class BibBookWindow : Window, iDynamicListView
    {
        private bool _testing = Login.Login._testing;
        iDbResult result;
        private int _currentUser;
        private int _lokalID;

        private List<Autor> _selectedAutors = new List<Autor>();
        private List<Jezik> _selectedJeziks = new List<Jezik>();
        private List<Zanr> _selectedZanrs = new List<Zanr>();
        private List<IzdKuca> _selectedIKs = new List<IzdKuca>();
        public BibBookWindow(int currentUser, Knjiga toEdit = null)
        {


            InitializeComponent();

            if (toEdit == null)
            {
                SetAddView();
            }
            else
            {
                SetEditView();
            }


            if (_testing)
            {
                _lokalID = 1;
                MakeMockLists();
                return;
            }

            this._currentUser = currentUser;
            RasporedjenBibliotekar rasporedjenBibliotekar = DBHelper.GetLatestRasporedjenBibliotekar(_currentUser);
            _lokalID = rasporedjenBibliotekar.IDBK;


            RefreshLists();
            if (toEdit != null)
            {
                FillInputFields(toEdit);
            }
        }


        #region INIT CONTROLS
        private void SetAddView()
        {
            lb_Add_Book.Visibility = Visibility.Visible;
            grd_Add_Btns.Visibility = Visibility.Visible;
            lb_AddHere.Visibility = Visibility.Visible;
            sp_AddHere.Visibility = Visibility.Visible;

            lb_Edit_Book.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;
        }
        private void SetEditView()
        {
            lb_Add_Book.Visibility = Visibility.Collapsed;
            grd_Add_Btns.Visibility = Visibility.Collapsed;
            lb_AddHere.Visibility = Visibility.Collapsed;
            sp_AddHere.Visibility = Visibility.Collapsed;

            lb_Edit_Book.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Visible;
        }

        private void MakeMockLists()
        {
            Knjige.Items.Clear();
            Knjige.Items.Add(new ViewKnjiga(1,
                "Neki dugi naziv knjige", 44,
                new List<Autor>()
                {
                    new Autor(1, "Stevan kuzmanović"),
                    new Autor(2, "Sophie Deloraint")
                },
                new List<Jezik>()
                {
                    new Jezik("SRB", "Srpski"),
                    new Jezik("FRA", "Francuski")
                },
                new List<Zanr>()
                {
                    new Zanr("TRIL", "Triler"),
                    new Zanr("SIFI", "Naučna fantastika")
                },
                new List<IzdKuca>()
                {
                    new IzdKuca(1, "Pingvin"),
                    new IzdKuca(2, "Matica Srpska")
                },
                null, "XIV vek p.n.e.",
                244, 14, 1, 1, "A4"));

            cb_Format.Items.Clear();
            cb_Format.Items.Add(new Format("A4"));
            lbx_Autori.Items.Clear();
            lbx_Autori.Items.Add(new Autor(-1, "+ Dodaj novog autora")); lbx_Jezici.Items.Clear();
            lbx_Jezici.Items.Add(new Jezik("0000", "+ Dodaj nov jezik")); lbx_Zanrovi.Items.Clear();
            lbx_Zanrovi.Items.Add(new Zanr("0000", "+ Dodaj nov žanr")); lbx_IzdKuce.Items.Clear();
            lbx_IzdKuce.Items.Add(new IzdKuca(-1, "+ Dodaj novu izdavačku kuću"));
        }
        public void RefreshLists()
        {
            if (_testing) { MakeMockLists(); return; }
            FillBookList();
            FillFormati();
            FillAutoriList();
            FillJeziciList();
            FillZanroviList();
            FillIKList();
        }
        private void FillBookList()
        {
            Knjiga k = DBHelper.GetKnjiga(1);
            List<Autor> autori = DBHelper.GetAllBookAuthors(k);
            List<Jezik> jezici = DBHelper.GetAllBookJeziks(k);
            List<Zanr> zanrovi = DBHelper.GetAllBookZanrs(k);
            List<IzdKuca> iks = DBHelper.GetAllBookIzdKucas(k);

            Knjige.Items.Clear();
            Knjige.Items.Add(new ViewKnjiga(k.IDKnjiga, k.Naziv, k.BrIzd, autori, jezici, zanrovi, iks, k.GodIzd, k.VrIzd, k.BrStrana, k.VelicinaFonta, k.Korice, k.Ograniceno, k.Format));
        }
        private void FillFormati()
        {
            cb_Format.Items.Clear();
            foreach (Format f in DBHelper.GetAllFormats())
            {
                cb_Format.Items.Add(f);
            }
        }
        private void FillAutoriList()
        {
            lbx_Autori.Items.Clear();
            lbx_Autori.Items.Add(new Autor(-1, "+ Dodaj novog autora"));
            foreach (Autor a in DBHelper.GetAllAutors())
            {
                lbx_Autori.Items.Add(a);
            }
        }
        private void FillJeziciList()
        {
            lbx_Jezici.Items.Clear();
            lbx_Jezici.Items.Add(new Jezik("0000", "+ Dodaj nov jezik"));
            foreach (Jezik j in DBHelper.GetAllJeziks())
            {
                lbx_Jezici.Items.Add(j);
            }
        }
        private void FillZanroviList()
        {
            lbx_Zanrovi.Items.Clear();
            lbx_Zanrovi.Items.Add(new Zanr("0000", "+ Dodaj nov žanr"));
            foreach (Zanr z in DBHelper.GetAllZanrs())
            {
                lbx_Zanrovi.Items.Add(z);
            }
        }
        private void FillIKList()
        {
            lbx_IzdKuce.Items.Clear();
            lbx_IzdKuce.Items.Add(new IzdKuca(-1, "+ Dodaj novu izdavačku kuću"));
            foreach (IzdKuca ik in DBHelper.GetAllIzdKucas())
            {
                lbx_IzdKuce.Items.Add(ik);
            }
        }

        private void FillInputFields(Knjiga k)
        {
            tb_Name.Text = k.Naziv;
            tb_BrIzd.Text = k.BrIzd.ToString();
            tb_VrIzd.Text = k.GodIzd != null ? k.GodIzd.ToString() : k.VrIzd;
            tb_BrStr.Text = k.BrStrana.ToString();
            tb_VelFont.Text = k.VelicinaFonta.ToString();
            cb_Korice.Text = k.Korice == 0 ? "Meke" : "Tvrde";
            cb_Ogr.IsChecked = Convert.ToBoolean(k.Ograniceno);

            InitCbFormati(k);
            InitLbxAutori(k);
            InitLbxJezici(k);
            InitLbxZanrovi(k);
            InitLbxIzdKuce(k);
        }
        private void InitCbFormati(Knjiga k)
        {
            string nf = k.Format;
            foreach (Format f in cb_Format.Items)
            {
                if (f.NazivFormata == nf)
                {
                    cb_Format.SelectedItem = f;
                    break;
                }
            }
        }
        private void InitLbxAutori(Knjiga k)
        {
            List<Autor> autors = new List<Autor>();
            List<int> ids = new List<int>();
            foreach (Pise p in DBHelper.GetAllPiseWithBook(k))
            {
                autors.Add(DBHelper.GetAutor(p.IDAutor));
                ids.Add(p.IDAutor);
            }

            for (int i = 1; i < lbx_Autori.Items.Count; i++)
            {
                if (ids.Contains((lbx_Autori.Items[i] as Autor).IDAutor))
                {
                    (lbx_Autori as ListBox).SelectedItems.Add(lbx_Autori.Items[i]);
                    _selectedAutors.Add(lbx_Autori.Items[i] as Autor);
                }
            }
        }
        private void InitLbxJezici(Knjiga k)
        {
            List<Jezik> jeziks = new List<Jezik>();
            List<string> oznjs = new List<string>();
            foreach (KnjigaNaJeziku knj in DBHelper.GetAllKnjigaNaJezikuWithBook(k))
            {
                jeziks.Add(DBHelper.GetJezik(knj.OZNJ));
                oznjs.Add(knj.OZNJ);
            }

            for (int i = 1; i < lbx_Jezici.Items.Count; i++)
            {
                if (oznjs.Contains((lbx_Jezici.Items[i] as Jezik).OZNJ))
                {
                    (lbx_Jezici as ListBox).SelectedItems.Add(lbx_Jezici.Items[i]);
                    _selectedJeziks.Add(lbx_Jezici.Items[i] as Jezik);
                }
            }
        }
        private void InitLbxZanrovi(Knjiga k)
        {
            List<Zanr> zanrovi = new List<Zanr>();
            List<string> oznzs = new List<string>();
            foreach (PripadaZanru pz in DBHelper.GetAllPripadaZanruWithBook(k))
            {
                zanrovi.Add(DBHelper.GetZanr(pz.OZNZ));
                oznzs.Add(pz.OZNZ);
            }

            for (int i = 1; i < lbx_Zanrovi.Items.Count; i++)
            {
                if (oznzs.Contains((lbx_Zanrovi.Items[i] as Zanr).OZNZ))
                {
                    (lbx_Zanrovi as ListBox).SelectedItems.Add(lbx_Zanrovi.Items[i]);
                    _selectedZanrs.Add(lbx_Zanrovi.Items[i] as Zanr);
                }
            }
        }
        private void InitLbxIzdKuce(Knjiga k)
        {
            List<IzdKuca> izdKuce = new List<IzdKuca>();
            List<int> idiks = new List<int>();
            foreach (IzdajeKnjigu izk in DBHelper.GetAllIzdajeKnjiguWithBook(k))
            {
                izdKuce.Add(DBHelper.GetIzdKuca(izk.IDIK));
                idiks.Add(izk.IDIK);
            }

            for (int i = 1; i < lbx_IzdKuce.Items.Count; i++)
            {
                if (idiks.Contains((lbx_IzdKuce.Items[i] as IzdKuca).IDIK))
                {
                    (lbx_IzdKuce as ListBox).SelectedItems.Add(lbx_IzdKuce.Items[i]);
                    _selectedIKs.Add(lbx_IzdKuce.Items[i] as IzdKuca);
                }
            }
        }
        #endregion

        private bool ValidateInputFields()
        {
            return Validator.StringNumber(tb_Name.Text.Trim())
                && Validator.PozNumber(tb_BrIzd.Text.Trim())
                && Validator.StringNumber(tb_VrIzd.Text.Trim())
                && Validator.PozNumber(tb_BrIzd.Text.Trim())
                && Validator.PozNumber(tb_BrStr.Text.Trim())
                && Validator.PozNumber(tb_VelFont.Text.Trim())
                && (cb_AddHere.IsChecked == true ? Validator.PozNumber(tb_Kolicina.Text) : true);
        }
        private void cb_AddHere_Click(object sender, RoutedEventArgs e)
        {
            lb_Kolicina.IsEnabled = (bool)cb_AddHere.IsChecked;
            tb_Kolicina.IsEnabled = (bool)cb_AddHere.IsChecked;
        }


        #region BUTTONS

        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            Knjiga toAdd = new Knjiga(tb_Name.Text, tb_BrIzd.Text, tb_VrIzd.Text, tb_BrStr.Text, tb_VelFont.Text, cb_Korice.Text, cb_Ogr.IsChecked, cb_Format.Text);

            int bookId = 0;
            if ((bookId = DBHelper.AddBookOnly(toAdd)) == 0) return;

            foreach (Jezik j in _selectedJeziks)
            {
                if (!DBHelper.AddKnjigaNaJeziku(new KnjigaNaJeziku(bookId, j.OZNJ))) return;
            }
            foreach (Zanr z in _selectedZanrs)
            {
                if (!DBHelper.AddPripadaZanru(new PripadaZanru(bookId, z.OZNZ))) return;
            }
            foreach (Autor a in _selectedAutors)
            {
                if (!DBHelper.AddPise(new Pise(bookId, a.IDAutor))) return;
            }
            foreach (IzdKuca ik in _selectedIKs)
            {
                if (!DBHelper.AddIzdajeKnjigu(new IzdajeKnjigu(bookId, ik.IDIK))) return;
            }

            if (cb_AddHere.IsChecked == true)
            {
                if (!DBHelper.AddKnjigaULokalu(bookId, _lokalID, int.Parse(tb_Kolicina.Text)))
                {
                    MessageBox.Show("Došlo je do greške pri čuvanju knjige u lokalu!");
                }
            }
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region EDIT
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #endregion
        #region LISTS
        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj is T)
                return obj as T;

            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }
            return null;
        }
        private void lbx_Autori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Tuple<int,Autor> a = ((ListBox)sender).SelectedItem as Tuple<int, Autor>;
            ////List<Autor> ass = ((ListBox)sender).SelectedItems as List<Autor>;

            //var sel = ((ListBox)sender).SelectedValue;
            //if (sel == null) return;

            //int i = ((ListBox)sender).SelectedIndex;
            //if (i < 0) return;

            //ListBoxItem gg = FindDescendant<ListBoxItem>(lbx_Autori.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem);

            //if (gg.IsSelected)
            //{
            //    _selectedAutors.Add(sel as Autor);
            //}
            //else
            //{
            //    _selectedAutors.Remove(sel as Autor);
            //}
            //return;
        }

        private void lbx_Jezici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbx_Zanrovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbx_IzdKuce_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        private void btn_Add_Book_Click(object sender, RoutedEventArgs e)
        {

        }
        #region SORTING

        private void btn_Sort_naziv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Sort_Autori_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_jezici_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_izdKuce_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_korice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_brStr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_zanrovi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_format_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_ograniceno_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_vrIzd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sort_velFonta_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        private void btn_sort_brIzd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Knjige_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}