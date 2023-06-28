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
        private  string Community { get; set; }

        [JsonPropertyName("page")]
        private  int Page { get; set; }

        [JsonPropertyName("limit")]
        private  int Limit { get; set; }

        [JsonPropertyName("sort")]
        private  string Sort { get; set; }

        [JsonPropertyName("type")]
        private  string Type { get; set; }

        [JsonPropertyName("saved_only")]
        private  bool SavedOnly { get; set; }
    }
}
