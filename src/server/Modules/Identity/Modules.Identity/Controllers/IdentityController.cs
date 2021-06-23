using FluentPOS.Modules.Identity.Core.Abstractions;
using FluentPOS.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Constants;

namespace FluentPOS.Modules.Identity.Controllers
{
    internal class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _identityService.GetAllAsync();
            return Ok(users);
        }

        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _identityService.GetAsync(id);
            return Ok(user);
        }

        [Authorize(Policy = Permissions.Users.View)]
        [HttpGet("roles/{id}")]
        public async Task<IActionResult> GetRolesAsync(string id)
        {
            var userRoles = await _identityService.GetRolesAsync(id);
            return Ok(userRoles);
        }

        [AllowAnonymous]
        [HttpPost("/api/identity/register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.RegisterAsync(request, origin));
        }

        [HttpGet("/api/identity/confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code));
        }

        [HttpPost("/api/identity/forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.ForgotPasswordAsync(request, origin));
        }

        [HttpPost("/api/identity/reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _identityService.ResetPasswordAsync(request));
        }
    }
}