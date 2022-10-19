namespace Common.Services.Settings.Parameters
{
    public interface IParameter<T>
    {
        event ParameterValueChangedEventHandler<T> Changed;

        string Key { get; }

        T DefaultValue { get; }

        T Get();

        void Set(T value);
    }
}
