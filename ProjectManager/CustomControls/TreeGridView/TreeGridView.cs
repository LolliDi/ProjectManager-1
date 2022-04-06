using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ProjectManager.CustomControls
{
    class TreeGridView : TreeView
    {
        static TreeGridView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeGridView), new FrameworkPropertyMetadata(typeof(TreeGridView)));
            
            ColumnsProperty = DependencyProperty.Register(nameof(Columns), typeof(GridViewColumnCollection), typeof(TreeGridView), new PropertyMetadata(new GridViewColumnCollection()));
            TargetColumnProperty = DependencyProperty.Register(nameof(TargetColumn), typeof(IEnumerable), typeof(TreeGridView), new PropertyMetadata(null));
            IndentProperty = DependencyProperty.Register(nameof(Indent), typeof(double), typeof(TreeGridView), new PropertyMetadata(10d));
        }

        public GridViewColumnCollection Columns
        {
            get { return (GridViewColumnCollection)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public IEnumerable TargetColumn
        {
            get { return (IEnumerable)GetValue(TargetColumnProperty); }
            set { SetValue(TargetColumnProperty, value); }
        }
        public double Indent
        {
            get { return (double)GetValue(IndentProperty); }
            set { SetValue(IndentProperty, value); }
        }

        public static readonly DependencyProperty IndentProperty;
        public static readonly DependencyProperty ColumnsProperty;
        public static readonly DependencyProperty TargetColumnProperty;
    }
}
