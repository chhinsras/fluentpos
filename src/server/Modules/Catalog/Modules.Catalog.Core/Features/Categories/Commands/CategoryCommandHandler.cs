using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Events;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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

        public CategoryCommandHandler(ICatalogDbContext context, IMapper mapper, IUploadService uploadService, IStringLocalizer<CategoryCommandHandler> localizer, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(RegisterCategoryCommand command, CancellationToken cancellationToken)
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

        public async Task<Result<Guid>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
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
                await _cache.RemoveAsync(CatalogCacheKeys.GetCategoryByIdCacheKey(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Category Not Found!"], HttpStatusCode.NotFound);
            }
        }

        public async Task<Result<Guid>> Handle(RemoveCategoryCommand command, CancellationToken cancellationToken)
        {
            var isCategoryUsed = await IsCategoryUsed(command.Id);
            if (!isCategoryUsed)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
                category.AddDomainEvent(new CategoryRemovedEvent(category.Id));
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CatalogCacheKeys.GetCategoryByIdCacheKey(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(category.Id, _localizer["Category Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Deletion Not Allowed"], System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<bool> IsCategoryUsed(Guid categoryId)
        {
            return await _context.Products.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}