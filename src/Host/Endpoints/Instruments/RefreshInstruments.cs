using BlazorKiteConnect.Server.Application.Interface.FundsAndMargin;
using BlazorKiteConnect.Server.Application.Interface.Instruments;
using FastEndpoints;

namespace BlazorKiteConnect.Server.Endpoints.Instruments
{
    public class RefreshInstruments : EndpointWithoutRequest
    {
        private readonly IInstrumentsService _instrumentsService;

        public RefreshInstruments(IInstrumentsService instrumentsService)
        {            
            _instrumentsService = instrumentsService;
        }
        public override void Configure()
        {
            Get("api/instruments/refresh");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var result = await _instrumentsService.Create("")
                .PrepareRequestBody()
                .AddRequiredHeader()
                .SendRequestAsync();

            var message = result.GetResponse();
            
            await SendAsync(message, 200, ct);            
        }
    }
}
