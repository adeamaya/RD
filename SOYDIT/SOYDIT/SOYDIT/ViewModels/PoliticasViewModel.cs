using Newtonsoft.Json;
using SOYDIT.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOYDIT.ViewModels
{
    class PoliticasViewModel :BaseViewModel
    {
		#region Properties
		private Politica _politica;

		public Politica Politica
		{
			get { return _politica; }
			set { SetProperty(ref _politica, value); }
		}

		private string _message;

		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		#endregion

		public PoliticasViewModel()
		{
			GetPolitica();
		}

		public void GetPolitica()
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
					string parametros = "{\"tipoConsulta\": \"Politicas\",\"Clave\":\"0\"}";
					string respCache = conServicio.ConnService(conServicio.urlDrupalCache, parametros);
					new_C = JsonConvert.DeserializeObject<ManageCache>(respCache);
					old_C = old_C.GetKeyCache("Politicas", "0");
					//old_C.DeleteCacheVersion();					
					if (old_C == null)
					{
						UpdatePoliticas();
						new_C.AddKeyCache("Politicas", Politica.JsonPolitica);
						//Message = "sin cache: version new:" + new_C.Version + " - version old:-- key new:" + new_C.Clave + " version old:--";
					}

					else if (new_C.Version != old_C.Version || new_C.Clave != old_C.Clave)
					{
						UpdatePoliticas();
						//Message = "sin cache: version new:"+new_C.Version + " - version old:" + old_C.Version + " key new:" + new_C.Clave + " version old:" + old_C.Clave;						
						new_C.AddKeyCache("Politicas", Politica.JsonPolitica);
					}

					else
					{
						//Message = "con cache: version new:" + new_C.Version + " - version old:" + old_C.Version + " clave new:" + new_C.Clave + " clave old:" + old_C.Clave;						
						Politica = JsonConvert.DeserializeObject<Politica>(old_C.JsonString);
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

		public void UpdatePoliticas()
		{
			string parametros = "{\"tipoConsulta\": \"Politicas\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			Politica = JsonConvert.DeserializeObject<Politica>(resp);
			Politica.JsonPolitica = resp;
		}
	}
}
