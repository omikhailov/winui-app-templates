using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Unity;
using Common.DI.Attributes;
using Common.ModelLayer;
using Hamburger.BL.Services.Settings;

namespace Hamburger.BL.ViewModels.Settings.NavigationView
{
    [Singleton]

    public class NavigationOptionsViewModel : INavigationOptionsViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Dependency]
        public ISettingsService SettingsService { get; set; }

        public NavigationViewPaneDisplayMode DisplayMode
        {
            get
            {
                return SettingsService.PaneDisplayMode.Get();
            }
            set
            {
                if (SettingsService.PaneDisplayMode.Get() == value) return;

                SettingsService.PaneDisplayMode.Set(value);

                this.Raise(PropertyChanged);
            }
        }

        public string MainFrameNavigationHistory
        {
            get
            {
                return SettingsService.MainFrameNavigationHistory.Get();
            }
            set
            {
                SettingsService.MainFrameNavigationHistory.Set(value);
            }
        }
    }
}
