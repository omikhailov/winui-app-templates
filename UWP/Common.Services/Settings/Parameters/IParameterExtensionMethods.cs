using System;
using CommunityToolkit.Mvvm.Messaging;
using Common.Services.Settings.Messages;

namespace Common.Services.Settings.Parameters
{
    public static class IParameterExtensionMethods
    {
        public static IParameter<T> WithCache<T>(this IParameter<T> parameter)
        {
            return new CachedParameter<T>(parameter, TimeSpan.Zero);
        }

        public static IParameter<T> WithCacheAndThrottling<T>(this IParameter<T> parameter, TimeSpan delay)
        {
            return new CachedParameter<T>(parameter, delay);
        }

        public static IParameter<T> WithMessage<T>(this IParameter<T> parameter)
        {
            parameter.Changed += p => WeakReferenceMessenger.Default.Send(new ParameterChangedMessage<T>(p));

            return parameter;
        }
    }
}
