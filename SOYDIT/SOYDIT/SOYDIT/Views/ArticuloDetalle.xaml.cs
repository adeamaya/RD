using SOYDIT.Models;
using SOYDIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOYDIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticuloDetalle : ContentPage
	{
		public ArticuloDetalle (string idArticulo)
		{
			InitializeComponent ();
			BindingContext = new ArticuloDetallePageViewModel(idArticulo);
		}
	}
}