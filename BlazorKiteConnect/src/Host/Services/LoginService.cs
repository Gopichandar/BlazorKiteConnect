using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.KiteModel;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace BlazorKiteConnect.Server.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<KiteSettings> _kiteSettings;
        private readonly IOptions<AppDetails> _appDetails;

        private string requestToken;
        private string hexhashString;
        private HttpRequestMessage httpRequestMessage;
        private GetTokenReponse responseModel;

        public LoginService(IHttpClientFactory httpClientFactory, IOptions<KiteSettings> kiteSettings, IOptions<AppDetails> appDetails)
        {
            _httpClient = httpClientFactory.CreateClient();
            _kiteSettings = kiteSettings;
            _appDetails = appDetails;
        }

        public IPrepareRequestBody<GetTokenReponse> Create(string requestToken)
        {
            this.requestToken = requestToken;
            return this;
        }

        public IAddRequiredHeader<GetTokenReponse> PrepareRequestBody()
        {
            string checksumValue = PrepareChecksum();
            Dictionary<string, string> nvc = new Dictionary<string, string>
            {
                { "api_key", _appDetails.Value.AppKey },
                { "request_token", requestToken },
                { "checksum", hexhashString }
            };
            httpRequestMessage = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, _kiteSettings.Value.GetToken) { Content = new FormUrlEncodedContent(nvc) };
            return this;
        }

        public ISendRequest<GetTokenReponse> AddRequiredHeader()
        {
            httpRequestMessage.Headers.Add("X-Kite-Version", "3");
            return this;
        }

        public async Task<IGetResponse<GetTokenReponse>> SendRequestAsync()
        {
            var response = await _httpClient.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GetTokenReponse>();
                return this;
            }
            throw new UnauthorizedAccessException(response.StatusCode.ToString());
        }

        public GetTokenReponse GetResponse()
        {
            return responseModel;
        }

        private string PrepareChecksum()
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
            return hexhashString;
        }    
    }
}
