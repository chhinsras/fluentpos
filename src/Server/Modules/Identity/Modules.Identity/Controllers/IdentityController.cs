using FluentPOS.Modules.Identity.Core.Abstractions;
using FluentPOS.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Controllers
{
    internal class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.RegisterAsync(request, origin));
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] int userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code));
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.ForgotPasswordAsync(request.Email, origin));
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return Ok(await _identityService.ResetPasswordAsync(request));
        }
    }
}