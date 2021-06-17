using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetAllPagedCustomersQuery : IRequest<PaginatedResult<GetAllPagedCustomersResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }

        public GetAllPagedCustomersQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }
}
