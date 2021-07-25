using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluentPOS.Modules.People.Core.Features.Carts.Events
{
    public class CartEventHandler :
        INotificationHandler<CartCreatedEvent>,
        INotificationHandler<CartRemovedEvent>
    {
        private readonly ILogger<CartEventHandler> _logger;
        private readonly IStringLocalizer<CartEventHandler> _localizer;

        public CartEventHandler(ILogger<CartEventHandler> logger, IStringLocalizer<CartEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(CartCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CartCreatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CartRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CartRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}