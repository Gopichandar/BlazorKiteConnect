

namespace BlazorKiteConnect.Shared.KiteModel;

public class Instrument
{
    public int InstrumentToken { get; set; }
    public int ExchangeToken { get; set; }
    public string TradingSymbol { get; set; }
    public string Name { get; set; }
    public decimal LastPrice { get; set; }
    public DateTime Expiry { get; set; }
    public decimal Strike { get; set; }
    public decimal TickSize { get; set; }
    public int LotSize { get; set; }
    public string InstrumentType { get; set; }
    public string Segment { get; set; }
    public string Exchange { get; set; }

}
