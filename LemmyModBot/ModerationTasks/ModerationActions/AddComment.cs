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
        private IApiConnection connection;

        public AddComment(IApiConnection connection)
        {
            this.connection = connection;
        }

        public void ActionComment(GetCommentsResponse.CommentWrapper comment)
        {
            throw new NotImplementedException();
        }

        public void ActionPost(GetPostsResponse.PostWrapper post)
        {
            connection.SendRequest<CreateCommentRequest, CreateCommentResponse>(new ApiOperation<CreateCommentRequest>()
            {
                Operation = CreateCommentRequest.OperationName,
                Data = new CreateCommentRequest("Please can you edit your port title to add a [Tag] that indicates the spoiler range for the post", post.PostData.Id, null)
            });
        }
    } 
}
