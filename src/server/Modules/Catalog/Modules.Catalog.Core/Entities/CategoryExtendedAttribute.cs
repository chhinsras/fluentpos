using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class CategoryExtendedAttribute : ExtendedAttribute<Guid, Category>
    {
        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}