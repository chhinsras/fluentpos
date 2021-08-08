using System;
using System.Collections.Generic;
using FluentPOS.Shared.DTOs.People.CartItems;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartByIdResponse(Guid Id, Guid CustomerId, DateTime Timestamp)
    {
         public ICollection<GetCartItemByIdResponse> CartItems { get; set; }
    }
}
