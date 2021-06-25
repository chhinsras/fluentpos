using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using FluentPOS.Shared.Core.Contracts;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class FluentRole : IdentityRole, IEntity<string>
    {
        public string Description { get; set; }
        public virtual ICollection<FluentRoleClaim> RoleClaims { get; set; }
        public virtual ICollection<RoleExtendedAttribute> ExtendedAttributes { get; set; }

        public FluentRole() : base()
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
            ExtendedAttributes = new HashSet<RoleExtendedAttribute>();
        }

        public FluentRole(string roleName, string roleDescription = null) : base(roleName)
        {
            RoleClaims = new HashSet<FluentRoleClaim>();
            ExtendedAttributes = new HashSet<RoleExtendedAttribute>();
            Description = roleDescription;
        }
    }
}