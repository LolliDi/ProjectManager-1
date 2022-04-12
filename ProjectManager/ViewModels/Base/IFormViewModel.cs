using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    interface IFormViewModel
    {
        FormNavigationService FormNavigationService { get; set; }
        ICommand SubmitCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}
