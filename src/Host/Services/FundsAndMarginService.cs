using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.FundsAndMargin;
using BlazorKiteConnect.Shared.KiteModel;

namespace BlazorKiteConnect.Server.Services;

public class FundsAndMarginService : KiteServiceBase<string, FundsAndMarginResponse>, IFundsAndMarginService
{
    public FundsAndMarginService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        : base("https://api.kite.trade/user/margins", HttpMethod.Get, currentUserService, httpClientFactory)
    {
        
    }    
}
