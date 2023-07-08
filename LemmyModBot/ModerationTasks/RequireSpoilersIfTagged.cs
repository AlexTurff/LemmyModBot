using LemmyModBot.Configuration;
using LemmyModBot.ModerationTasks.ModerationActions;
using LemmyModBot.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks
{
    internal class RequireSpoilersIfTagged : ModerationTaskBase
    {

        public RequireSpoilersIfTagged(CommunityModTask modTaskDetails)
        {
            Active = modTaskDetails.Active;
            ContentType = modTaskDetails.ParseContentType();
            Actions = modTaskDetails.ParseModerationActions();

            ActionJobs = Actions.Select(a => Program.ModerationActionFactory.GetAction(a, modTaskDetails)).ToList();
            ModTaskDetails = modTaskDetails;
        }

        public override bool Active { get; }

        public override UserContentType ContentType { get; }

        public override List<ModerationAction> Actions { get; }

        public override string Name => "RequireTag";

        private List<IModerationAction> ActionJobs { get; }

        private CommunityModTask ModTaskDetails { get; }

        private static Regex TagRegex = new Regex("(?!\\[No Spoilers\\])(\\[.*\\]).*");
        private static Regex SpoilerMarkupRegex = new Regex(".*:: Spoiler.*:::.*", RegexOptions.Singleline|RegexOptions.IgnoreCase);

        public override void ValidateComment(GetCommentsResponse.CommentWrapper comment)
        {
            // do nothing
        }

        public override void ValidatePost(GetPostsResponse.PostWrapper post)
        {
            if (TagRegex.IsMatch(post.PostData.Name) && !string.IsNullOrWhiteSpace(post.PostData.Body) && !SpoilerMarkupRegex.IsMatch(post.PostData.Body))
            {
                foreach (var action in ActionJobs)
                {
                    action.ActionPost(post);
                }
            }
        }
    }
}
