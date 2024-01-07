using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeVenta.Maui.Core.Network
{
    public interface IDataStore
    {

        Task<HttpResponseMessage> GetAsync<T>(Uri url);
        Task<HttpResponseMessage> PostAsync<T>(T dto, Uri url);
        Task<HttpResponseMessage> PutAsync<T>(T dto, Uri url);

        Task<HttpResponseMessage> PatchAsync<T>(T dto, Uri url);

        Task<HttpResponseMessage> DeleteAsync<T>(Uri url);

        Task<HttpResponseMessage> GetAsync<T>(string url);
        Task<HttpResponseMessage> PostAsync<T>(T dto, string url);
        Task<HttpResponseMessage> PutAsync<T>(T dto, string url);

        Task<HttpResponseMessage> PatchAsync<T>(T dto, string url);

        Task<HttpResponseMessage> DeleteAsync<T>(string url);
    }
}
