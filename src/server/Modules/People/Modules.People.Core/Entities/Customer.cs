using System.Collections.Generic;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }

        public virtual ICollection<CustomerExtendedAttribute> ExtendedAttributes { get; set; }

        public Customer() : base()
        {
            ExtendedAttributes = new HashSet<CustomerExtendedAttribute>();
        }
    }
}