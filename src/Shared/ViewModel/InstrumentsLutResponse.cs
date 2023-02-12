using BlazorKiteConnect.Shared.KiteModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorKiteConnect.Shared.ViewModel
{  
    public class InstrumentsLutResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public InstrumentsLutResponseData Data { get; set; }
    }

    public class InstrumentsLutResponseData
    {
        [JsonPropertyName("lut")]
        public DateTime Lut { get; set; }
    }   
}
