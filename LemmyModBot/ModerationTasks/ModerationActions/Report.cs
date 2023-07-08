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
    internal class Report : IModerationAction
    {
        private readonly IApiConnection connection;
        private readonly CommunityModTask modTaskDetails;

        public Report(IApiConnection connection, Configuration.CommunityModTask modTaskDetails)
        {
            this.connection = connection;
            this.modTaskDetails = modTaskDetails;
        }

        public void ActionComment(GetCommentsResponse.CommentWrapper comment)
        {
            connection.SendRequest<ReportCommentRequest, ReportCommentResponse>(
                new ReportCommentRequest(comment.CommentData.Id,modTaskDetails.ReportOrRemoveComment));
        }

        public void ActionPost(GetPostsResponse.PostWrapper post)
        {
            connection.SendRequest<ReportPostRequest, ReportPostResponse>(
                new ReportPostRequest(post.PostData.Id, modTaskDetails.ReportOrRemoveComment));
        }
    }
}
