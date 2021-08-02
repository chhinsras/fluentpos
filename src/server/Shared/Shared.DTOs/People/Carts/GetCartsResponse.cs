using System;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartsResponse(Guid Id, Guid CustomerId, DateTime Timestamp);
}