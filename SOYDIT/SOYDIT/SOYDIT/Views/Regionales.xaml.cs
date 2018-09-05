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
	public partial class Regionales : ContentPage
	{
		public Regionales ()
		{
			InitializeComponent ();
			ListaRegionales.ItemSelected += ListaRegionales_ItemSelected;
		}

		private void ListaRegionales_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var regional = e.SelectedItem as Regional;
			if (regional != null)
			{
				Navigation.PushAsync(new RegionalDetalle(regional.IdRegional.ToString()));
			}
		}
	}

}