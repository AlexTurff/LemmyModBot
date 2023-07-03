using LemmyModBot.ResponseModels;

namespace LemmyModBot.ModerationTasks
{
    internal abstract class ModerationTaskBase
    {
        public abstract string Name { get; }
        public abstract  bool Active { get; }
        public abstract UserContentType ContentType { get; }
        public abstract List<ModerationAction> Actions { get; }

        public abstract void ValidatePost(GetPostsResponse.PostWrapper postr);
        public abstract void ValidateComment(GetCommentsResponse.CommentWrapper comment);
    }
}
