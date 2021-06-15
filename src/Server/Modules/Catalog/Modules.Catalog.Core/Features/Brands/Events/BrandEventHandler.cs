using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Events
{
    public class BrandEventHandler :
         INotificationHandler<BrandRegisteredEvent>,
        INotificationHandler<BrandUpdatedEvent>,
        INotificationHandler<BrandRemovedEvent>
    {
        private readonly ILogger<BrandEventHandler> logger;

        public BrandEventHandler(ILogger<BrandEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(BrandRegisteredEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"BrandRegisteredEvent Raised.");
            return Task.CompletedTask;
        }

        public Task Handle(BrandUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"BrandUpdatedEvent Raised.");
            return Task.CompletedTask;
        }

        public Task Handle(BrandRemovedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"BrandRemovedEvent Raised. {notification.Id} Removed.");
            return Task.CompletedTask;
        }
    }
}