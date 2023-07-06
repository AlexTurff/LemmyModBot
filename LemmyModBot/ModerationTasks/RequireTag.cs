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
    internal class RequireTag : ModerationTaskBase
    {

        public RequireTag(CommunityModTask modTaskDetails)
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

        private static Regex TagRegex = new Regex("^\\[.*\\].*$");

        public override void ValidateComment(GetCommentsResponse.CommentWrapper comment)
        {
            //todo improve structure so this doesn't happen
            throw new NotImplementedException();
        }

        public override void ValidatePost(GetPostsResponse.PostWrapper post)
        {
            if (!TagRegex.IsMatch(post.PostData.Name))
            {
                foreach (var action in ActionJobs)
                {
                    action.ActionPost(post);
                }
            }
        }
    }
}
