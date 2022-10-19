using System.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace Hamburger.BL.ViewModels.Settings.NavigationView
{
    public interface INavigationOptionsViewModel : INotifyPropertyChanged
    {
        NavigationViewPaneDisplayMode DisplayMode { get; set; }

        string MainFrameNavigationHistory { get; set; }
    }
}