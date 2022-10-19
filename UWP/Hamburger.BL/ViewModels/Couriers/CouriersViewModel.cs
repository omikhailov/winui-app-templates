using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Common.ModelLayer;
using Hamburger.BL.Models.Entities;
using Hamburger.BL.Services.Data;

namespace Hamburger.BL.ViewModels.Couriers
{
    public class CouriersViewModel : ICouriersViewModel, INotifyPropertyChanged
    {
        public CouriersViewModel()
        {
            LoadCouriers();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Dependency]
        public IDataService DataService { get; set; }

        public ObservableCollection<Courier> Couriers { get; } = new ObservableCollection<Courier>();

        private Courier _selectedCourier;

        public Courier SelectedCourier
        {
            get
            {
                return _selectedCourier;
            }
            set
            {
                this.Set(ref _selectedCourier, value, PropertyChanged);
            }
        }

        private void LoadCouriers()
        {
            _ = CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            {
                var source = DataService.GetCouriers();

                Couriers.Clear();

                foreach (var courier in source) Couriers.Add(courier);
            });
        }
    }
}
