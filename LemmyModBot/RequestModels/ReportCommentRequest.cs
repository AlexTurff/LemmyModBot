using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    internal class ReportCommentRequest: RequestBase
    {
        public ReportCommentRequest() { }
        public ReportCommentRequest(int commentId, string reason)
        {
            CommentId = commentId;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        public override string OperationRoute => "/comment/report";

        public override HttpMethod Operation => HttpMethod.Post;

        [JsonPropertyName("comment_id")]
        public int CommentId { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
