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
        private HttpApiConnection connection;
        private Dictionary<CommunityIdentifier, List<ModerationTaskBase>> moderationTasks;

        public ModerationRunner(HttpApiConnection connection, Dictionary<CommunityIdentifier, List<ModerationTaskBase>> moderationTasks)
        {
            this.connection = connection;
            this.moderationTasks = moderationTasks;

            LastPostQueryPerCommunity = new Dictionary<string, DateTime>();
            LastCommentQueryPerCommunity = new Dictionary<string, DateTime>();

            foreach (var community in moderationTasks.Keys)
            {
                LastPostQueryPerCommunity.Add(community.CommunityName, DateTime.UtcNow);
                LastCommentQueryPerCommunity.Add(community.CommunityName, DateTime.UtcNow);
            }
        }

        private Dictionary<string, DateTime> LastPostQueryPerCommunity { get; set; }
        private Dictionary<string, DateTime> LastCommentQueryPerCommunity { get; set; }

        public void Run()
        {
            foreach (var community in moderationTasks.Keys)
            {
                var modTasks = moderationTasks[community];

                var postQueryComparisonTime = LastPostQueryPerCommunity[community.CommunityName];
                LastPostQueryPerCommunity[community.CommunityName] = DateTime.UtcNow;
                var posts = connection.SendRequest<GetPostsRequest, GetPostsResponse>(new GetPostsRequest(community.CommunityName, 1, 40, "New"));

                foreach (var task in modTasks.Where(t => t.ContentType.HasFlag(UserContentType.PostBody) || t.ContentType.HasFlag(UserContentType.PostTitle)))
                {
                    //todo update model to DateTIme?
                    foreach (var post in posts.Posts.Where(p => DateTime.Parse(p.PostData.Published) >= postQueryComparisonTime && p.Creator.Name != connection.Username))
                    {
                        task.ValidatePost(post);
                    }
                }

                if (modTasks.Any(t => t.ContentType.HasFlag(UserContentType.Comment)))
                {
                    var commentQueryComparisonTime = LastCommentQueryPerCommunity[community.CommunityName];
                    LastCommentQueryPerCommunity[community.CommunityName] = DateTime.UtcNow;

                    var comments = new List<GetCommentsResponse.CommentWrapper>();
                    var commentsResponse = connection.SendRequest<GetCommunityCommentsRequest, GetCommentsResponse>(new GetCommunityCommentsRequest(community.CommunityName, 1, "New"));
                    comments.AddRange(commentsResponse.Comments.Where(c =>  c.Creator.Name != connection.Username));

                    int counter = 2;
                    while (comments.Last().CommentData.Published >= commentQueryComparisonTime)
                    {
                        commentsResponse = connection.SendRequest<GetCommunityCommentsRequest, GetCommentsResponse>(new GetCommunityCommentsRequest(community.CommunityName, counter, "New"));
                        comments.AddRange(commentsResponse.Comments.Where(c => c.Creator.Name != connection.Username));

                        if(commentsResponse.Comments.Count(c => c.Creator.Name != connection.Username) == 0)
                        {
                            break;
                        }
                    }
                    
                    foreach (var task in modTasks.Where(t => t.ContentType.HasFlag(UserContentType.Comment)))
                    {
                        foreach (var comment in comments.Where(p => p.CommentData.Published >= commentQueryComparisonTime))
                        {
                            task.ValidateComment(comment);
                        }
                    }
                }
            }
        }
    }
}
