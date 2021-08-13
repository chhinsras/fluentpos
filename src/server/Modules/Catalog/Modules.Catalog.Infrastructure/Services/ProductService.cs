// --------------------------------------------------------------------------------------------------
// <copyright file="ProductService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Features.Products.Queries;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Catalog.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetProductByIdResponse>> GetDetailsAsync(Guid productId)
        {
            return await _mediator.Send(new GetProductByIdQuery(productId, false));
        }
    }
}