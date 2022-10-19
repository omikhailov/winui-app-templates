using System;
using System.ComponentModel;
using Hamburger.BL.Models.Entities;

namespace Hamburger.BL.ViewModels.Orders
{
    public interface IOrderViewModel : INotifyPropertyChanged
    {
        Order Model { get; set; }
    }
}