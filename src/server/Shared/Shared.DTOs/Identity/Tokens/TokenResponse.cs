using System;

namespace FluentPOS.Shared.DTOs.Identity.Tokens
{
    public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
}