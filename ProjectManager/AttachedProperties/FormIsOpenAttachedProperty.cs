using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.AttachedProperties
{
    class FormIsOpenAttachedProperty
    {


        public static bool GetValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOpenProperty);
        }

        public static void SetValue(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOpenProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(FormIsOpenAttachedProperty), new PropertyMetadata(false));


    }
}
