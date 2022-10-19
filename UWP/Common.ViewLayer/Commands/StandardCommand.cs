using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Common.ViewLayer.Commands
{
    public class StandardCommand : StandardUICommand
    {
        public StandardCommand() : base()
        {
            Init();
        }

        public StandardCommand(StandardUICommandKind kind) : base(kind)
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
        }

        private void OnCanExecuteRequested(XamlUICommand sender, CanExecuteRequestedEventArgs args)
        {
            args.CanExecute = CanExecute;
        }
    }
}
