namespace Common.Services.Lifecycle.Messages
{
    public class LowMemoryMessage : OptionallyAsynchronousMessage
    {
        public LowMemoryMessage(bool isRunningInBackground)
        {
            IsRunningInBackground = isRunningInBackground;
        }

        public bool IsRunningInBackground { get; }
    }
}
