using FluentPOS.Shared.Abstractions.Exceptions;
using System.Collections.Generic;

namespace FluentPOS.Modules.Identity.Core.Exceptions
{
    public class IdentityException : CustomException
    {
        public IdentityException(string message) : base(message)
        {
        }
        public IdentityException(string message, List<string> errors) : base(message)
        {
            base.ErrorMessages = errors;
        }
    }
}