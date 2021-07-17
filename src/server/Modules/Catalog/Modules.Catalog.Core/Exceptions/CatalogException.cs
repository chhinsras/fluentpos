using FluentPOS.Shared.Core.Exceptions;
using System.Net;

namespace FluentPOS.Modules.Catalog.Core.Exceptions
{
    public class CatalogException : CustomException
    {
        public CatalogException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, statusCode: statusCode)
        {
        }
    }
}