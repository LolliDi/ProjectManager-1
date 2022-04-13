using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManager.Views.Forms
{
    /// <summary>
    /// Логика взаимодействия для TaskResourcesForm.xaml
    /// </summary>
    public partial class TaskResourcesForm : UserControl
    {
        public TaskResourcesForm()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender.GetType().IsSubclassOf(typeof(FrameworkElement)))
            {
                DragDrop.DoDragDrop((FrameworkElement)sender, new DataObject(DataFormats.Serializable, ((FrameworkElement)sender).DataContext), DragDropEffects.Move);
            }
        }
    }
}
