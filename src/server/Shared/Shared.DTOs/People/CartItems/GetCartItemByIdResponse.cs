using System;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public record GetCartItemByIdResponse(Guid Id, Guid CartId, Guid ProductId, int Quantity);
}