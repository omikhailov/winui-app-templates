using System;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using CommunityToolkit.Mvvm.Messaging;
using Common.Services.Navigation.Messages;

namespace Common.ViewLayer.Behaviors
{
    public class NavigateBackWhenSystemBackButtonPressedBehavior : Behavior<Frame>, IRecipient<SystemBackButtonPressedOnMainPageMessage>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            WeakReferenceMessenger.Default.UnregisterAll(this);
        }     

        public void Receive(SystemBackButtonPressedOnMainPageMessage message)
        {
            if (!message.Handled && AssociatedObject.CanGoBack)
            {
                AssociatedObject.GoBack();

                message.Handled = true;
            }
        }
    }
}
