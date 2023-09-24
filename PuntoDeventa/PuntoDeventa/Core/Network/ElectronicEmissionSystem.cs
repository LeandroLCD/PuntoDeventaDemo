using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    internal class ElectronicEmissionSystem : IElectronicEmissionSystem
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<HttpResponseMessage> GetAsync(string ApiKey, Uri url)
        {
            if (string.IsNullOrEmpty(ApiKey))
            {
                throw new Exception("No se logro obtener el Api Key del contribuyente.");
            }

            _httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);

            return await _httpClient.GetAsync(url);

        }
    }
}
