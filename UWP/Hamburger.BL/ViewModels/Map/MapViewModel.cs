using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using CommunityToolkit.Mvvm.Messaging;
using Unity;
using Common.ViewModelLayer;
using Common.ModelLayer;
using Common.Services.Lifecycle.Messages;
using Hamburger.BL.Services.Data;
using Hamburger.BL.Services.Settings;

namespace Hamburger.BL.ViewModels.Map
{
    public class MapViewModel : IMapViewModel, INotifyPropertyChanged,
        INavigatedToHandler,
        INavigatedFromHandler,
        IRecipient<AppEnteredBackgroundMessage>, 
        IRecipient<AppLeavingBackgroundMessage>
    {
        public MapViewModel(IDataService dataService)
        {
            DataService = dataService;

            Layers.Add(CouriersLayer);

            UpdateCourierPoints();

            CouriersTimer.Tick += (s, e) => UpdateCourierPoints();

            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Dependency]
        public ISettingsService SettingsService { get; set; }

        public IDataService DataService { get; set; }

        private bool _isActive;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;

                if (_isActive)
                {
                    CouriersTimer.Start();
                }
                else
                {
                    CouriersTimer.Stop();
                }
            }
        }

        public Geopoint Center
        {
            get
            {
                var position = SettingsService.MapCenter.Get();

                return new Geopoint(position);
            }
            set
            {
                var position = SettingsService.MapCenter.Get();

                if (!position.Equals(value.Position))
                {
                    SettingsService.MapCenter.Set(value.Position);

                    this.Raise(PropertyChanged);
                }
            }
        }

        public double Zoom
        {
            get
            {
                return SettingsService.MapZoom.Get();
            }
            set
            {
                var zoom = SettingsService.MapZoom.Get();

                if (zoom != value)
                {
                    SettingsService.MapZoom.Set(value);

                    this.Raise(PropertyChanged);
                }
            }
        }

        public ObservableCollection<MapLayer> Layers { get; } = new ObservableCollection<MapLayer>();

        public MapElementsLayer CouriersLayer { get; set; } = new MapElementsLayer() { ZIndex = 1 };

        private DispatcherTimer CouriersTimer { get; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(60) };

        private void UpdateCourierPoints()
        {
            var icon = new MapIcon();

            _ = CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                CouriersLayer.MapElements = DataService.GetCouriers()
                .Select(courier =>
                    (MapElement)(new MapIcon() {
                        Title = courier.FullName,
                        Location = new Geopoint(courier.Location),
                        CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible, 
                        Tag = courier
                    }))
                .ToList();
            });
        }

        public void Receive(AppEnteredBackgroundMessage message)
        {
            CouriersTimer.Stop();
        }

        public void Receive(AppLeavingBackgroundMessage message)
        {
            if (IsActive) CouriersTimer.Start();
        }

        public void NavigatedToHandler(object parameter)
        {
            IsActive = true;
        }

        public void NavigatedFromHandler(object parameter)
        {
            IsActive = false;
        }
    }
}
