// --------------------------------------------------------------------------------------------------
// <copyright file="RolesController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Modules.Identity.Core.Abstractions;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Identity.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Identity.Controllers
{
    [ApiVersion("1")]
    internal sealed class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly IRoleClaimService _roleClaimService;

        public RolesController(IRoleService roleService, IRoleClaimService roleClaimService)
        {
            _roleService = roleService;
            _roleClaimService = roleClaimService;
        }

        /// <summary>
        /// Get All Roles (basic, admin etc.)
        /// </summary>
        /// <returns>Status 200 OK.</returns>
        [HttpGet]
        [Authorize(Policy = Permissions.Roles.View)]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Add a Role.
        /// </summary>
        /// <returns>Status 200 OK.</returns>
        [HttpPost]
        [Authorize(Policy = Permissions.Roles.Create)]
        public async Task<IActionResult> PostAsync(RoleRequest request)
        {
            var response = await _roleService.SaveAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role.
        /// </summary>
        /// <returns>Status 200 OK.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Roles.Delete)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _roleService.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Get Permissions By Role Id.
        /// </summary>
        /// <returns>Status 200 Ok.</returns>
        [HttpGet("permissions/byrole/{roleId}")]
        [Authorize(Policy = Permissions.RoleClaims.View)]
        public async Task<IActionResult> GetPermissionsByRoleIdAsync([FromRoute] string roleId)
        {
            var response = await _roleClaimService.GetAllPermissionsAsync(roleId);
            return Ok(response);
        }

        /// <summary>
        /// Get All Role Claims.
        /// </summary>
        /// <returns>Status 200 Ok.</returns>
        [HttpGet("permissions")]
        [Authorize(Policy = Permissions.RoleClaims.View)]
        public async Task<IActionResult> GetAllClaimsAsync()
        {
            var response = await _roleClaimService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get a Role Claim By Id.
        /// </summary>
        /// <returns>Status 200 Ok.</returns>
        [HttpGet("permissions/{id}")]
        [Authorize(Policy = Permissions.RoleClaims.View)]
        public async Task<IActionResult> GetClaimByIdAsync([FromRoute] int id)
        {
            var response = await _roleClaimService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Edit a Role Claims.
        /// </summary>
        [HttpPut("permissions/update")]
        [Authorize(Policy = Permissions.RoleClaims.Edit)]
        public async Task<IActionResult> UpdatePermissionsAsync(PermissionRequest request)
        {
            var response = await _roleClaimService.UpdatePermissionsAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role Claim By Id.
        /// </summary>
        /// <returns>Status 200 Ok.</returns>
        [HttpDelete("permissions/{id}")]
        [Authorize(Policy = Permissions.RoleClaims.Delete)]
        public async Task<IActionResult> DeleteClaimByIdAsync([FromRoute] int id)
        {
            var response = await _roleClaimService.DeleteAsync(id);
            return Ok(response);
        }
    }
}