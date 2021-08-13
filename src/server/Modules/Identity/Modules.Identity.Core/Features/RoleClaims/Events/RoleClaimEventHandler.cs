// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimEventHandler.cs" company="FluentPOS">
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

namespace FluentPOS.Modules.Identity.Core.Features.RoleClaims.Events
{
    public class RoleClaimEventHandler :
        INotificationHandler<RoleClaimAddedEvent>,
        INotificationHandler<RoleClaimUpdatedEvent>,
        INotificationHandler<RoleClaimDeletedEvent>
    {
        private readonly ILogger<RoleClaimEventHandler> _logger;
        private readonly IStringLocalizer<RoleClaimEventHandler> _localizer;

        public RoleClaimEventHandler(
            ILogger<RoleClaimEventHandler> logger,
            IStringLocalizer<RoleClaimEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimAddedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimUpdatedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(RoleClaimDeletedEvent notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}