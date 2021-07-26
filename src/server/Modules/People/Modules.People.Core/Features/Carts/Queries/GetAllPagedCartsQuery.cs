using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    public class GetAllPagedCartsQuery : IRequest<PaginatedResult<GetAllPagedCartsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string[] OrderBy { get; set; }
        public string SearchString { get; }
        public Guid? CustomerId { get; }

        public GetAllPagedCartsQuery(int pageNumber, int pageSize, string searchString, string orderBy, Guid? customerId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
            CustomerId = customerId;
        }
    }
}