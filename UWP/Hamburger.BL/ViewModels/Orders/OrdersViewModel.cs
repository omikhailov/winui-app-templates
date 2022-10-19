using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Unity;
using Hamburger.BL.Models.Entities;
using Hamburger.BL.Services.Data;
using Common.ModelLayer;
using Common.ViewModelLayer;
using Common.ViewModelLayer.DataGrid;

namespace Hamburger.BL.ViewModels.Orders
{
    public class OrdersViewModel : IOrdersViewModel, ISortableViewModel, INotifyPropertyChanged
    {
        public OrdersViewModel()
        {
            LoadOrders();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Dependency]
        public IDataService DataService { get; set; }

        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();

        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get
            {
                return _selectedOrder;
            }
            set
            {
                this.Set(ref _selectedOrder, value, PropertyChanged);
            }
        }

        public void DeleteSelectedOrder()
        {
            SelectedOrder = Orders.RemoveSelected(SelectedOrder, o => DataService.DeleteOrder(o));
        }

        private void LoadOrders(Func<IQueryable<Order>, IQueryable<Order>> sorting = null)
        {
            _ = CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                var source = DataService.GetOrders();

                if (sorting != null) source = sorting(source);
                
                Orders.Clear();

                foreach (var order in source) Orders.Add(order);
            });
        }

        public void Sort(string columnName, bool? ascending)
        {
            if (ascending == null)
            {
                LoadOrders();
            }
            else
            {
                if (columnName == "Status")
                {
                    if (ascending.Value)
                    {
                        LoadOrders(source => source.OrderBy(o => o.Status));
                    }
                    else
                    {
                        LoadOrders(source => source.OrderByDescending(o => o.Status));
                    }                    
                }
                else
                if (columnName == "Price")
                {
                    if (ascending.Value)
                    {
                        LoadOrders(source => source.OrderBy(o => o.Price));
                    }
                    else
                    {
                        LoadOrders(source => source.OrderByDescending(o => o.Price));
                    }
                }
                else
                if (columnName == "Client")
                {
                    if (ascending.Value)
                    {
                        LoadOrders(source => source.OrderBy(o => o.Client.LastName).ThenBy(o => o.Client.FirstName).ThenBy(o => o.Client.Id));
                    }
                    else
                    {
                        LoadOrders(source => source.OrderByDescending(o => o.Client.LastName).ThenByDescending(o => o.Client.FirstName).ThenBy(o => o.Client.Id));
                    }
                }
                else
                if (columnName == "Address")
                {
                    if (ascending.Value)
                    {
                        LoadOrders(source => source.OrderBy(o => o.Address.Country).ThenBy(o => o.Address.City).ThenBy(o => o.Address.Line)
                                            .ThenBy(o => o.Client.Address.Country).ThenBy(o => o.Client.Address.City).ThenBy(o => o.Client.Address.Line));
                    }
                    else
                    {
                        LoadOrders(source => source.OrderByDescending(o => o.Address.Country).ThenByDescending(o => o.Address.City).ThenByDescending(o => o.Address.Line)
                                            .ThenByDescending(o => o.Client.Address.Country).ThenByDescending(o => o.Client.Address.City).ThenByDescending(o => o.Client.Address.Line));
                    }
                }
            }
        }
    }
}
