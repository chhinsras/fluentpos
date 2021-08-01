using System;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    public class GetAllPagedCartsQuery : IRequest<PaginatedResult<GetAllPagedCartsResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string[] OrderBy { get; private set; }
        public string SearchString { get; private set; }
        public Guid? CustomerId { get; private set; }
    }
}