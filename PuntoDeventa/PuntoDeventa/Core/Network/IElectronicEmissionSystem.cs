using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    public interface IElectronicEmissionSystem
    {
        Task<HttpResponseMessage> GetAsync(string ApiKey, Uri url);
    }
}
