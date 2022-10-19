using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Common.ViewLayer.TemplateSelectors
{
    public class EmptyWhenNullTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null) new DataTemplate();

            return Template;
        }
    }
}
