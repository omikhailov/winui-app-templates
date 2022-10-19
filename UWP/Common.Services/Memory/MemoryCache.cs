using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Common.Services.Memory
{
    public static class MemoryCache
    {
        static MemoryCache()
        {
            _dictionary = new ConcurrentDictionary<string, MemoryCacheItem>();
        }

        private static ConcurrentDictionary<string, MemoryCacheItem> _dictionary;

        private static ReaderWriterLockSlim _cleanupLock = new ReaderWriterLockSlim();

        public static bool TryGet<T>(string key, out T value)
        {
            try
            {
                _cleanupLock.EnterReadLock();

                var exists = _dictionary.TryGetValue(key, out var item);

                if (exists)
                {
                    value = (T)item.Value;
                }
                else
                {
                    value = default(T);
                }

                return exists;
            }
            finally
            {
                _cleanupLock.ExitReadLock();
            }
        }

        public static void Set(string key, object value)
        {
            try
            {
                _cleanupLock.EnterReadLock();

                var exists = _dictionary.TryGetValue(key, out var item);

                if (exists)
                {
                    lock (item) item.Value = value;
                }
                else
                {
                    _dictionary[key] = new MemoryCacheItem() { Value = value };
                }
            }
            finally
            {
                _cleanupLock.ExitReadLock();
            }
        }

        public static bool Remove(string key)
        {
            try
            {
                _cleanupLock.EnterReadLock();

                return _dictionary.TryRemove(key, out _);
            }
            finally
            {
                _cleanupLock.ExitReadLock();
            }
        }

        public static bool Pin(string key)
        {
            try
            {
                _cleanupLock.EnterReadLock();

                var found = _dictionary.TryGetValue(key, out var item);

                if (found) item.IsPinned = true;

                return found;
            }
            finally
            {
                _cleanupLock.ExitReadLock();
            }
        }

        public static bool Unlock(string key)
        {
            try
            {
                _cleanupLock.EnterReadLock();

                var found = _dictionary.TryGetValue(key, out var item);

                if (found) item.IsPinned = false;

                return found;
            }
            finally
            {
                _cleanupLock.ExitReadLock();
            }
        }

        public static void Clear()
        {
            try
            {
                _cleanupLock.EnterWriteLock();

                var pinnedItems = _dictionary.Where(pair => pair.Value.IsPinned);

                var newDictionary = new ConcurrentDictionary<string, MemoryCacheItem>(pinnedItems);

                _dictionary = newDictionary;
            }
            finally
            {
                _cleanupLock.ExitWriteLock();
            }
        }
    }
}
