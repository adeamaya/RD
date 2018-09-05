using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SOYDIT.Models;
using SOYDIT.Views;
using System.Windows.Input;
using SOYDIT.Helpers;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SOYDIT.ViewModels
{
    class EdicionDetalleViewModel : BaseViewModel
	{
		#region Commands
		public INavigation Navigation { get; set; }
		//public ICommand LoginCommand { get; set; }
		#endregion

		#region Properties
		private Edicion _edicion = new Edicion();

		public Edicion Edicion
		{
			get { return _edicion; }
			set { SetProperty(ref _edicion, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public EdicionDetalleViewModel(string consulta,string idEdicion ="0")
		{
			GetEdicion(consulta,idEdicion);			
		}

		public void GetEdicion(string tipo, string IdEdicion="0")
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
					string parametros = "{\"tipoConsulta\": \"" + tipo + "\",\"Clave\": \"" + IdEdicion + "\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);			
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache(tipo, IdEdicion);
					/***Si no existe el objeto en cache**/
					if (old_C == null)
					{						
						UpdateEdicion(tipo, IdEdicion);
						new_C.AddKeyCache(tipo, Edicion.JsonEdicion);
						new_C.AddKeyCache("ArticulosEdicion", Edicion.JsonListArticulos);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}
					/***Si existe caché pero la version o el id del objeto es diferente**/
					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave )
					{						
						UpdateEdicion(tipo, IdEdicion);
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;
						new_C.AddKeyCache(tipo, Edicion.JsonEdicion);
						new_C.AddKeyCache("ArticulosEdicion", Edicion.JsonListArticulos);
					}
					/***Si hay caché y no ha cambiado la version ni el id del objeto, traer datos de cache**/
					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;
						Edicion = JsonConvert.DeserializeObject<Edicion>(old_C.JsonString);
						Edicion.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Edicion.ImagenBlob)));
						old_C = old_C.GetKeyCache("ArticulosEdicion", IdEdicion);
						List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(old_C.JsonString);
						foreach (Articulo art in listaArticulos)
						{
							art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
						}
						Edicion.ListArticulos = listaArticulos;
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


		private void UpdateEdicion(string tipo,string IdEdicion)
		{
			/************************Traer datos actualizados del servicio**********************************************/
					ManageCache new_C = new ManageCache();
			Servicio conServicio = new Servicio();
			string parametros = "{\"tipoConsulta\": \"" + tipo + "\",\"IdEdicion\": \"" + IdEdicion + "\"}";
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Edicion = JsonConvert.DeserializeObject<Edicion>(resp);
			Edicion.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Edicion.ImagenBlob)));
			Edicion.JsonEdicion = resp;
			parametros = "{\"tipoConsulta\": \"ArticulosEdicion\",\"IdEdicion\": \"" + Edicion.IdEdicion + "\"}";
			string resp2 = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Edicion.JsonListArticulos = resp2;
			List<Articulo> listaArticulos = JsonConvert.DeserializeObject<List<Articulo>>(resp2);
			foreach (Articulo art in listaArticulos)
			{
				art.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(art.ImagenBlob)));
			}
			Edicion.ListArticulos = listaArticulos;

		}
	}
}