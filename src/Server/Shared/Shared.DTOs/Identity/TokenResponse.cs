using System;

namespace FluentPOS.Shared.DTOs.Identity
{
    public record TokenResponse(string token,string refreshToken, DateTime RefreshTokenExpiryTime);
}
