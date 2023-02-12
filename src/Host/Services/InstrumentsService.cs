using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.Instruments;
using BlazorKiteConnect.Shared.KiteModel;
using MudBlazor.Charts;

namespace BlazorKiteConnect.Server.Services;

public class InstrumentsService : KiteServiceBase<string, string>, IInstrumentsService
{
    public InstrumentsService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory)
        : base("https://api.kite.trade/instruments", HttpMethod.Get, currentUserService, httpClientFactory)
    {

    }
    public override async Task<IGetResponse<string>> SendRequestAsync()
    {
        response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            responseModel = await response.Content.ReadAsStringAsync();

            var instrumentlist = responseModel.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Skip(1).Select(item =>
            {
                // Split the line into an array of strings
                string[] values = item.Split(',');

                return new Instrument
                {
                    InstrumentToken = int.Parse(values[0]),
                    ExchangeToken = int.Parse(values[1]),
                    TradingSymbol = values[2],
                    Name = values[3].Trim('"'),
                    LastPrice = decimal.Parse(values[4]),
                    Expiry = DateTime.Parse(values[5]),
                    Strike = decimal.Parse(values[6]),
                    TickSize = decimal.Parse(values[7]),
                    LotSize = int.Parse(values[8]),
                    InstrumentType = values[9],
                    Segment = values[10],
                    Exchange = values[11]
                };
            });

            return this;
        }
        throw new UnauthorizedAccessException(response.StatusCode.ToString());
    }
}


