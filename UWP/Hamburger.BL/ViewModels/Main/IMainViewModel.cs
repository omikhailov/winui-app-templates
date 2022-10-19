using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Hamburger.BL.Models.Main;
using Hamburger.BL.ViewModels.Settings.NavigationView;
using Windows.UI;

namespace Hamburger.BL.ViewModels.Main
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        INavigationOptionsViewModel NavigationOptions { get; set; }

        List<PageModel> MainMenuItems { get; }

        List<PageModel> FooterMenuItems { get; }

        PageModel SelectedItem { get; set; }
        Color? SystemButtonsForeground { get; set; }

        void OnFrameNavigated(object sender, NavigationEventArgs a);
    }
}
