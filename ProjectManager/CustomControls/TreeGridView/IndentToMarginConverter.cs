using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectManager.CustomControls
{
    class IndentToMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DependencyObject Element = values[0] as DependencyObject;
            double indent = values[1] == null ? 0 : (double)values[1];
            int level = -1;
            while (Element.GetType() != typeof(TreeGridView))
            {
                if (typeof(TreeViewItem).IsAssignableFrom(Element.GetType()))
                {
                    level++;
                }
                Element = VisualTreeHelper.GetParent(Element);
            }
            return new Thickness(indent * level, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
