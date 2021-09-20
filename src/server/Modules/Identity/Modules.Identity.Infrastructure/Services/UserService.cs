// --------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Identity.Core.Abstractions;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Core.Exceptions;
using FluentPOS.Modules.Identity.Core.Features.Users.Events;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<FluentUser> _userManager;
        private readonly RoleManager<FluentRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UserService> _localizer;

        public UserService(
            UserManager<FluentUser> userManager,
            IMapper mapper,
            RoleManager<FluentRole> roleManager,
            IStringLocalizer<UserService> localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _localizer = localizer;
        }

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var result = _mapper.Map<List<UserResponse>>(users);
            return await Result<List<UserResponse>>.SuccessAsync(result);
        }

        public async Task<IResult<UserResponse>> GetAsync(string userId)
        {
            var user = await _userManager.Users.AsNoTracking().Where(u => u.Id == userId).FirstOrDefaultAsync();
            var result = _mapper.Map<UserResponse>(user);
            return await Result<UserResponse>.SuccessAsync(result);
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
            foreach (var role in roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                viewModel.Add(userRolesViewModel);
            }

            var result = new UserRolesResponse { UserRoles = viewModel };
            return await Result<UserRolesResponse>.SuccessAsync(result);
        }

        public async Task<IResult<string>> UpdateAsync(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);

            if (user == null) throw new IdentityException(string.Format(_localizer["User Id {0} is not found."], request.Id));

            _mapper.Map<UpdateUserRequest, FluentUser>(request, user);

            user.AddDomainEvent(new UserUpdatedEvent(user));

            if (request.CurrentPassword != null && request.Password != null)
            {
                await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return await Result<string>.SuccessAsync(user.Id,  _localizer["User Updated Succesffully."]);
            }
            else
            {
                throw new IdentityException(_localizer["Validation Errors Occurred."], result.Errors.Select(a => _localizer[a.Description].ToString()).ToList());
            }
        }

        public async Task<IResult<string>> UpdateUserRolesAsync(string userId, UserRolesRequest request)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return await Result<string>.FailAsync(_localizer["User Not Found."]);
            }

            if (await _userManager.IsInRoleAsync(user, RoleConstants.SuperAdmin))
            {
                return await Result<string>.FailAsync(_localizer["Not Allowed."]);
            }

            foreach (var userRole in request.UserRoles)
            {
                // Check if Role Exists
                if (await _roleManager.FindByNameAsync(userRole.RoleName) != null)
                {
                    if (userRole.Selected)
                    {
                        if (!await _userManager.IsInRoleAsync(user, userRole.RoleName))
                        {
                            await _userManager.AddToRoleAsync(user, userRole.RoleName);
                        }
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                    }
                }
            }

            return await Result<string>.SuccessAsync(userId, string.Format(_localizer["User Roles Updated Successfully."]));
        }
    }
}