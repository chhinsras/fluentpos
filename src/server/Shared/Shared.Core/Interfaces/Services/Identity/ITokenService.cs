using FluentPOS.Shared.Core.Wrapper;
using System.Threading.Tasks;
using FluentPOS.Shared.DTOs.Identity.Tokens;

namespace FluentPOS.Shared.Core.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }
}