using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.Customers
{
    public class PaginatedCustomerFilter : PaginatedFilter
    {
        public string SearchString { get; set; }
    }
}