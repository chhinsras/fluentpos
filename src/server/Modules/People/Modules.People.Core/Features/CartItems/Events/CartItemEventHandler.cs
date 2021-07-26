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

        public Task Handle(CartItemAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemAddedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CartItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CartItemRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CartItemRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}