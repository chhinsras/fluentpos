using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenResponse>> GetTokenAsync(TokenRequest model, string ipAddress);

        Task<IResult<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest model, string ipAddress);
    }
}