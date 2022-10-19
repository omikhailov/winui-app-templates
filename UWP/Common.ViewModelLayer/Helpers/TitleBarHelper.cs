using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;

namespace Common.ViewModelLayer.Helpers
{
    public static class TitleBarHelper
    {
        public static void ExtendView()
        {
            var appViewTitleBar = ApplicationView.GetForCurrentView().TitleBar;

            appViewTitleBar.ButtonBackgroundColor = Colors.Transparent;

            appViewTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            
            var coreAppViewTitleBar = CoreApplication.GetCurrentView().TitleBar;
            
            if (coreAppViewTitleBar != null) coreAppViewTitleBar.ExtendViewIntoTitleBar = true;
        }

        public static void ExtendView(AppWindow window)
        {
            var titleBar = window.TitleBar;

            titleBar.ButtonBackgroundColor = Colors.Transparent;

            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            titleBar.ExtendsContentIntoTitleBar = true;
        }
    }
}
