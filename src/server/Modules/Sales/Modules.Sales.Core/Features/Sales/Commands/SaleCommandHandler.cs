// --------------------------------------------------------------------------------------------------
// <copyright file="SaleCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Sales.Core.Abstractions;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Commands
{
    internal sealed class SaleCommandHandler :
        IRequestHandler<RegisterSaleCommand, Result<Guid>>
    {
        private readonly IStockService _stockService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SaleCommandHandler> _localizer;

        public SaleCommandHandler(
            IMapper mapper,
            IStringLocalizer<SaleCommandHandler> localizer,
            ISalesDbContext salesContext,
            ICartService cartService,
            IProductService productService,
            IStockService stockService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
            _stockService = stockService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var order = Order.InitializeOrder();
            var cartDetails = await _cartService.GetDetailsAsync(command.CartId);

            // Do all mandatory null checks
            if (cartDetails?.Data == null) throw new Exception();
            if (cartDetails.Data.Customer == null) throw new Exception("Customer Invalid!");
            if (cartDetails.Data.CartItems == null) throw new Exception("Empty Cart!");
            var customer = cartDetails.Data.Customer;

            order.AddCustomer(customer);
            var items = cartDetails.Data.CartItems;
            foreach (var item in items)
            {
                var productResponse = await _productService.GetDetailsAsync(item.ProductId);
                if (productResponse.Succeeded)
                {
                    var product = productResponse.Data;
                    order.AddProduct(item.ProductId, product.Name, item.Quantity, product.Price, product.Tax);
                }
            }

            await _salesContext.Orders.AddAsync(order, cancellationToken);
            await _salesContext.SaveChangesAsync(cancellationToken);
            await _cartService.RemoveCartAsync(command.CartId);
            foreach(var product in order.Products)
            {
                //Inventory Operations Here
                await _stockService.RecordTransaction(product.ProductId, product.Quantity, product.OrderId.ToString());
            }
            return await Result<Guid>.SuccessAsync(order.Id, _localizer["Order Created"]);
        }
    }
}