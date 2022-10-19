using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;

namespace Common.ViewLayer.Behaviors
{
    public class PreviewKeyDownTriggerBehavior : Trigger<UIElement>
    {
        public VirtualKey Key
        {
            get { return (VirtualKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register(nameof(Key), typeof(VirtualKey), typeof(PreviewKeyDownTriggerBehavior), new PropertyMetadata(VirtualKey.None));

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PreviewKeyDown += HandlePreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.PreviewKeyDown -= HandlePreviewKeyDown;
        }

        private void HandlePreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Key && Actions.Count > 0)
            {
                Interaction.ExecuteActions(sender, Actions, e);

                e.Handled = true;
            }
        }
    }
}
