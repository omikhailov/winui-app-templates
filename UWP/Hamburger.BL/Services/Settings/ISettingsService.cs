using Microsoft.UI.Xaml.Controls;
using Common.Services.Settings;
using Windows.Devices.Geolocation;
using Common.Services.Settings.Parameters;

namespace Hamburger.BL.Services.Settings
{
    public interface ISettingsService : ISettingsServiceBase
    {
        IParameter<NavigationViewPaneDisplayMode> PaneDisplayMode { get; }

        IParameter<string> MainFrameNavigationHistory { get; }

        IParameter<string> LastOpenedPageName { get; }
        IParameter<BasicGeoposition> MapCenter { get; }
        IParameter<double> MapZoom { get; }
    }
}