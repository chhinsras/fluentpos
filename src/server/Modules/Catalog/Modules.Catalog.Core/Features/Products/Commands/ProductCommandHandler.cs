// --------------------------------------------------------------------------------------------------
// <copyright file="ProductCommandHandler.cs" company="FluentPOS">
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
using FluentPOS.Modules.Catalog.Core.Features.Products.Events;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands
{
    internal class ProductCommandHandler :
        IRequestHandler<RegisterProductCommand, Result<Guid>>,
        IRequestHandler<RemoveProductCommand, Result<Guid>>,
        IRequestHandler<UpdateProductCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<ProductCommandHandler> _localizer;

        public ProductCommandHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IUploadService uploadService,
            IStringLocalizer<ProductCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Products.AnyAsync(p => p.BarcodeSymbology == command.BarcodeSymbology, cancellationToken))
            {
                throw new CatalogException(_localizer["Barcode already exists."], HttpStatusCode.BadRequest);
            }

            var product = _mapper.Map<Product>(command);
            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.BarcodeSymbology}.{uploadRequest.Extension}";
                product.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
            }

            product.AddDomainEvent(new ProductRegisteredEvent(product));
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Saved"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (await _context.Products.Where(p => p.Id != command.Id).AnyAsync(p => p.BarcodeSymbology == command.BarcodeSymbology, cancellationToken))
            {
                throw new CatalogException(_localizer["Barcode already exists."], HttpStatusCode.BadRequest);
            }

            var product = await _context.Products.Where(p => p.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (product != null)
            {
                product = _mapper.Map<Product>(command);
                var uploadRequest = command.UploadRequest;
                if (uploadRequest != null)
                {
                    uploadRequest.FileName = $"P-{command.BarcodeSymbology}.{uploadRequest.Extension}";
                    product.ImageUrl = await _uploadService.UploadAsync(uploadRequest);
                }

                product.AddDomainEvent(new ProductUpdatedEvent(product));
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Product>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Updated"]);
            }
            else
            {
                throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            }
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var product = await _context.Products.Where(p => p.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            if (product != null)
            {
                product.AddDomainEvent(new ProductRemovedEvent(product.Id));
                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Product>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            }
            else
            {
                throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            }
        }
    }
}