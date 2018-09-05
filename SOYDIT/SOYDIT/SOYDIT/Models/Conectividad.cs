using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace SOYDIT.Models
{
    class Conectividad
    {

		public bool IsConnected()
		{
			if (!CrossConnectivity.IsSupported)
				return true;

			//Do this only if you need to and aren't listening to any other events as they will not fire.
			var connectivity = CrossConnectivity.Current;

			try
			{
				return connectivity.IsConnected;
			}
			finally
			{
				CrossConnectivity.Dispose();
			}

		}
	}
}
