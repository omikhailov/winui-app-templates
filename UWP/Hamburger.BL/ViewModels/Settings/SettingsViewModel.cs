using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Unity;
using Common.ModelLayer;
using Hamburger.BL.Services.Licensing;
using Hamburger.BL.Services.Settings;
using Hamburger.BL.Services.Views;
using Hamburger.BL.ViewModels.Settings.NavigationView;

namespace Hamburger.BL.ViewModels.Settings
{
    public class SettingsViewModel : ISettingsViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Dependency]
        public ISettingsService SettingsService { get; set; }

        [Dependency]
        public ILicensingService LicensingService { get; set; }

        [Dependency]
        public IViewsService ViewsService { get; set; }

        [Dependency]
        public INavigationOptionsViewModel Navigation { get; set; }

        public ElementTheme Theme
        {
            get
            {
                return SettingsService.Theme.Get();
            }
            set
            {
                if (SettingsService.Theme.Get() == value) return;

                SettingsService.Theme.Set(value);

                foreach (var page in ViewsService.ActivePages) page.RequestedTheme = value;

                this.Raise(PropertyChanged);
            }
        }

        public string AppName 
        { 
            get
            {
                return ResourceLoader.GetForCurrentView().GetString("AppName");
            }
        }

        public string AppVersion
        {
            get
            {
                return LicensingService.AppVersion;
            }
        }

        public async Task Review()
        {
            await LicensingService.ReviewApp();
        }

        public async Task ShowPrivacyPolicy()
        {
            await Launcher.LaunchFileAsync(await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/Policy/PrivacyPolicy.html")));
        }
    }
}
