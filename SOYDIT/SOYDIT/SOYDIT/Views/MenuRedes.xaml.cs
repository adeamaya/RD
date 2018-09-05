using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SOYDIT.Models;

namespace SOYDIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuRedes : MasterDetailPage
    {
        public MenuRedes()
        {
            InitializeComponent();
			Detail = new NavigationPage(new EdicionDetalle("UltimaEdicion"));
			List<MiMenu> menu = new List<MiMenu>
			{
				new MiMenu{ Id=1, pagina  = new Regionales(),TituloMenu = "Compartir con Gmail", MenuDetalle="Gmail", icono="Gmail.png"},
				new MiMenu{ Id=2, pagina  = new Destacados(),TituloMenu = "FaceBook", MenuDetalle="FaceBook", icono="FaceBook.png"},
				new MiMenu{ Id=3, pagina  = new Ediciones(),TituloMenu = "WhatsApp", MenuDetalle="WhatsApp", icono="WhatsApp.png"},
				new MiMenu{ Id=4, pagina  = new Favoritos(),TituloMenu = "Add to Yammer", MenuDetalle="Yammer", icono="Yammer.png"},
				new MiMenu{ Id=5, pagina  = new Editorial(),TituloMenu = "Messenger", MenuDetalle="Messenger", icono="Messenger.png"}
			};
			ListMenu.ItemSelected += ListView_ItemSelected;
			ListMenu.ItemsSource = menu;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as MiMenu;
			if (menu != null)
			{
				IsPresented = false;
				//Detail = new NavigationPage(menu.pagina);
				CompartirRedes compartir = new CompartirRedes();
				compartir.CompartirAPP();
			}
			ListMenu.SelectedItem = null;
		}
    }
}