using System;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public record GetAllPagedCartItemsResponse(Guid Id, Guid CartId, Guid ProductId, int Quantity);
}