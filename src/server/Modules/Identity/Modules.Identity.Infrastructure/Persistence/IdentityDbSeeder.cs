using FluentPOS.Modules.Identity.Core.Constants;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Core.Enums;
using FluentPOS.Shared.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence
{
    internal class IdentityDbSeeder : IDatabaseSeeder
    {
        private readonly ILogger<IdentityDbSeeder> _logger;
        private readonly IdentityDbContext _db;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly IStringLocalizer<IdentityDbSeeder> _localizer;
        private readonly RoleManager<ExtendedIdentityRole> _roleManager;

        public IdentityDbSeeder(
            ILogger<IdentityDbSeeder> logger,
            IdentityDbContext db,
            RoleManager<ExtendedIdentityRole> roleManager,
            UserManager<ExtendedIdentityUser> userManager,
            IStringLocalizer<IdentityDbSeeder> localizer)
        {
            _logger = logger;
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _localizer = localizer;
        }

        public void Initialize()
        {
            AddDefaultRoles();
            AddSuperAdmin();
            AddStaff();
            _db.SaveChanges();
        }

        private void AddDefaultRoles()
        {
            Task.Run(async () =>
            {
                foreach (var roleName in Enum.GetNames(typeof(Roles)))
                {
                    var role = new ExtendedIdentityRole(roleName);
                    var roleInDb = await _roleManager.FindByNameAsync(roleName);
                    if (roleInDb == null)
                    {
                        await _roleManager.CreateAsync(role);
                        _logger.LogInformation(string.Format(_localizer["Added '{0}' to Roles"], roleName));
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddSuperAdmin()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new ExtendedIdentityRole(Roles.SuperAdmin.ToString());
                var adminRoleInDb = await _roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                }
                //Check if User Exists
                var superUser = new ExtendedIdentityUser
                {
                    FirstName = "Mukesh",
                    LastName = "Murugan",
                    Email = "superadmin@fluentpos.com",
                    UserName = "superadmin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, Roles.SuperAdmin.ToString());
                    if (result.Succeeded)
                    {
                        //TODO - add permissions

                        _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddStaff()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new ExtendedIdentityRole(Roles.Staff.ToString());
                var basicRoleInDb = await _roleManager.FindByNameAsync(Roles.Staff.ToString());
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                }
                //Check if User Exists
                var basicUser = new ExtendedIdentityUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "staff@fluentpos.com",
                    UserName = "staff",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, Roles.Staff.ToString());
                    _logger.LogInformation(_localizer["Seeded Default Staff."]);
                }
            }).GetAwaiter().GetResult();
        }
    }
}