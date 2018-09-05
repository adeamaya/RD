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
	public partial class Favoritos : ContentPage
	{
		public Favoritos ()
		{
			InitializeComponent ();
			ListaArticulos.ItemSelected += ListaFavoritos_ItemSelected;
		}

		private void ListaFavoritos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var articulo = e.SelectedItem as Articulo;
			if (articulo != null)
			{
				Navigation.PushAsync(new ArticuloDetalle(articulo.IdArticulo.ToString()));
			}
		}
	}
}