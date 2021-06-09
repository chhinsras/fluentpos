namespace FluentPOS.Shared.DTOs.Identity
{
    public record RefreshTokenRequest(string token, string refreshToken);
}
