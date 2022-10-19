using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Microsoft.UI.Xaml.Controls;

namespace Hamburger.Converters.Settings
{
    public class NavigationViewPaneDisplayModeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Enum.Parse<NavigationViewPaneDisplayMode>((string)value);
        }
    }
}
