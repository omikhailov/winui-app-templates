using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Common.ViewLayer.Pages;
using Common.ViewModelLayer.Helpers;

namespace Common.ViewLayer.Commands
{
    public class OpenInNewWindowCommand : DependencyObject, ICommand
    {
        public string Page
        {
            get { return (string)GetValue(PageProperty); }

            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(nameof(Page), typeof(string), typeof(OpenInNewWindowCommand), new PropertyMetadata(null));

        public object Parameter
        {
            get { return (object)GetValue(ParameterProperty); }

            set { SetValue(ParameterProperty, value); }
        }
                
        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(nameof(Parameter), typeof(object), typeof(OpenInNewWindowCommand), new PropertyMetadata(null));

        public bool ParameterRequired
        {
            get { return (bool)GetValue(ParameterRequiredProperty); }

            set { SetValue(ParameterRequiredProperty, value); }
        }

        public static readonly DependencyProperty ParameterRequiredProperty = DependencyProperty.Register(nameof(ParameterRequired), typeof(bool), typeof(OpenInNewWindowCommand), new PropertyMetadata(false));

        public Size RequestedSize
        {
            get { return (Size)GetValue(RequestedSizeProperty); }

            set { SetValue(RequestedSizeProperty, value); }
        }
        
        public static readonly DependencyProperty RequestedSizeProperty = DependencyProperty.Register(nameof(RequestedSize), typeof(Size), typeof(OpenInNewWindowCommand), new PropertyMetadata(Size.Empty));
                
        public DisplayRegion RequestedDisplayRegion
        {
            get { return (DisplayRegion)GetValue(RequestedDisplayRegionProperty); }

            set { SetValue(RequestedDisplayRegionProperty, value); }
        }
                
        public static readonly DependencyProperty RequestedDisplayRegionProperty = DependencyProperty.Register(nameof(RequestedDisplayRegion), typeof(DisplayRegion), typeof(OpenInNewWindowCommand), new PropertyMetadata(null));

        public bool ExtendViewIntoTitleBar
        {
            get { return (bool)GetValue(ExtendViewIntoTitleBarProperty); }

            set { SetValue(ExtendViewIntoTitleBarProperty, value); }
        }

        public static readonly DependencyProperty ExtendViewIntoTitleBarProperty = DependencyProperty.Register(nameof(ExtendViewIntoTitleBar), typeof(bool), typeof(OpenInNewWindowCommand), new PropertyMetadata(false));

        public async void Execute(object parameter)
        {
            if (parameter == null) parameter = Parameter;

            if (parameter == null && ParameterRequired) return;

            var pageName = Page;

            if (!string.IsNullOrEmpty(pageName))
            {
                var pageType = Assembly.GetEntryAssembly().GetTypes().FirstOrDefault(t => t.IsClass && t.Name == pageName);

                if (pageType != null)
                {
                    var windowTask = AppWindow.TryCreateAsync();

                    var frame = new Frame();

                    frame.Navigate(pageType, parameter);

                    var window = await windowTask;

                    if (RequestedDisplayRegion != null) window.RequestMoveToDisplayRegion(RequestedDisplayRegion);

                    if (RequestedSize != Size.Empty) window.RequestSize(RequestedSize);

                    if (ExtendViewIntoTitleBar) TitleBarHelper.ExtendView(window);

                    ElementCompositionPreview.SetAppWindowContent(window, frame);

                    if (frame.Content is ISecondaryPage page) page.Window = window;
                    
                    //TODO: Replace with AppWindow.Closed when MS will fix a bug with disappearing main window

                    window.CloseRequested += (s, e) =>                     
                    {
                        if (frame.Content is ISecondaryPage p) p.Window = null;

                        frame.Content = null;
                        
                        frame = null;
                    };

                    await window.TryShowAsync();
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
