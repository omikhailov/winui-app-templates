using System;
using System.Linq;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Common.ViewLayer.TemplateSelectors
{
    public class DynamicTemplateSelector : DataTemplateSelector
    {
        public string ResourceDictionaryLocation { get; set; }

        public string TemplateName { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dictionary = new ResourceDictionary();
            
            dictionary.Source = new Uri(ResourceDictionaryLocation, UriKind.RelativeOrAbsolute);

            if (string.IsNullOrEmpty(TemplateName))
            {
                return dictionary.First().Value as DataTemplate;
            }
            else
            {
                return dictionary[TemplateName] as DataTemplate;
            }
        }
    }
}
