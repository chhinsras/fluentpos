using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.Identity.Core.Features.Roles.Events
{
    public class RoleEventHandler :
        INotificationHandler<RoleAddedEvent>,
        INotificationHandler<RoleUpdatedEvent>,
        INotificationHandler<RoleDeletedEvent>
    {
        private readonly ILogger<RoleEventHandler> _logger;
        private readonly IStringLocalizer<RoleEventHandler> _localizer;

        public RoleEventHandler(
            ILogger<RoleEventHandler> logger,
            IStringLocalizer<RoleEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(RoleAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(RoleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(RoleDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}