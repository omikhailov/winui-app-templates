using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class OrderLine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; internal set; }

        private Product _product;

        public Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                this.Set(ref _product, value, PropertyChanged);
            }
        }

        private decimal _quantity;

        public decimal Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                this.Set(ref _quantity, value, PropertyChanged);
            }
        }

        public decimal Price
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
