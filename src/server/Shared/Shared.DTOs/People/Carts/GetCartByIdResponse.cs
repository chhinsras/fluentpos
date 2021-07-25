using System;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartByIdResponse(Guid Id, Guid CustomerId, DateTime Timestamp);
}
