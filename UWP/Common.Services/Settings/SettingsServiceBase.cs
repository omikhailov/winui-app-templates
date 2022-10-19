using Windows.UI.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Common.Services.Lifecycle.Messages;
using Common.Services.Settings.Parameters;

namespace Common.Services.Settings
{
    public class SettingsServiceBase : IRecipient<PrelaunchMessage>, ISettingsServiceBase
    {
        public SettingsServiceBase()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public IParameter<ElementTheme> Theme { get; } = Parameter<ElementTheme>.Create(nameof(Theme), ElementTheme.Default).WithCache().WithMessage();
        
        public virtual void Receive(PrelaunchMessage message)
        {
            Theme.Get();
        }
    }
}
