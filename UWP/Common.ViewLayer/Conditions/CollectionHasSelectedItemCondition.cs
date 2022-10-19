using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Conditions
{
    public class CollectionHasSelectedItemCondition : Condition, INotifyPropertyChanged
    {
        public override bool IsTrue { get { return SelectedItemIsSet && CollectionHasItems; } }

        private bool _selectedItemIsSet;

        public bool SelectedItemIsSet
        {
            get
            {
                return _selectedItemIsSet;
            }
            protected set
            {
                if (_selectedItemIsSet != value)
                {
                    _selectedItemIsSet = value;

                    NotifyPropertyAndValueChanged();
                }
            }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }

            set { SetValue(SelectedItemProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(CollectionHasSelectedItemCondition), new PropertyMetadata(null, OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var condition = (CollectionHasSelectedItemCondition)d;

            condition.SelectedItemIsSet = e.NewValue != null;
        }

        private bool _collectionHasItems;

        public bool CollectionHasItems
        {
            get
            {
                return _collectionHasItems;
            }
            set
            {
                if (_collectionHasItems != value)
                {
                    _collectionHasItems = value;

                    NotifyPropertyAndValueChanged();
                }
            }
        }

        public IEnumerable Collection
        {
            get { return (IEnumerable)GetValue(CollectionProperty); }

            set { SetValue(CollectionProperty, value); }
        }

        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register(nameof(Collection), typeof(IEnumerable), typeof(CollectionHasSelectedItemCondition), new PropertyMetadata(null, OnCollectionChanged));

        private static void OnCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var condition = (CollectionHasSelectedItemCondition)d;

            condition.CollectionHasItems = e.NewValue != null && HasItems((IEnumerable)e.NewValue);

            if (e.OldValue is INotifyCollectionChanged oldCollection && oldCollection != null)
            {
                oldCollection.CollectionChanged -= condition.NotifyCollectionChangedHandler;
            }

            if (e.NewValue is INotifyCollectionChanged newCollection && newCollection != null)
            {
                newCollection.CollectionChanged += condition.NotifyCollectionChangedHandler;
            }
        }

        private void NotifyCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionHasItems = HasItems((IEnumerable)sender);
        }

        private static bool HasItems(IEnumerable collection)
        {
            var enumerator = collection.GetEnumerator();

            return enumerator.MoveNext();
        }
    }
}
