// --------------------------------------------------------------------------------------------------
// <copyright file="CategoryCommandHandler.cs" company="FluentPOS">
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
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Events;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands
{
    internal class CategoryCommandHandler :
        IRequestHandler<RegisterCategoryCommand, Result<Guid>>,
        IRequestHandler<RemoveCategoryCommand, Result<Guid>>,
        IRequestHandler<UpdateCategoryCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<CategoryCommandHandler> _localizer;

        public CategoryCommandHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<CategoryCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterCategoryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Categories.AnyAsync(c => c.Name == command.Name, cancellationToken))
            {
                throw new CatalogException(_localizer["Category with the same name already exists."], HttpStatusCode.BadRequest);
            }

            var category = _mapper.Map<Category>(command);
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
                category.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            }

            category.AddDomainEvent(new CategoryRegisteredEvent(category));
            await _context.Categories.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var category = await _context.Categories.Where(c => c.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            if (category != null)
            {
                if (await _context.Categories.AnyAsync(c => c.Id != command.Id && c.Name == command.Name, cancellationToken))
                {
                    throw new CatalogException(_localizer["Category with the same name already exists."], HttpStatusCode.BadRequest);
                }

                category = _mapper.Map<Category>(command);
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"C-{command.Name}{uploadRequest.Extension}";
                    category.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
                }

                category.AddDomainEvent(new CategoryUpdatedEvent(category));
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Category>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Category Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveCategoryCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            bool isCategoryUsed = await IsCategoryUsedAsync(command.Id);
            if (!isCategoryUsed)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
                category.AddDomainEvent(new CategoryRemovedEvent(category.Id));
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Category>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Deletion Not Allowed"], HttpStatusCode.BadRequest);
            }
        }

        public async Task<bool> IsCategoryUsedAsync(Guid categoryId)
        {
            return await _context.Products.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}