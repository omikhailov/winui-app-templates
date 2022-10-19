using System.Collections.ObjectModel;
using System.ComponentModel;
using Common.ViewModelLayer.DataGrid;
using Hamburger.BL.Models.Entities;

namespace Hamburger.BL.ViewModels.Orders
{
    public interface IOrdersViewModel : ISortableViewModel, INotifyPropertyChanged
    {
        ObservableCollection<Order> Orders { get; }

        Order SelectedOrder { get; set; }

        void DeleteSelectedOrder();
    }
}