using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public class PaginatedCartItemFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid? CartId { get; set; }

        public Guid? ProductId { get; set; }
    }
}