using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class ExtendedAttributeQueryHandler
    {
        // for localization
    }

    public class ExtendedAttributeQueryHandler<TEntity, TExtendedAttribute> :
        IRequestHandler<GetAllPagedExtendedAttributesQuery<TEntity>, PaginatedResult<GetAllPagedExtendedAttributesResponse>>,
        IRequestHandler<GetExtendedAttributeByIdQuery<TEntity>, Result<GetExtendedAttributeByIdResponse>>
            where TEntity : BaseEntity
            where TExtendedAttribute : ExtendedAttribute<TEntity>
    {
        private readonly IExtendedAttributeDbContext<TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeQueryHandler> _localizer;

        public ExtendedAttributeQueryHandler(IExtendedAttributeDbContext<TEntity, TExtendedAttribute> context, IMapper mapper, IStringLocalizer<ExtendedAttributeQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedExtendedAttributesResponse>> Handle(GetAllPagedExtendedAttributesQuery<TEntity> request, CancellationToken cancellationToken)
        {
            Expression<Func<TExtendedAttribute, GetAllPagedExtendedAttributesResponse>> expression = e => new GetAllPagedExtendedAttributesResponse(e.Id, e.EntityId, e.Type, e.Key, e.Decimal, e.Text, e.DateTime, e.Json, e.ExternalId, e.Group, e.Description, e.IsActive);

            var queryable = _context.ExtendedAttributes.OrderBy(x => x.Id).AsQueryable();

            // apply filter parameters
            if (request.EntityId != null) queryable = queryable.Where(b => b.EntityId == request.EntityId);
            if (request.Type != null) queryable = queryable.Where(b => b.Type == request.Type);
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(b => b.Key.Contains(request.SearchString)
                        || (b.Decimal == null || b.Type == ExtendedAttributeType.Decimal && b.Decimal != null && b.Decimal.ToString().Contains(request.SearchString))
                        || (b.Text == null || b.Type == ExtendedAttributeType.Text && b.Text != null && b.Text.Contains(request.SearchString))
                        || (b.DateTime == null || b.Type == ExtendedAttributeType.DateTime && b.DateTime != null && b.DateTime.ToString().Contains(request.SearchString))
                        || (b.Json == null || b.Type == ExtendedAttributeType.Json && b.Json != null && b.Json.Contains(request.SearchString))
                        || (b.ExternalId == null || b.ExternalId != null && b.ExternalId.Contains(request.SearchString))
                        || (b.Group == null || b.Group != null && b.Group.Contains(request.SearchString))
                        || (b.Description == null || b.Description != null && b.Description.Contains(request.SearchString))
                    );
            }

            var extendedAttributeList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (extendedAttributeList == null) throw new CustomException(string.Format(_localizer["{0} Extended Attributes Not Found!"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            var mappedExtendedAttributes = _mapper.Map<PaginatedResult<GetAllPagedExtendedAttributesResponse>>(extendedAttributeList);

            return mappedExtendedAttributes;
        }

        public async Task<Result<GetExtendedAttributeByIdResponse>> Handle(GetExtendedAttributeByIdQuery<TEntity> query, CancellationToken cancellationToken)
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(b => b.Id == query.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null) throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found!"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);
            var mappedExtendedAttribute = _mapper.Map<GetExtendedAttributeByIdResponse>(extendedAttribute);
            return await Result<GetExtendedAttributeByIdResponse>.SuccessAsync(mappedExtendedAttribute);
        }
    }
}