﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuntoDeventa.Core.Network
{
    public interface IDataStore
    {

        Task<HttpResponseMessage> GetAsync<T>(Uri url);
        Task<HttpResponseMessage> PostAsync<T>(T DTO, Uri url);
        Task<HttpResponseMessage> PutAsync<T>(T DTO, Uri url);

        Task<HttpResponseMessage> DeleteAsync<T>(Uri url);
    }
}
