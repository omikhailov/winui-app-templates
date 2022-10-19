using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Conditions
{
    public abstract class Condition : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract bool IsTrue { get; }

        public virtual bool IsFalse { get { return !IsTrue; } }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void NotifyValueChanged()
        {
            NotifyPropertyChanged(nameof(IsTrue));

            NotifyPropertyChanged(nameof(IsFalse));
        }

        protected void NotifyPropertyAndValueChanged([CallerMemberName] string propertyName = null)
        {
            NotifyPropertyChanged(propertyName);

            NotifyValueChanged();
        }
    }
}
