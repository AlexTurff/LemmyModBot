using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    internal class CreateCommentRequest : RequestBase
    {
        public CreateCommentRequest() { }
        public CreateCommentRequest(string content, int postId, int? parentCommentId)
        {
            this.CommentContent = content;
            this.PostId = postId;
            this.ParentCommentId = parentCommentId;
            this.FormGuid = Guid.NewGuid().ToString();
           
        }

        [JsonIgnore]
        public override string OperationRoute => "/comment";

        [JsonIgnore]
        public override HttpMethod Operation => HttpMethod.Post;


        [JsonPropertyName("content")]
        public string CommentContent { get; set; }

        [JsonPropertyName("form_id")]
        public string FormGuid { get; set; }

        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentCommentId { get; set; }
    }
}
