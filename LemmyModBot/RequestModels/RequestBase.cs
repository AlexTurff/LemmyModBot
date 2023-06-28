using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public abstract class RequestBase
    {
        [JsonPropertyName("auth")]
        public string Jwt { get; set; }
    }
}
