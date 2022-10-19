using System;
using System.Linq;
using Hamburger.BL.Models.Entities;

namespace Hamburger.BL.Services.Data
{
    public interface IDataService
    {
        void CreateOrder(Order order);
        void DeleteOrder(Guid id);
        void DeleteOrder(Order order);
        Courier GetCourier(Guid id);
        IQueryable<Courier> GetCouriers();
        Order GetOrder(Guid id);
        IQueryable<Order> GetOrders();
        void UpdateOrder(Order order);
    }
}