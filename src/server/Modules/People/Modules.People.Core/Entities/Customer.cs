using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }

        protected override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}