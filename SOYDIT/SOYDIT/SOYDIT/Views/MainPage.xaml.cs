using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SOYDIT.Models;
using System;
//using BarrelFile = MonkeyCache.FileStore.Barrel;
using SOYDIT.Helpers;

namespace SOYDIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			InitializeComponent ();
			MyMenu();
		}

		public void MyMenu()
		{
			Detail = new NavigationPage(new EdicionDetalle("UltimaEdicion"));
			List<MiMenu> menu = new List<MiMenu>
			{
				new MiMenu{ Id=1, pagina  = new Regionales(),TituloMenu = "Regionales", MenuDetalle="Regionales", icono="regionales.png"},
				new MiMenu{ Id=2, pagina  = new Destacados(),TituloMenu = "Destacados", MenuDetalle="Destacados", icono="destacados.png"},
				new MiMenu{ Id=3, pagina  = new Ediciones(),TituloMenu = "Ediciones", MenuDetalle="Ediciones", icono="ediciones.png"},
				new MiMenu{ Id=4, pagina  = new Favoritos(),TituloMenu = "Mis Noticias Favoritas", MenuDetalle="Favoritos", icono="favoritos.png"},
				new MiMenu{ Id=5, pagina  = new Editorial(),TituloMenu = "Editorial", MenuDetalle="Editorial", icono="editorial.png"},
				new MiMenu{ Id=6, pagina  = new Institucional(),TituloMenu = "Sitio WEB Institucional", MenuDetalle="Web Institucional", icono="institucional.png"},
				new MiMenu{ Id=7, pagina  = new MenuRedes(),TituloMenu = "Comparta esta APP", MenuDetalle="Compartir", icono="compartir.png"},
				new MiMenu{ Id=8, pagina  = new AcercaDe(),TituloMenu = "Acerca de Nosotros", MenuDetalle="Acerca de", icono="acercade.png"},			
				new MiMenu{ Id=9, pagina  = new Politicas(),TituloMenu = "Politicas", MenuDetalle="Politicas", icono="politicas.png"},
				new MiMenu{ Id=10, pagina  = new LoginPage(),TituloMenu = "Cerrar sesión", MenuDetalle="Salir", icono="salir.png"}
			};
			ListMenu.ItemSelected += ListMenu_ItemSelected;
			ListMenu.ItemsSource = menu;
		}

		private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var menu = e.SelectedItem as MiMenu;
			if (menu != null)
			{
				IsPresented = false;
				if (menu.Id == 6)
				{
					Revista rev = new Revista();
					string URL = rev.GetWebInstitucional();
					Device.OpenUri(new Uri(URL));
				}
				else if (menu.Id == 7)
				{
					CompartirRedes compartir = new CompartirRedes();
					compartir.CompartirAPP();
				}
				else if (menu.Id == 10)
				{
					Logout();
				}
				else Detail = new NavigationPage(menu.pagina);
			}
			ListMenu.SelectedItem = null;
		}

		public async void Logout()
		{
			IsBusy = true;
			Title = string.Empty;

			try
			{
				Settings.IsLoggedIn = false;
				//await Task.Delay(4000);
				//await Navigation.PushAsync(new LoginPage());
				App.Current.MainPage = new NavigationPage(new LoginPage());
				IsBusy = false;
			}
			catch (Exception e)
			{
				IsBusy = false;
				await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
			}
		}
	}
}