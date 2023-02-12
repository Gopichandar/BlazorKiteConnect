using BlazorKiteConnect.Server.Constants;
using BlazorKiteConnect.Shared.ViewModel;
using FastEndpoints;

namespace BlazorKiteConnect.Server.Endpoints.Instruments
{
    public class InstrumentsLut : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("api/instruments/lut");
            //AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            InstrumentsLutResponse message = new InstrumentsLutResponse()
            {
                Status = "success",
                Data = new InstrumentsLutResponseData()
                {
                    Lut = File.GetLastWriteTime(Settings.PersistancePath)
                }
            };
            await SendAsync(message, 200, ct);
        }
    }
}
