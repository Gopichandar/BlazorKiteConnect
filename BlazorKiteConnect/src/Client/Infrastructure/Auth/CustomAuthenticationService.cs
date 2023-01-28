using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Text.Json;


namespace BlazorKiteConnect.Client.Infrastructure.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(claimsPrincipal);
    }

    public void SetAuthInfo(string email, string name)
    {
        var identity = new ClaimsIdentity(new[]{
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Name, name),
        new Claim("UserId", email)
            }, "AuthCookie");
        claimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

