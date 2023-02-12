using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.KiteModel;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace BlazorKiteConnect.Server.Services
{
    public class LoginService : KiteServiceBase<string, GetTokenReponse>, ILoginService
    {
        private readonly IOptions<KiteSettings> _kiteSettings;
        private readonly IOptions<AppDetails> _appDetails;

        private string requestToken;
        private string hexhashString;

        public LoginService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory, IOptions<KiteSettings> kiteSettings, IOptions<AppDetails> appDetails)
            : base(currentUserService, httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _kiteSettings = kiteSettings;
            _appDetails = appDetails;
        }

        public override IPrepareRequestBody<GetTokenReponse> Create(string requestToken)
        {
            this.requestToken = requestToken;
            PrepareChecksum();
            Dictionary<string, string> nvc = new Dictionary<string, string>
            {
                { "api_key", _appDetails.Value.AppKey },
                { "request_token", requestToken },
                { "checksum", hexhashString }
            };
            request = new HttpRequestMessage(HttpMethod.Post, _kiteSettings.Value.GetToken) { Content = new FormUrlEncodedContent(nvc) };
            return this;
                    }

        public override IAddRequiredHeader<GetTokenReponse> PrepareRequestBody()
        {
            return this;
        }       

        private void PrepareChecksum()
        {
            var concatenatedString = _appDetails.Value.AppKey + requestToken + _appDetails.Value.AppSecret;
            using var sha256 = SHA256.Create();
            var hashedValue = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(concatenatedString));
            StringBuilder hexhash = new StringBuilder();
            foreach (byte b in hashedValue)
            {
                hexhash.Append(b.ToString("x2"));
            }
            hexhashString = hexhash.ToString();            
        }    
    }
}
