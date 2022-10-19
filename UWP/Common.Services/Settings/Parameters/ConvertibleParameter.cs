using System;
using System.Globalization;
using Windows.Storage;

namespace Common.Services.Settings.Parameters
{
    public class ConvertibleParameter<T> : Parameter<T>
    {
        public ConvertibleParameter(string key, T defaultValue, StorageType storageType) : base(key, defaultValue, storageType) { }

        public override T Get()
        {
            if (!Container.Values.ContainsKey(Key)) return DefaultValue;

            var stringValue = (string)Container.Values[Key];

            if (string.IsNullOrEmpty(stringValue)) return DefaultValue;

            var result = ((IConvertible)stringValue).ToType(typeof(T), CultureInfo.InvariantCulture);

            return (T)result;
        }

        public override void Set(T value)
        {
            var stringValue = string.Empty;

            if (value != null) stringValue = ((IConvertible)value).ToString(CultureInfo.InvariantCulture);

            Container.Values[Key] = stringValue;

            OnChanged();
        }
    }
}
