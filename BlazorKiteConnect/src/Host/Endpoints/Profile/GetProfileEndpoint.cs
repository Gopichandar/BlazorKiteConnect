using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.KiteModel;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorKiteConnect.Shared.Constants;
using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class GetProfileEndpoint : EndpointWithoutRequest
    {
        private readonly HttpClient _httpClient;        
        private readonly ICurrentUserService _currentUserService;

        public GetProfileEndpoint(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
        {
            _httpClient = httpClientFactory.CreateClient();            
            _currentUserService = currentUserService;
        }
        public override void Configure()
        {
            Get("api/profile");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (!User.Identity.IsAuthenticated)
            {
                await SendUnauthorizedAsync();
                return;
            }

            Dictionary<string, string> nvc = new Dictionary<string, string>
            {
                { "api_key", _currentUserService.ApiKey },
                { "request_token", _currentUserService.RequestToken },                
            };
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://api.kite.trade/user/profile") { Content = new FormUrlEncodedContent(nvc) };
            request.Headers.Add("X-Kite-Version", "3");
            request.Headers.Add("Authorization", $"token {_currentUserService.ApiKey}:{_currentUserService.AccessToken}");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<GetProfileResponse>();

                if (message != null && message.Status == "success")
                {  
                    await SendAsync(message, 200, ct);
                    return;
                }
            }
            await SendUnauthorizedAsync(ct);
        }
    }    
}
