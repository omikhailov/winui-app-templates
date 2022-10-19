using System.Collections.ObjectModel;
using System.ComponentModel;
using Hamburger.BL.Models.Entities;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace Hamburger.BL.ViewModels.Map
{
    public interface IMapViewModel : INotifyPropertyChanged
    {
        Geopoint Center { get; set; }

        ObservableCollection<MapLayer> Layers { get; }
        double Zoom { get; set; }
        bool IsActive { get; set; }
    }
}