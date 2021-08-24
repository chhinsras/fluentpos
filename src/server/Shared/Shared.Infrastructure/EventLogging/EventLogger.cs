// --------------------------------------------------------------------------------------------------
// <copyright file="EventLogger.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Interfaces.Services.Identity;

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

        public async Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event
        {
            if (@event is EventLog eventLog)
            {
                await _context.EventLogs.AddAsync(eventLog);
                await _context.SaveChangesAsync();
            }
            else
            {
                string serializedData = _jsonSerializer.Serialize(@event, @event.GetType());

                string userEmail = _user.GetUserEmail();
                if (string.IsNullOrWhiteSpace(userEmail))
                {
                    userEmail = "Anonymous";
                }

                var userId = _user.GetUserId();
                var thisEvent = new EventLog(
                    @event,
                    serializedData,
                    changes,
                    userEmail,
                    userId);
                await _context.EventLogs.AddAsync(thisEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}