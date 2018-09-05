using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace SOYDIT.Models
{
    class User
    {
		public string id { get; set; }
		//campos enviados al servicio
		public string Email { get; set; }
		public string Password { get; set; }
		//campos consumidos del servicio
		public string valido { get; set; }
		public string mensaje { get; set; }


		public bool ValidarUsuario(string cadena)
		{
			bool resultado = true;
			//caracteres permitidos abcdefghijklmnopqrstuvwxyz1234567890.@_-	
			Regex rgx = new Regex("^([a-z0-9_-]{1}).?([a-z0-9_-]{1,})(@icbf.gov.co)$");
			if (rgx.IsMatch(cadena))
				resultado = true;
			else resultado = false;

			return resultado;			
		}

		public bool ValidarClave(string cadena)
		{
			bool resultado = true;
			Regex rgx = new Regex("^(?=.*\\d)(?=.*[\\u0021-\\u002b\\u003c-\\u0040])(?=.*[A-Z])(?=.*[a-z])\\S{8,16}$");
			if (rgx.IsMatch(cadena))
				resultado = true;
			else resultado = false;

			return resultado;
		}



	}
}
