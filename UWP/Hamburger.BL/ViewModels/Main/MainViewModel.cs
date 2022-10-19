using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.UI.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Unity;
using Common.ModelLayer;
using Common.Services.Settings.Messages;
using Hamburger.BL.ViewModels.Settings.NavigationView;
using Hamburger.BL.Models.Main;
using Hamburger.BL.Services.Settings;
using Hamburger.BL.Services.Telemetry;

namespace Hamburger.BL.ViewModels.Main
{
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged, 
        IRecipient<ParameterChangedMessage<ElementTheme>>, 
        IRecipient<ParameterChangedMessage<Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode>>
    {
        private const int NavigationHistoryDepth = 20;

        public MainViewModel(ISettingsService settingsService)
        {
            SettingsService = settingsService;

            MainMenuItems = new List<PageModel>()
            {
                new PageModel() { Name = "Welcome", Icon = Symbol.Home },
                null,
                new PageModel() { Name = "Orders", Icon = Symbol.Street },
                new PageModel() { Name = "Couriers", Icon = Symbol.People },
                new PageModel() { Name = "Map", Icon = Symbol.Map }
            };

            FooterMenuItems = new List<PageModel>()
            {
                new PageModel() { Name = "Settings", Icon = Symbol.Setting }
            };

            var lastPageName = SettingsService.LastOpenedPageName.Get();

            _selectedItem = AllPages.FirstOrDefault(item => item.Name == lastPageName) ?? AllPages.FirstOrDefault();

            SetSystemButtonsForeground();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public ISettingsService SettingsService { get; }

        [Dependency]
        public INavigationOptionsViewModel NavigationOptions { get; set; }

        [Dependency]
        public ITelemetryService TelemetryService { get; set; }

        public List<PageModel> MainMenuItems { get; }

        public List<PageModel> FooterMenuItems { get; }

        private IEnumerable<PageModel> AllPages
        {
            get
            {
                return MainMenuItems.Concat(FooterMenuItems).Where(item => item != null);
            }
        }

        private PageModel _selectedItem;

        public PageModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                this.Set(ref _selectedItem, value, PropertyChanged);

                SettingsService.LastOpenedPageName.Set(value.Name);

                TelemetryService.PageHit(value.Name);
            }
        }

        private Color? _systemButtonsForeground;

        public Color? SystemButtonsForeground
        {
            get
            {
                return _systemButtonsForeground;
            }
            set
            {
                this.Set(ref _systemButtonsForeground, value, PropertyChanged);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnFrameNavigated(object sender, NavigationEventArgs a)
        {
            var fullName = a.SourcePageType?.Name;

            if (fullName != SelectedItem.Name + "Page")
            {
                var model = AllPages.FirstOrDefault(m => m.Name + "Page" == fullName);

                SelectedItem = model;

                var frame = (Frame)sender;

                while (frame.BackStackDepth > NavigationHistoryDepth) frame.BackStack.RemoveAt(0);

                NavigationOptions.MainFrameNavigationHistory = ((Frame)sender).GetNavigationState();
            }
        }

        private void SetSystemButtonsForeground()
        {
            if (SettingsService.PaneDisplayMode.Get() == Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top ||
                SettingsService.Theme.Get() == ElementTheme.Dark || 
                Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                SystemButtonsForeground = Colors.White;
            }
            else
            {
                SystemButtonsForeground = Colors.Black;
            }
        }

        public void Receive(ParameterChangedMessage<ElementTheme> message)
        {
            if (message.Key == nameof(ISettingsService.Theme)) SetSystemButtonsForeground();
        }

        public void Receive(ParameterChangedMessage<Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode> message)
        {
            if (message.Key == nameof(ISettingsService.PaneDisplayMode)) SetSystemButtonsForeground();
        }
    }
}
