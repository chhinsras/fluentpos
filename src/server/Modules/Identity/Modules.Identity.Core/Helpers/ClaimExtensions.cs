// --------------------------------------------------------------------------------------------------
// <copyright file="ClaimExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Identity.Roles;
using Microsoft.AspNetCore.Identity;

namespace FluentPOS.Modules.Identity.Core.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetAllPermissions(this List<RoleClaimModel> allPermissions)
        {
            foreach (var module in typeof(Permissions).GetNestedTypes())
            {
                string moduleName = string.Empty;
                string moduleDescription = string.Empty;

                if (module.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttribute)
                {
                    moduleName = displayNameAttribute.DisplayName;
                }

                if (module.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                {
                    moduleDescription = descriptionAttribute.Description;
                }

                foreach (var fi in module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                {
                    object propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                    {
                        allPermissions.Add(new() { Value = propertyValue.ToString(), Type = ApplicationClaimTypes.Permission, Group = moduleName, Description = moduleDescription });
                    }
                }
            }
        }

        public static async Task<IdentityResult> AddPermissionClaimAsync(this RoleManager<FluentRole> roleManager, FluentRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }

        public static async Task AddCustomPermissionClaimAsync(this RoleManager<FluentRole> roleManager, FluentRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new(ApplicationClaimTypes.Permission, permission));
            }
        }
    }
}