using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.Core.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; } = new();

        public CustomException(string message, List<string> errors = default) : base(message)
        {
            this.ErrorMessages = errors;
        }
    }
}