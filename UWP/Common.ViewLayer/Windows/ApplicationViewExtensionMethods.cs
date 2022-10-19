using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Unity;
using Common.DI;
using Common.Services.Views;
using Common.ViewLayer.Pages;

namespace Common.ViewLayer.Windows
{
    public static class ApplicationViewExtensionMethods
    {
        public static void CloseSecondaryWindowsWhenConsolidated(this ApplicationView view)
        {
            if (view == null) return;

            view.Consolidated -= View_Consolidated;

            view.Consolidated += View_Consolidated;
        }

        private static async void View_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            async Task CloseWindow(AppWindow window)
            {
                await window.CloseAsync();
            }

            var viewsService = Container.Instance.Resolve<IViewsServiceBase>();

            var closingTasks = new List<Task>();

            foreach (var page in viewsService.ActivePages.OfType<ISecondaryPage>())
            {
                if (page.Window != null) closingTasks.Add(CloseWindow(page.Window));
            }

            await Task.WhenAll(closingTasks);
        }
    }
}
