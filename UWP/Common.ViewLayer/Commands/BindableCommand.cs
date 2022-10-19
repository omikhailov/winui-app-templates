using System;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Commands
{
    public abstract class BindableCommand : DependencyObject, ICommand
    {
        public virtual bool CanExecute
        {
            get { return (bool)GetValue(CanExecuteProperty); }

            set { SetValue(CanExecuteProperty, value); }
        }
        
        public static readonly DependencyProperty CanExecuteProperty = DependencyProperty.Register(nameof(CanExecute), typeof(bool), typeof(BindableCommand), new PropertyMetadata(false, OnCanExecuteChanged));

        private static void OnCanExecuteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                var command = (BindableCommand)d;

                command.RaiseCanExecuteChanged();
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;

        protected virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
