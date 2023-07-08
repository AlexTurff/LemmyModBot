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
        public string ReportOrRemoveComment { get; set; }
        public string PrivateMessageContent { get; set; }
        public string RegularExpression { get; set; }
        public string OnlyActionIfPostTitleMatchRegex { get; set; }

        //todo remove enum and just use reflection in Action factory?
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
                    case "Report":
                        parsedActionsList.Add(ModerationAction.Report);
                        break;
                    case "UserMessage":
                    case "MessageUser":
                        parsedActionsList.Add(ModerationAction.UserMessage);
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
                case "PostTitle":
                    return UserContentType.PostTitle;
                case "PostBody":
                    return UserContentType.PostBody;
                case "Comment":
                    return UserContentType.Comment;
                case "PostTitlesAndComments":
                    return UserContentType.PostTitlesAndComments;
                case "PostBodiesAndComments":
                    return UserContentType.PostBodiesAndComments;
                case "Posts":
                    return UserContentType.Posts;
                default:
                    throw new ArgumentException($"{ContentType} is not supported.");
            }
        }
    }
}
