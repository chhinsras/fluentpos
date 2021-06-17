using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string name, string phone, string email, string imageUrl, string type)
        {
            Name = name;
            Phone = phone;
            Email = email;
            ImageUrl = imageUrl;
            Type = type;
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }
        public string ImageUrl { get; }
        public string Type { get; }
    }
}