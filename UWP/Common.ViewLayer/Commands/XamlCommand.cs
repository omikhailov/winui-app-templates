using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Common.ViewLayer.Commands
{
    public class XamlCommand : XamlUICommand
    {
        public XamlCommand() : base()
        {
            Init();
        }

        public bool CanExecute
        {
            get { return (bool)GetValue(CanExecuteProperty); }

            set 
            { 
                SetValue(CanExecuteProperty, value);

                NotifyCanExecuteChanged();
            }
        }
                
        public static readonly DependencyProperty CanExecuteProperty = DependencyProperty.Register(nameof(CanExecute), typeof(bool), typeof(StandardCommand), new PropertyMetadata(true));

        private void Init()
        {
            CanExecuteRequested += OnCanExecuteRequested;

            var hotkeys = string.Join(',', KeyboardAccelerators.Take(2).Select(a => a.ToString()));

            if (!string.IsNullOrEmpty(hotkeys)) Description += " " + hotkeys;
        }

        private void OnCanExecuteRequested(XamlUICommand sender, CanExecuteRequestedEventArgs args)
        {
            args.CanExecute = CanExecute;
        }
    }
}
