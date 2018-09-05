using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOYDIT.Models 
{
	class MiMenu
	{
		public int Id
		{
			get;
			set;
		}

		public string TituloMenu 
		{
			get;
			set;
		}

		public string MenuDetalle
		{
			get;
			set;
		}

		public ImageSource icono
		{
			get;
			set;
		}

		public Page pagina
		{
			get;
			set;
		}
	}
}
