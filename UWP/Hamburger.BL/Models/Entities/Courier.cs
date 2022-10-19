using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class Courier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; internal set; }

        private string _firstName;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                this.Set(ref _firstName, value, PropertyChanged);

                this.Raise(PropertyChanged, nameof(FullName));
            }
        }

        private string _lastName;

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                this.Set(ref _lastName, value, PropertyChanged);

                this.Raise(PropertyChanged, nameof(FullName));
            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                this.Set(ref _description, value, PropertyChanged);
            }
        }

        private string _photo;

        public string Photo
        {
            get
            {
                return _photo;
            }
            set
            {
                this.Set(ref _photo, value, PropertyChanged);
            }
        }

        private BasicGeoposition _location;

        public BasicGeoposition Location
        {
            get
            {
                return _location;
            }
            set
            {
                this.Set(ref _location, value, PropertyChanged);
            }
        }

        private ObservableCollection<Order> _bag = new ObservableCollection<Order>();

        public ObservableCollection<Order> Bag
        {
            get
            {
                return _bag;
            }
            set
            {
                this.Set(ref _bag, value, PropertyChanged);
            }
        }
    }
}
