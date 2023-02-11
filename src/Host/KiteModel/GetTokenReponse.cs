using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorKiteConnect.Server.KiteModel
{
    public class GetTokenResponseMetadata
    {
        [JsonPropertyName("demat_consent")]
        public string DematConsent { get; set; }
    }

    public class GetTokenReponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public GetTokenReponseData Data { get; set; }
    }

    public class GetTokenReponseData
    {
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
        public string AvatarUrl { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("public_token")]
        public string PublicToken { get; set; }

        [JsonPropertyName("enctoken")]
        public string Enctoken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("silo")]
        public string Silo { get; set; }

        [JsonPropertyName("login_time")]
        public string LoginTime { get; set; }

        [JsonPropertyName("meta")]
        public GetTokenResponseMetadata Meta { get; set; }
    }

  


}
