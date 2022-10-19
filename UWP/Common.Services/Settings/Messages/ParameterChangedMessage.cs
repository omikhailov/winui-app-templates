using CommunityToolkit.Mvvm.Messaging.Messages;
using Common.Services.Settings.Parameters;

namespace Common.Services.Settings.Messages
{
    public class ParameterChangedMessage<T> : ValueChangedMessage<T>
    {
        public ParameterChangedMessage(IParameter<T> parameter) : base(parameter.Get())
        {
            Key = parameter.Key;
        }

        public string Key { get; }
    }
}
