using System;

namespace FluentPOS.Shared.DTOs.Identity.EventLogs
{
    public class LogEventRequest
    {
        public LogEventRequest()
        {
        }

        public string Event { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}