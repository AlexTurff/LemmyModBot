using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public class GetPostsRequest : RequestBase
    {
        public GetPostsRequest() { }
        public GetPostsRequest(string community, int page, int limit = 40, string sort = "Active", string type = "All", bool savedOnly = false)
        {
            this.Community = community;
            this.Page = page;
            this.Limit = limit;
            this.Sort = sort;
            this.Type = type;
            this.SavedOnly = savedOnly;
        }


        [JsonIgnore]
        public static string OperationName = "GetPosts";      


        [JsonPropertyName("community_name")]
        public string Community { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("saved_only")]
        public bool SavedOnly { get; set; }
    }
}
