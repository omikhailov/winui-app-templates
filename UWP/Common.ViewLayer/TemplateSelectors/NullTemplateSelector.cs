using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Common.ViewLayer.TemplateSelectors
{
    public class NullTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }

        public DataTemplate NotNullTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
            {
                return NullTemplate;
            }
            else
            {
                return NotNullTemplate;
            }
        }
    }
}
