using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.ResponseModels
{
    internal class SendPrivateMessageResponse
    {
        [JsonPropertyName("private_message_view")]

        public PrivateMessageView PrivateMessageViewObject { get; set; }

        public class PrivateMessageView
        {
            [JsonPropertyName("private_message")]
            public PrivateMessage PrivateMessage { get; set; }

            [JsonPropertyName("creator")]
            public Creator Creator { get; set; }

            [JsonPropertyName("recipient")]
            public Recipient Recipient { get; set; }
        }

        public class PrivateMessage
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("creator_id")]
            public int CreatorId { get; set; }

            [JsonPropertyName("recipient_id")]
            public int RecipientId { get; set; }

            [JsonPropertyName("content")]
            public string Content { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("read")]
            public bool Read { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("ap_id")]
            public string ApId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }
        }

        public class Creator
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("banned")]
            public bool Banned { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("actor_id")]
            public string ActorId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("admin")]
            public bool Admin { get; set; }

            [JsonPropertyName("bot_account")]
            public bool BotAccount { get; set; }

            [JsonPropertyName("instance_id")]
            public int InstanceId { get; set; }
        }

        public class Recipient
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("avatar")]
            public string Avatar { get; set; }

            [JsonPropertyName("banned")]
            public bool Banned { get; set; }

            [JsonPropertyName("published")]
            public DateTime Published { get; set; }

            [JsonPropertyName("actor_id")]
            public string ActorId { get; set; }

            [JsonPropertyName("local")]
            public bool Local { get; set; }

            [JsonPropertyName("deleted")]
            public bool Deleted { get; set; }

            [JsonPropertyName("admin")]
            public bool Admin { get; set; }

            [JsonPropertyName("bot_account")]
            public bool BotAccount { get; set; }

            [JsonPropertyName("instance_id")]
            public int InstanceId { get; set; }
        }
    }
}
