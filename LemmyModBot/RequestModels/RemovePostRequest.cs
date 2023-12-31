﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    internal class RemovePostRequest: RequestBase
    {
        public RemovePostRequest() { }
        public RemovePostRequest(int postId, string reason)
        {
            PostId = postId;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            Remove = true;
        }

        [JsonIgnore]
        public override string OperationRoute => "/post/remove";

        [JsonIgnore]
        public override HttpMethod Operation => HttpMethod.Post;

        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("removed")]
        public bool Remove { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
