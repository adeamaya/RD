using Newtonsoft.Json;
using SOYDIT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOYDIT.ViewModels
{
    class AcercaDeViewModel :BaseViewModel
    {
		#region Properties
		private Acerca _acerca;

		public Acerca Acerca
		{
			get { return _acerca; }
			set { SetProperty(ref _acerca, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public AcercaDeViewModel()
		{
			GetAcercaDe();
		}

		public void GetAcercaDe()
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
					string parametros = "{\"tipoConsulta\": \"AcercaDe\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("AcercaDe", "0");
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdateAcercaDe();
						new_C.AddKeyCache("AcercaDe", Acerca.JsonAcercaDe);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}

					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdateAcercaDe();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("AcercaDe", Acerca.JsonAcercaDe);
					}

					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;						
						Acerca = JsonConvert.DeserializeObject<Acerca>(old_C.JsonString);
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


		public void UpdateAcercaDe()
		{
			string parametros = "{\"tipoConsulta\": \"AcercaDe\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Acerca = JsonConvert.DeserializeObject<Acerca>(resp);
			Acerca.JsonAcercaDe = resp;
		}

	}
}
