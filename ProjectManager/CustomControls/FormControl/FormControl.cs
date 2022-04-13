using ProjectManager.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManager.CustomControls
{
    public class FormControl : PopupControl
    {
        static FormControl()
        {
            HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(FormControl), new PropertyMetadata("Заголовок"));
            SubmitCommandProperty = DependencyProperty.Register(nameof(SubmitCommand), typeof(ICommand), typeof(FormControl), new PropertyMetadata(null));
            CancelCommandProperty = DependencyProperty.Register(nameof(CancelCommand), typeof(ICommand), typeof(FormControl), new PropertyMetadata(null));
            ItemProperty = DependencyProperty.Register(nameof(ItemProperty), typeof(object), typeof(FormControl), new PropertyMetadata(null));
        }

        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty SubmitCommandProperty;
        public static readonly DependencyProperty CancelCommandProperty;
        public static readonly DependencyProperty ItemProperty;

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public object Item
        {
            get { return (object)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public ICommand SubmitCommand
        {
            get { return (ICommand)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
    }
}
