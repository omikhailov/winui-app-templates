using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class Address : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; internal set; }

        private string _line;

        public string Line
        {
            get
            {
                return _line;
            }
            set
            {
                this.Set(ref _line, value, PropertyChanged);
            }
        }

        private string _city;

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                this.Set(ref _city, value, PropertyChanged);
            }
        }

        private string _country;

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                this.Set(ref _country, value, PropertyChanged);
            }
        }

    }
}
