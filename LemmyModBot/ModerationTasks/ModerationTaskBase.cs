namespace LemmyModBot.ModerationTasks
{
    internal abstract class ModerationTaskBase
    {
        public abstract string Name { get; }
        public abstract  bool Active { get; }
        public abstract UserContentType ContentType { get; }
        public abstract ModerationAction Action { get; }
    }
}
