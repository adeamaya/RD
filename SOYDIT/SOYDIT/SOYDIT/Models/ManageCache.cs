using MonkeyCache;
using System;
using System.Collections.Generic;
using System.Text;
using BarrelFile = MonkeyCache.FileStore.Barrel;
//using BarrelFileEd = MonkeyCache.FileStore.Barrel;

namespace SOYDIT.Models
{
    class ManageCache
    {
		public string tipoConsulta { get; set; }
		public string Clave { get; set; }
		public string Version { get; set; }
		public string JsonString { get; set; }
		IBarrel fileVersion = BarrelFile.Current;
		//IBarrel fileEdicion = BarrelFileEd.Current;


		public void AddKeyCache(string tipoConsulta, string Json)
		{
			string clave = tipoConsulta + this.Clave;
			this.JsonString = Json;
			GetCurrent().Add<ManageCache>(clave, this, TimeSpan.FromDays(1));			
		}

		public ManageCache GetKeyCache(string tipoConsulta,string key)
		{
			string clave = tipoConsulta + key;			
			var cache = GetCurrent().Get<ManageCache>(clave);
			return cache;
		}

		

		public void DeleteCacheVersion()
		{
			GetCurrent().EmptyAll();
		}


		private IBarrel GetCurrent()
		{
			IBarrel current = null;
			//if(cur=="Version") 
			current = fileVersion;

			//if (cur == "Edicion") current = fileEdicion;
			return current;
		}


	}
}