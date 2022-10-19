using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hamburger.BL.Models.Main;
using Hamburger.Views;
using Microsoft.Xaml.Interactions.Core;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hamburger.Actions.Main
{
    public class NavigateToSelectedItemAction : DependencyObject, IAction
    {
        public Frame ContentFrame
        {
            get { return (Frame)GetValue(ContentFrameProperty); }

            set { SetValue(ContentFrameProperty, value); }
        }

        public static readonly DependencyProperty ContentFrameProperty = DependencyProperty.Register(nameof(ContentFrame), typeof(Frame), typeof(NavigateToInvokedItemAction), new PropertyMetadata(null));

        public object Execute(object sender, object parameter)
        {
            if (ContentFrame == null) return null;

            var model = ((Microsoft.UI.Xaml.Controls.NavigationView)sender).SelectedItem as PageModel;

            if (model == null) return null;

            var typeName = model.Name + "Page";

            if (ContentFrame.SourcePageType?.Name == typeName) return null;

            var pageType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsClass && t.Name == typeName);

            if (pageType == null) return null;

            ContentFrame.Navigate(pageType, model.Parameter);

            return null;
        }
    }
}
