using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Conditions
{
    public class IntIsGreaterThanCondition : Condition
    {
        private bool _isTrue;

        public override bool IsTrue
        {
            get
            {
                return _isTrue;
            }
        }

        public int Left
        {
            get { return (int)GetValue(LeftProperty); }

            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(int), typeof(IntIsGreaterThanCondition), new PropertyMetadata(0, ValueChanged));

        public int Right
        {
            get { return (int)GetValue(RightProperty); }

            set { SetValue(RightProperty, value); }
        }
                
        public static readonly DependencyProperty RightProperty = DependencyProperty.Register(nameof(Right), typeof(int), typeof(IntIsGreaterThanCondition), new PropertyMetadata(0, ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var condition = (IntIsGreaterThanCondition)d;

            var isTrue = condition.Left > condition.Right;

            if (condition._isTrue != isTrue)
            {
                condition._isTrue = isTrue;

                condition.NotifyValueChanged();
            }
        }
    }
}
