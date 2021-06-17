using FluentPOS.Shared.Core.Exceptions;
using System.Net;

namespace FluentPOS.Modules.People.Core.Exceptions
{
    public class PeopleException : CustomException
    {
        public PeopleException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, statusCode: statusCode)
        {
        }
    }
}