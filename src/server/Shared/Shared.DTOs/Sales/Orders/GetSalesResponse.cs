using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record GetSalesResponse(Guid Id, string ReferenceNumber, DateTime TimeStamp, Guid CustomerId, string CustomerName, string CustomerPhone,
                                string CustomerEmail, decimal SubTotal, decimal Tax, decimal Discount, decimal Total, bool IsPaid, string Note);
}