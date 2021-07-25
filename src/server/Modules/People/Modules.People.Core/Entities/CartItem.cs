using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}