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
	public partial class Ediciones : ContentPage
	{
		public Ediciones ()
		{
			InitializeComponent();
		}




		async private void Button_Clicked(object sender, EventArgs a)
		{
			try
			{
				Button button = (Button)sender;
				string ID = button.CommandParameter.ToString();
				var edicion = a.GetType();
				if (edicion != null)
				{			
					var detailPage = new EdicionDetalle("Edicion",ID);			
					await Navigation.PushAsync(detailPage);
				}
			
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