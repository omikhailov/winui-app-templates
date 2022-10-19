using System;
using System.ComponentModel;
using Unity;
using Common.ModelLayer;
using Hamburger.BL.Services.Data;
using Hamburger.BL.Models.Entities;
using Hamburger.BL.Services.Settings;
using Common.ViewModelLayer;

namespace Hamburger.BL.ViewModels.Orders
{
    public class OrderViewModel : IOrderViewModel, INotifyPropertyChanged, INavigatedToHandler
    {
        [Dependency]
        public IDataService DataService { get; set; }

        private Order _model;

        public Order Model
        {
            get
            {
                return _model;
            }
            set
            {
                this.Set(ref _model, value, PropertyChanged);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NavigatedToHandler(object parameter)
        {
            Model = DataService.GetOrder((Guid)parameter);
        }
    }
}
