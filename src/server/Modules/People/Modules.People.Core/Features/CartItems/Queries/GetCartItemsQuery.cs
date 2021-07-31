using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    public class GetCartItemsQuery : IRequest<PaginatedResult<GetCartItemsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string[] OrderBy { get; set; }
        public Guid? CartId { get; }

        public GetCartItemsQuery(PaginatedCartItemFilter request)
        {
            PageNumber = request.PageNumber;
            PageSize = 50;
            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                OrderBy = request.OrderBy.Split(',');
            }
            CartId = request.CartId;
        }
    }
}