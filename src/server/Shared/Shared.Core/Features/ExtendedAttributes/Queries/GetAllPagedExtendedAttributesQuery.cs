#nullable enable
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetAllPagedExtendedAttributesQuery<TEntityId, TEntity> : IRequest<PaginatedResult<GetAllPagedExtendedAttributesResponse<TEntityId>>>
        where TEntity : class, IEntity<TEntityId>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SearchString { get; }
        public TEntityId? EntityId { get; }
        public ExtendedAttributeType? Type { get; }

        public GetAllPagedExtendedAttributesQuery(int pageNumber, int pageSize, string? searchString, TEntityId? entityId, ExtendedAttributeType? type)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            EntityId = entityId;
            Type = type;
        }
    }
}