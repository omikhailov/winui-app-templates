using System.Text.Json;

namespace Common.Services.Settings.Parameters
{
    public class JsonSerializableParameter<T> : Parameter<T>
    {
        public JsonSerializableParameter(string key, T defaultValue, StorageType storageType) : base(key, defaultValue, storageType) { }

        public override T Get()
        {
            if (!Container.Values.ContainsKey(Key)) return DefaultValue;

            var stringValue = (string)Container.Values[Key];

            if (string.IsNullOrEmpty(stringValue)) return DefaultValue;

            return (T)JsonSerializer.Deserialize(stringValue, typeof(T));
        }

        public override void Set(T value)
        {
            var stringValue = string.Empty;

            if (value != null) stringValue = JsonSerializer.Serialize(value);

            Container.Values[Key] = stringValue;

            OnChanged();
        }
    }
}
