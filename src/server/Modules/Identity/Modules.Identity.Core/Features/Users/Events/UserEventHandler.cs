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

        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(UserRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(UserUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(UserDeletedEvent)} Raised. {notification.Id} Deleted."]);
            return Task.CompletedTask;
        }
    }
}