using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;


namespace SOYDIT.Models
{
	class Edicion
	{
		public int IdRevista { get; set; }
		public string IdEdicion { get; set; }
		public string NumeroEdicion { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public string ImagenBlob { get; set; }
		public ImageSource ImagenSourc { get; set; }
		public DateTime Fecha { get; set; }
		public int Version { get; set; }
		public string JsonEdicion { get; set; }
		public string JsonListArticulos { get; set; }
		public List<Articulo> ListArticulos { get; set; }
	}
}
