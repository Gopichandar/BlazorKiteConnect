namespace BlazorKiteConnect.Server.Application.Interface
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? Name { get; }
        string? Email { get; }
        string? ApiKey { get; }
        string? RequestToken { get; }
        string? AccessToken { get; }

    }
}
