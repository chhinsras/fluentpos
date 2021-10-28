using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record ProductResponse
    (
        Guid OrderId,
        Guid ProductId,
        int Quantity,
        string Category,
        string Brand,
        decimal Price,
        decimal Tax,
        decimal Discount,
        decimal Total
    );
}