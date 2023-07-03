namespace LemmyModBot.Configuration
{
    public class CommunityModTask
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public bool Active { get; set; }
        public List<string> Actions { get; set; }
        public string Comment { get; set; }
    }
}
