using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Shared.Constants;
using System.Security.Claims;

namespace BlazorKiteConnect.Server.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            ApiKey = httpContextAccessor.HttpContext?.User?.FindFirstValue(KiteClaimTypes.ApiKey);
            RequestToken = httpContextAccessor.HttpContext?.User?.FindFirstValue(KiteClaimTypes.RequestToken);
            AccessToken = httpContextAccessor.HttpContext?.User?.FindFirstValue(KiteClaimTypes.AccessToken);
        }

        public string? UserId { get; }
                     
        public string? Name { get; }
                     
        public string? Email { get; }
                     
        public string? ApiKey { get; }
                     
        public string? RequestToken { get; }
                     
        public string? AccessToken { get; }
    }
}
