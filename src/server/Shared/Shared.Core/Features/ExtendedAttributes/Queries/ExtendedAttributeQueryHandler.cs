using AutoMapper;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class ExtendedAttributeQueryHandler
    {
        // for localization
    }

    public class ExtendedAttributeQueryHandler<TEntityId, TEntity, TExtendedAttribute> :
        IRequestHandler<GetAllPagedExtendedAttributesQuery<TEntityId, TEntity>, PaginatedResult<GetAllPagedExtendedAttributesResponse<TEntityId>>>,
        IRequestHandler<GetExtendedAttributeByIdQuery<TEntityId, TEntity>, Result<GetExtendedAttributeByIdResponse<TEntityId>>>
            where TEntity : class, IEntity<TEntityId>
            where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
        private readonly IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeQueryHandler> _localizer;

        public ExtendedAttributeQueryHandler(IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> context, IMapper mapper, IStringLocalizer<ExtendedAttributeQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedExtendedAttributesResponse<TEntityId>>> Handle(GetAllPagedExtendedAttributesQuery<TEntityId, TEntity> request, CancellationToken cancellationToken)
        {
            Expression<Func<TExtendedAttribute, GetAllPagedExtendedAttributesResponse<TEntityId>>> expression = e => new GetAllPagedExtendedAttributesResponse<TEntityId>(e.Id, e.EntityId, e.Type, e.Key, e.Decimal, e.Text, e.DateTime, e.Json, e.Boolean, e.Integer, e.ExternalId, e.Group, e.Description, e.IsActive);

            var queryable = _context.ExtendedAttributes.OrderBy(x => x.Id).AsQueryable();

            if (request.OrderBy?.Any() == true)
            {
                var ordering = string.Join(",", request.OrderBy);
                queryable = queryable.OrderBy(ordering);
            }
            else
            {
                queryable = queryable.OrderBy(a => a.Id);
            }

            // apply filter parameters
            if (request.EntityId != null && !request.EntityId.Equals(default(TEntityId))) queryable = queryable.Where(b => b.EntityId.Equals(request.EntityId));
            if (request.Type != null) queryable = queryable.Where(b => b.Type == request.Type);
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(b => b.Key.Contains(request.SearchString)
                        || b.Type == ExtendedAttributeType.Decimal && b.Decimal != null && b.Decimal.ToString().Contains(request.SearchString)
                        || b.Type == ExtendedAttributeType.Text && b.Text != null && b.Text.Contains(request.SearchString)
                        || b.Type == ExtendedAttributeType.DateTime && b.DateTime != null && b.DateTime.ToString().Contains(request.SearchString)
                        || b.Type == ExtendedAttributeType.Json && b.Json != null && b.Json.Contains(request.SearchString)
                        || b.Type == ExtendedAttributeType.Integer && b.Integer != null && b.Integer.ToString().Contains(request.SearchString)
                        || b.ExternalId != null && b.ExternalId.Contains(request.SearchString)
                        || b.Group != null && b.Group.Contains(request.SearchString)
                        || b.Description != null && b.Description.Contains(request.SearchString)
                    );
            }

            var extendedAttributeList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (extendedAttributeList == null) throw new CustomException(string.Format(_localizer["{0} Extended Attributes Not Found!"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            var mappedExtendedAttributes = _mapper.Map<PaginatedResult<GetAllPagedExtendedAttributesResponse<TEntityId>>>(extendedAttributeList);

            return mappedExtendedAttributes;
        }

        public async Task<Result<GetExtendedAttributeByIdResponse<TEntityId>>> Handle(GetExtendedAttributeByIdQuery<TEntityId, TEntity> query, CancellationToken cancellationToken)
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(b => b.Id == query.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null) throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found!"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);
            var mappedExtendedAttribute = _mapper.Map<GetExtendedAttributeByIdResponse<TEntityId>>(extendedAttribute);
            return await Result<GetExtendedAttributeByIdResponse<TEntityId>>.SuccessAsync(mappedExtendedAttribute);
        }
    }
}