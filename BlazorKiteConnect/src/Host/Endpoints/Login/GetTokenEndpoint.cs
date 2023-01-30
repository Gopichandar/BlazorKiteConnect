using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Server.KiteModel;
using BlazorKiteConnect.Shared.Constants;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class GetTokenEndpoint : EndpointWithoutRequest
    {
        private readonly ILoginService _loginService;

        public GetTokenEndpoint(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public override void Configure()
        {
            Get("api/login/token");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {           
            string? requestToken = Query<string>("request_token");

            var result = await _loginService.Create(requestToken)                
                .PrepareRequestBody()
                .AddRequiredHeader()
                .SendRequestAsync();

            var message = result.GetResponse();
            
            if (message != null && message.Status == "success")
            {                    
                // validate the user credentials and create a claims identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, message.Data.UserName),
                    new Claim(ClaimTypes.NameIdentifier, message.Data.UserId),
                    new Claim(ClaimTypes.Email, message.Data.Email),
                    new Claim(KiteClaimTypes.ApiKey, message.Data.ApiKey),
                    new Claim(KiteClaimTypes.RequestToken, requestToken),
                    new Claim(KiteClaimTypes.AccessToken, message.Data.AccessToken),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // sign in the user with a cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                await SendAsync(message, 200, ct);
                return;
            }            
            await SendUnauthorizedAsync(ct);
        }
    }    
}
