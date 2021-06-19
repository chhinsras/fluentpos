using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Core.Abstractions
{
    public interface IIdentityService
    {
        Task<IResult> RegisterAsync(RegisterRequest request, string origin);

        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);

        Task<IResult> ForgotPasswordAsync(string emailId, string origin);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);
    }
}