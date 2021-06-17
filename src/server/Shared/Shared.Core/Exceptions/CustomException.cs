using System;
using System.Collections.Generic;
using System.Net;

namespace FluentPOS.Shared.Core.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; } = new();
        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            this.ErrorMessages = errors;
            this.StatusCode = statusCode;
        }
    }
}