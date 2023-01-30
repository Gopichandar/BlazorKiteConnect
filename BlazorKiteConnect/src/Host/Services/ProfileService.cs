using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Profile;
using BlazorKiteConnect.Shared.KiteModel;
using System.Net.Http;

namespace BlazorKiteConnect.Server.Services
{
    public class ProfileService : IProfileService
    {
        public ProfileService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        {
            _currentUserService = currentUserService;
            _httpClient = httpClientFactory.CreateClient();
        }
        
        private readonly ICurrentUserService _currentUserService;
        private HttpClient _httpClient;
        private HttpRequestMessage request;
        private HttpResponseMessage response;
        private GetProfileResponse responseModel;
        public IPrepareRequestBody<GetProfileResponse> Create(string addtionalParam)
        {
            var nvc = new Dictionary<string, string>
            {
                { "api_key", _currentUserService.ApiKey },
                { "request_token", _currentUserService.RequestToken },
            };
            request = new HttpRequestMessage(HttpMethod.Get, "https://api.kite.trade/user/profile") { Content = new FormUrlEncodedContent(nvc) };
            return this;
        }
        public ISendRequest<GetProfileResponse> AddRequiredHeader()
        {
            request.Headers.Add("X-Kite-Version", "3");
            request.Headers.Add("Authorization", $"token {_currentUserService.ApiKey}:{_currentUserService.AccessToken}");
            return this;
        }
       

        public IAddRequiredHeader<GetProfileResponse> PrepareRequestBody()
        {
            return this;
        }

        public async Task<IGetResponse<GetProfileResponse>> SendRequestAsync()
        {
            response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<GetProfileResponse>();
                return this;
            }
            throw new UnauthorizedAccessException(response.StatusCode.ToString());
        }
        public GetProfileResponse GetResponse()
        {
            return responseModel;
        }
    }
}
