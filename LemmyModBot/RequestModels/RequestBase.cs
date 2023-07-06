using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public abstract class RequestBase
    {
        [JsonPropertyName("auth")]
        public string Jwt { get; set; }

        [JsonIgnore]
        public abstract string OperationRoute { get; }

        [JsonIgnore]
        public abstract HttpMethod Operation { get; }
    }
}
