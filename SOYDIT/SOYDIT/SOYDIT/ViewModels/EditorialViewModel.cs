using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SOYDIT.Models;
using Newtonsoft.Json;
using System.IO;

namespace SOYDIT.ViewModels
{
    class EditorialViewModel : BaseViewModel
    {
		#region Commands
		public INavigation Navigation { get; set; }
		#endregion

		#region Properties
		private Articulo _articulo = new Articulo();

		public Articulo Articulo
		{
			get { return _articulo; }
			set { SetProperty(ref _articulo, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public EditorialViewModel()
		{
			GetArticulo();
		}


		public void GetArticulo()
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
					string parametros = "{\"tipoConsulta\": \"Editorial\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Editorial", "0");
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateEditorial();						
						new_C.AddKeyCache("Editorial", Articulo.JsonArticulo);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}

					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateEditorial();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Editorial", Articulo.JsonArticulo);
					}

					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;						
						Articulo = JsonConvert.DeserializeObject<Articulo>(old_C.JsonString);
						Articulo.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Articulo.ImagenBlob)));
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

		public void UpdateEditorial()
		{
			string parametros = "{\"tipoConsulta\": \"Editorial\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);			
			Articulo = JsonConvert.DeserializeObject<Articulo>(resp);
			Articulo.JsonArticulo = resp;
			Articulo.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Articulo.ImagenBlob)));
		}

	}
}
