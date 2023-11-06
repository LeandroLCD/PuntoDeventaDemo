using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    public interface IElectronicEmissionSystem
    {
        Task<HttpResponseMessage> GetAsync(string apiKey, Uri url);

        Task<HttpResponseMessage> GetAsync(Uri url);
        Task<HttpResponseMessage> PostAsync<T>(T model, Uri url);
    }
}
