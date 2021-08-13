// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommand.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using FluentPOS.Modules.Sales.Core.Abstractions;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Commands
{
    public class RegisterSaleCommand : IRequest<Result<Guid>>
    {
        public Guid CartId { get; set; }
    }

    internal sealed class RegisterSaleCommandHandler : IRequestHandler<RegisterSaleCommand, Result<Guid>>
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<RegisterSaleCommandHandler> _localizer;

        public RegisterSaleCommandHandler(IMapper mapper, IStringLocalizer<RegisterSaleCommandHandler> localizer, ISalesDbContext salesContext, ICartService cartService, IProductService productService)
        {
            _mapper = mapper;
            _localizer = localizer;
            _salesContext = salesContext;
            _cartService = cartService;
            _productService = productService;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // From CartId
            // Get Customer ID, Use Intergration Services to Get Customer Details
            // Get CartItem Details, Use Intergration Services to Get Product Details
            // Calculate Tax, Total
            // Save to Sales.Order,Transactions and Product
            // Delete CartItem and Cart
            var order = Order.InitializeOrder();
            var cartDetails = await _cartService.GetDetailsAsync(command.CartId);

            // Do all mandatory null checks
            if (cartDetails == null || cartDetails.Data == null) throw new Exception();
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
            await _salesContext.Orders.AddAsync(order);
            await _salesContext.SaveChangesAsync(default);
            await _cartService.RemoveCartAsync(command.CartId);
            return await Result<Guid>.SuccessAsync(order.Id, _localizer["Order Created"]);
        }
    }
}