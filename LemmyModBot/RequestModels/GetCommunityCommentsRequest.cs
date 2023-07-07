using System.Text.Json.Serialization;

namespace LemmyModBot.RequestModels
{
    public class GetCommunityCommentsRequest : RequestBase
    {
        public GetCommunityCommentsRequest() { }
        public GetCommunityCommentsRequest(string communityName, int page, string sort = "New", string type = "All", int commentsPerPage = 40)
        {
            Sort = sort;
            Type = type;
            CommunityName = communityName;
            Page = page;
            CommentsPerPage = commentsPerPage;
            //SavedOnly = savedOnly;
        }


        public override string OperationRoute => "/comment/list";

        public override HttpMethod Operation => HttpMethod.Get;


        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("limit")]
        public int CommentsPerPage { get; set; }

        [JsonPropertyName("sort")]
        public string Sort { get; set; }

        [JsonPropertyName("type_")]
        public string Type { get; set; }

        [JsonPropertyName("community_name")]
        public string CommunityName { get; set; }

        //[JsonPropertyName("saved_only")]
        //public bool SavedOnly { get; set; }


    }
}
