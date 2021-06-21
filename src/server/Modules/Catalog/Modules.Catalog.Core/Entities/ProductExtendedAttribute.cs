using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class ProductExtendedAttribute : ExtendedAttribute<Guid, Product>
    {
        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}