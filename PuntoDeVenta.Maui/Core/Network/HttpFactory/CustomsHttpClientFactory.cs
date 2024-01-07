namespace PuntoDeVenta.Maui.Core.Network.HttpFactory
{
    using System;
    using System.Collections.Concurrent;
    using System.Net.Http;

    public class CustomHttpClientFactory : ICustomHttpClientFactory
    {
        private ConcurrentDictionary<string, Lazy<HttpClient>> _clients;

        public CustomHttpClientFactory()
        {
            _clients = new ConcurrentDictionary<string, Lazy<HttpClient>>();
        }
        public HttpClient CreateClient(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("El nombre del cliente http no puede ser nulo o vacío", nameof(name));
            }

            return _clients.GetOrAdd(name, CreateLazyHttpClient(name, null, null)).Value;
        }
        public HttpClient CreateClient(string name, Uri baseUrl, Dictionary<string, string> headers = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("El nombre del cliente http no puede ser nulo o vacío", nameof(name));
            }

            return _clients.GetOrAdd(name, CreateLazyHttpClient(name, baseUrl, headers)).Value;
        }


        private Lazy<HttpClient> CreateLazyHttpClient(string name, Uri uri, Dictionary<string, string> headers)
        {
            return new Lazy<HttpClient>(() =>
            {
                var httpClient = new HttpClient();

                if (uri is not null)
                    httpClient.BaseAddress = uri;

                if (headers is not null)
                    foreach (var it in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(it.Key, it.Value);
                    }

                return httpClient;
            });
        }
    }

}
