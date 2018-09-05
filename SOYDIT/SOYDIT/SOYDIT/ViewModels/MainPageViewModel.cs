using System;
using System.Collections.Generic;
using System.Text;
using SOYDIT.Helpers;
using SOYDIT.Views;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SOYDIT.ViewModels
{
	public class MainPageViewModel : BaseViewModel
	{
		public INavigation Navigation { get; set; }
		public ICommand LogoutCommand { get; set; }
		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		public MainPageViewModel()
		{

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
