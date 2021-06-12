using System;
using System.Collections.Generic;

namespace FluentPOS.Shared.Abstractions.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public CustomException(string message) : base(message)
        {
        }
        public CustomException(List<string> messages)
        {
            this.ErrorMessages = messages;
        }
    }
}