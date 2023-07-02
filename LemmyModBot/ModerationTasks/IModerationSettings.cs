namespace LemmyModBot.ModerationTasks
{
    internal interface IModerationSettings
    {
        public string Name { get; }
        public bool Active { get; }
        public UserContentType ContentType { get; }
        public ModerationAction Action { get; }
    }
}
