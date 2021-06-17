using FluentPOS.Shared.DTOs.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.People.Customers
{
    public class PaginatedCustomerFilter : PaginatedFilter
    {
        public string SearchString { get; set; }
    }
}
