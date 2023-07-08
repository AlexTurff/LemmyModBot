using LemmyModBot.Configuration;
using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks.ModerationActions
{
    internal class MessageUser : IModerationAction
    {
        private readonly IApiConnection connection;
        private readonly CommunityModTask modTaskDetails;

        public MessageUser(IApiConnection connection, Configuration.CommunityModTask modTaskDetails)
        {
            this.connection = connection;
            this.modTaskDetails = modTaskDetails;
        }

        public void ActionComment(GetCommentsResponse.CommentWrapper comment)
        {
            connection.SendRequest<SendPrivateMessageRequest, SendPrivateMessageResponse>(
                new SendPrivateMessageRequest(comment.Creator.Id, $"{modTaskDetails.PrivateMessageContent} Comment: {comment.CommentData.ApId}"));
        }

        public void ActionPost(GetPostsResponse.PostWrapper post)
        {
            connection.SendRequest<SendPrivateMessageRequest, SendPrivateMessageResponse>(
                new SendPrivateMessageRequest(post.Creator.Id, $"{modTaskDetails.PrivateMessageContent} Post: {post.PostData.ApId}"));
        }
    }
}
