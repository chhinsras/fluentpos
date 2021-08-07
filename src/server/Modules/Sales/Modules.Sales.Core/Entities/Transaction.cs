using System;
using FluentPOS.Modules.Sales.Core.Enums;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Sales.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid OrderId { get; set; }
        public PaymentType PaymentType { get; set; }
        public DateTime TimeStamp { get; private set; }
        public decimal Amount { get; set; }
        public decimal TenderedAmount { get; set; }
        public string Note { get; set; }

    }
}