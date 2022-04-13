using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectManager.CustomControls
{
    public class PopupControl : UserControl
    {
        static PopupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupControl), new FrameworkPropertyMetadata(typeof(PopupControl)));

            BackgroundProperty.OverrideMetadata(typeof(PopupControl), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black) { Opacity = 0.5 }));
            PaddingProperty.OverrideMetadata(typeof(PopupControl), new FrameworkPropertyMetadata(new Thickness(10)));
            IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(PopupControl), new PropertyMetadata(false));
        }

        private FrameworkElement PART_Background;

        public static readonly DependencyProperty IsOpenProperty;

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Background = (FrameworkElement)GetTemplateChild("PART_Background");
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (PART_Background != null && PART_Background.IsMouseDirectlyOver)
            {
                IsOpen = false;
            }
        }
    }
}
