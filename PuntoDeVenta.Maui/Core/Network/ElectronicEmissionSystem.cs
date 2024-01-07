using Newtonsoft.Json;
using PuntoDeVenta.Maui.Core.LocalData.Preferences;
using PuntoDeVenta.Maui.Core.Network.HttpFactory;
using PuntoDeVenta.Maui.Data.DTO;
using System.Text;

namespace PuntoDeVenta.Maui.Core.Network
{
    internal class ElectronicEmissionSystem : IElectronicEmissionSystem
    {
        private HttpClient _httpClient;
        private readonly EcommerceDTO _ecommerceData;

        //public ElectronicEmissionSystem()
        //{
        //    var preferences = DependencyService.Get<IDataPreferences>();
        //    _ecommerceData = preferences.GetEcommerceData();
        //    _httpClient = new HttpClient();
        //}
        public ElectronicEmissionSystem(ICustomHttpClientFactory factory, IDataPreferences preferences)
        {
            _ecommerceData = preferences.GetEcommerceData();
            _httpClient = factory.CreateClient("emisionSystem",
                new Uri(Properties.Resources.BaseUrlEelectronicEmission));

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
