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
            ItemProperty = DependencyProperty.Register(nameof(Item), typeof(object), typeof(FormControl), new PropertyMetadata(null));
            SubmitCommandParameterProperty = DependencyProperty.Register(nameof(SubmitCommandParameter), typeof(object), typeof(FormControl), new PropertyMetadata(null));
            CancelCommandParameterProperty = DependencyProperty.Register(nameof(CancelCommandParameter), typeof(object), typeof(FormControl), new PropertyMetadata(null));
        }

        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty SubmitCommandProperty;
        public static readonly DependencyProperty CancelCommandProperty;
        public static readonly DependencyProperty ItemProperty;
        public static readonly DependencyProperty SubmitCommandParameterProperty;
        public static readonly DependencyProperty CancelCommandParameterProperty;

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
            get { return (ICommand)GetValue(SubmitCommandProperty); }
            set { SetValue(SubmitCommandProperty, value); }
        }

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }
        public object SubmitCommandParameter
        {
            get { return (object)GetValue(SubmitCommandParameterProperty); }
            set { SetValue(SubmitCommandParameterProperty, value); }
        }

        public object CancelCommandParameter
        {
            get { return (object)GetValue(CancelCommandParameterProperty); }
            set { SetValue(CancelCommandParameterProperty, value); }
        }

    }
}
