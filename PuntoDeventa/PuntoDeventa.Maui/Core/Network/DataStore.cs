using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    internal class DataStore : IDataStore
    {
        private static HttpClient _httpClient;

        public DataStore()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync<T>(Uri url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(T DTO, Uri url)
        {
            return await _httpClient.PostAsync(url, FactoryContent(DTO));
        }

        public async Task<HttpResponseMessage> PutAsync<T>(T DTO, Uri url)
        {
            return await _httpClient.PutAsync(url, FactoryContent(DTO));

        }

        public async Task<HttpResponseMessage> PatchAsync<T>(T DTO, Uri url)
        {
            return await _httpClient.PatchAsync(url, FactoryContent(DTO));
        }

        private protected StringContent FactoryContent<T>(T dto)
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var body = JsonConvert.SerializeObject(dto, settings);
            return new StringContent(body, Encoding.UTF8, "application/json");

        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(Uri url)
        {
            return await _httpClient.DeleteAsync(url);
        }
    }
}
