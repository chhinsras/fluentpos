using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public class PaginatedCartItemFilter : PaginatedFilter
    {
        public Guid? CartId { get; set; }
    }
}