// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Utilities;
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

    public class ExtendedAttributeQueryHandler<TEntityId, TEntity, TExtendedAttribute> :
        IRequestHandler<GetExtendedAttributesQuery<TEntityId, TEntity>, PaginatedResult<GetExtendedAttributesResponse<TEntityId>>>,
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

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetExtendedAttributesResponse<TEntityId>>> Handle(GetExtendedAttributesQuery<TEntityId, TEntity> request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<TExtendedAttribute, GetExtendedAttributesResponse<TEntityId>>> expression = e => new GetExtendedAttributesResponse<TEntityId>(e.Id, e.EntityId, e.Type, e.Key, e.Decimal, e.Text, e.DateTime, e.Json, e.Boolean, e.Integer, e.ExternalId, e.Group, e.Description, e.IsActive);

            var queryable = _context.ExtendedAttributes.AsNoTracking().OrderBy(x => x.Id).AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            // apply filter parameters
            if (request.EntityId != null && !request.EntityId.Equals(default(TEntityId)))
            {
                queryable = queryable.Where(b => b.EntityId.Equals(request.EntityId));
            }

            if (request.Type != null)
            {
                queryable = queryable.Where(b => b.Type == request.Type);
            }

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                string lowerSearchString = request.SearchString.ToLower();
                queryable = queryable.Where(x => EF.Functions.Like(x.Key.ToLower(), $"%{lowerSearchString}%")
                        || (x.Type == ExtendedAttributeType.Decimal && x.Decimal != null && EF.Functions.Like(x.Decimal.ToString().ToLower(), $"%{lowerSearchString}%"))
                        || (x.Type == ExtendedAttributeType.Text && x.Text != null && EF.Functions.Like(x.Text.ToLower(), $"%{lowerSearchString}%"))
                        || (x.Type == ExtendedAttributeType.DateTime && x.DateTime != null && EF.Functions.Like(x.DateTime.ToString().ToLower(), $"%{lowerSearchString}%"))
                        || (x.Type == ExtendedAttributeType.Json && x.Json != null && EF.Functions.Like(x.Json.ToLower(), $"%{lowerSearchString}%"))
                        || (x.Type == ExtendedAttributeType.Integer && x.Integer != null && EF.Functions.Like(x.Integer.ToString().ToLower(), $"%{lowerSearchString}%"))
                        || (x.ExternalId != null && EF.Functions.Like(x.ExternalId.ToLower(), $"%{lowerSearchString}%"))
                        || (x.Group != null && EF.Functions.Like(x.Group.ToLower(), $"%{lowerSearchString}%"))
                        || (x.Description != null && EF.Functions.Like(x.Description.ToLower(), $"%{lowerSearchString}%"))
                        || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{lowerSearchString}%"));
            }

            var extendedAttributeList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (extendedAttributeList == null)
            {
                throw new CustomException(string.Format(_localizer["{0} Extended Attributes Not Found!"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetExtendedAttributesResponse<TEntityId>>>(extendedAttributeList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetExtendedAttributeByIdResponse<TEntityId>>> Handle(GetExtendedAttributeByIdQuery<TEntityId, TEntity> query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (extendedAttribute == null)
            {
                throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found!"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            var mappedExtendedAttribute = _mapper.Map<GetExtendedAttributeByIdResponse<TEntityId>>(extendedAttribute);
            return await Result<GetExtendedAttributeByIdResponse<TEntityId>>.SuccessAsync(mappedExtendedAttribute);
        }
    }
}