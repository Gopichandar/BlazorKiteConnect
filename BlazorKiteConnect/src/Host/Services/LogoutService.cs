using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Services
{
    public class LogoutService : ILogoutService
    {
        public LogoutService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        {
            _currentUserService = currentUserService;
            _httpClient = httpClientFactory.CreateClient();
        }
        
        private readonly ICurrentUserService _currentUserService;
        private HttpClient _httpClient;
        private HttpRequestMessage request;
        private HttpResponseMessage response;
        private LogoutResponse responseModel;
        public IPrepareRequestBody<LogoutResponse> Create(string addtionalParam)
        {
            var nvc = new Dictionary<string, string>
            {
                { "api_key", _currentUserService.ApiKey },
                { "access_token", _currentUserService.AccessToken },
            };
            request = new HttpRequestMessage(HttpMethod.Delete, "https://api.kite.trade/session/token?" + new FormUrlEncodedContent(nvc).ReadAsStringAsync().Result);
            return this;
        }
        public ISendRequest<LogoutResponse> AddRequiredHeader()
        {
            request.Headers.Add("X-Kite-Version", "3");            
            return this;
        }
       

        public IAddRequiredHeader<LogoutResponse> PrepareRequestBody()
        {
            return this;
        }

        public async Task<IGetResponse<LogoutResponse>> SendRequestAsync()
        {
            response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<LogoutResponse>();
                return this;
            }
            throw new UnauthorizedAccessException(response.StatusCode.ToString());
        }
        public LogoutResponse GetResponse()
        {
            return responseModel;
        }
    }
}
