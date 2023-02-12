using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Services
{
    public class LogoutService : KiteServiceBase<string, LogoutResponse>, ILogoutService
    {
        public LogoutService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
            : base(currentUserService, httpClientFactory)

        {
            
        }        
        
        public override IPrepareRequestBody<LogoutResponse> Create(string addtionalParam)
        {
            var nvc = new Dictionary<string, string>
            {
                { "api_key", _currentUserService.ApiKey },
                { "access_token", _currentUserService.AccessToken },
            };
            request = new HttpRequestMessage(HttpMethod.Delete, "https://api.kite.trade/session/token?" + new FormUrlEncodedContent(nvc).ReadAsStringAsync().Result);
            return this;
        }
    }
}
