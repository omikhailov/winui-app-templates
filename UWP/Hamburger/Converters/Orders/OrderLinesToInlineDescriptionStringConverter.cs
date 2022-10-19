using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Hamburger.BL.Models.Entities;

namespace Hamburger.Converters.Orders
{
    public class OrderLinesToInlineDescriptionStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var orderLines = value as IEnumerable<OrderLine>;

            if (orderLines == null) return string.Empty;

            var result = new StringBuilder();

            var firstLine = true;

            foreach (var line in orderLines)
            {
                if (!firstLine) result.Append(", ");

                result.Append(line.Product.Title);

                result.Append(" X ");

                result.Append(line.Quantity.ToString());

                firstLine = false;
            }

            return result.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
