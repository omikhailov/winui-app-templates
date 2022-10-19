using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using Hamburger.BL.Models.Main;
using System.Reflection;

namespace Hamburger.Actions.Main
{
    public class SetInitialFrameStateAction : DependencyObject, IAction
    {
        public PageModel DefaultPageModel { get; set; }

        public string NavigationStateString { get; set; }

        public object Execute(object sender, object parameter)
        {
            var frame = (Frame)sender;

            if (!string.IsNullOrEmpty(NavigationStateString))            
            {
                frame.SetNavigationState(NavigationStateString, false);
            }
            else
            {
                if (DefaultPageModel == null) return null;

                var typeName = DefaultPageModel.Name + "Page";

                var pageType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsClass && t.Name == typeName);

                if (pageType == null) return null;

                frame.Navigate(pageType, DefaultPageModel.Parameter);

            }

            return null;
        }
    }
}
