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
using BlazorKiteConnect.Server.Application.Interface.FundsAndMargin;

namespace BlazorKiteConnect.Server.Endpoints.FundsAndMargin
{
    public class GetProfileEndpoint : EndpointWithoutRequest
    {   
        private readonly IFundsAndMarginService _fundsAndMarginService;

        public GetProfileEndpoint(IFundsAndMarginService fundsAndMarginService)
        {            
            _fundsAndMarginService = fundsAndMarginService;
        }
        public override void Configure()
        {
            Get("api/margins");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _fundsAndMarginService.Create("")
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
