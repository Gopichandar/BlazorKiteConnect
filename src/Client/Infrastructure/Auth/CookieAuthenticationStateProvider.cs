using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Client.Infrastructure.Auth;
public class CookieAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;

    public CookieAuthenticationStateProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await GetCurrentUserAsync();
        return user is null
            ? new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))
            : new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(user.Claims, "Cookies")));
    }

    public async Task MarkUserAsAuthenticated(string email)
    {
        var user = await GetCurrentUserAsync();
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(principal));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<ClaimsPrincipal> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync("api/profile");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<GetProfileResponse>();
            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {  
                new Claim(ClaimTypes.NameIdentifier, user.Data.UserId),
                new Claim(ClaimTypes.Email, user.Data.Email),

            }, "Cookies"));
        }

        return null;
    }
}
