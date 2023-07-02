using LemmyModBot.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LemmyModBot.ResponseModels.GetPostsResponse;

namespace LemmyModBot.ModerationTasks
{
    internal class ModerationTaskFactory
    {
        public ModerationTaskFactory(ConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        public ConfigurationManager ConfigurationManager { get; }

        public Dictionary<Community,ModerationTaskBase> GetModerationTasks()
        {
            var config = new Communities();
            ConfigurationManager.Bind("Communities", config);

            if(config.List == null || !config.List.Any())
            {
                return new Dictionary<Community, ModerationTaskBase>();
            }

            var communitySettingsList = new Dictionary<Community,ModerationTaskBase>();                       

            foreach(var communityConfigItem in config.List)
            {
                var community = new Community(communityConfigItem.Name);
                var moderationTasks = communityConfigItem.Tasks.Where(t => t.Active).Select(t => GetTask(t)).ToList();

                if (moderationTasks.Count == 1)
                {
                    communitySettingsList.Add(community, moderationTasks[0]);
                }
                else
                {
                    communitySettingsList.Add(community, new CompositeModerationTask(moderationTasks));
                }
            }

            return communitySettingsList;
        }

        private ModerationTaskBase GetTask(CommunityModTask modTaskConfig)
        {
            switch (modTaskConfig.Name) {
                case "RequireTag":
                    return new RequireTag();
                default:
                    throw new NotImplementedException($"{modTaskConfig.Name} not implemented.");
            }
        }
    }
}
