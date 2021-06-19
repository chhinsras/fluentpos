using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public sealed class ExtendedIdentityRole : IdentityRole
    {
        public ExtendedIdentityRole()
        {
        }

        public ExtendedIdentityRole(string roleName) : base(roleName)
        {
        }
    }
}