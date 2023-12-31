﻿using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public class GetPostCommentsRequest : RequestBase
    {
        public GetPostCommentsRequest() { }
        public GetPostCommentsRequest(int postId, int maxDepth, string sort = "New", string type = "All")
        {
            PostId = postId;
            MaxDepth = maxDepth;
            Sort = sort;
            Type = type;
            //SavedOnly = savedOnly;
        }


        [JsonIgnore]
        public override string OperationRoute => "/comment/list";

        [JsonIgnore]
        public override HttpMethod Operation => HttpMethod.Get;


        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("max_depth")]
        public int MaxDepth { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        //[JsonPropertyName("saved_only")]
        //public bool SavedOnly { get; set; }


    }
}
