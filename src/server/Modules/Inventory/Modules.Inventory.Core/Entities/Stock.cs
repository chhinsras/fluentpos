using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class Stock : BaseEntity
    {
        public Guid ProductId { get; set; }
        public decimal AvailableQuantity { get; set; }
        public DateTime LastUpdatedOn { get; set; }

    }
}