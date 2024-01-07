using Newtonsoft.Json;
using PuntoDeVenta.Maui.Core.Network.HttpFactory;
using System.Text;

namespace PuntoDeVenta.Maui.Core.Network
{
    internal class DataStore : IDataStore
    {
        private static HttpClient _httpClient { get; set; }

        public DataStore(ICustomHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(
                "dataStore", 
                new Uri(Properties.Resources.BaseUrlRealDataBase));
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

        public async Task<HttpResponseMessage> DeleteAsync<T>(Uri url)
        {
            return await _httpClient.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> GetAsync<T>(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(T DTO, string url)
        {
            return await _httpClient.PostAsync(url, FactoryContent(DTO));
        }

        public async Task<HttpResponseMessage> PutAsync<T>(T DTO, string url)
        {
            return await _httpClient.PutAsync(url, FactoryContent(DTO));

        }

        public async Task<HttpResponseMessage> PatchAsync<T>(T DTO, string url)
        {
            return await _httpClient.PatchAsync(url, FactoryContent(DTO));
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string url)
        {
            return await _httpClient.DeleteAsync(url);
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


    }
}
