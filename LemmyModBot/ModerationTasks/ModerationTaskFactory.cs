using LemmyModBot.Configuration;
using LemmyModBot.ModerationTasks.ModerationActions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LemmyModBot.ResponseModels.GetPostsResponse;
using static System.Collections.Specialized.BitVector32;

namespace LemmyModBot.ModerationTasks
{
    internal class ModerationTaskFactory
    {
        public ModerationTaskFactory(ConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        public ConfigurationManager ConfigurationManager { get; }

        public Dictionary<CommunityIdentifier, List<ModerationTaskBase>> GetModerationTasks()
        {
            var config = new Communities();
            ConfigurationManager.Bind("Communities", config);

            if (config.List == null || !config.List.Any())
            {
                return new Dictionary<CommunityIdentifier, List<ModerationTaskBase>>();
            }

            var communitySettingsList = new Dictionary<CommunityIdentifier, List<ModerationTaskBase>>();

            foreach (var communityConfigItem in config.List)
            {
                var community = new CommunityIdentifier(communityConfigItem.Name);
                var moderationTasks = communityConfigItem.Tasks.Where(t => t.Active).Select(t => GetTask(t)).ToList();

                communitySettingsList.Add(community, moderationTasks);

            }

            return communitySettingsList;
        }

        private ModerationTaskBase GetTask(CommunityModTask modTaskConfig)
        {
            switch (modTaskConfig.Name)
            {
                case "RequireTag":
                    return new RequireTag(modTaskConfig);
                default:
                    throw new NotImplementedException($"{modTaskConfig.Name} not implemented.");
            }
        }        
    }
}
