using BlazorKiteConnect.Server.Configuration;
using FastEndpoints;
using Microsoft.Extensions.Options;

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class AuthorizeEndpoint : EndpointWithoutRequest
    {
        private readonly IOptions<KiteSettings> _kiteSettings;
        private readonly IOptions<AppDetails> _appDetails;

        public AuthorizeEndpoint(IOptions<KiteSettings> kiteSettings, IOptions<AppDetails> appDetails)
        {
            _kiteSettings = kiteSettings;
            _appDetails = appDetails;
        }

        public override void Configure()
        {
            Get("/api/login/authorize");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await SendRedirectAsync(_kiteSettings.Value.AuthorizeUrl + _appDetails.Value.AppKey);
        }
    }
}
