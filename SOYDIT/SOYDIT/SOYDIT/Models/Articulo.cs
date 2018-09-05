using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SOYDIT.Models
{
    class Articulo
    {
		public int IdArticulo { get; set; }
		public int IdEdicion { get; set; }
		public int IdRegional { get; set; }
		public int Visualizaciones { get; set; }
		public string Tipo { get; set; }
		public string Titulo { get; set; }
		public string ImagenBlob { get; set; }
		public ImageSource ImagenSourc { get; set; }
		public string Texto { get; set; }
		public bool Favorito { get; set; }
		public DateTime Fecha { get; set; }
		public int Version { get; set; }
		public string tipoConsulta { get; set; }
		public string JsonArticulo { get; set; }
	}
}
