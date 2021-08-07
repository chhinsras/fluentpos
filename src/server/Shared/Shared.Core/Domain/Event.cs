using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Shared.Core.Domain
{
    public abstract class Event : Message, INotification
    {
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IEnumerable<Type> RelatedEntities { get; protected set; }

        public DateTime Timestamp { get; private set; }
        public string Description { get; set; }
        protected Event(string description = null)
        {
            Timestamp = DateTime.Now;
            Description = description;
        }
    }
}