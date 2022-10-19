using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.WindowManagement;
using Microsoft.Xaml.Interactivity;
using Common.ViewLayer.Pages;

namespace Common.ViewLayer.Behaviors
{
    public class CloseAppWindowWhenItHidesInTabletModeBehavior : Behavior<Page>
    {
        private AppWindow _window;

        protected override void OnAttached()
        {
            base.OnAttached();

            var secondaryPage = (ISecondaryPage)AssociatedObject;

            var bindablePage = (INotifyPropertyChanged)AssociatedObject;

            var window = secondaryPage.Window;

            _window = window;

            SubscribeTo(_window);

            bindablePage.PropertyChanged += OnPagePropertyChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var secondaryPage = (ISecondaryPage)AssociatedObject;

            var bindablePage = (INotifyPropertyChanged)AssociatedObject;

            bindablePage.PropertyChanged -= OnPagePropertyChanged;

            UnsubscribeFrom(_window);

            _window = null;
        }

        private void OnPagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = (ISecondaryPage)sender;

            if (e.PropertyName == nameof(ISecondaryPage.Window) && page.Window != _window)
            {
                UnsubscribeFrom(_window);

                _window = page.Window;

                SubscribeTo(_window);
            }
        }

        private void SubscribeTo(AppWindow window)
        {
            if (window != null) window.Changed += WindowChanged;
        }

        private void UnsubscribeFrom(AppWindow window)
        {
            if (window != null) window.Changed -= WindowChanged;
        }

        private async void WindowChanged(AppWindow sender, AppWindowChangedEventArgs args)
        {
            if (args.DidVisibilityChange && !sender.IsVisible && sender.WindowingEnvironment.Kind == WindowingEnvironmentKind.Tiled)
            {
                sender.Changed -= WindowChanged;
                
                await sender.CloseAsync();
            }
        }
    }
}
