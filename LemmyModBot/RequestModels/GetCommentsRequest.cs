using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public class GetCommentsRequest : RequestBase
    {
        public GetCommentsRequest() { }
        public GetCommentsRequest(int postId, int maxDepth, string sort = "New", string type = "All", bool savedOnly = false)
        {
            PostId = postId;
            MaxDepth = maxDepth;
            Sort = sort;
            Type = type;
            SavedOnly = savedOnly;
        }


        [JsonIgnore]
        public static string OperationName = "GetComments";      


        [JsonPropertyName("post_id")]
        public int PostId { get; set; }

        [JsonPropertyName("max_depth")]
        public int MaxDepth { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("saved_only")]
        public bool SavedOnly { get; set; }


    }
}
