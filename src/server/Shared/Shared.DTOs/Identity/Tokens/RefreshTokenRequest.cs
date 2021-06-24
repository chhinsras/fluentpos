namespace FluentPOS.Shared.DTOs.Identity.Tokens
{
    public record RefreshTokenRequest(string Token, string RefreshToken);
}