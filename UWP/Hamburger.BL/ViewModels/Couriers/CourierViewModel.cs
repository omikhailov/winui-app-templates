using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Common.ModelLayer;
using Common.ViewModelLayer;
using Hamburger.BL.Models.Entities;
using Hamburger.BL.Services.Data;

namespace Hamburger.BL.ViewModels.Couriers
{
    public class CourierViewModel : ICourierViewModel, INotifyPropertyChanged, INavigatedToHandler
    {
        [Dependency]
        public IDataService DataService { get; set; }

        private Courier _model;

        public Courier Model
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
            if (parameter is Courier courier)
            {
                Model = courier;
            }
            else
            {
                Model = DataService.GetCourier((Guid)parameter);
            }
        }
    }
}
