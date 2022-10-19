using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Services.Lifecycle;
using Common.Services.Memory;

namespace Common.Services.Settings.Parameters
{
    public class CachedParameter<T> : IParameter<T>
    {
        private IParameter<T> _parameter;

        private TimeSpan _delay;

        private Task _setValueTask;

        private DeferredSavingOperation _savingOperation;

        internal CachedParameter(IParameter<T> parameter, TimeSpan delay)
        {
            _parameter = parameter;

            _delay = delay;
        }

        public event ParameterValueChangedEventHandler<T> Changed;

        public string Key
        {
            get
            {
                return _parameter.Key;
            }
        }

        public T DefaultValue
        {
            get
            {
                return _parameter.DefaultValue;
            }
        }

        public T Get()
        {
            if (MemoryCache.TryGet(Key, out T cached)) return cached;

            var result = _parameter.Get();

            MemoryCache.Set(Key, result);

            return result;
        }

        public void Set(T value)
        {
            var oldValue = Get();

            if (!EqualityComparer<T>.Default.Equals(oldValue, value))
            {
                MemoryCache.Set(Key, value);

                if (_delay == TimeSpan.Zero)
                    _parameter.Set(value);
                else
                {
                    MemoryCache.Pin(Key);

                    if (_savingOperation == null)
                        _savingOperation = new DeferredSavingOperation(SaveCachedValue, _delay);
                    else
                        _savingOperation.Schedule();
                }

                Changed?.Invoke(this);
            }
        }

        private void SaveCachedValue()
        {
            if (MemoryCache.TryGet(Key, out T cached))
            {
                _parameter.Set(cached);

                MemoryCache.Unlock(Key);
            }
        }
    }
}
