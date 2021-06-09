using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Core.Entities
{
    public class ExtendedIdentityRole : IdentityRole<int>
    {
        public ExtendedIdentityRole()
        {

        }
        public ExtendedIdentityRole(string roleName)
        {
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }
    }
}
