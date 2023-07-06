using LemmyModBot.ModerationTasks;

namespace LemmyModBot.Configuration
{
    internal class CommunityModTask
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public bool Active { get; set; }
        public List<string> Actions { get; set; }
        public string Comment { get; set; }

        public List<ModerationAction> ParseModerationActions()
        {
            var parsedActionsList = new List<ModerationAction>();

            foreach (var action in Actions)
            {
                switch (action)
                {
                    case "AddComment":
                        parsedActionsList.Add(ModerationAction.Comment);
                        break;
                    case "Remove":
                        parsedActionsList.Add(ModerationAction.Remove);
                        break;
                    default:
                        throw new ArgumentException($"{action} is not supported.");
                }
            }

            return parsedActionsList;
        }

        public UserContentType ParseContentType()
        {
            switch (ContentType)
            {
                case "Post":
                    return UserContentType.Post;
                case "Comment":
                    return UserContentType.Comment;
                case "PostsAndComments":
                    return UserContentType.PostsAndComments;
                default:
                    throw new ArgumentException($"{ContentType} is not supported.");
            }
        }
    }
}
