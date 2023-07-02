using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks
{
    internal class RequireTag : ModerationTaskBase
    {

        public RequireTag(bool active, UserContentType contentType, ModerationAction action)
        {
            Active = active;
            ContentType = contentType;
            Action = action;
        }

        public override bool Active { get; }

        public override UserContentType ContentType { get; }

        public override ModerationAction Action { get; }

        public override string Name => "RequireTag";
    }
}
