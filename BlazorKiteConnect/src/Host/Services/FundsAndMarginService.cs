using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.FundsAndMargin;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Services;

public class FundsAndMarginService : IFundsAndMarginService
{
    private readonly ICurrentUserService _currentUserService;
    private HttpClient _httpClient;
    private HttpRequestMessage request;
    private HttpResponseMessage response;
    private FundsAndMarginResponse responseModel;

    public FundsAndMarginService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
    {
        _currentUserService = currentUserService;
        _httpClient = httpClientFactory.CreateClient();
    }
    public ISendRequest<FundsAndMarginResponse> AddRequiredHeader()
    {
        request.Headers.Add("X-Kite-Version", "3");
        request.Headers.Add("Authorization", $"token {_currentUserService.ApiKey}:{_currentUserService.AccessToken}");
        return this;
    }

    public IPrepareRequestBody<FundsAndMarginResponse> Create(string addtionalParams)
    {
        var nvc = new Dictionary<string, string>
        {
            { "api_key", _currentUserService.ApiKey },
            { "request_token", _currentUserService.RequestToken },
        };
        request = new HttpRequestMessage(HttpMethod.Get, "https://api.kite.trade/user/margins") { Content = new FormUrlEncodedContent(nvc) };
        return this;
    }

    public FundsAndMarginResponse GetResponse()
    {
        return responseModel;
    }

    public IAddRequiredHeader<FundsAndMarginResponse> PrepareRequestBody()
    {
        return this;
    }

    public async Task<IGetResponse<FundsAndMarginResponse>> SendRequestAsync()
    {
        response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            responseModel = await response.Content.ReadFromJsonAsync<FundsAndMarginResponse>();
            return this;
        }
        throw new UnauthorizedAccessException(response.StatusCode.ToString());
    }
}
