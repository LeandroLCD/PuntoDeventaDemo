using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    internal class DataStore : IDataStore
    {
        readonly HttpClient _httpClient = new HttpClient();

        public async Task<HttpResponseMessage> GetAsync<T>(Uri url)
        {

            return await _httpClient.GetAsync(url);

        }

        public async Task<HttpResponseMessage> PostAsync<T>(T DTO, Uri url)
        {

            var Settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string body = JsonConvert.SerializeObject(DTO, Settings);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, content);

        }

        public async Task<HttpResponseMessage> PutAsync<T>(T DTO, Uri url)
        {
            var Settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string body = JsonConvert.SerializeObject(DTO, Settings);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync(url, content);

        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(Uri url)
        {

            return await _httpClient.DeleteAsync(url);

        }
    }
}
