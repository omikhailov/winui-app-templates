using System;
using Windows.Storage;

namespace Common.Services.Settings.Parameters
{
    public abstract class Parameter<T> : IParameter<T>
    {
        public Parameter(string key, T defaultValue, StorageType storageType)
        {
            Key = key;

            DefaultValue = defaultValue;

            if (storageType == StorageType.Local) Container = ApplicationData.Current.LocalSettings;

            else if (storageType == StorageType.Roaming) Container = ApplicationData.Current.RoamingSettings;
        }

        public virtual event ParameterValueChangedEventHandler<T> Changed;

        public virtual string Key { get; }

        public virtual T DefaultValue { get; }

        protected virtual ApplicationDataContainer Container { get; }

        public abstract T Get();

        public abstract void Set(T value);

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this);
        }

        public static IParameter<T> Create(string key, T defaultValue = default, StorageType storageType = StorageType.Local)
        {
            var type = typeof(T);

            var isNative = type == typeof(string) || type == typeof(bool) || type == typeof(int) || type == typeof(long) || type == typeof(short) ||
                type == typeof(ulong) || type == typeof(uint) || type == typeof(ushort) || type == typeof(byte) ||
                type == typeof(float) || type == typeof(double) ||
                type == typeof(DateTimeOffset) || type == typeof(TimeSpan) ||
                type == typeof(Guid) ||
                type == typeof(Windows.Foundation.Point) || type == typeof(Windows.Foundation.Size) || type == typeof(Windows.Foundation.Rect) ||
                type == typeof(ApplicationDataCompositeValue);

            if (isNative) return new NativeParameter<T>(key, defaultValue, storageType);

            if (type.IsEnum) return new EnumParameter<T>(key, defaultValue, storageType);

            if (!type.IsAbstract)
            {
                if (type.GetInterface(nameof(IConvertible)) != null) return new ConvertibleParameter<T>(key, defaultValue, storageType);

                if (type.IsClass) return new JsonSerializableParameter<T>(key, defaultValue, storageType);
            }

            throw new NotSupportedException("Use custom parameter for this type");
        }
    }
}
