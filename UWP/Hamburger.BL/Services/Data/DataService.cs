using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DI.Attributes;
using Hamburger.BL.Models.Entities;
using Windows.Devices.Geolocation;

namespace Hamburger.BL.Services.Data
{
    [Singleton]

    public class DataService : IDataService
    {
        public DataService()
        {
            GenerateSampleData();

        }

        private void GenerateSampleData()
        {
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Hamburger", Price = 1.00M, Description = "The original burger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard. Hamburger contains no artificial flavors, preservatives or added colors from artificial sources." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Double Hamburger", Price = 2.29M, Description = "The Double Hamburger starts with a 100% pure beef burger seasoned with just a pinch of salt and pepper. Then, the burger is topped with a tangy pickle, chopped onions, ketchup and mustard. Double Hamburger contains no artificial flavors, preservatives or added colors from artificial sources." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Cheeseburger", Price = 1.19M, Description = "Our simple, classic cheeseburger begins with a 100% pure beef burger seasoned with just a pinch of salt and pepper. It is topped with a tangy pickle, chopped onions, ketchup, mustard, and a slice of melty cheese. It contains no artificial flavors, preservatives or added colors from artificial sources." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Double Cheeseburger", Price = 2.49M, Description = "The Double Cheeseburger features two 100% pure beef burger patties seasoned with just a pinch of salt and pepper. It's topped with tangy pickles, chopped onions, ketchup, mustard and two slices of melty American cheese. There are 450 calories in a Double Cheeseburger. It contains no artificial flavors, preservatives or added colors from artificial sources." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Burrito", Price = 1.00M, Description = "Burrito is loaded with fluffy scrambled egg, pork sausage, melty cheese, green chiles and onion! It's wrapped in a soft tortilla, making it the perfect grab and go breakfast." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Fries", Price = 2.19M, Description = "Our Fries are made with premium potatoes such as the Russet Burbank and the Shepody. With 0g of trans fat per labeled serving, these epic fries are crispy and golden on the outside and fluffy on the inside." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Lemonade", Price = 1.00M, Description = "This sweet, blended pink lemonade slushie goes great with Burgers and Fries." });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Coffee", Price = 2.10M, Description = "Made with rich chocolate flavor and a hint of coffee, our recipe is blended with ice and covered with whipped topping and chocolatey drizzle. " });
            _products.Add(new Product() { Id = Guid.NewGuid(), Title = "Muffin", Price = 1.50M, Description = "A soft and fluffy muffin baked with real blueberries and topped with a streusel crumb topping that goes wonderfully with our Coffee." });

            _addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Hamburg", Country = "Germany", Line = "S-Bahnhof, Sternschanze 1, 20357" });
            _addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Hamburg", Country = "Germany", Line = "Duvenstedter Berg 63, 22397" });
            _addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Hamburg", Country = "Germany", Line = "P, Am Alten Posthaus 6, 22041" });
            _addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Hamburg", Country = "Germany", Line = "Stephanstraße 103, 22047" });
            _addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Hamburg", Country = "Germany", Line = "Bostelreihe 7-9, 22083" });

            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Homer", LastName = "Simpson", Address = _addresses[0] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Marge", LastName = "Simpson", Address = _addresses[0] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Bart", LastName = "Simpson", Address = _addresses[0] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Apu", LastName = "Nahasapeemapetilon", Address = _addresses[1] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Clancy", LastName = "Wiggum", Address = _addresses[2] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Edna", LastName = "Krabappel", Address = _addresses[3] });
            _clients.Add(new Client() { Id = Guid.NewGuid(), FirstName = "Ned", LastName = "Flanders", Address = _addresses[4] });

            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Created, Client = _clients[0] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Accepted, Client = _clients[1] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Processed, Client = _clients[2] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Processed, Client = _clients[3] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Processed, Client = _clients[4] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Processed, Client = _clients[5] });
            _orders.Add(new Order() { Id = Guid.NewGuid(), Status = OrderStatus.Processed, Client = _clients[6] });
                        
            for (int i = 0; i < 123; i++)
            {
                _orders.Add(new Order() { Id = Guid.NewGuid(), Status = _random.NextDouble() > 0.1 ? OrderStatus.Delivered : OrderStatus.Cancelled, Client = PeekOne(_clients) });
            }

            foreach (var order in _orders)
            {
                foreach (var orderLine in PeekMultiple(_products, _random.Next(1, 5)).Select(p => new OrderLine() { Id = Guid.NewGuid(), Product = p, Quantity = _random.Next(1, 3) }))
                {
                    order.OrderLines.Add(orderLine);
                }
            }

            var courierDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            _couriers.Add(new Courier() { Id = Guid.NewGuid(), FirstName = "Nelson", LastName = "Muntz", Location = new BasicGeoposition() { Latitude = 53.56111900486864, Longitude = 9.990247806108995 }, Photo = @"https://iconape.com/wp-content/png_logo_vector/nelson-muntzlos-simpsons-logo.png", Description = courierDescription });
            _couriers.Add(new Courier() { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Sideshow", Location = new BasicGeoposition() { Latitude = 53.564423007984175, Longitude = 9.96585296554614 }, Photo = @"https://upload.wikimedia.org/wikipedia/ru/thumb/e/eb/Sideshow_Bob.gif/222px-Sideshow_Bob.gif", Description = courierDescription });
            _couriers.Add(new Courier() { Id = Guid.NewGuid(), FirstName = "Ralph", LastName = "Wiggum", Location = new BasicGeoposition() { Latitude = 53.56287713039916, Longitude = 10.003108684013407 }, Photo = @"http://www.simpsonsforever.com/images/episodes/characters/8.jpg", Description = courierDescription });
            _couriers.Add(new Courier() { Id = Guid.NewGuid(), FirstName = "Todd", LastName = "Flanders", Location = new BasicGeoposition() { Latitude = 53.55087634707536, Longitude = 9.996564805992703 }, Photo = @"https://static.wikia.nocookie.net/simpsons/images/1/11/Todd_Flanders2.png/revision/latest/scale-to-width-down/185", Description = courierDescription });

            for (int i = 0; i < 7; i++)
            {
                var courier = PeekOne(_couriers);

                courier.Bag.Add(_orders[i]);
            }
        }

        private static Random _random = new Random(679867);

        private List<Product> _products = new List<Product>();

        private List<Address> _addresses = new List<Address>();

        private List<Client> _clients = new List<Client>();

        private List<Order> _orders = new List<Order>();

        private List<Courier> _couriers = new List<Courier>();

        public Order GetOrder(Guid id)
        {
            return _orders.Find(o => o.Id == id);
        }

        public IQueryable<Order> GetOrders()
        {
            return _orders.AsQueryable();
        }

        public void CreateOrder(Order order)
        {
            _orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            DeleteOrder(order.Id);

            CreateOrder(order);
        }

        public void DeleteOrder(Guid id)
        {
            var index = _orders.FindIndex(o => o.Id == id);

            if (index >= 0) _orders.RemoveAt(index);
        }

        public void DeleteOrder(Order order)
        {
            if (order == null) return;

            var index = _orders.IndexOf(order);

            if (index >= 0) _orders.RemoveAt(index);
        }

        public Courier GetCourier(Guid id)
        {
            return _couriers.Find(c => c.Id == id);
        }

        public IQueryable<Courier> GetCouriers()
        {
            return _couriers.AsQueryable();
        }

        private static T PeekOne<T>(IList<T> items)
        {
            return items[_random.Next(0, items.Count)];
        }

        private static IEnumerable<T> PeekMultiple<T>(ICollection<T> items, int count)
        {
            if (items.Count < count) throw new InvalidOperationException();

            var free = new List<T>(items);

            for (int i = 0; i < count; i++)
            {
                var index = _random.Next(0, free.Count);

                var peeked = free[index];

                free.RemoveAt(index);

                yield return peeked;
            }

            yield break;
        }
    }
}
