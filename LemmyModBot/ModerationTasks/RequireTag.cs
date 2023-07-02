using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks
{
    internal class RequireTag : ModerationTaskBase, IModerationSettings
    {

        public RequireTag(bool active, UserContentType contentType, ModerationAction action)
        {
            Active = active;
            ContentType = contentType;
            Action = action;
        }

        public bool Active { get; }

        public UserContentType ContentType { get; }

        public ModerationAction Action { get; }

        public string Name => "RequireTag";
    }
}
