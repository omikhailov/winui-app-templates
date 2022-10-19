using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Common.ViewLayer.Converters
{
    public class XamlUICommandToTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var command = (XamlUICommand)value;

            if (value == null) return string.Empty;

            var parsed = int.TryParse(parameter as string, out var count);

            if (!parsed) count = 2;

            var hotkeysString = string.Join(", ", command.KeyboardAccelerators.Take(count).Select(GetHotkeyDescription));

            if (!string.IsNullOrEmpty(hotkeysString)) return $"{command.Description} ({hotkeysString})";

            return command.Description;            
        }

        private static VirtualKeyModifiers[] ValidModifiers = new[] { VirtualKeyModifiers.Control, VirtualKeyModifiers.Menu, VirtualKeyModifiers.Shift, VirtualKeyModifiers.Windows };

        private static string GetHotkeyDescription(KeyboardAccelerator a)
        {
            var keys = ValidModifiers.Where(m => a.Modifiers.HasFlag(m)).Select(m => m.ToString()).Concat(new[] { a.Key.ToString() });

            return string.Join('+', keys);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
