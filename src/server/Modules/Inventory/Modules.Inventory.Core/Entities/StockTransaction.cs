using System;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class StockTransaction : BaseEntity
    {
        public StockTransaction(Guid productId, decimal quantity, TransactionType type)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Type = type;
        }

        public Guid ProductId { get; private set; }

        public DateTime Timestamp { get; private set; }

        public decimal Quantity { get; private set; }
        
        public TransactionType Type { get; private set; }
    }
}