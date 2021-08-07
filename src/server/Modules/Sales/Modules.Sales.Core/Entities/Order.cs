using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Sales.Core.Entities
{
    public class Order : BaseEntity
    {
        public DateTime TimeStamp { get; private set; }
        public Guid CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; private set; }
        public bool IsPaid { get; set; }
        public string Note { get; set; }
    }
}