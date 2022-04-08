using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels.FormBase
{
    interface IForm
    {
        ICommand SubmitFormCommand { get; set; }
        ICommand CloseFormCommand { get; set; }
    }
}
