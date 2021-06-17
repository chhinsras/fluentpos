using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryEventHandler :
         INotificationHandler<CategoryRegisteredEvent>,
        INotificationHandler<CategoryUpdatedEvent>,
        INotificationHandler<CategoryRemovedEvent>
    {
        private readonly ILogger<CategoryEventHandler> _logger;
        private readonly IStringLocalizer<CategoryEventHandler> _localizer;

        public CategoryEventHandler(ILogger<CategoryEventHandler> logger, IStringLocalizer<CategoryEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(CategoryRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CategoryRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CategoryUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CategoryRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CategoryRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}
