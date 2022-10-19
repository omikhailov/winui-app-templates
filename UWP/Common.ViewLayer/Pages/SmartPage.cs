using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Unity;
using Common.Services.Settings;
using Common.Services.Views;
using Common.ViewModelLayer;

namespace Common.ViewLayer.Pages
{
    public class SmartPage : Page, INotifyPropertyChanged
    {
        public SmartPage()
        {
            this.Unloaded += Unload;

            this.ActualThemeChanged += (s, e) => OnPropertyChanged(nameof(ActualTheme));
        }

        private object _viewModel;

        public bool IsActive { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RegisterActive();

            SetTheme();

            _viewModel = TrySetViewModel();

            if (_viewModel is INavigatedToHandler handler) handler.NavigatedToHandler(e.Parameter);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (_viewModel is INavigatingFromHandler handler) e.Cancel = handler.NavigatingFromHandler(e.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            RegisterInactive();
            
            if (_viewModel is INavigatedFromHandler handler) handler.NavigatedFromHandler(e.Parameter);
        }

        private void Unload(object sender, RoutedEventArgs e)
        {
            Unregister();

            if (_viewModel is IUnloadHandler handler) handler.UnloadHandler();
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object TrySetViewModel()
        {
            object viewModel = null;

            var pageType = this.GetType();

            var interfaceDefinition = pageType.GetInterfaces().Where(i => i.IsGenericType).FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IViewModelBoundPage<>));

            if (interfaceDefinition != null)
            {
                var viewModelType = interfaceDefinition.GetGenericArguments()[0];

                viewModel = Common.DI.Container.Instance.Resolve(viewModelType);                

                pageType.GetProperty("ViewModel").SetValue(this, viewModel);

                DataContext = viewModel;
            }

            return viewModel;
        }

        private void SetTheme()
        {
            var settingsService = Common.DI.Container.Instance.Resolve<ISettingsServiceBase>();

            RequestedTheme = settingsService.Theme.Get();
        }

        private void RegisterActive()
        {
            var viewsService = Common.DI.Container.Instance.Resolve<IViewsServiceBase>();

            viewsService.RegisterActivePage(this);

            IsActive = true;
        }

        private void RegisterInactive()
        {
            var viewsService = Common.DI.Container.Instance.Resolve<IViewsServiceBase>();

            viewsService.RegisterInactivePage(this);

            IsActive = false;
        }

        private void Unregister()
        {
            var viewsService = Common.DI.Container.Instance.Resolve<IViewsServiceBase>();

            viewsService.UnregisterPage(this);
        }
    }
}
