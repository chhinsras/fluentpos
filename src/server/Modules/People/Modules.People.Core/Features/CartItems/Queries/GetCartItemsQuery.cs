using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    public class GetCartItemsQuery : IRequest<PaginatedResult<GetCartItemsResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string[] OrderBy { get; private set; }
        public string SearchString { get; private set; }
        public Guid? CartId { get; private set; }
        public Guid? ProductId { get; private set; }
    }
}