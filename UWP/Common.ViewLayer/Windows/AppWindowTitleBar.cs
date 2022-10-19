using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.WindowManagement;

namespace Common.ViewLayer.Windows
{
    public class AppWindowTitleBar : DependencyObject
    {
        public AppWindow Window
        {
            get { return (AppWindow)GetValue(WindowProperty); }

            set { SetValue(WindowProperty, value); }
        }

        public static readonly DependencyProperty WindowProperty = DependencyProperty.Register(nameof(Window), typeof(AppWindow), typeof(AppWindowTitleBar), new PropertyMetadata(null, WindowChanged));

        private static void WindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var appWindowTitleBar = (AppWindowTitleBar)d;

            appWindowTitleBar.SetButtonsForeground();
        }

        public Color? Foreground
        {
            get { return (Color?)GetValue(ForegroundProperty); }

            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Color?), typeof(AppWindowTitleBar), new PropertyMetadata(null, ForegroundChanged));

        private static void ForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var appWindowTitleBar = (AppWindowTitleBar)d;

            appWindowTitleBar.SetButtonsForeground();
        }

        private void SetButtonsForeground()
        {
            if (Window == null) return;

            Window.TitleBar.ButtonForegroundColor = Foreground;
        }
    }
}
