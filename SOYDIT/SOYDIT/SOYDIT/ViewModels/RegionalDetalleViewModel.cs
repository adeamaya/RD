using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SOYDIT.Models;
using System.IO;
using Newtonsoft.Json;

namespace SOYDIT.ViewModels
{
    class RegionalDetalleViewModel :BaseViewModel
    {

		#region Commands
		public INavigation Navigation { get; set; }
		#endregion

		#region Properties
		private Regional _regional = new Regional();

		public Regional Regional
		{
			get { return _regional; }
			set { SetProperty(ref _regional, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public RegionalDetalleViewModel(string idRegional)
		{
			GetRegional(idRegional);
		}

		public void GetRegional(string idRegional)
		{
			IsBusy = true;
			Title = string.Empty;
			Conectividad conInternet = new Conectividad();
			ManageCache new_C = new ManageCache();
			ManageCache old_C = new ManageCache();
			Message = "";
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
					string parametros = "{\"tipoConsulta\": \"Regional\",\"Clave\": \"" + idRegional + "\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);				
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Regional", idRegional);
				
					if (old_C == null)
					{
						UpdateRegional(idRegional);
						new_C.AddKeyCache("Regional", Regional.JsonRegional);
						new_C.AddKeyCache("ArticulosRegional", Regional.JsonListArticulos);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}
					
					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateRegional(idRegional);
					//	Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;
						new_C.AddKeyCache("Regional", Regional.JsonRegional);
						new_C.AddKeyCache("ArticulosRegional", Regional.JsonListArticulos);
					}
				
					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;
						Regional = JsonConvert.DeserializeObject<Regional>(old_C.JsonString);
						Regional.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Regional.ImagenBlob)));
						old_C = old_C.GetKeyCache("ArticulosRegional", idRegional);
						List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(old_C.JsonString);
						foreach (Articulo art in listaArticulos)
						{
							art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
						}
						Regional.ListArticulos = listaArticulos;
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


		private void UpdateRegional(string idRegional)
		{
			string parametros = "{\"tipoConsulta\": \"Regional\",\"IdRegional\": \"" + idRegional + "\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Regional = JsonConvert.DeserializeObject<Regional>(resp);
			Regional.JsonRegional = resp;
			Regional.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Regional.ImagenBlob)));
			parametros = "{\"tipoConsulta\": \"ArticulosRegional\",\"IdRegional\": \"" + Regional.IdRegional + "\"}";
			string resp2 = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Regional.JsonListArticulos = resp2;
			List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(resp2);
			foreach (Articulo art in listaArticulos)
			{
				art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
			}
			Regional.ListArticulos = listaArticulos;
		}
	}
}
