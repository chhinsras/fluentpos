using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Interfaces.Services.Identity;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.EventLogging
{
    internal class EventLogger : IEventLogger
    {
        private readonly ICurrentUser _user;
        private readonly IApplicationDbContext _context;
        private readonly IJsonSerializer _jsonSerializer;

        public EventLogger(
            ICurrentUser user,
            IApplicationDbContext context,
            IJsonSerializer jsonSerializer)
        {
            _user = user;
            _context = context;
            _jsonSerializer = jsonSerializer;
        }

        public async Task Save<T>(T @event, (string oldValues,string newValues) changes) where T : Event
        {
            var serializedData = _jsonSerializer.Serialize(@event, @event.GetType());

            var userEmail = _user.GetUserEmail();
            var userId = _user.GetUserId();
            var thisEvent = new EventLog(
                @event,
                serializedData,
                changes,
                string.IsNullOrWhiteSpace(userEmail) ? _user.Name : userEmail,
                userId);
            await _context.EventLogs.AddAsync(thisEvent);
            await _context.SaveChangesAsync(default);
        }
    }
}