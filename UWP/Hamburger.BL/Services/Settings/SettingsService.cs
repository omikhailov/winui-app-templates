using System;
using Windows.Devices.Geolocation;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Common.DI.Attributes;
using Common.Services.Settings;
using Common.Services.Lifecycle.Messages;
using Common.Services.Settings.Parameters;

namespace Hamburger.BL.Services.Settings
{
    [Singleton(LazyLoading = false)]

    public class SettingsService : SettingsServiceBase, ISettingsService, ISettingsServiceBase, IRecipient<PrelaunchMessage>
    {
        public SettingsService() : base() { }

        public IParameter<NavigationViewPaneDisplayMode> PaneDisplayMode { get; } = Parameter<NavigationViewPaneDisplayMode>.Create(nameof(PaneDisplayMode), NavigationViewPaneDisplayMode.Auto)
            .WithCache()
            .WithMessage();

        public IParameter<string> MainFrameNavigationHistory { get; } = Parameter<string>.Create(nameof(MainFrameNavigationHistory))
            .WithCache();

        public IParameter<string> LastOpenedPageName { get; } = Parameter<string>.Create(nameof(LastOpenedPageName), default, StorageType.Roaming)
            .WithCache();

        public IParameter<BasicGeoposition> MapCenter { get; } = 
            new BasicGeopositionParameter(nameof(MapCenter), new BasicGeoposition() { Latitude = 53.55111323960243, Longitude = 9.99359923906035 }, StorageType.Roaming)
            .WithCacheAndThrottling(TimeSpan.FromSeconds(1));

        public IParameter<double> MapZoom { get; } = Parameter<double>.Create(nameof(MapZoom), 13.5, StorageType.Roaming)
            .WithCacheAndThrottling(TimeSpan.FromSeconds(1));

        public override void Receive(PrelaunchMessage message)
        {
            base.Receive(message);

            PaneDisplayMode.Get();

            MainFrameNavigationHistory.Get();

            LastOpenedPageName.Get();
        }
    }
}
