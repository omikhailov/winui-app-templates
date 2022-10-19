using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Conditions
{
    public class CollectionHasItemsCondition : Condition, INotifyPropertyChanged
    {
        private bool _isTrue;

        public override bool IsTrue 
        { 
            get 
            { 
                return _isTrue; 
            }
        }

        public IEnumerable Collection
        {
            get { return (IEnumerable)GetValue(CollectionProperty); }

            set { SetValue(CollectionProperty, value); }
        }

        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register(nameof(Collection), typeof(IEnumerable), typeof(CollectionHasItemsCondition), new PropertyMetadata(null, OnCollectionChanged));

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var condition = (CollectionHasItemsCondition)d;

            condition.SetIsTrue(e.NewValue != null && HasItems((IEnumerable)e.NewValue));

            if (e.OldValue is INotifyCollectionChanged oldCollection && oldCollection != null)
            {
                oldCollection.CollectionChanged -= condition.NotifyCollectionChangedHandler;
            }

            if (e.NewValue is INotifyCollectionChanged newCollection && newCollection != null)
            {
                newCollection.CollectionChanged += condition.NotifyCollectionChangedHandler;
            }
        }

        private void SetIsTrue(bool value)
        { 
            if (_isTrue != value)
            {
                _isTrue = value;

                NotifyValueChanged();
            }
        }

        private void NotifyCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetIsTrue(HasItems((IEnumerable)sender));
        }

        private static bool HasItems(IEnumerable collection)
        {
            var enumerator = collection.GetEnumerator();

            return enumerator.MoveNext();
        }
    }
}
