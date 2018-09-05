using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using SOYDIT.Models;
using Newtonsoft.Json;

namespace SOYDIT.ViewModels
{
    class RegionalesPageViewModel : BaseViewModel
	{
		#region Commands
			public INavigation Navigation { get; set; }
		#endregion

		#region Properties		
		public List<Regional> ListRegionales { get; set; }
		public string JsonListRegionales { get; set; }

		private string _message;

			public string Message
			{
				get { return _message; }
				set { SetProperty(ref _message, value); }
			}
		#endregion

		public RegionalesPageViewModel()
		{
			GetRegionales();
		}

		public void GetRegionales()
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
					string parametros = "{\"tipoConsulta\": \"Regionales\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Regionales", "0");
					
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateRegionales();
						new_C.AddKeyCache("Regionales", JsonListRegionales);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}

					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateRegionales();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Regionales", JsonListRegionales);
					}

					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;						
						ListRegionales = JsonConvert.DeserializeObject<List<Regional>>(old_C.JsonString);
						foreach (Regional reg in ListRegionales)
						{
							reg.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(reg.ImagenBlob)));
						}
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



		private void UpdateRegionales()
		{
			/************************Traer datos actualizados del servicio**********************************************/
			string parametros = "{\"tipoConsulta\": \"Regionales\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			JsonListRegionales = resp;
			ListRegionales = JsonConvert.DeserializeObject<List<Regional>>(resp);
			foreach (Regional reg in ListRegionales)
			{
				reg.ImagenSourc = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(reg.ImagenBlob)));
			}
			
		}

	}
}
