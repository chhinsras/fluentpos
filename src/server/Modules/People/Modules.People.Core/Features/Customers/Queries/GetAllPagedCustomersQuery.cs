using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetAllPagedCustomersQuery : IRequest<PaginatedResult<GetAllPagedCustomersResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string[] OrderBy { get; private set; }
        public string SearchString { get; private set; }
    }
}