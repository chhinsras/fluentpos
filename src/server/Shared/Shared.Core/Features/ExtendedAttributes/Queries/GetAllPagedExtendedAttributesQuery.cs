#nullable enable
using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetAllPagedExtendedAttributesQuery<TEntity> : IRequest<PaginatedResult<GetAllPagedExtendedAttributesResponse>>
        where TEntity : BaseEntity
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SearchString { get; }
        public Guid? EntityId { get; }
        public ExtendedAttributeType? Type { get; }

        public GetAllPagedExtendedAttributesQuery(int pageNumber, int pageSize, string? searchString, Guid? entityId, ExtendedAttributeType? type)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            EntityId = entityId;
            Type = type;
        }
    }
}