using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.Abstractions.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public CustomException(string message, List<string> errors = default) : base(message)
        {
            this.ErrorMessages = errors;
        }
    }
}