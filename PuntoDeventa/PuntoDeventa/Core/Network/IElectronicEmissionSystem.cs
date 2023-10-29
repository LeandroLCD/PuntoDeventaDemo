﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    public interface IElectronicEmissionSystem
    {
        Task<HttpResponseMessage> GetAsync(string ApiKey, Uri url);
        Task<HttpResponseMessage> PostAsync<T>(T model, string ApiKey, Uri url);
    }
}
