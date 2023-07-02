using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static LemmyModBot.ResponseModels.GetPostsResponse;

namespace LemmyModBot.ResponseModels
{
    internal partial class GetCommentsResponse
    {
        [JsonPropertyName("comments")]
        public List<CommentWrapper> Comments { get; set; }

        public class CommentWrapper
        {
            [JsonPropertyName("comment")]
            public Comment CommentData { get; set; }

            [JsonPropertyName("creator")]
            public Creator Creator { get; set; }

            [JsonPropertyName("post")]
            public Post Post { get; set; }

            [JsonPropertyName("community")]
            public Community Community { get; set; }

            [JsonPropertyName("counts")]
            public Counts Counts { get; set; }

            [JsonPropertyName("creator_banned_from_community")]
            public bool CreatorBannedFromCommunity { get; set; }

            [JsonPropertyName("subscribed")]
            public string Subscribed { get; set; }

            [JsonPropertyName("saved")]
            public bool Saved { get; set; }

            [JsonPropertyName("creator_blocked")]
            public bool CreatorBlocked { get; set; }

            [JsonPropertyName("my_vote")]
            public object MyVote { get; set; }
        }

        public class Comment
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("creator_id")]
            public int CreatorId { get; set; }

            [JsonPropertyName("post_id")]
            public int PostId { get; set; }

            [JsonPropertyName("content")]
            public string Content { get; set; }

            [JsonPropertyName("removed")]
            public bool Removed { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("updated")]
            public object Updated { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("ap_id")]
            public string ApId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("path")]
            public string Path { get; set; }

            [JsonPropertyName("distinguished")]
            public bool Distinguished { get; set; }

            [JsonPropertyName("language_id")]
            public int LanguageId { get; set; }
        }

        public class Creator
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("display_name")]
            public object DisplayName { get; set; }

            [JsonPropertyName("avatar")]
            public string Avatar { get; set; }

            [JsonPropertyName("banned")]
            public bool Banned { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("updated")]
            public object Updated { get; set; }

            [JsonPropertyName("actor_id")]
            public string ActorId { get; set; }

            [JsonPropertyName("bio")]
            public object Bio { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("banner")]
            public object Banner { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("inbox_url")]
            public string InboxUrl { get; set; }

            [JsonPropertyName("shared_inbox_url")]
            public string SharedInboxUrl { get; set; }

            [JsonPropertyName("matrix_user_id")]
            public object MatrixUserId { get; set; }

            [JsonPropertyName("admin")]
            public bool Admin { get; set; }

            [JsonPropertyName("bot_account")]
            public bool BotAccount { get; set; }

            [JsonPropertyName("ban_record")]
            public object BanRecord { get; set; }
        }

        public class Post
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("body")]
            public string Body { get; set; }

            [JsonPropertyName("creator_id")]
            public int CreatorId { get; set; }

            [JsonPropertyName("community_id")]
            public int CommunityId { get; set; }

            [JsonPropertyName("removed")]
            public bool Removed { get; set; }

            [JsonPropertyName("locked")]
            public bool Locked { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("updated")]
            public DateTime Updated { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("nsfw")]
            public bool Nsfw { get; set; }

            [JsonPropertyName("embed_title")]
            public object EmbedTitle { get; set; }

            [JsonPropertyName("embed_description")]
            public object EmbedDescription { get; set; }

            [JsonPropertyName("embed_video_url")]
            public object EmbedVideoUrl { get; set; }

            [JsonPropertyName("thumbnail_url")]
            public string ThumbnailUrl { get; set; }

            [JsonPropertyName("ap_id")]
            public string ApId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("language_id")]
            public int LanguageId { get; set; }

            [JsonPropertyName("featured_community")]
            public bool FeaturedCommunity { get; set; }

            [JsonPropertyName("featured_local")]
            public bool FeaturedLocal { get; set; }
        }

        public class Community
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("removed")]
            public bool Removed { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("updated")]
            public DateTime Updated { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("nsfw")]
            public bool Nsfw { get; set; }

            [JsonPropertyName("actor_id")]
            public string ActorId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("icon")]
            public object Icon { get; set; }

            [JsonPropertyName("banner")]
            public object Banner { get; set; }

            [JsonPropertyName("hidden")]
            public bool Hidden { get; set; }

            [JsonPropertyName("posting_restricted_to_mods")]
            public bool PostingRestrictedToMods { get; set; }

            [JsonPropertyName("instance_id")]
            public int InstanceId { get; set; }
        }

        public class Counts
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("post_id")]
            public int PostId { get; set; }

            [JsonPropertyName("comments")]
            public int Comments { get; set; }

            [JsonPropertyName("score")]
            public int Score { get; set; }

            [JsonPropertyName("upvotes")]
            public int Upvotes { get; set; }

            [JsonPropertyName("downvotes")]
            public int Downvotes { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("newest_comment_time_necro")]
            public DateTime NewestCommentTimeNecro { get; set; }

            [JsonPropertyName("newest_comment_time")]
            public DateTime NewestCommentTime { get; set; }

            [JsonPropertyName("featured_community")]
            public bool FeaturedCommunity { get; set; }

            [JsonPropertyName("featured_local")]
            public bool FeaturedLocal { get; set; }

            [JsonPropertyName("hot_rank")]
            public int HotRank { get; set; }

            [JsonPropertyName("hot_rank_active")]
            public int HotRankActive { get; set; }
        }
    }
}
