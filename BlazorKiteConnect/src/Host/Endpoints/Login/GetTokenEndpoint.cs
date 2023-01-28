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

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class GetTokenEndpoint : EndpointWithoutRequest
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<KiteSettings> _kiteSettings;
        private readonly IOptions<AppDetails> _appDetails;

        public GetTokenEndpoint(IHttpClientFactory httpClientFactory, IOptions<KiteSettings> kiteSettings, IOptions<AppDetails> appDetails)
        {
            _httpClient = httpClientFactory.CreateClient();
            _kiteSettings = kiteSettings;
            _appDetails = appDetails;
        }
        public override void Configure()
        {
            Get("api/login/token");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {           
            string? requestToken = Query<string>("request_token");
            var concatenatedString = _appDetails.Value.AppKey + requestToken + _appDetails.Value.AppSecret;
            var sha256 = SHA256.Create();
            var hashedValue = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(concatenatedString));
            StringBuilder hexhash = new StringBuilder();
            foreach (byte b in hashedValue)
            {
                hexhash.Append(b.ToString("x2"));
            }            
            Dictionary<string, string> nvc = new Dictionary<string, string>
            {
                { "api_key", _appDetails.Value.AppKey },
                { "request_token", requestToken },
                { "checksum", hexhash.ToString() }
            };
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, _kiteSettings.Value.GetToken) { Content = new FormUrlEncodedContent(nvc) };
            request.Headers.Add("X-Kite-Version", "3");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadFromJsonAsync<GetTokenReponse>();

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
            }
            await SendUnauthorizedAsync(ct);
        }
    }    
}
