// --------------------------------------------------------------------------------------------------
// <copyright file="ProductEventHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductEventHandler :
        INotificationHandler<ProductRegisteredEvent>,
        INotificationHandler<ProductUpdatedEvent>,
        INotificationHandler<ProductRemovedEvent>
    {
        private readonly ILogger<ProductEventHandler> _logger;
        private readonly IStringLocalizer<ProductEventHandler> _localizer;

        public ProductEventHandler(
            ILogger<ProductEventHandler> logger,
            IStringLocalizer<ProductEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ProductRegisteredEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ProductRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ProductUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ProductRemovedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ProductRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}