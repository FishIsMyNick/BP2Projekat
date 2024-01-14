using BP2ProjekatCornerLibrary.Helpers;
using BP2ProjekatCornerLibrary.Models.NonContext;
using BP2ProjekatCornerLibrary.Models;
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

namespace BP2ProjekatCornerLibrary.Views.Worker.Bibliotekar
{
    /// <summary>
    /// Interaction logic for BibIzdNewsWindow.xaml
    /// </summary>
    public partial class BibIzdNewsWindow : Window, iDynamicListView, iSortedListView
    {
        private bool _testing = Login.Login._testing;
        private int _currentUser;
        private int _lokalID;
        private IzdanjeSStiva _izdStivaToEdit;
        private SerijskoStivo _selectedSStivo;
        public List<Image> Arrows { get; set; }

        private int _tipSStiva = 1;
        public BibIzdNewsWindow(int currentUser, SerijskoStivo ssToEdit, int? izdToEdit = null)
        {
            _selectedSStivo = ssToEdit;
            InitializeComponent();

            Arrows = new List<Image>
            {
                img_sort_brIzd,
                img_sort_cena,
                img_sort_dat,
                img_sort_format,
                img_sort_izdKuce,
                img_sort_jezici,
                img_sort_period
            };
            DisableAllArrows();

            lb_izd_name.Content = ssToEdit.Naziv;

            if (izdToEdit == null)
            {
                SetSelectView();
            }
            else
            {
                _izdStivaToEdit = DBHelper.GetIzdanjeSStiva(ssToEdit.IDSStivo, (int)izdToEdit);
                FillInputFields(_izdStivaToEdit);
                SetEditView(_izdStivaToEdit);
            }

            _currentUser = currentUser;
            RasporedjenBibliotekar rasporedjenBibliotekar = DBHelper.GetLatestRasporedjenBibliotekar(_currentUser);
            _lokalID = rasporedjenBibliotekar.IDBK;


            RefreshLists();
        }

        #region INIT CONTROLS
        private void SetAddView()
        {
            ClearInputFields();
            this.Width = 700;
            tb_BrIzd.IsEnabled = true;
            lb_Name_edit.Content = _selectedSStivo.Naziv;
            lb_Add_Izdanje.Visibility = Visibility.Visible;
            grd_Add_Btns.Visibility = Visibility.Visible;

            lb_Edit_Izdanje.Visibility = Visibility.Collapsed;
            grd_Edit_Btns.Visibility = Visibility.Collapsed;

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;

            tb_BrIzd.Text = (DBHelper.GetLatestIzdanjeSStiva(_selectedSStivo.IDSStivo).BrIzd + 1).ToString();
        }
        private void SetEditView(IzdanjeSStiva ss)
        {
            this.Width = 700;
            tb_BrIzd.IsEnabled = false;
            lb_Name_edit.Content = _selectedSStivo.Naziv;
            _izdStivaToEdit = ss;
            lb_Add_Izdanje.Visibility = Visibility.Collapsed;
            grd_Add_Btns.Visibility = Visibility.Collapsed;

            lb_Edit_Izdanje.Visibility = Visibility.Visible;
            grd_Edit_Btns.Visibility = Visibility.Visible;

            FillInputFields(ss);

            view_edit.Visibility = Visibility.Visible;
            view_select.Visibility = Visibility.Collapsed;
        }
        private void SetSelectView()
        {
            IzdNovina.SelectedItem = null;
            this.Width = 1325;
            view_edit.Visibility = Visibility.Collapsed;
            view_select.Visibility = Visibility.Visible;
        }
        public void RefreshLists()
        {
            FillNewsList();
        }


        private void FillNewsList()
        {
            IzdNovina.Items.Clear();
            foreach (IzdanjeSStiva iss in DBHelper.GetAllIzdanjeSStivaFromSS(_selectedSStivo))
            {
                List<Jezik> jezici = DBHelper.GetAllSStivoJeziks(_selectedSStivo);
                List<IzdKuca> iks = DBHelper.GetAllSStivoIzdKucas(_selectedSStivo);

                IzdNovina.Items.Add(new ViewIzdSStivo(iss.IDSStivo, iss.BrIzd, iss.DatIzd, iss.Cena, _selectedSStivo.Naziv, _tipSStiva, _selectedSStivo.Format, _selectedSStivo.Period, jezici, iks));
            }
        }
        private void ClearInputFields()
        {
            tb_BrIzd.Text = "";
            tb_Dan.Text = "";
            tb_Mesec.Text = "";
            tb_Godina.Text = "";
            tb_Cena.Text = "";
            tb_Kolicina.Text = "";
            SetAddHere(false);
        }
        private void FillInputFields(IzdanjeSStiva ss)
        {
            tb_BrIzd.Text = ss.BrIzd.ToString();
            tb_Dan.Text = ss.DatIzd.Day.ToString();
            tb_Mesec.Text = ss.DatIzd.Month.ToString();
            tb_Godina.Text = ss.DatIzd.Year.ToString();
            tb_Cena.Text = ss.Cena.ToString();
            cb_AddHere.IsChecked = CheckIsInStore();
            SetAddHere(CheckIsInStore());

            if (DBHelper.GetAllIzdSStivoULokalu($"IDSStivo={ss.IDSStivo} and BrIzd={ss.BrIzd} and IDBK={_lokalID}").Count > 0)
            {
                cb_AddHere.IsChecked = true;
                tb_Kolicina.Text = DBHelper.GetLatestIzdSStivoULokalau(ss.IDSStivo, ss.BrIzd, _lokalID).Kolicina.ToString();
            }
        }
        private void FillInputFields(ViewIzdSStivo vk)
        {
            FillInputFields(vk as IzdanjeSStiva);
        }
        private void SetAddHere(bool on)
        {
            tb_Kolicina.IsEnabled = on;
            lb_Kolicina.IsEnabled = on;
            lb_AddHere.IsEnabled = on;
        }
        #endregion

        #region BUTTONS
        private bool ValidateInputFields()
        {
            return Validator.PozNumber(tb_BrIzd.Text)
                && Validator.Date(tb_Dan.Text, tb_Mesec.Text, tb_Godina.Text)
                && Validator.PozDecimal(tb_Cena.Text)
                && cb_AddHere.IsChecked == true ? Validator.PozNumber0(tb_Kolicina.Text) : true;
        }
        #region ADD
        private void btn_Add_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields()) return;

            IzdanjeSStiva toAdd = new IzdanjeSStiva(_selectedSStivo.IDSStivo, int.Parse(tb_BrIzd.Text), new DateTime(int.Parse(tb_Godina.Text), int.Parse(tb_Mesec.Text), int.Parse(tb_Dan.Text)), decimal.Parse(tb_Cena.Text));


            if (DBHelper.CheckIfEditionExists(toAdd.IDSStivo, toAdd.BrIzd))
            {
                MessageBox.Show("Serijsko štivo sa odabranim brojem izdanja već postoji!");
                return;
            }

            if (!DBHelper.AddIzdanjeSStiva(toAdd))
            {
                MessageBox.Show("Došlo je do greške pri dodavanju izdanja serijskog štiva!");
                return;
            }

            if (cb_AddHere.IsChecked == true)
            {
                if (!DBHelper.AddIzdSStivoULokalu(new IzdSStivoULokalu(toAdd.IDSStivo, toAdd.BrIzd, _lokalID, int.Parse(tb_Kolicina.Text))))
                {
                    MessageBox.Show("Došlo je do greške pri dodavanju izdanja serijskog štiva u filijali!");
                    return;
                }
            }

            MessageBox.Show("Uspešno ste dodali novo izdanje!");
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


            IzdanjeSStiva toEdit = new IzdanjeSStiva(_selectedSStivo.IDSStivo, int.Parse(tb_BrIzd.Text), new DateTime(int.Parse(tb_Godina.Text), int.Parse(tb_Mesec.Text), int.Parse(tb_Dan.Text)), decimal.Parse(tb_Cena.Text));


            if (!DBHelper.UpdateIzdSStiva(toEdit))
            {
                MessageBox.Show("Došlo je do greške pri ažuriranju izdanja serijskog štiva!");
                return;
            }

            if (cb_AddHere.IsChecked == true)
            {
                if (CheckIsInStore())
                {
                    IzdSStivoULokalu latest = DBHelper.GetLatestIzdSStivoULokalau(toEdit.IDSStivo, toEdit.BrIzd, _lokalID);
                    if (!CheckIsSameKolicinaInStore())
                    {
                        if (!DBHelper.AddIzdSStivoULokalu(new IzdSStivoULokalu(toEdit.IDSStivo, toEdit.BrIzd, _lokalID, int.Parse(tb_Kolicina.Text))))
                        {
                            MessageBox.Show("Došlo je do greške pri ažuriranju izdanja serijskog štiva u filijali!");
                            return;
                        }
                    }
                }
                else if (!DBHelper.AddIzdSStivoULokalu(new IzdSStivoULokalu(toEdit.IDSStivo, toEdit.BrIzd, _lokalID, int.Parse(tb_Kolicina.Text))))
                {
                    MessageBox.Show("Došlo je do greške pri dodavanju izdanja serijskog štiva u filijali!");
                    return;
                }
            }
            MessageBox.Show("Uspešno ste izmenili izdanje!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!DBHelper.DeleteIzdSStivo(_izdStivaToEdit, _currentUser))
            {
                MessageBox.Show("Došlo je do greške pri brisanju podataka!");
                return;
            }
            MessageBox.Show("Uspešno ste obrisali podatke o izdanju novinama!");
            RefreshLists();
            SetSelectView();
        }

        private void btn_Edit_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            SetSelectView();
        }
        #endregion
        #endregion



        private void btn_Add_News_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
            SetAddView();
        }

        private void cb_AddHere_Click(object sender, RoutedEventArgs e)
        {
            SetAddHere((bool)cb_AddHere.IsChecked);
        }
        private void tb_Kolicina_TextChanged(object sender, TextChangedEventArgs e)
        {
            lb_AlreadyInStore.Visibility = CheckIsSameKolicinaInStore() ? Visibility.Visible : Visibility.Collapsed;
        }
        private IzdSStivoULokalu _checkIzd = null;
        private bool CheckIsSameKolicinaInStore()
        {
            if (_checkIzd == null) _checkIzd = DBHelper.GetLatestIzdSStivoULokalau(_izdStivaToEdit.IDSStivo, _izdStivaToEdit.BrIzd, _lokalID);
            return _checkIzd.Kolicina == (Validator.PozNumberNoCom(tb_Kolicina.Text) ? int.Parse(tb_Kolicina.Text) : -1);
        }
        private bool CheckIsInStore()
        {
            return DBHelper.GetAllIzdSStivoULokalu($"IDSStivo={_izdStivaToEdit.IDSStivo} and BrIzd={_izdStivaToEdit.BrIzd} and IDBK={_lokalID}").Count > 0;
        }
        private void IzdNovina_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewIzdSStivo sel = ((ListView)sender).SelectedValue as ViewIzdSStivo;
            if (sel != null)
            {
                _izdStivaToEdit = DBHelper.GetIzdanjeSStiva(sel.IDSStivo, sel.BrIzd);
                FillInputFields(_izdStivaToEdit);
                SetEditView(_izdStivaToEdit);
            }
        }


        #region SORTING

        private List<ViewIzdSStivo> GetAllIzdNovFromList()
        {
            List<ViewIzdSStivo> ret = new List<ViewIzdSStivo>();
            foreach (var izm in IzdNovina.Items)
            {
                ret.Add(izm as ViewIzdSStivo);
            }
            return ret;
        }
        private void SortIzdText(string propName, bool ascending)
        {
            List<ViewIzdSStivo> sorted = Sorter.SortText<ViewIzdSStivo>(GetAllIzdNovFromList(), propName, ascending);
            IzdNovina.Items.Clear();
            foreach (ViewIzdSStivo izd in sorted)
            {
                IzdNovina.Items.Add(izd);
            }
        }
        private void SortIzdDate(string propName, bool ascending)
        {
            List<ViewIzdSStivo> sorted = Sorter.SortDate<ViewIzdSStivo>(GetAllIzdNovFromList(), propName, ascending);
            IzdNovina.Items.Clear();
            foreach (ViewIzdSStivo izd in sorted)
            {
                IzdNovina.Items.Add(izd);
            }
        }
        private void SortIzdInt(string propName, bool ascending)
        {
            List<ViewIzdSStivo> sorted = Sorter.SortInt<ViewIzdSStivo>(GetAllIzdNovFromList(), propName, ascending);
            IzdNovina.Items.Clear();
            foreach (ViewIzdSStivo izd in sorted)
            {
                IzdNovina.Items.Add(izd);
            }
        }
        private void SortIzdDecimal(string propName, bool ascending)
        {
            List<ViewIzdSStivo> sorted = Sorter.SortDecimal<ViewIzdSStivo>(GetAllIzdNovFromList(), propName, ascending);
            IzdNovina.Items.Clear();
            foreach (ViewIzdSStivo izd in sorted)
            {
                IzdNovina.Items.Add(izd);
            }
        }


        private bool s_brI = false;
        private void btn_sort_brIzd_Click(object sender, RoutedEventArgs e)
        {
            s_brI = !s_brI;
            SetArrow(img_sort_brIzd, s_brI);
            SortIzdInt("BrIzd", s_brI);
        }
        private bool s_dat = false;
        private void btn_sort_datIzd_Click(object sender, RoutedEventArgs e)
        {
            s_dat = !s_dat;
            SetArrow(img_sort_dat, s_dat);
            SortIzdDate("DatIzd", s_dat);
        }
        private bool s_cen = false;
        private void btn_sort_cena_Click(object sender, RoutedEventArgs e)
        {
            s_cen = !s_cen;
            SetArrow(img_sort_cena, s_cen);
            SortIzdDecimal("Cena", s_cen);
        }
        private bool s_jez = false;
        private void btn_sort_jezici_Click(object sender, RoutedEventArgs e)
        {
            s_jez = !s_jez;
            SetArrow(img_sort_jezici, s_jez);
            SortIzdText("ListJezici", s_jez);
        }
        private bool s_for = false;
        private void btn_sort_format_Click(object sender, RoutedEventArgs e)
        {
            s_for = !s_for;
            SetArrow(img_sort_format, s_for);
            SortIzdText("DispFormat", s_for);
        }
        private bool s_ik = false;
        private void btn_sort_izdKuce_Click(object sender, RoutedEventArgs e)
        {
            s_ik = !s_ik;
            SetArrow(img_sort_izdKuce, s_ik);
            SortIzdText("ListIzdKuce", s_ik);
        }
        private bool s_per = false;


        private void btn_sort_period_Click(object sender, RoutedEventArgs e)
        {
            
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

    }
}
