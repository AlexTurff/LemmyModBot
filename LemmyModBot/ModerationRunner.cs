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

        private DateTime LastPostQuery { get; set; }
        private DateTime LastCommentQuery { get; set; }

        public void Run() {
            foreach (var community in moderationTasks.Keys)
            {
                var modTasks = moderationTasks[community];

                LastPostQuery = DateTime.UtcNow;

                var posts = connection.SendRequest<GetPostsRequest, GetPostsResponse>(new ApiOperation<GetPostsRequest>()
                    { Operation = GetPostsRequest.OperationName, Data = new GetPostsRequest(community.CommunityName, 1,40,"New") });

                foreach (var task in modTasks.Where(t=> t.ContentType.HasFlag(UserContentType.Post)))
                {
                    //todo update model to DateTIme?
                    foreach(var post in posts.Posts.Where(p=>DateTime.Parse(p.PostData.Published) >= LastPostQuery))
                    {
                        task.ValidatePost(post);
                    }
                }

                if(modTasks.Any(t => t.ContentType.HasFlag(UserContentType.Comment))){
                    foreach (var post in posts.Posts)
                    {
                        LastCommentQuery = DateTime.UtcNow;

                        var comments = connection.SendRequest<GetCommentsRequest, GetCommentsResponse>(new ApiOperation<GetCommentsRequest>()
                        { Operation = GetCommentsRequest.OperationName, Data = new GetCommentsRequest(post.PostData.Id,20) });

                        foreach (var task in modTasks.Where(t => t.ContentType.HasFlag(UserContentType.Comment)))
                        {
                            foreach (var comment in comments.Comments.Where(p => p.CommentData.Published >= LastCommentQuery))
                            {
                                task.ValidateComment(comment);
                            }
                        }
                    }

                }
            } 
        }
    }
}
