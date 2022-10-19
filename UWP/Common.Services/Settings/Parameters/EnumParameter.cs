using System;

namespace Common.Services.Settings.Parameters
{
    public class EnumParameter<T> : Parameter<T>
    {
        public EnumParameter(string key, T defaultValue, StorageType storageType) : base(key, defaultValue, storageType) { }

        public override T Get()
        {
            if (!Container.Values.ContainsKey(Key)) return DefaultValue;

            if (!Enum.TryParse(typeof(T), (string)Container.Values[Key], out var result)) return DefaultValue;

            return (T)result;
        }

        public override void Set(T value)
        {
            Container.Values[Key] = value.ToString();

            OnChanged();
        }
    }
}
