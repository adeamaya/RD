using System;
using Xamarin.Forms;
using SOYDIT.Views;
using Xamarin.Forms.Xaml;
using SOYDIT.Helpers;
using DLToolkit.Forms.Controls;
using BarrelFile = MonkeyCache.FileStore.Barrel;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace SOYDIT
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
			BarrelFile.ApplicationId = "com.refractored.SOYDIT";
			FlowListView.Init();
			//Settings.IsLoggedIn = false;
			if (Settings.IsLoggedIn)
			{
				MainPage = new NavigationPage(new MainPage());
			}
			else
			{
				MainPage = new NavigationPage(new LoginPage());
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
