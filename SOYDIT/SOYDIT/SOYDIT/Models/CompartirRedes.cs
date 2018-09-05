using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Share;

namespace SOYDIT.Models
{
    class CompartirRedes
    {
		public string URLUWP { get; set; }
		public string URLiOS { get; set; }
		public string URLAndroid { get; set; }
		public string URLApp { get; set; }
		public string AppCompartir { get; set; }
		public string Text { get; set; }

		public void CompartirAPP()
		{
			GetURLApp();
			CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
			{
				Text = URLApp,
				Title = "Compartiendo APP Revista SOYDIT"
			});
		}


		public void CompartirArticulo(Articulo articulo)
		{
			GetURLApp();
			CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
			{
				Text = articulo.Texto,
				Title = "Compartiendo Articulo Revista SOYDIT - " + articulo.Titulo
			});
		}


		public void GetURLApp()
		{
			string parametros = "{\"tipoConsulta\": \"CompartirAPP\"}";
			Servicio conServicio = new Servicio();
			string resp = conServicio.ConnService(conServicio.urlDrupal, parametros);
			CompartirRedes obj = JsonConvert.DeserializeObject<CompartirRedes>(resp);

			this.URLApp = string.Empty;
			if (Device.RuntimePlatform == Device.iOS)
				this.URLApp = obj.URLiOS;

			if (Device.RuntimePlatform == Device.UWP)
				this.URLApp = obj.URLUWP;

			if (Device.RuntimePlatform == Device.Android)
				this.URLApp = obj.URLAndroid;
		}
	}
}
