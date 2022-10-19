using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Common.DI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Common.ViewLayer.Pages
{
    public static class ViewModelBoundPageExtensionMethods
    {
        public static void SetViewModel<TViewModel>(this IViewModelBoundPage<TViewModel> page, NavigationCacheMode navigationCacheMode = NavigationCacheMode.Disabled)
        {
            page.ViewModel = Container.Instance.Resolve<TViewModel>();

            ((FrameworkElement)page).DataContext = page.ViewModel;
        }
    }
}
