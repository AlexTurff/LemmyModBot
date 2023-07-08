using LemmyModBot.Configuration;
using LemmyModBot.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks
{
    internal class PostTitleFilterRegexDecorator : ModerationTaskBase
    {
        public PostTitleFilterRegexDecorator(ModerationTaskBase decorated, CommunityModTask modTaskDetails)
        {
            Decorated = decorated;
            ModTaskDetails = modTaskDetails;

            try
            {
                Regex = new Regex(ModTaskDetails.OnlyActionIfPostTitleMatchRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("RegularExpression setting must be set on a Regex Task");
            }
            catch (ArgumentException e)
            {
                throw new Exception("RegularExpression is invalid: ", e);
            }
        }
        private ModerationTaskBase Decorated { get; }
        private CommunityModTask ModTaskDetails { get; }
        private Regex Regex { get; }
        public override string Name => Decorated.Name;

        public override bool Active => Decorated.Active;

        public override UserContentType ContentType => Decorated.ContentType;

        public override List<ModerationAction> Actions => Decorated.Actions;

        public override void ValidateComment(GetCommentsResponse.CommentWrapper comment)
        {
            if(Regex != null && Regex.IsMatch(comment.Post.Name))
            {
                Decorated.ValidateComment(comment);
            }
        }

        public override void ValidatePost(GetPostsResponse.PostWrapper post)
        {
            if (Regex != null && Regex.IsMatch(post.PostData.Name))
            {
                Decorated.ValidatePost(post);
            }
        }
    }
}
