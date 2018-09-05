using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace SOYDIT.Models
{
    class Regional
    {

		public int IdRegional { get; set; }
		public string Nombre { get; set; }
		public string ImagenBlob { get; set; }
		public ImageSource ImagenSourc { get; set; }
		public int Version { get; set; }
		public List<Articulo> ListArticulos { get; set; }
		public string JsonRegional { get; set; }
		public string JsonListArticulos { get; set; }
	}
}
