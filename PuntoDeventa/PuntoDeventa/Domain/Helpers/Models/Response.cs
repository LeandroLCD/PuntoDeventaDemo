using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeventa.Domain.Helpers.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }
    }
}
