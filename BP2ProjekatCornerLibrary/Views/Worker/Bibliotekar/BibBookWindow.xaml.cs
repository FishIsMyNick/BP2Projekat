using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Helpers.Classes;
using BP2ProjekatCornerLibrary.Models;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    public partial class BibBookWindow : Window, iDynamicListView, iSortedListView
    {
        iDbResult result;
        private int _currentUser;
        private int _lokalID;
        private Knjiga _knjigaToEdit;
        public List<Image> Arrows { get; set; }

        public BibBookWindow(int currentUser, Knjiga toEdit = null)
        {
            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_autori,
                img_sort_brIzd,
                img_sort_brStr,
                img_sort_format,
                img_sort_izdKuce,
                img_sort_jezici,
                img_sort_korice,
                img_sort_naziv,
                img_sort_ograniceno,
                img_sort_velFonta,
                img_sort_vrIzd,
                img_sort_zanrovi
            };
            DisableAllArrows();

            _knjigaToEdit = toEdit;
            if (_knjigaToEdit == null)
            {
                SetSelectView();
            }
            else
            {
                SetEditView(toEdit);
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

            lb_Edit_Book.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
        }
        private void SetEditView(Knjiga k)
        {
            _knjigaToEdit = k;
            lb_Add_Book.Visibility = Visibility.Collapsed;
            grd_Add_Btns.Visibility = Visibility.Collapsed;
            lb_AddHere.Visibility = Visibility.Collapsed;
            sp_AddHere.Visibility = Visibility.Collapsed;

            lb_Edit_Book.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Visible;

            FillInputFields(k);

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
        }
        private void SetSelectView()
        {
            Knjige.SelectedItem = null;
            view_edit.Visibility = Visibility.Collapsed;
            view_select.Visibility = Visibility.Visible;
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
            Knjige.Items.Add(new ViewKnjiga(2,
               "Neki dugi naziv knjige", 44,
               new List<Autor>()
               {
                    new Autor(3, "Branko Radičević "),
                    new Autor(4, "Adolf Dubistenschlos")
               },
               new List<Jezik>()
               {
                    new Jezik("SRB", "Srpski"),
                    new Jezik("DEU", "Nemački")
               },
               new List<Zanr>()
               {
                    new Zanr("COM", "Komedija"),
                    new Zanr("DRAM", "Drama")
               },
               new List<IzdKuca>()
               {
                    new IzdKuca(1, "Pingvin"),
                    new IzdKuca(2, "Matica Srpska")
               },
               2002, null,
               244, 14, 1, 0, "A5"));

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
            FillBookList();
            FillFormati();
            FillAutoriList();
            FillJeziciList();
            FillZanroviList();
            FillIKList();
        }
        private void FillBookList()
        {
            Knjige.Items.Clear();
            foreach (Knjiga k in DBHelper.GetAllKnjigas())
            {
                List<Autor> autori = DBHelper.GetAllBookAuthors(k);
                List<Jezik> jezici = DBHelper.GetAllBookJeziks(k);
                List<Zanr> zanrovi = DBHelper.GetAllBookZanrs(k);
                List<IzdKuca> iks = DBHelper.GetAllBookIzdKucas(k);

                Knjige.Items.Add(new ViewKnjiga(k.IDKnjiga, k.Naziv, k.BrIzd, autori, jezici, zanrovi, iks, k.GodIzd, k.VrIzd, k.BrStrana, k.VelicinaFonta, k.Korice, k.Ograniceno, k.Format));
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
        private void FillAutoriList()
        {
            lbx_Autori.Items.Clear();
            foreach (Autor a in DBHelper.GetAllAutors())
            {
                lbx_Autori.Items.Add(new ViewAutor(a));
            }
        }
        private void FillJeziciList()
        {
            lbx_Jezici.Items.Clear();
            foreach (Jezik j in DBHelper.GetAllJeziks())
            {
                lbx_Jezici.Items.Add(j);
            }
        }
        private void FillZanroviList()
        {
            lbx_Zanrovi.Items.Clear();
            foreach (Zanr z in DBHelper.GetAllZanrs())
            {
                lbx_Zanrovi.Items.Add(z);
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
            tb_BrIzd.Text = "";
            tb_VrIzd.Text = "";
            tb_BrStr.Text = "";
            tb_VelFont.Text = "";
            cb_Korice.Text = "";
            cb_Ogr.IsChecked = false;
            cb_AddHere.IsChecked = false;
            tb_Kolicina.Text = "";

            InitCbFormati();
            InitCbKorice();
            InitLbxAutori();
            InitLbxJezici();
            InitLbxZanrovi();
            InitLbxIzdKuce();
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
            InitCbKorice(k);
            InitLbxAutori(k);
            InitLbxJezici(k);
            InitLbxZanrovi(k);
            InitLbxIzdKuce(k);
        }
        private void FillInputFields(ViewKnjiga vk)
        {
            FillInputFields(vk as Knjiga);
        }
        private void InitCbKorice(Knjiga k = null)
        {
            if (k != null)
            {
                cb_Korice.Text = EnumsHelper.GetKorice(k.Korice);
            }
            else
            {
                cb_Korice.SelectedIndex = 0;
            }
        }
        private void InitCbFormati(Knjiga k = null)
        {
            if (k == null) { cb_Format.SelectedIndex = 0; return; }
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
        private void InitLbxAutori(Knjiga k = null)
        {
            lbx_Autori.SelectedItems.Clear();
            if (k == null) { return; }
            List<Autor> autors = new List<Autor>();
            List<int> ids = new List<int>();
            foreach (Pise p in DBHelper.GetAllPiseWithBook(k))
            {
                autors.Add(DBHelper.GetAutor(p.IDAutor));
                ids.Add(p.IDAutor);
            }

            for (int i = 0; i < lbx_Autori.Items.Count; i++)
            {
                if (ids.Contains((lbx_Autori.Items[i] as Autor).IDAutor))
                {
                    (lbx_Autori as ListBox).SelectedItems.Add(lbx_Autori.Items[i]);
                }
            }
        }
        private void InitLbxJezici(Knjiga k = null)
        {
            lbx_Jezici.SelectedItems.Clear();
            if (k == null) { return; }
            List<Jezik> jeziks = new List<Jezik>();
            List<string> oznjs = new List<string>();
            foreach (KnjigaNaJeziku knj in DBHelper.GetAllKnjigaNaJezikuWithBook(k))
            {
                jeziks.Add(DBHelper.GetJezik(knj.OZNJ));
                oznjs.Add(knj.OZNJ);
            }

            for (int i = 0; i < lbx_Jezici.Items.Count; i++)
            {
                if (oznjs.Contains((lbx_Jezici.Items[i] as Jezik).OZNJ))
                {
                    (lbx_Jezici as ListBox).SelectedItems.Add(lbx_Jezici.Items[i]);
                }
            }
        }
        private void InitLbxZanrovi(Knjiga k = null)
        {
            lbx_Zanrovi.SelectedItems.Clear();
            if (k == null) { return; }
            List<Zanr> zanrovi = new List<Zanr>();
            List<string> oznzs = new List<string>();
            foreach (PripadaZanru pz in DBHelper.GetAllPripadaZanruWithBook(k))
            {
                zanrovi.Add(DBHelper.GetZanr(pz.OZNZ));
                oznzs.Add(pz.OZNZ);
            }

            for (int i = 0; i < lbx_Zanrovi.Items.Count; i++)
            {
                if (oznzs.Contains((lbx_Zanrovi.Items[i] as Zanr).OZNZ))
                {
                    (lbx_Zanrovi as ListBox).SelectedItems.Add(lbx_Zanrovi.Items[i]);
                }
            }
        }
        private void InitLbxIzdKuce(Knjiga k = null)
        {
            lbx_IzdKuce.SelectedItems.Clear();
            if (k == null) { return; }
            List<IzdKuca> izdKuce = new List<IzdKuca>();
            List<int> idiks = new List<int>();
            foreach (IzdajeKnjigu izk in DBHelper.GetAllIzdajeKnjiguWithBook(k))
            {
                izdKuce.Add(DBHelper.GetIzdKuca(izk.IDIK));
                idiks.Add(izk.IDIK);
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

        private bool ValidateInputFields()
        {
            return Validator.Naziv(tb_Name.Text.Trim())
                && Validator.PozNumber(tb_BrIzd.Text.Trim())
                && Validator.VrIzd(tb_VrIzd.Text.Trim())
                && Validator.PozNumber(tb_BrStr.Text.Trim())
                && Validator.PozNumber(tb_VelFont.Text.Trim())
                && (cb_AddHere.IsChecked == true ? Validator.PozNumber0(tb_Kolicina.Text) : true);
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

            foreach (var j in lbx_Jezici.SelectedItems)
            {
                if (!DBHelper.AddKnjigaNaJeziku(new KnjigaNaJeziku(bookId, (j as Jezik).OZNJ))) return;
            }
            foreach (var z in lbx_Zanrovi.SelectedItems)
            {
                if (!DBHelper.AddPripadaZanru(new PripadaZanru(bookId, (z as Zanr).OZNZ))) return;
            }
            foreach (var a in lbx_Autori.SelectedItems)
            {
                if (!DBHelper.AddPise(new Pise(bookId, (a as Autor).IDAutor))) return;
            }
            foreach (var ik in lbx_IzdKuce.SelectedItems)
            {
                if (!DBHelper.AddIzdajeKnjigu(new IzdajeKnjigu(bookId, (ik as IzdKuca).IDIK))) return;
            }

            if (cb_AddHere.IsChecked == true)
            {
                if (!DBHelper.AddKnjigaULokalu(bookId, _lokalID, int.Parse(tb_Kolicina.Text)))
                {
                    MessageBox.Show("Došlo je do greške pri čuvanju knjige u lokalu!");
                    return;
                }
            }

            MessageBox.Show("Uspešno ste dodali novu knjigu!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Add_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            SetSelectView();
        }

        #endregion

        #region EDIT
        private void btn_Edit_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            Knjiga toEdit = new Knjiga(_knjigaToEdit);
            toEdit.Naziv = tb_Name.Text;
            toEdit.BrIzd = int.Parse(tb_BrIzd.Text);
            toEdit.BrStrana = int.Parse(tb_BrStr.Text);
            int godIzd = 0;
            if (int.TryParse(tb_VrIzd.Text, out godIzd))
            {
                toEdit.GodIzd = godIzd;
                toEdit.VrIzd = null;
            }
            else
            {
                toEdit.VrIzd = tb_VrIzd.Text;
                toEdit.GodIzd = null;
            }
            toEdit.VelicinaFonta = int.Parse(tb_VelFont.Text);
            toEdit.Korice = cb_Korice.SelectedIndex;
            toEdit.Ograniceno = cb_Ogr.IsChecked == true ? 1 : 0;
            toEdit.Format = cb_Format.Text;

            List<Autor> aToEdit = new List<Autor>();
            foreach (var a in lbx_Autori.SelectedItems)
            {
                aToEdit.Add(a as Autor);
            }
            List<Zanr> zToEdit = new List<Zanr>();
            foreach (var a in lbx_Zanrovi.SelectedItems)
            {
                zToEdit.Add(a as Zanr);
            }
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

            if (!DBHelper.UpdateKnjiga(toEdit, aToEdit, iToEdit, zToEdit, jToEdit))
            {
                MessageBox.Show("Došlo je do greške pri čuvanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste izmenili podatke o knjizi!");
            RefreshLists();
            SetSelectView();
        }
        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteKnjiga(_knjigaToEdit, _currentUser))
            {
                MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali podatke o knjizi!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            SetSelectView();
        }

        #endregion

        #region COMMON
        private void btn_Add_Autor_Click(object sender, RoutedEventArgs e)
        {
            Window addAutorWin = new BibAutorWindow(_currentUser, toAdd: true, caller: this);
            addAutorWin.ShowDialog();
        }

        private void btn_Add_Jezik_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_Zanr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Add_IK_Click(object sender, RoutedEventArgs e)
        {
            Window addIKWin = new BibIzdKucaWindow(_currentUser, toAdd: true, caller: this); addIKWin.ShowDialog();
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
            //var sel = ((ListBox)sender).SelectedValue;
            //if (sel == null) return;

            //if (((ListBox)sender).SelectedIndex == 0)
            //{
            //    //TODO: AddAutor window
            //lbx_Autori.SelectedItems.RemoveAt(0);
            //}
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
            ClearInputFields();
            SetAddView();
        }


        private void Knjige_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewKnjiga sel = ((ListView)sender).SelectedValue as ViewKnjiga;
            if (sel != null)
            {
                FillInputFields(sel);
                SetEditView(DBHelper.GetKnjiga(sel.IDKnjiga));
            }
        }

        #region SORTING
        public void DisableAllArrows()
        {
            ArrowHelper.DisableAllArrows(Arrows);
        }
        public void SetArrow(Image arrow, bool ascending)
        {
            DisableAllArrows();
            ArrowHelper.SetArrow(arrow, ascending);
        }
        private void SortBooksString(string propName, bool ascending)
        {
            List<ViewKnjiga> toSort = new List<ViewKnjiga>();
            foreach (var k in Knjige.Items) toSort.Add(k as ViewKnjiga);
            Knjige.Items.Clear();
            foreach (ViewKnjiga vk in Sorter.SortText<ViewKnjiga>(toSort, propName, ascending)) Knjige.Items.Add(vk);
        }
        private void SortBooksNumber(string propName, bool ascending)
        {
            List<ViewKnjiga> toSort = new List<ViewKnjiga>();
            foreach (var k in Knjige.Items) toSort.Add(k as ViewKnjiga);
            Knjige.Items.Clear();
            foreach (ViewKnjiga vk in Sorter.SortInt<ViewKnjiga>(toSort, propName, ascending)) Knjige.Items.Add(vk);
        }
        private void SortBooksDate(string propName, bool ascending)
        {
            List<ViewKnjiga> toSort = new List<ViewKnjiga>();
            foreach (var k in Knjige.Items) toSort.Add(k as ViewKnjiga);
            Knjige.Items.Clear();
            foreach (ViewKnjiga vk in Sorter.SortGodina<ViewKnjiga>(toSort, propName, ascending)) Knjige.Items.Add(vk);
        }
        private void SortBooksBool(string propName, bool ascending)
        {
            List<ViewKnjiga> toSort = new List<ViewKnjiga>();
            foreach (var k in Knjige.Items) toSort.Add(k as ViewKnjiga);
            Knjige.Items.Clear();
            foreach (ViewKnjiga vk in Sorter.SortBoolInt<ViewKnjiga>(toSort, propName, ascending)) Knjige.Items.Add(vk);
        }


        private bool s_naz = false;
        private void btn_Sort_naziv_Click(object sender, RoutedEventArgs e)
        {
            s_naz = !s_naz;
            SetArrow(img_sort_naziv, s_naz);
            SortBooksString("Naziv", s_naz);
        }
        private bool s_aut = false;
        private void btn_Sort_Autori_Click(object sender, RoutedEventArgs e)
        {
            s_aut = !s_aut;
            SetArrow(img_sort_autori, s_aut);
            SortBooksString("ListAutori", s_aut);
        }
        private bool s_jez = false;
        private void btn_sort_jezici_Click(object sender, RoutedEventArgs e)
        {
            s_jez = !s_jez;
            SetArrow(img_sort_jezici, s_jez);
            SortBooksString("ListJezici", s_jez);
        }
        private bool s_ik = false;
        private void btn_sort_izdKuce_Click(object sender, RoutedEventArgs e)
        {
            s_ik = !s_ik;
            SetArrow(img_sort_izdKuce, s_ik);
            SortBooksString("ListIzdKuce", s_ik);
        }
        private bool s_kor = false;
        private void btn_sort_korice_Click(object sender, RoutedEventArgs e)
        {
            s_kor = !s_kor;
            SetArrow(img_sort_korice, s_kor);
            SortBooksString("DispKorice", s_kor);
        }
        private bool s_brS = false;
        private void btn_sort_brStr_Click(object sender, RoutedEventArgs e)
        {
            s_brS = !s_brS;
            SetArrow(img_sort_brStr, s_brS);
            SortBooksNumber("BrStrana", s_brS);
        }
        private bool s_zan = false;
        private void btn_sort_zanrovi_Click(object sender, RoutedEventArgs e)
        {
            s_zan = !s_zan;
            SetArrow(img_sort_zanrovi, s_zan);
            SortBooksString("ListZanrovi", s_zan);
        }
        private bool s_for = false;
        private void btn_sort_format_Click(object sender, RoutedEventArgs e)
        {
            s_for = !s_for;
            SetArrow(img_sort_format, s_for);
            SortBooksString("Format", s_for);
        }
        private bool s_ogr = false;
        private void btn_sort_ograniceno_Click(object sender, RoutedEventArgs e)
        {
            s_ogr = !s_ogr;
            SetArrow(img_sort_ograniceno, s_ogr);
            SortBooksBool("Ograniceno", s_ogr);
        }
        private bool s_vrI = false;
        private void btn_sort_vrIzd_Click(object sender, RoutedEventArgs e)
        {
            s_vrI = !s_vrI;
            SetArrow(img_sort_vrIzd, s_vrI);
            SortBooksDate("DispVrIzd", s_vrI);
        }
        private bool s_velF = false;
        private void btn_sort_velFonta_Click(object sender, RoutedEventArgs e)
        {
            s_velF = !s_velF;
            SetArrow(img_sort_velFonta, s_velF);
            SortBooksNumber("VelicinaFonta", s_velF);
        }
        private bool s_brI = false;
        private void btn_sort_brIzd_Click(object sender, RoutedEventArgs e)
        {
            s_brI = !s_brI;
            SetArrow(img_sort_brIzd, s_brI);
            SortBooksNumber("BrIzd", s_brI);
        }

        #endregion
        #region FILTERING

        #endregion
        private void ApplyFilters()
        {
            DisableAllArrows();

            List<ViewKnjiga> toShow = new List<ViewKnjiga>();
            List<ViewKnjiga> inList = new List<ViewKnjiga>();

            foreach (Knjiga k in DBHelper.GetAllKnjigas())
            {
                List<Autor> autori = DBHelper.GetAllBookAuthors(k);
                List<Jezik> jezici = DBHelper.GetAllBookJeziks(k);
                List<Zanr> zanrovi = DBHelper.GetAllBookZanrs(k);
                List<IzdKuca> iks = DBHelper.GetAllBookIzdKucas(k);

                inList.Add(new ViewKnjiga(k.IDKnjiga, k.Naziv, k.BrIzd, autori, jezici, zanrovi, iks, k.GodIzd, k.VrIzd, k.BrStrana, k.VelicinaFonta, k.Korice, k.Ograniceno, k.Format));
            }

            foreach (ViewKnjiga vk in inList)
            {
                if (vk.Naziv.ToLower().Contains(tb_f_naz.Text.ToLower())
                && vk.ListAutori.ToLower().Contains(tb_f_aut.Text.ToLower())
                && vk.BrIzd.ToString().ToLower().Contains(tb_f_brI.Text.ToLower())
                && vk.ListJezici.ToLower().Contains(tb_f_jez.Text.ToLower())
                && vk.ListZanrovi.ToLower().Contains(tb_f_zan.Text.ToLower())
                && vk.ListIzdKuce.ToLower().Contains(tb_f_ik.Text.ToLower())
                && vk.DispVrIzd.ToLower().Contains(tb_f_vrI.Text.ToLower())
                && vk.DispKorice.ToLower().Contains(tb_f_kor.Text.ToLower())
                && vk.BrStrana.ToString().Contains(tb_f_brS.Text.ToLower())
                && vk.VelicinaFonta.ToString().Contains(tb_f_vel.Text.ToLower())
                && vk.Format.ToLower().Contains(tb_f_for.Text.ToLower()))
                    toShow.Add(vk);
            }
            Knjige.Items.Clear();
            foreach (ViewKnjiga vk in toShow) Knjige.Items.Add(vk);
        }
        private void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void btn_clr_filter_Click(object sender, RoutedEventArgs e)
        {
            tb_f_naz.Text = "";
            tb_f_aut.Text = "";
            tb_f_brI.Text = "";
            tb_f_jez.Text = "";
            tb_f_zan.Text = "";
            tb_f_ik.Text = "";
            tb_f_vrI.Text = "";
            tb_f_kor.Text = "";
            tb_f_brS.Text = "";
            tb_f_vel.Text = "";
            tb_f_for.Text = "";
            ApplyFilters();
        }
    }
}
