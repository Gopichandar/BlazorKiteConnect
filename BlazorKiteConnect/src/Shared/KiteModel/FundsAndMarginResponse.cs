using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorKiteConnect.Shared.KiteModel
{
    public class FundsAndMarginResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public FundsAndMarginResponseData Data { get; set; }
    }
    
    public class FundsAndMarginResponseData
    {
        [JsonPropertyName("equity")]
        public SegmentBalance Equity { get; set; }

        [JsonPropertyName("commodity")]
        public SegmentBalance Commodity { get; set; }
    }

    public class SegmentBalance
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("net")]
        public double Net { get; set; }

        [JsonPropertyName("available")]
        public Available Available { get; set; }

        [JsonPropertyName("utilised")]
        public Utilised Utilised { get; set; }
    }

    public class Available
    {
        [JsonPropertyName("adhoc_margin")]
        public int AdhocMargin { get; set; }

        [JsonPropertyName("cash")]
        public double Cash { get; set; }

        [JsonPropertyName("opening_balance")]
        public double OpeningBalance { get; set; }

        [JsonPropertyName("live_balance")]
        public double LiveBalance { get; set; }

        [JsonPropertyName("collateral")]
        public int Collateral { get; set; }

        [JsonPropertyName("intraday_payin")]
        public int IntradayPayin { get; set; }
    }

    public class Utilised
    {
        [JsonPropertyName("debits")]
        public double Debits { get; set; }

        [JsonPropertyName("exposure")]
        public double Exposure { get; set; }

        [JsonPropertyName("m2m_realised")]
        public double M2mRealised { get; set; }

        [JsonPropertyName("m2m_unrealised")]
        public int M2mUnrealised { get; set; }

        [JsonPropertyName("option_premium")]
        public int OptionPremium { get; set; }

        [JsonPropertyName("payout")]
        public int Payout { get; set; }

        [JsonPropertyName("span")]
        public int Span { get; set; }

        [JsonPropertyName("holding_sales")]
        public int HoldingSales { get; set; }

        [JsonPropertyName("turnover")]
        public int Turnover { get; set; }

        [JsonPropertyName("liquid_collateral")]
        public int LiquidCollateral { get; set; }

        [JsonPropertyName("stock_collateral")]
        public int StockCollateral { get; set; }

        [JsonPropertyName("delivery")]
        public int Delivery { get; set; }
    }


}
