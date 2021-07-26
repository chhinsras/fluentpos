using System;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Cart : BaseEntity
    {
        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public DateTime Timestamp { get; private set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
            Timestamp = DateTime.Now;
        }
    }
}