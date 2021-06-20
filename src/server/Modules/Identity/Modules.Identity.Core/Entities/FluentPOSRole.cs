using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public sealed class FluentPOSRole : IdentityRole
    {
        public FluentPOSRole()
        {
        }

        public FluentPOSRole(string roleName) : base(roleName)
        {
        }
    }
}