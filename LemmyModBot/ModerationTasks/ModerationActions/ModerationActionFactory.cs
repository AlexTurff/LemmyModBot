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

        public IModerationAction GetAction(ModerationAction action)
        {
            switch (action)
            {
                case ModerationAction.None:
                    return null;
                case ModerationAction.Remove:
                    throw new NotImplementedException();
                case ModerationAction.Comment:
                    return new AddComment(Connection);
               default: throw new NotImplementedException();
            }
        }
    }
}
