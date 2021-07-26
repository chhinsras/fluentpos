using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}