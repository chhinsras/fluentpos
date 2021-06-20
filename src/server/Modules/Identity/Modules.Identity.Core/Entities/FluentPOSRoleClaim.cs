using Microsoft.AspNetCore.Identity;
using System;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class FluentPOSRoleClaim : IdentityRoleClaim<string>
    {
        public string Description { get; set; }
        public string Group { get; set; }
        public virtual FluentPOSRole Role { get; set; }

        public FluentPOSRoleClaim() : base()
        {
        }

        public FluentPOSRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }
    }
}
