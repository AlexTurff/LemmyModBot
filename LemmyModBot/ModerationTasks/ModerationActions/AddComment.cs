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
    internal class AddComment : IModerationAction
    {
        private readonly IApiConnection connection;
        private readonly CommunityModTask modTaskDetails;

        public AddComment(IApiConnection connection, Configuration.CommunityModTask modTaskDetails)
        {
            this.connection = connection;
            this.modTaskDetails = modTaskDetails;
        }

        public void ActionComment(GetCommentsResponse.CommentWrapper comment)
        {
            connection.SendRequest<CreateCommentRequest, CreateCommentResponse>(
                new CreateCommentRequest(modTaskDetails.Comment, comment.Post.Id, comment.CommentData.Id));
        }

        public void ActionPost(GetPostsResponse.PostWrapper post)
        {
            connection.SendRequest<CreateCommentRequest,CreateCommentResponse>(
                new CreateCommentRequest(modTaskDetails.Comment, post.PostData.Id, null));
        }
    } 
}
