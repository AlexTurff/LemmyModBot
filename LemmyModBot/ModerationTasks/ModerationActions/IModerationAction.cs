using LemmyModBot.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks.ModerationActions
{
    internal interface IModerationAction
    {
        //todo come up with better structure - can there just be one method?
        void ActionPost(GetPostsResponse.PostWrapper post);
        void ActionComment(GetCommentsResponse.CommentWrapper comment);
    }
}
