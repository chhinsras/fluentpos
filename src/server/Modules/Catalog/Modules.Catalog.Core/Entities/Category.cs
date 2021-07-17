using FluentPOS.Shared.Core.Domain;
using System.Collections.Generic;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }

        public virtual ICollection<CategoryExtendedAttribute> ExtendedAttributes { get; set; }

        public Category() : base()
        {
            ExtendedAttributes = new HashSet<CategoryExtendedAttribute>();
        }
    }
}