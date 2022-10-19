using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelLayer;

namespace Hamburger.BL.Models.Entities
{
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; internal set; }

        private ObservableCollection<OrderLine> _orderLines = new ObservableCollection<OrderLine>();

        public ObservableCollection<OrderLine> OrderLines
        {
            get
            {
                return _orderLines;
            }
            set
            {
                this.Set(ref _orderLines, value, PropertyChanged);
            }
        }

        public decimal Price
        {
            get
            {
                return OrderLines.Sum(l => l.Price);
            }
        }

        private OrderStatus _status;

        public OrderStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                this.Set(ref _status, value, PropertyChanged);
            }
        }

        private Client _client;

        public Client Client
        {
            get
            {
                return _client;
            }
            set
            {
                this.Set(ref _client, value, PropertyChanged);
            }
        }

        private Address _address;

        public Address Address
        {
            get
            {
                return _address ?? Client?.Address;
            }
            set
            {
                this.Set(ref _address, value, PropertyChanged);

                if (Client != null && Client.Address == null) Client.Address = value;
            }
        }

    }
}
