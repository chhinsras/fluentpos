using System.Collections.Generic;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IIdentityService
    {
        Task<Result<List<UserResponse>>> GetAllAsync();

        Task<IResult<UserResponse>> GetAsync(string userId);

        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult<UserRolesResponse>> GetRolesAsync(string id);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}