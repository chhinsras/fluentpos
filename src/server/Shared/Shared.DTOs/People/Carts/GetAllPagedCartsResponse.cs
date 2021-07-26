using System;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetAllPagedCartsResponse(Guid Id, Guid CustomerId, DateTime Timestamp);
}