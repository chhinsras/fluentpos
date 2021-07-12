using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentPOS.Shared.DTOs.Identity.Roles;

namespace FluentPOS.Modules.Identity.Core.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetAllPermissions(this List<RoleClaimResponse> allPermissions)
        {
            var modules = typeof(Permissions).GetNestedTypes();

            foreach (var module in modules)
            {
                var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (FieldInfo fi in fields)
                {
                    var propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                        allPermissions.Add(new RoleClaimResponse { Value = propertyValue.ToString(), Type = ApplicationClaimTypes.Permission, Group = module.Name });
                }
            }

        }
        public static async Task<IdentityResult> AddPermissionClaim(this RoleManager<FluentRole> roleManager, FluentRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }

        public static async Task AddCustomPermissionClaim(this RoleManager<FluentRole> roleManager, FluentRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
    }
}