using System;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartByCustomerIdResponse(Guid Id, Guid CustomerId, DateTime Timestamp);
}