using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Unity;
using Common.Services.Views;
using Common.Services.Settings;

namespace Common.ViewLayer.Pages
{
    public static class PageExtensionMethods
    {
        public static void Register(this Page page)
        {
            var viewsService = Common.DI.Container.Instance.Resolve<IViewsServiceBase>();

            viewsService.RegisterActivePage(page);
        }

        public static void SetTheme(this Page page)
        {
            var settingsService = Common.DI.Container.Instance.Resolve<ISettingsServiceBase>();

            page.RequestedTheme = settingsService.Theme.Get();
        }

        public static void SetTheme(this Page page, Func<PropertyChangedEventHandler> propertyChangedEventHandlerGetter)
        {
            if (propertyChangedEventHandlerGetter != null)
            {
                page.ActualThemeChanged += (s, e) => propertyChangedEventHandlerGetter()?.Invoke(page, new PropertyChangedEventArgs(nameof(page.ActualTheme)));
            }

            SetTheme(page);
        }
    }
}
