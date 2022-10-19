using System.ComponentModel;
using Windows.UI.Xaml;
using Hamburger.BL.ViewModels.Settings.NavigationView;
using System.Threading.Tasks;

namespace Hamburger.BL.ViewModels.Settings
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        INavigationOptionsViewModel Navigation { get; set; }

        ElementTheme Theme { get; set; }
        string AppName { get; }
        string AppVersion { get; }

        Task Review();
        Task ShowPrivacyPolicy();
    }
}