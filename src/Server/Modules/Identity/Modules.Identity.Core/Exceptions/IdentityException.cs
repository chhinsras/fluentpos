using FluentPOS.Shared.Abstractions.Exceptions;

namespace FluentPOS.Modules.Identity.Core.Exceptions
{
    public class IdentityException : CustomException
    {
        public IdentityException(string message) : base(message)
        {
        }
    }
}
