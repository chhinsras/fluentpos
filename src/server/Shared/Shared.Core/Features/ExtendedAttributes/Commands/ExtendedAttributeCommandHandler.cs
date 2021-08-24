// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Events;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class ExtendedAttributeCommandHandler
    {
        // for localization
    }

    public class ExtendedAttributeCommandHandler<TEntityId, TEntity, TExtendedAttribute> :
        IRequestHandler<RemoveExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>,
        IRequestHandler<AddExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>,
        IRequestHandler<UpdateExtendedAttributeCommand<TEntityId, TEntity>, Result<Guid>>
            where TEntity : class, IEntity<TEntityId>
            where TExtendedAttribute : ExtendedAttribute<TEntityId, TEntity>
    {
        private readonly IDistributedCache _cache;
        private readonly IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeCommandHandler> _localizer;

        public ExtendedAttributeCommandHandler(
            IExtendedAttributeDbContext<TEntityId, TEntity, TExtendedAttribute> context,
            IMapper mapper,
            IStringLocalizer<ExtendedAttributeCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(AddExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var entity = await _context.Entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(command.EntityId), cancellationToken);
            if (entity == null)
            {
                throw new CustomException(string.Format(_localizer["{0} Not Found"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            bool isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.EntityId.Equals(command.EntityId) && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed)
            {
                throw new CustomException(string.Format(_localizer["This {0} Key is Already Used For This Entity"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            var extendedAttribute = _mapper.Map<TExtendedAttribute>(command);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeAddedEvent<TEntityId, TEntity>(extendedAttribute));
            await _context.ExtendedAttributes.AddAsync(extendedAttribute, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Saved"], typeof(TEntity).GetGenericTypeName()));
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.FirstOrDefaultAsync(ea => ea.Id == command.Id, cancellationToken);
            if (extendedAttribute == null)
            {
                throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            _context.ExtendedAttributes.Remove(extendedAttribute);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeRemovedEvent<TEntity>(command.Id));
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, ExtendedAttribute<TEntityId, TEntity>>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Deleted"], typeof(TEntity).GetGenericTypeName()));
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateExtendedAttributeCommand<TEntityId, TEntity> command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(ea => ea.Id.Equals(command.Id)).FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null)
            {
                throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found!"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            if (!extendedAttribute.EntityId.Equals(command.EntityId))
            {
                throw new CustomException(string.Format(_localizer["{0} Not Found"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            bool isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.Id != extendedAttribute.Id && ea.EntityId.Equals(command.EntityId) && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed)
            {
                throw new CustomException(string.Format(_localizer["This {0} Key Is Already Used For This Entity"], typeof(TEntity).GetGenericTypeName()), statusCode: HttpStatusCode.NotFound);
            }

            extendedAttribute = _mapper.Map(command, extendedAttribute);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeUpdatedEvent<TEntityId, TEntity>(extendedAttribute));
            _context.ExtendedAttributes.Update(extendedAttribute);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, ExtendedAttribute<TEntityId, TEntity>>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Updated"], typeof(TEntity).GetGenericTypeName()));
        }
    }
}