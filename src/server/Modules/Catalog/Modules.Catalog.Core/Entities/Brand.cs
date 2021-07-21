using FluentPOS.Shared.Core.Domain;
using System.Collections.Generic;
using FluentPOS.Modules.Catalog.Core.Entities.ExtendedAttributes;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }

        public virtual ICollection<BrandExtendedAttribute> ExtendedAttributes { get; set; }

        public Brand() : base()
        {
            ExtendedAttributes = new HashSet<BrandExtendedAttribute>();
        }
    }
}