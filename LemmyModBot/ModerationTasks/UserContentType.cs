namespace LemmyModBot.ModerationTasks
{
    [Flags]
    internal enum UserContentType
    {
        None = 0,
        PostTitle = 1,
        PostBody = 2,
        Comment = 4,
        PostTitlesAndComments = PostTitle | Comment,
        PostBodiesAndComments = PostBody | Comment,
        Posts = PostBody | PostTitle,
    }
}
