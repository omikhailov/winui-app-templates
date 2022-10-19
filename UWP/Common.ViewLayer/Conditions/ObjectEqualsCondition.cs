using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Common.ViewLayer.Conditions
{
    public class ObjectEqualsCondition : Condition
    {
        private bool _isTrue;

        public override bool IsTrue { get { return _isTrue; } }

        public object Left
        {
            get { return (object)GetValue(LeftProperty); }

            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register(nameof(Left), typeof(object), typeof(ObjectEqualsCondition), new PropertyMetadata(null, OnValueChanged));

        public object Right
        {
            get { return (object)GetValue(RightProperty); }

            set { SetValue(RightProperty, value); }
        }

        public static readonly DependencyProperty RightProperty = DependencyProperty.Register(nameof(Right), typeof(object), typeof(ObjectEqualsCondition), new PropertyMetadata(null, OnValueChanged));

        public bool AllowNulls
        {
            get { return (bool)GetValue(AllowNullsProperty); }

            set { SetValue(AllowNullsProperty, value); }
        }

        public static readonly DependencyProperty AllowNullsProperty = DependencyProperty.Register(nameof(AllowNulls), typeof(bool), typeof(ObjectEqualsCondition), new PropertyMetadata(true, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var condition = (ObjectEqualsCondition)d;

            var left = condition.Left;

            var right = condition.Right;

            var isTrue = false;

            if (!condition.AllowNulls && left == null && right == null)
            {
                isTrue = false;
            }
            else
            {
                isTrue = object.Equals(left, right);
            }

            condition.SetIsTrue(isTrue);
        }

        private void SetIsTrue(bool value)
        {
            if (_isTrue != value)
            {
                _isTrue = value;

                NotifyValueChanged();
            }
        }
    }
}
