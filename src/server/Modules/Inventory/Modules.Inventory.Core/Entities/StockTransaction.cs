using System;
using FluentPOS.Modules.Inventory.Core.Enums;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Inventory.Core.Entities
{
    public class StockTransaction : BaseEntity
    {
        public Guid ProductId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public decimal Quantity{ get; set; }
        public TransactionType Type{ get; set; }
    }
}