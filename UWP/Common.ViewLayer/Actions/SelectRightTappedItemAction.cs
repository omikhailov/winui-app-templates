using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Common.ViewLayer.Actions
{
    public class SelectRightTappedItemAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var args = (RightTappedRoutedEventArgs)parameter;
            
            var source = (FrameworkElement)args.OriginalSource;

            var item = source.DataContext;

            if (sender is Selector selector)
            {
                selector.SelectedItem = item;
            }
            else
            {
                var senderType = sender.GetType();

                var property = senderType.GetProperty("SelectedItem");

                property.SetValue(sender, item);
            }

            return item;
        }
    }
}
