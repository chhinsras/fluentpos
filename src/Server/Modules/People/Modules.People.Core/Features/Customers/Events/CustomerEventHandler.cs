using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Events
{
    public class CustomerEventHandler :
         INotificationHandler<CustomerRegisteredEvent>,
        INotificationHandler<CustomerUpdatedEvent>,
        INotificationHandler<CustomerRemovedEvent>
    {
        private readonly ILogger<CustomerEventHandler> _logger;
        private readonly IStringLocalizer<CustomerEventHandler> _localizer;

        public CustomerEventHandler(ILogger<CustomerEventHandler> logger, IStringLocalizer<CustomerEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CustomerRegisteredEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CustomerUpdatedEvent)} Raised."]);
            return Task.CompletedTask;
        }

        public Task Handle(CustomerRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(CustomerRemovedEvent)} Raised. {notification.Id} Removed."]);
            return Task.CompletedTask;
        }
    }
}
