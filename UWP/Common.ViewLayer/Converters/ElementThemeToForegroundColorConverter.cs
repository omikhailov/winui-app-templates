using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Common.ViewLayer.Converters
{
    public class ElementThemeToForegroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var theme = (ElementTheme)value;

            if (theme == ElementTheme.Light || theme == ElementTheme.Default && Application.Current.RequestedTheme == ApplicationTheme.Light)
            {
                return Colors.Black;
            }
            else
            {
                return Colors.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
