// --------------------------------------------------------------------------------------------------
// <copyright file="UserEventHandler.cs" company="FluentPOS">
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

namespace FluentPOS.Modules.Identity.Core.Features.Users.Events
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserUpdatedEvent>,
        INotificationHandler<UserDeletedEvent>
    {
        private readonly ILogger<UserEventHandler> _logger;
        private readonly IStringLocalizer<UserEventHandler> _localizer;

        public UserEventHandler(
            ILogger<UserEventHandler> logger,
            IStringLocalizer<UserEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(UserDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}