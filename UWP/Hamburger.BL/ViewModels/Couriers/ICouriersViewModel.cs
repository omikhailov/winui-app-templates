using System.Collections.ObjectModel;
using System.ComponentModel;
using Hamburger.BL.Models.Entities;

namespace Hamburger.BL.ViewModels.Couriers
{
    public interface ICouriersViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Courier> Couriers { get; }

        Courier SelectedCourier { get; set; }
    }
}