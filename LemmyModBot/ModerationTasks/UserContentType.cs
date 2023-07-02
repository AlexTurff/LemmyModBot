namespace LemmyModBot.ModerationTasks
{
    [Flags]
    internal enum UserContentType
    {
        None = 0,
        Post = 1,
        Comment = 2,
        PostsAndComments = Post|Comment,
    }
}
