using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public class PaginatedCartFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid? CustomerId { get; set; }
    }
}