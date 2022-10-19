using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Windows
{
    public class MainWindowTitleBar : DependencyObject
    {
        public Color? Foreground
        {
            get { return (Color?)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Color?), typeof(MainWindowTitleBar), new PropertyMetadata(null, ForegroundCahnged));

        private static void ForegroundCahnged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var appViewTitleBar = ApplicationView.GetForCurrentView().TitleBar;

            appViewTitleBar.ButtonForegroundColor = (Color?)e.NewValue;
        }
    }
}
