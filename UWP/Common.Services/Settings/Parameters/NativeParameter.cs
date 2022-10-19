namespace Common.Services.Settings.Parameters
{
    public class NativeParameter<T> : Parameter<T>
    {
        public NativeParameter(string key, T defaultValue, StorageType storageType) : base(key, defaultValue, storageType) { }

        public override T Get()
        {
            if (!Container.Values.ContainsKey(Key)) return DefaultValue;

            return (T)Container.Values[Key];
        }

        public override void Set(T value)
        {
            Container.Values[Key] = value;

            OnChanged();
        }
    }
}
