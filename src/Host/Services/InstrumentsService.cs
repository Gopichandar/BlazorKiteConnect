using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.Common;
using BlazorKiteConnect.Server.Application.Interface.Instruments;
using BlazorKiteConnect.Server.Persistence;
using BlazorKiteConnect.Shared.KiteModel;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Charts;

namespace BlazorKiteConnect.Server.Services;

public class InstrumentsService : KiteServiceBase<string, string>, IInstrumentsService
{
    private readonly KiteAppContext _kiteAppContext;

    public InstrumentsService(ICurrentUserService currentUserService, IHttpClientFactory httpClientFactory, KiteAppContext kiteAppContext)
        : base("https://api.kite.trade/instruments", HttpMethod.Get, currentUserService, httpClientFactory)
    {
        _kiteAppContext = kiteAppContext;
    }
    public override async Task<IGetResponse<string>> SendRequestAsync()
    {
        response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            responseModel = await response.Content.ReadAsStringAsync();

            var instrumentlist = responseModel.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(item =>
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
                    Expiry = DateTime.TryParse(values[5], out var expiry) ? expiry : null,
                    Strike = decimal.Parse(values[6]),
                    TickSize = decimal.Parse(values[7]),
                    LotSize = int.Parse(values[8]),
                    InstrumentType = values[9],
                    Segment = values[10],
                    Exchange = values[11]
                };
            });

            await _kiteAppContext.Instruments.ExecuteDeleteAsync();

            await _kiteAppContext.Instruments.AddRangeAsync(instrumentlist);

            await _kiteAppContext.SaveChangesAsync();

            return this;
        }
        throw new UnauthorizedAccessException(response.StatusCode.ToString());
    }
}


