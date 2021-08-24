// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeEventHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Utilities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeEventHandler
    {
        // for localization
    }

    public class ExtendedAttributeEventHandler<TEntityId, TEntity> :
        INotificationHandler<ExtendedAttributeAddedEvent<TEntityId, TEntity>>,
        INotificationHandler<ExtendedAttributeUpdatedEvent<TEntityId, TEntity>>,
        INotificationHandler<ExtendedAttributeRemovedEvent<TEntity>>
            where TEntity : class, IEntity<TEntityId>
    {
        private readonly ILogger<ExtendedAttributeEventHandler> _logger;
        private readonly IStringLocalizer<ExtendedAttributeEventHandler> _localizer;

        public ExtendedAttributeEventHandler(ILogger<ExtendedAttributeEventHandler> logger, IStringLocalizer<ExtendedAttributeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeAddedEvent<TEntityId, TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeAddedEvent<TEntityId, TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeUpdatedEvent<TEntityId, TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeUpdatedEvent<TEntityId, TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised."]);
            return Task.CompletedTask;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public Task Handle(ExtendedAttributeRemovedEvent<TEntity> notification, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeRemovedEvent<TEntity>)} For {typeof(TEntity).GetGenericTypeName()} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}