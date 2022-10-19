using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;
using Hamburger.BL.Models.Entities;

namespace Hamburger.Converters.Orders
{
    public class OrderStatusToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var status = (OrderStatus)value;

            var resourceKey = "OrderStatus" + status;

            try
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();

                return resourceLoader.GetString(resourceKey);
            }
            catch
            {
                return status;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
