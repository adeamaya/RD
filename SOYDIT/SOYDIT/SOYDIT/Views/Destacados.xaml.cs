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
	public partial class Destacados : ContentPage
	{
		public Destacados()
		{
			InitializeComponent();
			ListaArticulos.ItemSelected += ListaDestacados_ItemSelected;
		}

		private void ListaDestacados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var articulo = e.SelectedItem as Articulo;
			if (articulo != null)
			{
				Navigation.PushAsync(new ArticuloDetalle(articulo.IdArticulo.ToString()));
			}

		}
	}
}