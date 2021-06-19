using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using FluentPOS.Modules.Identity.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Core.Helpers
{
    public static class ClaimsHelper
    {

        public static async Task<IdentityResult> AddPermissionClaim(this RoleManager<ExtendedIdentityRole> roleManager, ExtendedIdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }

        public static async Task AddCustomPermissionClaim(this RoleManager<ExtendedIdentityRole> roleManager, ExtendedIdentityRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }
        }
    }
}