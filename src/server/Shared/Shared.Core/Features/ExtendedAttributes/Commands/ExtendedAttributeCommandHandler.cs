using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Events;
using FluentPOS.Shared.Core.Interfaces;
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

    public class ExtendedAttributeCommandHandler<TEntity, TExtendedAttribute> :
        IRequestHandler<RemoveExtendedAttributeCommand<TEntity>, Result<Guid>>,
        IRequestHandler<AddExtendedAttributeCommand<TEntity>, Result<Guid>>,
        IRequestHandler<UpdateExtendedAttributeCommand<TEntity>, Result<Guid>>
            where TEntity : BaseEntity
            where TExtendedAttribute : ExtendedAttribute<TEntity>
    {
        private readonly IDistributedCache _cache;
        private readonly IExtendedAttributeDbContext<TEntity, TExtendedAttribute> _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExtendedAttributeCommandHandler> _localizer;

        public ExtendedAttributeCommandHandler(IExtendedAttributeDbContext<TEntity, TExtendedAttribute> context, IMapper mapper, IStringLocalizer<ExtendedAttributeCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(AddExtendedAttributeCommand<TEntity> command, CancellationToken cancellationToken)
        {
            var entity = await _context.Entities.AsNoTracking().FirstOrDefaultAsync(e => e.Id == command.EntityId, cancellationToken);
            if (entity == null) throw new CustomException(string.Format(_localizer["{0} Not Found"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            var isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.EntityId == command.EntityId && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed) throw new CustomException(string.Format(_localizer["This {0} Key is Already Used For This Entity"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            var extendedAttribute = _mapper.Map<TExtendedAttribute>(command);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeAddedEvent(extendedAttribute.Id,
                extendedAttribute.EntityId, extendedAttribute.Type, extendedAttribute.Key, extendedAttribute.Decimal,
                extendedAttribute.Text, extendedAttribute.DateTime, extendedAttribute.Json,
                extendedAttribute.ExternalId, extendedAttribute.Group, extendedAttribute.Description,
                extendedAttribute.IsActive));
            await _context.ExtendedAttributes.AddAsync(extendedAttribute, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Saved"], typeof(TEntity).Name));
        }

        public async Task<Result<Guid>> Handle(RemoveExtendedAttributeCommand<TEntity> command, CancellationToken cancellationToken)
        {
            var extendedAttribute = await _context.ExtendedAttributes.FirstOrDefaultAsync(ea => ea.Id == command.Id, cancellationToken);
            if (extendedAttribute == null) throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            _context.ExtendedAttributes.Remove(extendedAttribute);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeRemovedEvent(command.Id));
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.GetExtendedAttributeByIdCacheKey(typeof(TEntity).Name, command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Deleted"], typeof(TEntity).Name));
        }

        public async Task<Result<Guid>> Handle(UpdateExtendedAttributeCommand<TEntity> command, CancellationToken cancellationToken)
        {
            var extendedAttribute = await _context.ExtendedAttributes.Where(ea => ea.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (extendedAttribute == null) throw new CustomException(string.Format(_localizer["{0} Extended Attribute Not Found!"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            if (extendedAttribute.EntityId != command.EntityId) throw new CustomException(string.Format(_localizer["{0} Not Found"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            var isKeyUsed = await _context.ExtendedAttributes.AsNoTracking()
                .AnyAsync(ea => ea.Id != extendedAttribute.Id && ea.EntityId == command.EntityId && ea.Key.Equals(command.Key), cancellationToken);
            if (isKeyUsed) throw new CustomException(string.Format(_localizer["This {0} Key Is Already Used For This Entity"], typeof(TEntity).Name), statusCode: HttpStatusCode.NotFound);

            extendedAttribute = _mapper.Map<TExtendedAttribute>(command);
            extendedAttribute.AddDomainEvent(new ExtendedAttributeUpdatedEvent(extendedAttribute.Id,
                extendedAttribute.EntityId, extendedAttribute.Type, extendedAttribute.Key, extendedAttribute.Decimal,
                extendedAttribute.Text, extendedAttribute.DateTime, extendedAttribute.Json,
                extendedAttribute.ExternalId, extendedAttribute.Group, extendedAttribute.Description,
                extendedAttribute.IsActive));
            _context.ExtendedAttributes.Update(extendedAttribute);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.GetExtendedAttributeByIdCacheKey(typeof(TEntity).Name, command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(extendedAttribute.Id, string.Format(_localizer["{0} Extended Attribute Updated"], typeof(TEntity).Name));
        }
    }
}