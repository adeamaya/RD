using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SOYDIT.Models
{
    class Revista
    {
		public int IdRevista { get; set; }
		public string Nombre { get; set; }		
		public string LogoBlob { get; set; }
		public ImageSource LogoSourc { get; set; }		
		public int Version { get; set; }
		public string WebInstitucional { get; set; }
		public List<Edicion> ListEdiciones { get; set; }
		public string JsonRevista { get; set; }
		public string JsonListEdiciones { get; set; }


		public string GetWebInstitucional()
		{
			string URL = string.Empty;
			Revista revista = new Revista();
			try
			{
				string parametros = "{\"tipoConsulta\": \"Revista\",\"IdRevista\": \"0\"}";
				Servicio conServicio = new Servicio();
				string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
				revista = JsonConvert.DeserializeObject<Revista>(resp);
			}
			catch (Exception e)
			{
				App.Current.MainPage.DisplayAlert("Error", e.Message, "Ok");
			}
			return revista.WebInstitucional;
		}
	}
}
