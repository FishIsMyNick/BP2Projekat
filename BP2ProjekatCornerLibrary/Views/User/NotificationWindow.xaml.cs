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

namespace BP2ProjekatCornerLibrary.Views.User
{
	/// <summary>
	/// Interaction logic for NotificationsWindow.xaml
	/// </summary>
	public partial class NotificationWindow : Window
	{
		public int ID { get; set; }

		public List<Notifikacija> Notifikacije;
		public NotificationWindow(UserMainWindow userMainWindow)
		{
			//ID = userMainWindow.CurrentUser.Id;

			InitializeComponent();

			Notifikacije = GetNotifications();

			FillNotificationList(Notifikacije);
		}

		private void FillNotificationList(List<Notifikacija> notifikacije)
		{
			NotificationList.Items.Clear();
			if (notifikacije.Count == 0)
			{
				NotificationList.Items.Add(new Notifikacija());
			}
			else
			{
				foreach (Notifikacija n in notifikacije)
				{
					NotificationList.Items.Add(n);
				}
			}
		}

		private List<Notifikacija> GetNotifications()
		{
			//List<Ispunjenzahtevzaknjigu> izk = DBHelper.GetFulfilledBookRequests(ID);
			//List<Ispunjenzahtevzanovine> izn = DBHelper.GetFulfilledNewsRequests(ID);
			//List<Ispunjenzahtevzamagazin> izm = DBHelper.GetFulfilledMagazineRequests(ID);

			//int id = 0;
			//List<Notifikacija> ret = new List<Notifikacija>();
			//foreach (Ispunjenzahtevzaknjigu z in izk)
			//{
			//	Knjigaulokalu kul = DBHelper.GetBookInStore(z.Izknjiga, z.Izlokal);
			//	if (kul != null)
			//	{
			//		ret.Add(new Notifikacija(id, kul));
			//		id++;
			//	}
			//}
			//foreach (Ispunjenzahtevzanovine z in izn)
			//{
			//	Novineulokalu nul = DBHelper.GetNewsInStore(z.Iznovine, z.Izlokal);
			//	if (nul != null)
			//	{
			//		ret.Add(new Notifikacija(id, nul));
			//		id++;
			//	}
			//}
			//foreach (Ispunjenzahtevzamagazin z in izm)
			//{
			//	Magazinulokalu mul = DBHelper.GetMagazineInStore(z.Izmagazin, z.Izlokal);
			//	if (mul != null)
			//	{
			//		ret.Add(new Notifikacija(id, mul));
			//		id++;
			//	}
			//}
			//return ret;
			return null;
		}

		private void btnBack_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var item = ((FrameworkElement)e.OriginalSource).DataContext as Notifikacija;

			if (item != null && item.ID != -1)
			{
				Notifikacije.Remove(item);
				//switch (item.Tip)
				//{
				//	case TipKnjige.Knjiga:
				//		DBHelper.DeleteFulfilledBookRequest(ID, item.KnjigaID, item.LokalID);
				//		break;
				//	case TipKnjige.Novine:
				//		DBHelper.DeleteFulfilledNewsRequest(ID, item.KnjigaID, item.LokalID);
				//		break;
				//	case TipKnjige.Magazin:
				//		DBHelper.DeleteFulfilledMagazineRequest(ID, item.KnjigaID, item.LokalID);
				//		break;
				//}

				FillNotificationList(Notifikacije);
			}
		}
		//private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e) { }
	}
	public class Notifikacija
	{
		public int ID { get; set; } = 0;
		public string Text { get; set; }
		public string Location { get; set; }
		public TipKnjige Tip { get; set; }
		public int KnjigaID { get; set; }
		public int LokalID { get; set; }

		public Notifikacija()
		{
			ID = -1;
			Text = "Nemate notifikacija...";
			Location = "";
			Tip = TipKnjige.Knjiga;
		}
		public Notifikacija(int id, string text, string location, int knjigaID = -1, int lokalID = -1)
		{
			ID = id;
			Text = text;
			Location = location;
			Tip = TipKnjige.NA;
			KnjigaID = knjigaID;
			LokalID = lokalID;
		}
		//public Notifikacija(int id, Knjigaulokalu kul)
		//{
		//	ID = id;
		//	Knjiga k = DBHelper.GetBook(kul.Idk);
		//	Cornerlibrary l = DBHelper.GetStore(kul.Idl);

		//	Text = $"Stigla je knjiga koju ste tražili: \"{k.Naziv}\" od {k.Autor}.";
		//	Location = $"{l.Adresa}, {l.Grad}, {l.Drzava}";
		//	Tip = TipKnjige.Knjiga;
		//	KnjigaID = kul.Idk;
		//	LokalID = kul.Idl;
		//}
		//public Notifikacija(int id, Novineulokalu nul)
		//{
		//	ID = id;
		//	Novine n = DBHelper.GetNews(nul.Idn);
		//	Cornerlibrary l = DBHelper.GetStore(nul.Idl);

		//	Text = $"Stigle su novine koje ste tražili: \"{n.NazivNov}\".";
		//	Location = $"{l.Adresa}, {l.Grad}, {l.Drzava}";
		//	Tip = TipKnjige.Novine;
		//	KnjigaID = nul.Idn;
		//	LokalID = nul.Idl;
		//}
		//public Notifikacija(int id, Magazinulokalu mul)
		//{
		//	ID = id;
		//	Magazin m = DBHelper.GetMagazin(mul.Idm);
		//	Cornerlibrary l = DBHelper.GetStore(mul.Idl);

		//	Text = $"Stigao je magazin koji ste tražili: \"{m.NazivMag}\".";
		//	Location = $"{l.Adresa}, {l.Grad}, {l.Drzava}";
		//	Tip = TipKnjige.Magazin;
		//	KnjigaID = mul.Idm;
		//	LokalID = mul.Idl;
		//}
	}
	public enum TipKnjige
	{
		Knjiga,
		Novine,
		Magazin,
		NA
	}
}
