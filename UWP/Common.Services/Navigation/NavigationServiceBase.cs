using System;
using Windows.UI.Core;
using CommunityToolkit.Mvvm.Messaging;
using Common.Services.Navigation.Messages;

namespace Common.Services.Navigation
{
    public class NavigationServiceBase : INavigationServiceBase
    {
        public NavigationServiceBase()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
        }

        protected virtual void BackRequested(object sender, BackRequestedEventArgs e)
        {
            var controlsResponse = WeakReferenceMessenger.Default.Send(new SystemBackButtonPressedOnControlMessage());
            
            if (!controlsResponse.Handled)
            {
                var mainPageResponse = WeakReferenceMessenger.Default.Send(new SystemBackButtonPressedOnMainPageMessage());

                e.Handled = mainPageResponse.Handled;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
