using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    public class GetAllPagedCartItemsQuery : IRequest<PaginatedResult<GetAllPagedCartItemsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string[] OrderBy { get; set; }
        public string SearchString { get; }
        public Guid? CartId { get; }
        public Guid? ProductId { get; }

        public GetAllPagedCartItemsQuery(int pageNumber, int pageSize, string searchString, string orderBy, Guid? cartId, Guid? productId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
            CartId = cartId;
            ProductId = productId;
        }
    }
}