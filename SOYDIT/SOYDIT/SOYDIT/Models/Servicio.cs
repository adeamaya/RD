using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace SOYDIT.Models
{
	class Servicio
	{
		public const string c_urlDrupalCache = "http://localhost/pruebaServicio/cache.php"; //Servicio consulta ultima versión 
		public const string c_urlDrupal= "http://localhost/pruebaServicio/data.php";//servicio consulta datos actualizados
		public const string c_urlDA = "http://localhost/pruebaServicio/autenticacion.php";//Servicio autenticacion usuario

		public string urlDA
		{
			get
			{
				return c_urlDA;
			}
		}

		public string urlDrupal
		{
			get
			{
				return c_urlDrupal;
			}
		}


		public string urlDrupalCache
		{
			get
			{
				return c_urlDrupalCache;
			}
		}


		public string ConnService(string URL, string parametros)
		{
			HttpWebRequest request;
			request = WebRequest.Create(URL) as HttpWebRequest;
			request.Timeout = 10 * 1000;
			request.Method = "POST";
			//request.ContentLength = data.Length;
			request.ContentType = "application/json; charset=utf-8";

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(parametros);
				streamWriter.Flush();
			}

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			Stream stream = response.GetResponseStream();
			StreamReader reader = new StreamReader(stream);
			string textResult = reader.ReadToEnd();

			return textResult;
		}


    }
}
