using Newtonsoft.Json;
using PuntoDeventa.Core.LocalData.Preferences;
using PuntoDeventa.Data.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Core.Network
{
    internal class ElectronicEmissionSystem : IElectronicEmissionSystem
    {
        private static HttpClient _httpClient;
        private readonly EcommerceDTO _ecommerceData;

        public ElectronicEmissionSystem()
        {
            var preferences = DependencyService.Get<IDataPreferences>();
            _ecommerceData = preferences.GetEcommerceData();
            _httpClient = new HttpClient();


        }
        public ElectronicEmissionSystem(HttpClient client, IDataPreferences preferences)
        {
            _httpClient = client;
            _ecommerceData = preferences.GetEcommerceData();

        }
        public async Task<HttpResponseMessage> GetAsync(string apiKey, Uri url)
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", apiKey);

            return await _httpClient.GetAsync(url);
        }
        public async Task<HttpResponseMessage> GetAsync(Uri url)
        {
            AddHeader();
            return await _httpClient.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync<T>(T model, Uri url)
        {
            AddHeader();
            var body = JsonConvert.SerializeObject(model);

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            return _httpClient.PostAsync(url, content);

        }

        private void AddHeader()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("apikey")) return;
            _httpClient.DefaultRequestHeaders.Add("apikey", _ecommerceData.ApiKey);

        }
    }
}
