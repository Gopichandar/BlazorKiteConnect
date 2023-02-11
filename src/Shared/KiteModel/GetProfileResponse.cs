using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorKiteConnect.Shared.KiteModel
{  

    public class GetProfileResponseData
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("user_type")]
        public string UserType { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("user_shortname")]
        public string UserShortname { get; set; }

        [JsonPropertyName("broker")]
        public string Broker { get; set; }

        [JsonPropertyName("exchanges")]
        public List<string> Exchanges { get; set; }

        [JsonPropertyName("products")]
        public List<string> Products { get; set; }

        [JsonPropertyName("order_types")]
        public List<string> OrderTypes { get; set; }

        [JsonPropertyName("avatar_url")]
        public object AvatarUrl { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("demat_consent")]
        public string DematConsent { get; set; }
    }

    public class GetProfileResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public GetProfileResponseData Data { get; set; }
    }
}
