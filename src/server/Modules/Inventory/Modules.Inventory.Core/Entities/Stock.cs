using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class Stock : BaseEntity
    {
        public Stock()
        {
        }

        public Stock(Guid productId)
        {
            this.ProductId = productId;
            this.LastUpdatedOn = DateTime.Now;
        }

        public Guid ProductId { get; private set; }
        public decimal AvailableQuantity { get; private set; }
        public DateTime LastUpdatedOn { get; private set; }
        public void IncreaseQuantity(decimal quantity)
        {
            this.AvailableQuantity = this.AvailableQuantity + quantity;
            this.LastUpdatedOn = DateTime.Now;
        }
        public void ReduceQuantity(decimal quantity)
        {
            this.AvailableQuantity = this.AvailableQuantity - quantity;
            this.LastUpdatedOn = DateTime.Now;
        }

    }
}