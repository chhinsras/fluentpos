 using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeEventHandler :
        INotificationHandler<ExtendedAttributeAddedEvent>,
        INotificationHandler<ExtendedAttributeUpdatedEvent>,
        INotificationHandler<ExtendedAttributeRemovedEvent>
    {
        private readonly ILogger<ExtendedAttributeEventHandler> _logger;
        private readonly IStringLocalizer<ExtendedAttributeEventHandler> _localizer;

        public ExtendedAttributeEventHandler(ILogger<ExtendedAttributeEventHandler> logger, IStringLocalizer<ExtendedAttributeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(ExtendedAttributeAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(ExtendedAttributeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(ExtendedAttributeRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(ExtendedAttributeRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}