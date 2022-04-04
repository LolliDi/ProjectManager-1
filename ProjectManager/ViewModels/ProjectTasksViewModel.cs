using ProjectManager.Commands;
using ProjectManager.Models;
using ProjectManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    class ProjectTasksViewModel : ViewModel
    {
        public ProjectTasksViewModel(NavigationService navigationService, Projects project)
        {
            NavigationService = navigationService;

            ToResourcePageCommand = new LambdaCommand(null);
            ToReportsPageCommand = new LambdaCommand(null);
            ToGanttChartPageCommand = new LambdaCommand(null);
            ToProjectPassportPageCommand = new LambdaCommand(null);
        }

        public NavigationService NavigationService { get; set; }
        public ICommand ToResourcePageCommand { get; set; }
        public ICommand ToReportsPageCommand { get; set; }
        public ICommand ToGanttChartPageCommand { get; set; }
        public ICommand ToProjectPassportPageCommand { get; set; }
    }
}
