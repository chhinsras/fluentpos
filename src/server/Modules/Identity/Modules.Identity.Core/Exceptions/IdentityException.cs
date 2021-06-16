using FluentPOS.Shared.Core.Exceptions;
using System.Collections.Generic;

namespace FluentPOS.Modules.Identity.Core.Exceptions
{
    public class IdentityException : CustomException
    {
        public IdentityException(string message, List<string> errors = default) : base(message, errors)
        {
        }
    }
}