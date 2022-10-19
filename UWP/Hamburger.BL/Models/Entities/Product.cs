using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class Product : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; internal set; }

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                this.Set(ref _title, value, PropertyChanged);
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

        private decimal _price;

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                this.Set(ref _price, value, PropertyChanged);
            }
        }

    }
}
