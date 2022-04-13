using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManager.AttachedProperties
{
    class DraggedItemAttachedProperty
    {

        public static object GetDraggedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(DraggedItemProperty);
        }

        public static void SetDraggedItem(DependencyObject obj, object value)
        {
            obj.SetValue(DraggedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for DraggedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DraggedItemProperty =
            DependencyProperty.RegisterAttached("DraggedItem", typeof(object), typeof(DraggedItemAttachedProperty), new PropertyMetadata(null));


    }
}
