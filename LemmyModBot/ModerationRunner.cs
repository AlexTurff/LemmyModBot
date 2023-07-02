using LemmyModBot.ModerationTasks;
using LemmyModBot.RequestModels;
using LemmyModBot.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot
{
    internal class ModerationRunner
    {
        private WebsocketApiConnection connection;
        private Dictionary<CommunityIdentifier, List<ModerationTaskBase>> moderationTasks;

        public ModerationRunner(WebsocketApiConnection connection, Dictionary<CommunityIdentifier, List<ModerationTaskBase>> moderationTasks)
        {
            this.connection = connection;
            this.moderationTasks = moderationTasks;
        }

        public void Run() {
            foreach (var community in moderationTasks.Keys)
            {
                var modTasks = moderationTasks[community];

                var posts = connection.SendRequest<GetPostsRequest, GetPostsResponse>(new ApiOperation<GetPostsRequest>()
                    { Operation = GetPostsRequest.OperationName, Data = new GetPostsRequest(community.CommunityName, 1,40,"New") });

                //todo validate

                if(modTasks.Any(t => t.ContentType.HasFlag(UserContentType.Comment))){
                    foreach (var post in posts.Posts)
                    {
                        var comments = connection.SendRequest<GetCommentsRequest, GetCommentsResponse>(new ApiOperation<GetCommentsRequest>()
                        { Operation = GetCommentsRequest.OperationName, Data = new GetCommentsRequest(post.PostData.Id,20) });

                        //todo validate
                    }
                   
                }
            } 
        }
    }
}
