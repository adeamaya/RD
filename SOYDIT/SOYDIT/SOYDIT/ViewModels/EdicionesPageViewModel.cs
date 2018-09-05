using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using SOYDIT.Helpers;
using SOYDIT.Models;
using SOYDIT.Views;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace SOYDIT.ViewModels
{
	class EdicionesPageViewModel : BaseViewModel
	{
		public INavigation Navigation { get; set; }
		public ICommand LogoutCommand { get; set; }
		//public ICommand GetEdicionCommand { get; private set; }//{ get; set; }
		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		private Revista _revista = new Revista();

		public Revista Revista
		{
			get { return _revista; }
			set { SetProperty(ref _revista, value); }
		}


		public EdicionesPageViewModel()
		{		
			GetEdiciones();			
		}	

		public void GetEdiciones()
		{
			IsBusy = true;
			Title = string.Empty;
			Conectividad conInternet = new Conectividad();
			Message = "";
			ManageCache new_C = new ManageCache();
			ManageCache old_C = new ManageCache();
			try
			{
				if (!conInternet.IsConnected())
				{
					Message = "Es necesario una conexión de internet para hacer uso de esta aplicación";
				}
				else
				{
					/************************Comparar version caché Vs. Versión en Drupal****************************************/
					Servicio conServicio = new Servicio();
					string parametros = "{\"tipoConsulta\": \"Ediciones\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Ediciones", "0");					
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateEdiciones();						
						new_C.AddKeyCache("Ediciones", Revista.JsonListEdiciones);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}
				
					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateEdiciones();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Ediciones", Revista.JsonListEdiciones);
					}
				
					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;
						
						List<Edicion> listaEdiciones = JsonConvert.DeserializeObject<List<Edicion>>(old_C.JsonString);
						foreach (Edicion edi in listaEdiciones)
						{
							edi.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(edi.ImagenBlob)));
						}
						Revista.ListEdiciones = listaEdiciones;
					}
					IsBusy = false;
				}
			}
			catch (Exception e)
			{
				IsBusy = false;
				App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
			}
		}

		public void UpdateEdiciones()
		{
			string parametros = "{\"tipoConsulta\": \"Revista\",\"IdRevista\": \"0\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);

			Revista = JsonConvert.DeserializeObject<Revista>(resp);
			parametros = "{\"tipoConsulta\": \"Ediciones\",\"IdRevista\": \"0\"}";
			string resp2 = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Revista.JsonListEdiciones = resp2;
			List<Edicion> listaEdiciones = JsonConvert.DeserializeObject<List<Edicion>>(resp2);
			foreach (Edicion edi in listaEdiciones)
			{
				edi.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(edi.ImagenBlob)));
			}
			Revista.ListEdiciones = listaEdiciones;
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