using LemmyModBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks.ModerationActions
{
    internal class ModerationActionFactory
    {
        public ModerationActionFactory(IApiConnection connection)
        {
            Connection = connection;
        }

        public IApiConnection Connection { get; }

        //TODO switch me to reflection too?
        public IModerationAction GetAction(ModerationAction action, CommunityModTask modTaskDetails)
        {
            switch (action)
            {
                case ModerationAction.None:
                    return null;
                case ModerationAction.Remove:
                    return new Remove(Connection, modTaskDetails);
                case ModerationAction.Comment:
                    return new AddComment(Connection, modTaskDetails);
                case ModerationAction.Report:
                    return new Report(Connection, modTaskDetails);
                case ModerationAction.UserMessage:
                    return new MessageUser(Connection, modTaskDetails);
                default: throw new NotImplementedException();
            }
        }
    }
}
