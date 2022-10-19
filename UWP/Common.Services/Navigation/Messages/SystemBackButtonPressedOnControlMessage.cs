using System;
using Windows.UI.Xaml;

namespace Common.Services.Navigation.Messages
{
    public class SystemBackButtonPressedOnControlMessage
    {
        public bool Handled { get; set; }

        public bool IsFor(UIElement element)
        {
            return element.XamlRoot.IsHostVisible;
        }
    }
}
