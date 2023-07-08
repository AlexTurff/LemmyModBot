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
    internal class MatchesRegex : ModerationTaskBase
    {

        public MatchesRegex(CommunityModTask modTaskDetails)
        {
            Active = modTaskDetails.Active;
            ContentType = modTaskDetails.ParseContentType();
            Actions = modTaskDetails.ParseModerationActions();

            ActionJobs = Actions.Select(a => Program.ModerationActionFactory.GetAction(a, modTaskDetails)).ToList();
            ModTaskDetails = modTaskDetails;

            try
            {
                Regex = new Regex(modTaskDetails.RegularExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            catch(ArgumentNullException)
            {
                throw new Exception("RegularExpression setting must be set on a Regex Task");
            }
            catch (ArgumentException e)
            {
                throw new Exception("RegularExpression is invalid: ", e);
            }
        }

        public override bool Active { get; }

        public override UserContentType ContentType { get; }

        public override List<ModerationAction> Actions { get; }

        public override string Name => "RequireTag";

        private List<IModerationAction> ActionJobs { get; }

        private CommunityModTask ModTaskDetails { get; }

        private Regex Regex { get; }

        public override void ValidateComment(GetCommentsResponse.CommentWrapper comment)
        {
            if (Regex.IsMatch(comment.CommentData.Content))
            {
                foreach (var action in ActionJobs)
                {
                    action.ActionComment(comment);
                }
            }
        }

        public override void ValidatePost(GetPostsResponse.PostWrapper post)
        {
            bool shouldAction = false;

            if (ContentType.HasFlag(UserContentType.PostTitle))
            {
                shouldAction = Regex.IsMatch(post.PostData.Name);
            }

            if (ContentType.HasFlag(UserContentType.PostBody))
            {
                shouldAction |= Regex.IsMatch(post.PostData.Body);
            }

            if (shouldAction)
            {
                foreach (var action in ActionJobs)
                {
                    action.ActionPost(post);
                }
            }
        }
    }
}
