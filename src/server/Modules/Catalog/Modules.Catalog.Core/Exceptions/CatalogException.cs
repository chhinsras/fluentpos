using FluentPOS.Shared.Core.Exceptions;

namespace FluentPOS.Modules.Catalog.Core.Exceptions
{
    public class CatalogException : CustomException
    {
        public CatalogException(string message) : base(message)
        {
        }
    }
}