// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterSaleCommandHandler.cs" company="FluentPOS">
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
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Commands
{
    internal sealed class RegisterSaleCommandHandler : IRequestHandler<RegisterSaleCommand, Result<Guid>>
    {
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<RegisterSaleCommandHandler> _localizer;

        public RegisterSaleCommandHandler(
            IMapper mapper,
            IStringLocalizer<RegisterSaleCommandHandler> localizer,
            ISalesDbContext salesContext)
        {
            _mapper = mapper;
            _localizer = localizer;
            _salesContext = salesContext;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task<Result<Guid>> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            // From CartId
            // Get Customer ID, Use Integration Services to Get Customer Details
            // Get CartItem Details, Use Integration Services to Get Product Details
            // Calculate Tax, Total
            // Save to Sales.Order,Transactions and Product
            // Delete CartItem and Cart

            return null;
        }
    }
}