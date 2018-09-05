using SOYDIT.Helpers;
using SOYDIT.Models;
using SOYDIT.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace SOYDIT.ViewModels
{
    class LoginPageViewModel : BaseViewModel
    {
		#region Commands
		public INavigation Navigation { get; set; }
		public ICommand LoginCommand { get; set; }
		#endregion

		#region Properties
		private User _user = new User();

		public User User
		{
			get { return _user; }
			set { SetProperty(ref _user, value); }
		}

		private Revista _revista = new Revista();

		public Revista Revista
		{
			get { return _revista; }
			set { SetProperty(ref _revista, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public LoginPageViewModel()
		{
			LoginCommand = new Command(Login);
			GetRevista();
			Conectividad conInternet = new Conectividad(); 
			if (!conInternet.IsConnected())
			{ Message = "Es necesario una conexión de internet para hacer uso de esta aplicación"; }
		}

		public async void Login()
		{
			IsBusy = true;
			Title = string.Empty;
			Conectividad conInternet = new Conectividad();
			Message = "";
			try
			{
				if (!conInternet.IsConnected())
				{
					Message = "Es necesario una conexión de internet para hacer uso de esta aplicación";
				}
				else
				{
					if (User.Email != null)
					{
						if (User.Password != null)
						{
							if (!User.ValidarUsuario(User.Email))
							{
								Message = "El campo no corresponde con el formato válido xxxxx@icbf.gov.co";
							}
							else if (!User.ValidarClave(User.Password))
							{
								Message = "Mínimo 6 caracteres y contar mínimo con: 1 mayúscula, 1 minúscula, 1 número y 1 carácter especial";
							}
							else
							{
								string parametros = JsonConvert.SerializeObject(User);
								Servicio conServicio = new Servicio();
								string resp = conServicio.ConnService(conServicio.urlDA, parametros);
								User = JsonConvert.DeserializeObject<User>(resp);
								if (User.valido == "true")
								{
									Settings.IsLoggedIn = true;
									//await Task.Delay(4000);
									//Message = User.mensaje;//parametros;
									//await Navigation.PushAsync(new MainPage());
									App.Current.MainPage = new NavigationPage(new MainPage());
								}
								else
								{
									Message = User.mensaje;
								}
							}
							IsBusy = false;
						}
						else
						{
							IsBusy = false;
							Message = "Por favor ingrese usuario y clave";
						}
					}
					else
					{
						IsBusy = false;
						Message = "Por favor ingrese usuario y clave";
					}

				}
				

			}
			catch (Exception e)
			{
				IsBusy = false;
				await App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
			}
		}



		public void GetRevista()
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
					string parametros = "{\"tipoConsulta\": \"Revista\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Revista", "0");
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateRevista();
						new_C.AddKeyCache("Revista", Revista.JsonRevista);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}
					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateRevista();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Revista", Revista.JsonRevista);
					}
					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;
						Revista = JsonConvert.DeserializeObject<Revista>(old_C.JsonString);
						Revista.LogoSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Revista.LogoBlob)));
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


		public void UpdateRevista()
		{
			string parametros = "{\"tipoConsulta\": \"Revista\",\"IdRevista\": \"0\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Revista = JsonConvert.DeserializeObject<Revista>(resp);
			Revista.LogoSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Revista.LogoBlob)));
			Revista.JsonRevista = resp;			
		}

	}
}
