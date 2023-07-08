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
    internal class Remove : IModerationAction
    {
        private readonly IApiConnection connection;
        private readonly CommunityModTask modTaskDetails;

        public Remove(IApiConnection connection, Configuration.CommunityModTask modTaskDetails)
        {
            this.connection = connection;
            this.modTaskDetails = modTaskDetails;
        }

        public void ActionComment(GetCommentsResponse.CommentWrapper comment)
        {
            connection.SendRequest<RemoveCommentRequest, RemoveCommentResponse>(
                new RemoveCommentRequest(comment.CommentData.Id, modTaskDetails.ReportOrRemoveComment));
        }

        public void ActionPost(GetPostsResponse.PostWrapper post)
        {
            connection.SendRequest<RemovePostRequest, RemovePostResponse>(
                new RemovePostRequest(post.PostData.Id, modTaskDetails.ReportOrRemoveComment));
        }
    }
}
