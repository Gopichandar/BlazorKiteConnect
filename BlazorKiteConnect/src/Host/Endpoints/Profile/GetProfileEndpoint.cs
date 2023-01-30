using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.KiteModel;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorKiteConnect.Shared.Constants;
using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Shared.KiteModel;
using BlazorKiteConnect.Server.Application.Interface.Profile;

namespace BlazorKiteConnect.Server.Endpoints.Login
{
    public class GetProfileEndpoint : EndpointWithoutRequest
    {        
        private readonly IProfileService _profileService;

        public GetProfileEndpoint(IProfileService profileService)
        {
            _profileService = profileService;
        }
        public override void Configure()
        {
            Get("api/profile");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _profileService.Create("")
                .PrepareRequestBody()
                .AddRequiredHeader()
                .SendRequestAsync();

            var message = result.GetResponse();
                
            if (message != null && message.Status == "success")
            {  
                await SendAsync(message, 200, ct);
                return;
            }           
            await SendUnauthorizedAsync(ct);
        }
    }    
}
