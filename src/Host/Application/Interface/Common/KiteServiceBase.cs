using BlazorKiteConnect.Server.Application.Model;
using BlazorKiteConnect.Server.Services;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Application.Interface.Common
{
    public abstract class KiteServiceBase<TRequest, TResponse> : ICompiledKiteApiService<TRequest, TResponse>
    {
        public KiteServiceBase(string url, HttpMethod httpMethod, ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        {
            _url = url;
            _httpMethod = httpMethod;
            _currentUserService = currentUserService;
            _httpClient = httpClientFactory.CreateClient();
        }

        public KiteServiceBase(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        {
            _url = "";
            _httpMethod = HttpMethod.Get;
            _currentUserService = currentUserService;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected readonly string _url;
        protected readonly HttpMethod _httpMethod;
        protected readonly HttpContentType _httpContentType;
        protected readonly ICurrentUserService _currentUserService;
        protected HttpClient _httpClient;
        protected HttpRequestMessage request;
        protected HttpResponseMessage response;
        protected TResponse responseModel;
        public virtual ISendRequest<TResponse> AddRequiredHeader()
        {
            request.Headers.Add("X-Kite-Version", "3");
            request.Headers.Add("Authorization", $"token {_currentUserService.ApiKey}:{_currentUserService.AccessToken}");
            return this;
        }

        public virtual IPrepareRequestBody<TResponse> Create(TRequest addtionalParams)
        {
            var nvc = new Dictionary<string, string>
            {
                { "api_key", _currentUserService.ApiKey },
                { "request_token", _currentUserService.RequestToken },
            };
            // "https://api.kite.trade/user/profile"
            request = new HttpRequestMessage(_httpMethod, _url) { Content = new FormUrlEncodedContent(nvc) };
            return this;
        }

        public virtual TResponse GetResponse()
        {
            return responseModel;
        }

        public virtual IAddRequiredHeader<TResponse> PrepareRequestBody()
        {
            return this;
        }

        public virtual async Task<IGetResponse<TResponse>> SendRequestAsync()
        {
            response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                responseModel = await response.Content.ReadFromJsonAsync<TResponse>();
                return this;
            }
            throw new UnauthorizedAccessException(response.StatusCode.ToString());
        }
    }
}
