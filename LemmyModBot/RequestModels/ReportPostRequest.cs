using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    internal class ReportPostRequest: RequestBase
    {
        public ReportPostRequest() { }
        public ReportPostRequest(int postId, string reason)
        {
            PostId = postId;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        [JsonIgnore]
        public override string OperationRoute => "/post/report";

        [JsonIgnore]
        public override HttpMethod Operation => HttpMethod.Post;

        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
