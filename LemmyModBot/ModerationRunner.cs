using LemmyModBot.ModerationTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot
{
    internal class ModerationRunner
    {
        private ApiConnection connection;
        private Dictionary<Community, ModerationTaskBase> moderationTasks;

        public ModerationRunner(ApiConnection connection, Dictionary<Community, ModerationTaskBase> moderationTasks)
        {
            this.connection = connection;
            this.moderationTasks = moderationTasks;
        }

        public void Run() {
            //var response = connection.SendRequest<GetPostsRequest, GetPostsResponse>(
            //    new ApiOperation<GetPostsRequest>() 
            //        { Operation = GetPostsRequest.OperationName, Data = new GetPostsRequest("imaginarycosmere", 1) });    
        }
    }
}
