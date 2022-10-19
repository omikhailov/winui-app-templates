namespace Common.Services.Memory
{
    internal class MemoryCacheItem
    {
        public object Value { get; set; }

        public bool IsPinned { get; set; }
    }
}
