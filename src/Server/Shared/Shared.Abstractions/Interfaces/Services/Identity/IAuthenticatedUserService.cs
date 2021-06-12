namespace FluentPOS.Shared.Abstractions.Interfaces.Services.Identity
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}