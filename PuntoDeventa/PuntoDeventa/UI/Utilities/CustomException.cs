﻿using System;

namespace PuntoDeventa.UI.Utilities
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; }

        public CustomException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
