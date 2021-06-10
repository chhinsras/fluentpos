using System;

namespace FluentPOS.Shared.Abstractions.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
