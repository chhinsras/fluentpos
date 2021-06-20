using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class FluentRole : IdentityRole
    {
        public string Description { get; set; }
        public virtual ICollection<FluentRoleClaim> RoleClaims { get; set; }

        public FluentRole() : base()
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
        }

        public FluentRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
            Description = roleDescription;
        }
    }
}