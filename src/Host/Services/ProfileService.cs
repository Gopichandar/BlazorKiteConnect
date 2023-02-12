using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.Profile;
using BlazorKiteConnect.Shared.KiteModel;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Net.Http;

namespace BlazorKiteConnect.Server.Services
{
    public class ProfileService : KiteServiceBase<string, GetProfileResponse>, IProfileService
    {
        public ProfileService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
             : base("https://api.kite.trade/user/profile", System.Net.Http.HttpMethod.Get, currentUserService, httpClientFactory)
        {
            
        }
    }
}
