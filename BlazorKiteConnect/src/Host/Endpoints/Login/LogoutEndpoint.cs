using BlazorKiteConnect.Server.Application.Interface.Login;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class LogoutEndpoint : EndpointWithoutRequest
    {
        private readonly ILogoutService _logoutService;

        public LogoutEndpoint(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }
        public override void Configure()
        {
            Delete("api/logout");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _logoutService.Create("")
                .PrepareRequestBody()
                .AddRequiredHeader()
                .SendRequestAsync();

            var message = result.GetResponse();

            if (message != null && message.Status == "success")
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);                    
                await SendAsync(message, 200, ct);
                return;
            }
            await SendUnauthorizedAsync(ct);
        }
    }
}
