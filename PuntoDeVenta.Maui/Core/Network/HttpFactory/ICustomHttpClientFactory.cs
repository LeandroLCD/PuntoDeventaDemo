using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVenta.Maui.Core.Network.HttpFactory
{
    public interface ICustomHttpClientFactory
    {
        HttpClient CreateClient(string name);

        HttpClient CreateClient(string name, Uri baseUrl, Dictionary<string, string> headers = null);
    }

}
