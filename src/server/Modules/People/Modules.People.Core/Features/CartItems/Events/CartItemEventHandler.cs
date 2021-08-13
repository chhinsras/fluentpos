// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemEventHandler.cs" company="FluentPOS">
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

namespace FluentPOS.Modules.People.Core.Features.CartItems.Events
{
    public class CartItemEventHandler :
        INotificationHandler<CartItemAddedEvent>,
        INotificationHandler<CartItemUpdatedEvent>,
        INotificationHandler<CartItemRemovedEvent>
    {
        private readonly ILogger<CartItemEventHandler> _logger;
        private readonly IStringLocalizer<CartItemEventHandler> _localizer;

        public CartItemEventHandler(
            ILogger<CartItemEventHandler> logger,
            IStringLocalizer<CartItemEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(CartItemAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(CartItemUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(CartItemRemovedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}