﻿using System.Collections.Generic;

namespace PuntoDeventa.Domain.Models
{
    internal class ResultType<T>
    {
        public ResultType()
        {
            Errors = new List<ErrorMessage>();
        }
        public bool Success { get; set; }

        public List<ErrorMessage> Errors { get; set; }

        public T Data { get; set; }
    }
}
