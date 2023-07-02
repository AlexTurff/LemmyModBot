using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot
{
    internal class CommunityIdentifier
    {
        public CommunityIdentifier(string communityName)
        {
            CommunityName = communityName;
        }

        public string CommunityName { get; }
    }
}
