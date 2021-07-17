using FluentPOS.Shared.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class FluentUser : IdentityUser, IEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<UserExtendedAttribute> ExtendedAttributes { get; set; }

        public FluentUser() : base()
        {
            ExtendedAttributes = new HashSet<UserExtendedAttribute>();
        }
    }
}