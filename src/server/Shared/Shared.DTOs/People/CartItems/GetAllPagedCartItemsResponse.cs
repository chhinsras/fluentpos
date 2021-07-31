using System;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public record GetCartItemsResponse(Guid Id, Guid CartId, Guid ProductId, int Quantity)
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Rate { get; set; }
    }
}