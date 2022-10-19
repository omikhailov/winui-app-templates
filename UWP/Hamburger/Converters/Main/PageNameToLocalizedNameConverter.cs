using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace Hamburger.Converters.Main
{
    public class PageNameToLocalizedNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var name = (string)value;

            var resourceKey = "PageName" + name;

            try
            {
                var resourceLoader = ResourceLoader.GetForCurrentView();

                return resourceLoader.GetString(resourceKey);
            }
            catch
            {
                return name;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
