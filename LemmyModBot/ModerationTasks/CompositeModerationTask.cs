using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmyModBot.ModerationTasks
{
    internal class CompositeModerationTask  :ModerationTaskBase
    {
        public CompositeModerationTask(List<ModerationTaskBase> modTasks)
        {
            ModTasks = modTasks;
        }

        private List<ModerationTaskBase> ModTasks { get; }
    } 
}
