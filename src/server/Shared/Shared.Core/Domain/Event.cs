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

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}