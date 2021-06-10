using FluentPOS.Shared.Abstractions.Exceptions;

namespace FluentPOS.Modules.Catalog.Core.Exceptions
{
    public class CatalogException : CustomException
    {
        public CatalogException(string message) : base(typeof(CatalogException).Name + " : " + message)
        {
        }
    }
}
