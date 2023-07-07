using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LemmyModBot.RequestModels
{
    internal class SendPrivateMessageRequest: RequestBase
    {
        public SendPrivateMessageRequest() { }
        public SendPrivateMessageRequest(int recipientUserId, string message)
        {
            RecipientUserId = recipientUserId;
            MessageContent = message ?? throw new ArgumentNullException(nameof(message));
        }

        public override string OperationRoute => "/private_message";

        public override HttpMethod Operation => HttpMethod.Post;

        [JsonPropertyName("recipient_id")]
        public int RecipientUserId { get; set; }

        [JsonPropertyName("content")]
        public string MessageContent { get; set; }
    }
}
