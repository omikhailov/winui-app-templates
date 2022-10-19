using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class Client : INotifyPropertyChanged
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
                if (this.Set(ref _firstName, value, PropertyChanged))
                {
                    this.Raise(PropertyChanged, nameof(FullName));
                }
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
                if (this.Set(ref _lastName, value, PropertyChanged))
                {
                    this.Raise(PropertyChanged, nameof(FullName));
                }
            }
        }

        public string FullName
        {
            get
            {
                var result = FirstName;

                if (!string.IsNullOrEmpty(LastName)) result += " " + LastName;

                return result;
            }
        }

        private Address _address;

        public Address Address
        {
            get
            {
                return _address;
            }
            set
            {
                this.Set(ref _address, value, PropertyChanged);
            }
        }
    }
}
