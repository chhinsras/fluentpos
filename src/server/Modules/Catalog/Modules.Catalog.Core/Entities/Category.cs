using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }

        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}