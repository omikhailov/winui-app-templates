using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Common.ViewLayer.TemplateSelectors
{
    public class SelectedListViewItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedTemplate { get; set; }

        public DataTemplate NotSelectedTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var listViewItem = container as ListViewItem;

            if (item == null) throw new NotImplementedException();

            if (listViewItem.Tag != null && long.TryParse(listViewItem.Tag.ToString(), out var token))
            {
                listViewItem.UnregisterPropertyChangedCallback(ListViewItem.IsSelectedProperty, token);
            }

            listViewItem.Tag = listViewItem.RegisterPropertyChangedCallback(ListViewItem.IsSelectedProperty, (s, e) =>
            {
                listViewItem.ContentTemplateSelector = null;

                listViewItem.ContentTemplateSelector = this;
            });

            if (listViewItem.IsSelected)
            {
                return SelectedTemplate;
            }
            else
            {
                return NotSelectedTemplate;
            }
        }
    }
}
