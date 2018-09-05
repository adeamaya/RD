using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SOYDIT.Models;
using Newtonsoft.Json;
using System.IO;
using DLToolkit.Forms.Controls;


namespace SOYDIT.ViewModels
{
    class DestacadosViewModel : BaseViewModel
    {
		#region Commands
		public INavigation Navigation { get; set; }
		//public ICommand LoginCommand { get; set; }
		#endregion

		#region Properties
		public List<Articulo> ListArticulos { get; set; }
		public string JsonListArticulos { get; set; }

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		/*public FlowObservableCollection<Articulo> ListArticulos
				{
					get { return GetField<FlowObservableCollection<Articulo>>(); }
					set { SetField(value); }
				}*/



		public DestacadosViewModel()
		{
			GetDestacados();
		}


		public void GetDestacados()
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
					string parametros = "{\"tipoConsulta\": \"Destacados\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Destacados", "0");
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateDestacados();
						new_C.AddKeyCache("Destacados", JsonListArticulos);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}

					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateDestacados();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Destacados", JsonListArticulos);
					}

					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;

						ListArticulos = JsonConvert.DeserializeObject<List<Articulo>>(old_C.JsonString);
						if (ListArticulos.Count > 0)
						{
							foreach (Articulo art in ListArticulos)
							{
								art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
							}
						}
						else { Message = "No hay artículos destacados"; }
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



		public void UpdateDestacados()
		{
			string parametros = "{\"tipoConsulta\": \"Destacados\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			JsonListArticulos = resp;
			ListArticulos = JsonConvert.DeserializeObject<List<Articulo>>(resp);
			if (ListArticulos.Count > 0)
			{
				foreach (Articulo art in ListArticulos)
				{
					art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
				}
			}
			else { Message = "No hay artículos destacados"; }
		}
	}
}
