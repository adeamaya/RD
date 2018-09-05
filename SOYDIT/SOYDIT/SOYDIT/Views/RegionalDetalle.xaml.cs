using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SOYDIT.ViewModels;
using SOYDIT.Models;

namespace SOYDIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegionalDetalle : ContentPage
	{
		public RegionalDetalle (string idRegional)
		{
			InitializeComponent ();
			BindingContext = new RegionalDetalleViewModel(idRegional);
			ListaArticulos.ItemSelected += ListaArticulos_ItemSelected;
		}


		private void ListaArticulos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var articulo = e.SelectedItem as Articulo;
			if (articulo != null)
			{
				Navigation.PushAsync(new ArticuloDetalle(articulo.IdArticulo.ToString()));
			}
		}
	}
}